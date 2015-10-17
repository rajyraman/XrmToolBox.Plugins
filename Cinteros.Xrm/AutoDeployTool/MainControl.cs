using System.Runtime.InteropServices;

namespace Cinteros.Xrm.AutoDeployTool
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using XrmToolBox.Extensibility;
    using XrmToolBox.Extensibility.Interfaces;
    using System.ServiceModel;

    public partial class MainControl : PluginControlBase, IGitHubPlugin
    {
        #region Public Constructors

        public MainControl()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Properties

        string IGitHubPlugin.RepositoryName => "XrmToolBox.Plugins";

        string IGitHubPlugin.UserName => "Cinteros";

        #endregion Public Properties

        #region Internal Properties

        internal DateTime LastRead
        {
            get;
            private set;
        }

        internal Guid PluginId
        {
            get;
            private set;
        }

        internal FileSystemWatcher Watcher
        {
            get;
            private set;
        }

        #endregion Internal Properties

        #region Private Methods

        private void StartWatch_Click(object sender, EventArgs e)
        {
            var sourceButton = ((Button)sender).Name;
            DialogResult selectedFolder = fbdWatch.ShowDialog();
            if (selectedFolder != DialogResult.OK) return;

            this.Watcher = new FileSystemWatcher();
            this.Watcher.Path = fbdWatch.SelectedPath;
            if (sourceButton == "bPluginStartWatch")
            {
                this.Watcher.Filter = "*.dll";
                lblPluginWatch.Text = $"Watching {fbdWatch.SelectedPath}";
                bPluginStopWatch.Enabled = true;
                tbLogPlugin.Text = "";
            }
            else if (sourceButton == "bJavascriptStartWatch")
            {
                this.Watcher.Filter = "*.js";
                lblJavascriptWatch.Text = $"Watching {fbdWatch.SelectedPath}";
                bJavascriptStopWatch.Enabled = true;
                tbLogJavascript.Text = "";
            }
            this.Watcher.IncludeSubdirectories = true;
            this.Watcher.NotifyFilter = NotifyFilters.LastWrite;
            this.Watcher.EnableRaisingEvents = true;

            this.Watcher.Changed -= Watcher_OnChanged;
            this.Watcher.Changed += Watcher_OnChanged;
        }

        private void Watcher_OnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            var fileName = fileSystemEventArgs.FullPath.Split('\\').Last();
            // Waiting for file to be fully available
            while (true)
            {
                try
                {
                    using (var stream = File.Open(fileSystemEventArgs.FullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        if (stream != null)
                        {
                            break;
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                }
                catch (IOException)
                {
                }
                catch (UnauthorizedAccessException)
                {
                }

                Thread.Sleep(500);
            }
            this.Invoke(new Action(() =>
            {
                try
                {
                    var lastWriteTime = File.GetLastWriteTime(fileSystemEventArgs.FullPath);
                    if (lastWriteTime == LastRead) return;
                    UpdateFile(fileSystemEventArgs.FullPath, fileName);
                    LastRead = lastWriteTime;
                }
                catch (Exception ex)
                {
                    Log(tbLogPlugin, $"Assembly '{fileName}' was not updated. The reason is exception raised: '{ex.Message}'.", MessageType.ERROR);
                }
            }));
        }

        private void UpdateFile(string filePath, string fileName)
        {
            if (fileName.EndsWith(".js"))
            {
                Log(tbLogJavascript, $"Javascript {fileName} changed in local filesystem");
                var result = UpdateFile(filePath, fileName, $@"
                <fetch count='1' >
                    <entity name='webresource' >
                    <attribute name='name' />
                    <attribute name='displayname' />
                    <filter>
                        <condition attribute='webresourcetype' operator='eq' value='3' />
                        <condition attribute='displayname' operator='eq' value='{fileName}' />
                    </filter>
                    </entity>
                </fetch>", ResourceType.Javascript);
                if (!result)
                {
                    Log(tbLogJavascript, $"Cannot locate {fileName} in CRM");
                    CreateJavascriptWebresource(filePath, fileName);
                }
                else
                {
                    Log(tbLogJavascript, $"Javascript {fileName} updated in CRM");
                }
            }
            else if (fileName.EndsWith(".dll"))
            {
                Log(tbLogPlugin, $"Assembly {fileName} changed in local filesystem");
                var result = UpdateFile(filePath, fileName, $@"
                <fetch count='1' >
                    <entity name='pluginassembly' >
                    <attribute name='pluginassemblyid' />
                    <attribute name='name' />
                    <attribute name='ismanaged' />
                    <attribute name='path' />
                    <filter>
                        <condition attribute='name' operator='eq' value='{fileName.Replace(".dll", "")}' />
                    </filter>
                    </entity>
                </fetch>", ResourceType.Assembly);
                if (!result)
                {
                    Log(tbLogPlugin, $"Cannot locate {fileName} in CRM");
                    var assemblyId = CreateAssembly(filePath);
                    if (assemblyId != Guid.Empty)
                    {
                        ParseAssemblyAndRegisterSteps(filePath, assemblyId);
                    }
                }
                else
                {
                    Log(tbLogPlugin, $"Assembly {fileName} updated in CRM");
                }
            }
        }

        private void ParseAssemblyAndRegisterSteps(string filePath, Guid assemblyId)
        {
            var assembly = Assembly.LoadFrom(filePath);
            foreach (var exportedType in assembly.ExportedTypes.Where(x => x.IsClass))
            {
                if (exportedType.IsSubclassOf(typeof(System.Activities.Activity)) ||
                    (exportedType.GetInterface(typeof(IPlugin).Name) != null && exportedType.BaseType != typeof(Object)))
                {
                    RegisterTypeAsStep(exportedType, assembly, assemblyId);
                }
            }
        }

        private void RegisterTypeAsStep(Type exportedType, Assembly assembly, Guid assemblyId)
        {
            var assemblyName = assembly.GetName();
            var assemblyStep = new Entity("plugintype");
            assemblyStep["pluginassemblyid"] = new EntityReference("pluginassembly", assemblyId);
            assemblyStep["typename"] = exportedType.FullName;
            assemblyStep["friendlyname"] = exportedType.Name;
            assemblyStep["name"] = exportedType.FullName;
            if (exportedType.IsSubclassOf(typeof(System.Activities.Activity)))
            {
                assemblyStep["workflowactivitygroupname"] = $"{assemblyName.Name} ({assemblyName.Version.ToString()})"; ;
            }
            try
            {
                Service.Create(assemblyStep);
                Log(tbLogPlugin, $"Registered step {exportedType.Name}");
            }
            catch (FaultException<OrganizationServiceFault> fe)
            {
                Log(tbLogPlugin, $"Unable to register step {exportedType.Name}. {fe.Message}", MessageType.ERROR);
            }
        }

        private void CreateJavascriptWebresource(string filePath, string fileName)
        {
            Log(tbLogJavascript, $"Attempting to create javascript webresource {fileName} in CRM");
            var jsWebResource = new Entity("webresource");
            jsWebResource["name"] = "scripts_/"+string.Join("\\", filePath.Split('\\').Skip(1)).Replace("\\","/");
            jsWebResource["displayname"] = fileName;
            jsWebResource["webresourcetype"] = new OptionSetValue(3);//javascript
            jsWebResource["content"] = Convert.ToBase64String(ReadFile(filePath));
            try
            {
                Service.Create(jsWebResource);
            }
            catch (FaultException<OrganizationServiceFault> fe)
            {
                Log(tbLogJavascript, $"Unable to create Javascript webresource {fileName}. {fe.Message}", MessageType.ERROR);
            }
            Log(tbLogJavascript, $"Created Javascript webresource {fileName} in CRM");
        }

        private Guid CreateAssembly(string filePath)
        {
            var assemblyId = Guid.Empty;
            Log(tbLogPlugin, $"Attempting to create assembly in {filePath} in CRM");
            var assembly = Assembly.LoadFrom(filePath);
            var assemblyName = assembly.GetName();
            var assemblyTokenBytes = assemblyName.GetPublicKeyToken();
            var crmAssembly = new Entity("pluginassembly");
            crmAssembly["name"] = assemblyName.Name;
            crmAssembly["sourcetype"] = new OptionSetValue(0); //database
            crmAssembly["content"] = Convert.ToBase64String(ReadFile(filePath));
            crmAssembly["culture"] = assemblyName.CultureInfo == System.Globalization.CultureInfo.InvariantCulture ? "neutral" : assemblyName.CultureInfo.Name;
            if(assemblyTokenBytes == null || assemblyTokenBytes.Length == 0)
            {
                crmAssembly["publickeytoken"] = null;
            }
            else
            {
                crmAssembly["publickeytoken"] = string.Join(string.Empty, assemblyTokenBytes.Select(b => b.ToString("X2")));
            }
            crmAssembly["version"] = assemblyName.Version.ToString();
            crmAssembly["name"] = assemblyName.Name;
            if (ConnectionDetail.UseOnline)
            {
                crmAssembly["isolationmode"] = new OptionSetValue((int)Common.SDK.IsolationMode.Sandbox);
            }
            else
            {
                crmAssembly["isolationmode"] = new OptionSetValue((int)Common.SDK.IsolationMode.None);
            }
            try
            {
                assemblyId = Service.Create(crmAssembly);
            }
            catch (FaultException<OrganizationServiceFault> fe)
            {
                Log(tbLogPlugin, $"Unable to create assembly {assemblyName.Name}. {fe.Message}", MessageType.ERROR);
            }
            Log(tbLogPlugin, $"Created assembly {assemblyName.Name} in CRM");
            return assemblyId;
        }

        private bool UpdateFile(string filePath, string fileName, string fetchXml, ResourceType resourceType)
        {
            var result = Service.RetrieveMultiple(new FetchExpression(fetchXml));
            if (!result.Entities.Any()) return false;
            var resourceToUpdate = result[0];
            resourceToUpdate["content"] = Convert.ToBase64String(ReadFile(filePath));
            try
            {
                Service.Update(resourceToUpdate);
                return true;
            }
            catch (FaultException<OrganizationServiceFault> fe)
            {
                if(resourceType == ResourceType.Javascript)
                    Log(tbLogJavascript, $"Unable to update Javascript webresource {fileName}. {fe.Message}", MessageType.ERROR);
                else
                    Log(tbLogPlugin, $"Unable to update assembly {fileName}. {fe.Message}", MessageType.ERROR);
                return false;
            }
        }

        private byte[] ReadFile(string fileName)
        {
            byte[] buffer = null;
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
            }
            return buffer;
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            // Preparing to dispose watcher
            if (this.Watcher != null)
            {
                this.Watcher.Changed -= this.Watcher_OnChanged;
            }

            this.CloseTool();
        }

        #endregion Private Methods

        private void StopWatch_Click(object sender, EventArgs e)
        {
            var sourceButton = ((Button)sender);
            sourceButton.Enabled = false;
            Watcher.Changed -= Watcher_OnChanged;
            if (sourceButton.Name == "bPluginStopWatch")
            {
                Log(tbLogPlugin, $"Stopped watching changes in {fbdWatch.SelectedPath}");
                lblPluginWatch.Text = "";
            }
            else if (sourceButton.Name == "bJavascriptStopWatch")
            {
                Log(tbLogJavascript, $"Stopped watching changes in {fbdWatch.SelectedPath}");
                lblJavascriptWatch.Text = "";
            }
        }

        private void Log(TextBox t, string message, MessageType messageType = MessageType.INFORMATION)
        {
            t.AppendText($"{DateTime.Now.ToString("G")}: { (messageType == MessageType.ERROR ? "ERROR " : string.Empty) }{message}{Environment.NewLine}");
        }
    }

    enum ResourceType
    {
        Javascript,
        Assembly
    }

    enum MessageType
    {
        INFORMATION,
        ERROR
    }
}
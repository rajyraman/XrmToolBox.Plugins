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

    public partial class MainControl : PluginControlBase, IGitHubPlugin
    {
        #region Public Constructors

        public MainControl()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Properties

        string IGitHubPlugin.RepositoryName
        {
            get
            {
                return "XrmToolBox.Plugins";
            }
        }

        string IGitHubPlugin.UserName
        {
            get
            {
                return "Cinteros";
            }
        }

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
                lblPluginWatch.Text = "Watching " + fbdWatch.SelectedPath;
            }
            else if (sourceButton == "bJavascriptStartWatch")
            {
                this.Watcher.Filter = "*.js";
                lblJavascriptWatch.Text = "Watching " + fbdWatch.SelectedPath;
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
                    tbLogPlugin.Text += string.Format("{0}: Assembly '{1}' was not updated. The reason is exception raised: '{2}'.\r\n", DateTime.Now, fileName, ex.Message);
                }
            }));
        }

        private void UpdateFile(string filePath, string fileName)
        {
            if (fileName.EndsWith(".js"))
            {
                tbLogJavascript.AppendText(string.Format("{1}: Javascript {0} changed in local filesystem{2} ", fileName, DateTime.Now.ToString("G"), Environment.NewLine));
                var result = UpdateFile(filePath, fileName, string.Format(@"
                <fetch count='1' >
                    <entity name='webresource' >
                    <attribute name='name' />
                    <attribute name='displayname' />
                    <filter>
                        <condition attribute='webresourcetype' operator='eq' value='3' />
                        <condition attribute='displayname' operator='eq' value='{0}' />
                    </filter>
                    </entity>
                </fetch>", fileName.Replace(".js", "")), "Javascript");
                if (!result)
                {
                    tbLogJavascript.AppendText(string.Format("{1}: Cannot locate {0} in CRM{2}", fileName,
                        DateTime.Now.ToString("G"), Environment.NewLine));
                }
                else
                {
                    tbLogJavascript.AppendText(string.Format("{1}: Javascript {0} updated in CRM{2}", fileName,
                        DateTime.Now.ToString("G"), Environment.NewLine));
                }
            }
            else if (fileName.EndsWith(".dll"))
            {
                tbLogPlugin.AppendText(string.Format("{1}: Assembly {0} changed in local filesystem{2}", fileName, DateTime.Now.ToString("G"), Environment.NewLine));
                var result = UpdateFile(filePath, fileName, string.Format(@"
                <fetch count='1' >
                    <entity name='pluginassembly' >
                    <attribute name='pluginassemblyid' />
                    <attribute name='name' />
                    <attribute name='ismanaged' />
                    <attribute name='path' />
                    <filter>
                        <condition attribute='name' operator='eq' value='{0}' />
                    </filter>
                    </entity>
                </fetch>", fileName.Replace(".dll", "")), "Assembly");
                if (!result)
                {
                    tbLogPlugin.AppendText(string.Format("{1}: Cannot locate {0} in CRM{2}", fileName,
                        DateTime.Now.ToString("G"), Environment.NewLine));
                }
                else
                {
                    tbLogPlugin.AppendText(string.Format("{1}: Assembly {0} updated in CRM{2}", fileName,
                        DateTime.Now.ToString("G"), Environment.NewLine));
                }
            }
        }

        private bool UpdateFile(string filePath, string fileName, string fetchXml, string resourceType)
        {
            var result = Service.RetrieveMultiple(new FetchExpression(fetchXml));
            if (!result.Entities.Any()) return false;
            var resourceToUpdate = result[0];
            resourceToUpdate["content"] = Convert.ToBase64String(ReadFile(filePath));
            Service.Update(resourceToUpdate);
            return true;
        }

        private Guid GetAssemblyId(string fileName)
        {
            var assembly = Assembly.Load(this.ReadFile(fileName));

            var chunks = assembly.FullName.Split(new string[] { ", ", "Version=", "Culture=", "PublicKeyToken=" }, StringSplitOptions.RemoveEmptyEntries);

            var query = new QueryExpression("pluginassembly");
            query.Criteria.AddCondition("name", ConditionOperator.Equal, chunks[0]);
            query.Criteria.AddCondition("version", ConditionOperator.Equal, chunks[1]);
            query.Criteria.AddCondition("culture", ConditionOperator.Equal, chunks[2]);
            query.Criteria.AddCondition("publickeytoken", ConditionOperator.Equal, chunks[3]);

            var plugin = this.Service == null ? null : this.Service.RetrieveMultiple(query).Entities.FirstOrDefault();

            if (plugin != null)
            {
                return plugin.Id;
            }
            else
            {
                return Guid.Empty;
            }
        }

        private void Plugin_Changed(object sender, FileSystemEventArgs e)
        {
            // Waiting for plugin become fully available for reading
            while (true)
            {
                try
                {
                    using (var stream = File.Open(e.FullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
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
                        var lastWriteTime = File.GetLastWriteTime(this.lPlugin.Text);
                        if (lastWriteTime != LastRead)
                        {
                            tbLogPlugin.Text += string.Format("{0}: Assembly '{1}' was changed.\r\n", DateTime.Now, Path.GetFileName(this.lPlugin.Text));

                            var plugin = new Entity("pluginassembly")
                            {
                                Id = this.PluginId
                            };

                            plugin["content"] = Convert.ToBase64String(this.ReadFile(this.lPlugin.Text));

                            this.Service.Update(plugin);

                            tbLogPlugin.Text += string.Format("{0}: Assembly '{1}' was updated on the server.\r\n", DateTime.Now, Path.GetFileName(this.lPlugin.Text));

                            LastRead = lastWriteTime;
                        }
                    }
                    catch (Exception ex)
                    {
                        tbLogPlugin.Text += string.Format("{0}: Assembly '{1}' was not updated. The reason is exception raised: '{2}'.\r\n", DateTime.Now, Path.GetFileName(this.lPlugin.Text), ex.Message);
                    }
                }));
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
                this.Watcher.Changed -= this.Plugin_Changed;
            }

            this.CloseTool();
        }

        #endregion Private Methods

        private void StopWatch_Click(object sender, EventArgs e)
        {
            var sourceButton = ((Button)sender).Name;
            Watcher.Changed -= Watcher_OnChanged;
            if (sourceButton == "bPluginStopWatch")
            {
                tbLogPlugin.AppendText(string.Format("{2}: Stopped watching changes in {0}{1}", fbdWatch.SelectedPath, Environment.NewLine, DateTime.Now.ToString("G")));
                lblPluginWatch.Text = "";
            }
            else if (sourceButton == "bJavascriptStopWatch")
            {
                tbLogJavascript.AppendText(string.Format("{2}: Stopped watching changes in {0}{1}", fbdWatch.SelectedPath, Environment.NewLine, DateTime.Now.ToString("G")));
                lblJavascriptWatch.Text = "";
            }
        }
    }
}
namespace Cinteros.Xrm.AutoDeployTool
{
    partial class MainControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainControl));
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.bPluginStartWatch = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbPlugin = new System.Windows.Forms.GroupBox();
            this.bPluginStopWatch = new System.Windows.Forms.Button();
            this.lblPluginWatch = new System.Windows.Forms.Label();
            this.lPlugin = new System.Windows.Forms.Label();
            this.tbLogPlugin = new System.Windows.Forms.TextBox();
            this.ofdPlugin = new System.Windows.Forms.OpenFileDialog();
            this.tabWatch = new System.Windows.Forms.TabControl();
            this.tabPluginPage = new System.Windows.Forms.TabPage();
            this.tabWebresources = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gbJavascript = new System.Windows.Forms.GroupBox();
            this.lblJavascriptWatch = new System.Windows.Forms.Label();
            this.bJavascriptStopWatch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bJavascriptStartWatch = new System.Windows.Forms.Button();
            this.tbLogJavascript = new System.Windows.Forms.TextBox();
            this.fbdWatch = new System.Windows.Forms.FolderBrowserDialog();
            this.tsMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbPlugin.SuspendLayout();
            this.tabWatch.SuspendLayout();
            this.tabPluginPage.SuspendLayout();
            this.tabWebresources.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gbJavascript.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMenu
            // 
            this.tsMenu.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsMenu.Size = new System.Drawing.Size(1100, 37);
            this.tsMenu.TabIndex = 0;
            this.tsMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(95, 34);
            this.tsbClose.Text = "Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // bPluginStartWatch
            // 
            this.bPluginStartWatch.Location = new System.Drawing.Point(11, 28);
            this.bPluginStartWatch.Margin = new System.Windows.Forms.Padding(6);
            this.bPluginStartWatch.Name = "bPluginStartWatch";
            this.bPluginStartWatch.Size = new System.Drawing.Size(149, 42);
            this.bPluginStartWatch.TabIndex = 1;
            this.bPluginStartWatch.Text = "Start Watching";
            this.bPluginStartWatch.UseVisualStyleBackColor = true;
            this.bPluginStartWatch.Click += new System.EventHandler(this.StartWatch_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.gbPlugin, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbLogPlugin, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1086, 658);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // gbPlugin
            // 
            this.gbPlugin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbPlugin.Controls.Add(this.bPluginStopWatch);
            this.gbPlugin.Controls.Add(this.lblPluginWatch);
            this.gbPlugin.Controls.Add(this.lPlugin);
            this.gbPlugin.Controls.Add(this.bPluginStartWatch);
            this.gbPlugin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPlugin.Location = new System.Drawing.Point(6, 6);
            this.gbPlugin.Margin = new System.Windows.Forms.Padding(6);
            this.gbPlugin.Name = "gbPlugin";
            this.gbPlugin.Padding = new System.Windows.Forms.Padding(6);
            this.gbPlugin.Size = new System.Drawing.Size(1074, 80);
            this.gbPlugin.TabIndex = 2;
            this.gbPlugin.TabStop = false;
            this.gbPlugin.Text = "Plugin/Workflow watch folder";
            // 
            // bPluginStopWatch
            // 
            this.bPluginStopWatch.Enabled = false;
            this.bPluginStopWatch.Location = new System.Drawing.Point(169, 28);
            this.bPluginStopWatch.Name = "bPluginStopWatch";
            this.bPluginStopWatch.Size = new System.Drawing.Size(158, 42);
            this.bPluginStopWatch.TabIndex = 4;
            this.bPluginStopWatch.Text = "Stop Watching";
            this.bPluginStopWatch.UseVisualStyleBackColor = true;
            this.bPluginStopWatch.Click += new System.EventHandler(this.StopWatch_Click);
            // 
            // lblPluginWatch
            // 
            this.lblPluginWatch.AutoSize = true;
            this.lblPluginWatch.Location = new System.Drawing.Point(333, 37);
            this.lblPluginWatch.Name = "lblPluginWatch";
            this.lblPluginWatch.Size = new System.Drawing.Size(0, 25);
            this.lblPluginWatch.TabIndex = 3;
            // 
            // lPlugin
            // 
            this.lPlugin.AutoSize = true;
            this.lPlugin.Location = new System.Drawing.Point(160, 37);
            this.lPlugin.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lPlugin.Name = "lPlugin";
            this.lPlugin.Size = new System.Drawing.Size(0, 25);
            this.lPlugin.TabIndex = 2;
            // 
            // tbLogPlugin
            // 
            this.tbLogPlugin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLogPlugin.Enabled = false;
            this.tbLogPlugin.Location = new System.Drawing.Point(6, 98);
            this.tbLogPlugin.Margin = new System.Windows.Forms.Padding(6);
            this.tbLogPlugin.Multiline = true;
            this.tbLogPlugin.Name = "tbLogPlugin";
            this.tbLogPlugin.Size = new System.Drawing.Size(1074, 554);
            this.tbLogPlugin.TabIndex = 3;
            // 
            // ofdPlugin
            // 
            this.ofdPlugin.FileName = "openFileDialog1";
            // 
            // tabWatch
            // 
            this.tabWatch.Controls.Add(this.tabPluginPage);
            this.tabWatch.Controls.Add(this.tabWebresources);
            this.tabWatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabWatch.Location = new System.Drawing.Point(0, 37);
            this.tabWatch.Name = "tabWatch";
            this.tabWatch.SelectedIndex = 0;
            this.tabWatch.Size = new System.Drawing.Size(1100, 701);
            this.tabWatch.TabIndex = 3;
            // 
            // tabPluginPage
            // 
            this.tabPluginPage.Controls.Add(this.tableLayoutPanel1);
            this.tabPluginPage.Location = new System.Drawing.Point(4, 33);
            this.tabPluginPage.Name = "tabPluginPage";
            this.tabPluginPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPluginPage.Size = new System.Drawing.Size(1092, 664);
            this.tabPluginPage.TabIndex = 0;
            this.tabPluginPage.Text = "Plugin/Workflow";
            this.tabPluginPage.UseVisualStyleBackColor = true;
            // 
            // tabWebresources
            // 
            this.tabWebresources.Controls.Add(this.tableLayoutPanel2);
            this.tabWebresources.Location = new System.Drawing.Point(4, 33);
            this.tabWebresources.Name = "tabWebresources";
            this.tabWebresources.Padding = new System.Windows.Forms.Padding(3);
            this.tabWebresources.Size = new System.Drawing.Size(1092, 664);
            this.tabWebresources.TabIndex = 1;
            this.tabWebresources.Text = "Javascript";
            this.tabWebresources.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.gbJavascript, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbLogJavascript, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1086, 658);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // gbJavascript
            // 
            this.gbJavascript.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbJavascript.Controls.Add(this.lblJavascriptWatch);
            this.gbJavascript.Controls.Add(this.bJavascriptStopWatch);
            this.gbJavascript.Controls.Add(this.label1);
            this.gbJavascript.Controls.Add(this.bJavascriptStartWatch);
            this.gbJavascript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbJavascript.Location = new System.Drawing.Point(6, 6);
            this.gbJavascript.Margin = new System.Windows.Forms.Padding(6);
            this.gbJavascript.Name = "gbJavascript";
            this.gbJavascript.Padding = new System.Windows.Forms.Padding(6);
            this.gbJavascript.Size = new System.Drawing.Size(1074, 80);
            this.gbJavascript.TabIndex = 2;
            this.gbJavascript.TabStop = false;
            this.gbJavascript.Text = "Javascript watch folder";
            // 
            // lblJavascriptWatch
            // 
            this.lblJavascriptWatch.AutoSize = true;
            this.lblJavascriptWatch.Location = new System.Drawing.Point(334, 36);
            this.lblJavascriptWatch.Name = "lblJavascriptWatch";
            this.lblJavascriptWatch.Size = new System.Drawing.Size(0, 25);
            this.lblJavascriptWatch.TabIndex = 6;
            // 
            // bJavascriptStopWatch
            // 
            this.bJavascriptStopWatch.Enabled = false;
            this.bJavascriptStopWatch.Location = new System.Drawing.Point(169, 29);
            this.bJavascriptStopWatch.Name = "bJavascriptStopWatch";
            this.bJavascriptStopWatch.Size = new System.Drawing.Size(158, 42);
            this.bJavascriptStopWatch.TabIndex = 5;
            this.bJavascriptStopWatch.Text = "Stop Watching";
            this.bJavascriptStopWatch.UseVisualStyleBackColor = true;
            this.bJavascriptStopWatch.Click += new System.EventHandler(this.StopWatch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 25);
            this.label1.TabIndex = 2;
            // 
            // bJavascriptStartWatch
            // 
            this.bJavascriptStartWatch.Location = new System.Drawing.Point(11, 28);
            this.bJavascriptStartWatch.Margin = new System.Windows.Forms.Padding(6);
            this.bJavascriptStartWatch.Name = "bJavascriptStartWatch";
            this.bJavascriptStartWatch.Size = new System.Drawing.Size(149, 42);
            this.bJavascriptStartWatch.TabIndex = 1;
            this.bJavascriptStartWatch.Text = "Start Watching";
            this.bJavascriptStartWatch.UseVisualStyleBackColor = true;
            this.bJavascriptStartWatch.Click += new System.EventHandler(this.StartWatch_Click);
            // 
            // tbLogJavascript
            // 
            this.tbLogJavascript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLogJavascript.Enabled = false;
            this.tbLogJavascript.Location = new System.Drawing.Point(6, 98);
            this.tbLogJavascript.Margin = new System.Windows.Forms.Padding(6);
            this.tbLogJavascript.Multiline = true;
            this.tbLogJavascript.Name = "tbLogJavascript";
            this.tbLogJavascript.Size = new System.Drawing.Size(1074, 554);
            this.tbLogJavascript.TabIndex = 3;
            // 
            // fbdWatch
            // 
            this.fbdWatch.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.fbdWatch.ShowNewFolderButton = false;
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tabWatch);
            this.Controls.Add(this.tsMenu);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(1100, 738);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gbPlugin.ResumeLayout(false);
            this.gbPlugin.PerformLayout();
            this.tabWatch.ResumeLayout(false);
            this.tabPluginPage.ResumeLayout(false);
            this.tabPluginPage.PerformLayout();
            this.tabWebresources.ResumeLayout(false);
            this.tabWebresources.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.gbJavascript.ResumeLayout(false);
            this.gbJavascript.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.Button bPluginStartWatch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gbPlugin;
        private System.Windows.Forms.OpenFileDialog ofdPlugin;
        private System.Windows.Forms.Label lPlugin;
        private System.Windows.Forms.TextBox tbLogPlugin;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.TabControl tabWatch;
        private System.Windows.Forms.TabPage tabPluginPage;
        private System.Windows.Forms.TabPage tabWebresources;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox gbJavascript;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bJavascriptStartWatch;
        private System.Windows.Forms.TextBox tbLogJavascript;
        private System.Windows.Forms.FolderBrowserDialog fbdWatch;
        private System.Windows.Forms.Label lblPluginWatch;
        private System.Windows.Forms.Button bPluginStopWatch;
        private System.Windows.Forms.Button bJavascriptStopWatch;
        private System.Windows.Forms.Label lblJavascriptWatch;
    }
}

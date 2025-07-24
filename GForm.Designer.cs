namespace GameHeader
{
    partial class GForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.msMainMenu = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFileReadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFileReadDir = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReadDirRec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFileSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveToCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEngine = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEngineAutodetect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEngineHandheld = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEngineGBA = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEngineGBC = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEngineGB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEngineNDS = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEngineConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEngineFDS = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEngineN64 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEngineNES = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEngineNGC = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEngineSNES = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpOutput = new System.Windows.Forms.TabPage();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.pbFiles = new System.Windows.Forms.ProgressBar();
            this.bPause = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.pbFile = new System.Windows.Forms.ProgressBar();
            this.ofGame = new System.Windows.Forms.OpenFileDialog();
            this.fbGame = new System.Windows.Forms.FolderBrowserDialog();
            this.sfOutput = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip.SuspendLayout();
            this.msMainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpOutput.SuspendLayout();
            this.tpLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 410);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(834, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee";
            // 
            // statusStripLabel
            // 
            this.statusStripLabel.Name = "statusStripLabel";
            this.statusStripLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // msMainMenu
            // 
            this.msMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmSave,
            this.tsmEngine,
            this.tsmHelp});
            this.msMainMenu.Location = new System.Drawing.Point(0, 0);
            this.msMainMenu.Name = "msMainMenu";
            this.msMainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.msMainMenu.Size = new System.Drawing.Size(834, 24);
            this.msMainMenu.TabIndex = 1;
            this.msMainMenu.Text = "menuStrip1";
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFileReadFile,
            this.tsmFileReadDir,
            this.tsmReadDirRec,
            this.tsmFileSeparator,
            this.tsmFileExit});
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(37, 20);
            this.tsmFile.Text = "File";
            // 
            // tsmFileReadFile
            // 
            this.tsmFileReadFile.Name = "tsmFileReadFile";
            this.tsmFileReadFile.Size = new System.Drawing.Size(221, 22);
            this.tsmFileReadFile.Text = "Read File(s)...";
            this.tsmFileReadFile.Click += new System.EventHandler(this.openRomToolStripMenuItem_Click);
            // 
            // tsmFileReadDir
            // 
            this.tsmFileReadDir.Name = "tsmFileReadDir";
            this.tsmFileReadDir.Size = new System.Drawing.Size(221, 22);
            this.tsmFileReadDir.Text = "Read Directory...";
            this.tsmFileReadDir.Click += new System.EventHandler(this.openDirectoryToolStripMenuItem_Click);
            // 
            // tsmReadDirRec
            // 
            this.tsmReadDirRec.Name = "tsmReadDirRec";
            this.tsmReadDirRec.Size = new System.Drawing.Size(221, 22);
            this.tsmReadDirRec.Text = "Read Directory... (Recursive)";
            this.tsmReadDirRec.Click += new System.EventHandler(this.openDirectoryRecursiveToolStripMenuItem_Click);
            // 
            // tsmFileSeparator
            // 
            this.tsmFileSeparator.Name = "tsmFileSeparator";
            this.tsmFileSeparator.Size = new System.Drawing.Size(218, 6);
            // 
            // tsmFileExit
            // 
            this.tsmFileExit.Name = "tsmFileExit";
            this.tsmFileExit.Size = new System.Drawing.Size(221, 22);
            this.tsmFileExit.Text = "Exit";
            this.tsmFileExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tsmSave
            // 
            this.tsmSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSaveToClipboard,
            this.tsmSaveToCSV,
            this.tsmSaveToFile});
            this.tsmSave.Name = "tsmSave";
            this.tsmSave.Size = new System.Drawing.Size(43, 20);
            this.tsmSave.Text = "Save";
            // 
            // tsmSaveToClipboard
            // 
            this.tsmSaveToClipboard.Name = "tsmSaveToClipboard";
            this.tsmSaveToClipboard.Size = new System.Drawing.Size(141, 22);
            this.tsmSaveToClipboard.Text = "To Clipboard";
            this.tsmSaveToClipboard.Click += new System.EventHandler(this.toClipboardToolStripMenuItem1_Click);
            // 
            // tsmSaveToCSV
            // 
            this.tsmSaveToCSV.Name = "tsmSaveToCSV";
            this.tsmSaveToCSV.Size = new System.Drawing.Size(141, 22);
            this.tsmSaveToCSV.Text = "To CSV";
            this.tsmSaveToCSV.Click += new System.EventHandler(this.tsmSaveToCSV_Click);
            // 
            // tsmSaveToFile
            // 
            this.tsmSaveToFile.Name = "tsmSaveToFile";
            this.tsmSaveToFile.Size = new System.Drawing.Size(141, 22);
            this.tsmSaveToFile.Text = "To File";
            this.tsmSaveToFile.Click += new System.EventHandler(this.toFileToolStripMenuItem1_Click);
            // 
            // tsmEngine
            // 
            this.tsmEngine.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmEngineAutodetect,
            this.tsmEngineHandheld,
            this.tsmEngineConsole});
            this.tsmEngine.Name = "tsmEngine";
            this.tsmEngine.Size = new System.Drawing.Size(55, 20);
            this.tsmEngine.Text = "Engine";
            // 
            // tsmEngineAutodetect
            // 
            this.tsmEngineAutodetect.Checked = true;
            this.tsmEngineAutodetect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmEngineAutodetect.Name = "tsmEngineAutodetect";
            this.tsmEngineAutodetect.Size = new System.Drawing.Size(193, 22);
            this.tsmEngineAutodetect.Text = "Detect From Extension";
            this.tsmEngineAutodetect.Click += new System.EventHandler(this.onlyHashCaclToolStripMenuItem_Click);
            // 
            // tsmEngineHandheld
            // 
            this.tsmEngineHandheld.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmEngineGBA,
            this.tsmEngineGBC,
            this.tsmEngineGB,
            this.tsmEngineNDS});
            this.tsmEngineHandheld.Name = "tsmEngineHandheld";
            this.tsmEngineHandheld.Size = new System.Drawing.Size(193, 22);
            this.tsmEngineHandheld.Text = "Handheld";
            // 
            // tsmEngineGBA
            // 
            this.tsmEngineGBA.Name = "tsmEngineGBA";
            this.tsmEngineGBA.Size = new System.Drawing.Size(97, 22);
            this.tsmEngineGBA.Text = "GBA";
            this.tsmEngineGBA.Click += new System.EventHandler(this.tsmEngineGBA_Click);
            // 
            // tsmEngineGBC
            // 
            this.tsmEngineGBC.Name = "tsmEngineGBC";
            this.tsmEngineGBC.Size = new System.Drawing.Size(97, 22);
            this.tsmEngineGBC.Text = "GBC";
            this.tsmEngineGBC.Click += new System.EventHandler(this.tsmEngineGBC_Click);
            // 
            // tsmEngineGB
            // 
            this.tsmEngineGB.Name = "tsmEngineGB";
            this.tsmEngineGB.Size = new System.Drawing.Size(97, 22);
            this.tsmEngineGB.Text = "GB";
            this.tsmEngineGB.Click += new System.EventHandler(this.tsmEngineGB_Click);
            // 
            // tsmEngineNDS
            // 
            this.tsmEngineNDS.Name = "tsmEngineNDS";
            this.tsmEngineNDS.Size = new System.Drawing.Size(97, 22);
            this.tsmEngineNDS.Text = "NDS";
            this.tsmEngineNDS.Click += new System.EventHandler(this.tsmEngineNDS_Click);
            // 
            // tsmEngineConsole
            // 
            this.tsmEngineConsole.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmEngineFDS,
            this.tsmEngineN64,
            this.tsmEngineNES,
            this.tsmEngineNGC,
            this.tsmEngineSNES});
            this.tsmEngineConsole.Name = "tsmEngineConsole";
            this.tsmEngineConsole.Size = new System.Drawing.Size(193, 22);
            this.tsmEngineConsole.Text = "Console";
            // 
            // tsmEngineFDS
            // 
            this.tsmEngineFDS.Name = "tsmEngineFDS";
            this.tsmEngineFDS.Size = new System.Drawing.Size(101, 22);
            this.tsmEngineFDS.Text = "FDS";
            this.tsmEngineFDS.Click += new System.EventHandler(this.tsmEngineFDS_Click);
            // 
            // tsmEngineN64
            // 
            this.tsmEngineN64.Name = "tsmEngineN64";
            this.tsmEngineN64.Size = new System.Drawing.Size(101, 22);
            this.tsmEngineN64.Text = "N64";
            this.tsmEngineN64.Click += new System.EventHandler(this.tsmEngineN64_Click);
            // 
            // tsmEngineNES
            // 
            this.tsmEngineNES.Name = "tsmEngineNES";
            this.tsmEngineNES.Size = new System.Drawing.Size(101, 22);
            this.tsmEngineNES.Text = "NES";
            this.tsmEngineNES.Click += new System.EventHandler(this.tsmEngineNES_Click);
            // 
            // tsmEngineNGC
            // 
            this.tsmEngineNGC.Name = "tsmEngineNGC";
            this.tsmEngineNGC.Size = new System.Drawing.Size(101, 22);
            this.tsmEngineNGC.Text = "NGC";
            this.tsmEngineNGC.Click += new System.EventHandler(this.tsmEngineNGC_Click);
            // 
            // tsmEngineSNES
            // 
            this.tsmEngineSNES.Name = "tsmEngineSNES";
            this.tsmEngineSNES.Size = new System.Drawing.Size(101, 22);
            this.tsmEngineSNES.Text = "SNES";
            this.tsmEngineSNES.Click += new System.EventHandler(this.tsmEngineSNES_Click);
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmHelpAbout});
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(44, 20);
            this.tsmHelp.Text = "Help";
            // 
            // tsmHelpAbout
            // 
            this.tsmHelpAbout.Name = "tsmHelpAbout";
            this.tsmHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.tsmHelpAbout.Text = "About";
            this.tsmHelpAbout.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tcMain);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pbFiles);
            this.splitContainer2.Panel2.Controls.Add(this.bPause);
            this.splitContainer2.Panel2.Controls.Add(this.bCancel);
            this.splitContainer2.Panel2.Controls.Add(this.pbFile);
            this.splitContainer2.Size = new System.Drawing.Size(834, 386);
            this.splitContainer2.SplitterDistance = 320;
            this.splitContainer2.TabIndex = 0;
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tpOutput);
            this.tcMain.Controls.Add(this.tpLog);
            this.tcMain.Location = new System.Drawing.Point(3, 3);
            this.tcMain.Multiline = true;
            this.tcMain.Name = "tcMain";
            this.tcMain.Padding = new System.Drawing.Point(16, 3);
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(828, 314);
            this.tcMain.TabIndex = 0;
            // 
            // tpOutput
            // 
            this.tpOutput.Controls.Add(this.tbOutput);
            this.tpOutput.Location = new System.Drawing.Point(4, 22);
            this.tpOutput.Name = "tpOutput";
            this.tpOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tpOutput.Size = new System.Drawing.Size(820, 288);
            this.tpOutput.TabIndex = 0;
            this.tpOutput.Text = "Output";
            this.tpOutput.UseVisualStyleBackColor = true;
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.Location = new System.Drawing.Point(3, 3);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutput.Size = new System.Drawing.Size(814, 285);
            this.tbOutput.TabIndex = 1;
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.tbLog);
            this.tpLog.Location = new System.Drawing.Point(4, 22);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(820, 288);
            this.tpLog.TabIndex = 1;
            this.tpLog.Text = "Log";
            this.tpLog.UseVisualStyleBackColor = true;
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLog.Location = new System.Drawing.Point(3, 3);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLog.Size = new System.Drawing.Size(814, 285);
            this.tbLog.TabIndex = 2;
            // 
            // pbFiles
            // 
            this.pbFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbFiles.Location = new System.Drawing.Point(9, 33);
            this.pbFiles.Name = "pbFiles";
            this.pbFiles.Size = new System.Drawing.Size(734, 22);
            this.pbFiles.TabIndex = 3;
            // 
            // bPause
            // 
            this.bPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bPause.Enabled = false;
            this.bPause.Location = new System.Drawing.Point(750, 5);
            this.bPause.Name = "bPause";
            this.bPause.Size = new System.Drawing.Size(76, 22);
            this.bPause.TabIndex = 2;
            this.bPause.Text = "Pause";
            this.bPause.UseVisualStyleBackColor = true;
            this.bPause.Click += new System.EventHandler(this.bPause_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Location = new System.Drawing.Point(750, 32);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(76, 23);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // pbFile
            // 
            this.pbFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbFile.Location = new System.Drawing.Point(9, 5);
            this.pbFile.Name = "pbFile";
            this.pbFile.Size = new System.Drawing.Size(734, 22);
            this.pbFile.TabIndex = 0;
            // 
            // ofGame
            // 
            this.ofGame.FileName = "openFileDialog1";
            this.ofGame.Filter = "All Files (*.*)|*.*|Supported Roms (*.gb;*.gbc;*.nds;*.zip)|*.gb;*.gbc;*.nds;*.n6" +
    "4;*.zip";
            this.ofGame.FilterIndex = 0;
            this.ofGame.Multiselect = true;
            // 
            // fbGame
            // 
            this.fbGame.ShowNewFolderButton = false;
            // 
            // GForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 432);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.msMainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMainMenu;
            this.Name = "GForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Header";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GForm_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.GForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.GForm_DragEnter);
            this.Resize += new System.EventHandler(this.GForm_Resize);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.msMainMenu.ResumeLayout(false);
            this.msMainMenu.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tpOutput.ResumeLayout(false);
            this.tpOutput.PerformLayout();
            this.tpLog.ResumeLayout(false);
            this.tpLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip msMainMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmFileReadFile;
        private System.Windows.Forms.ToolStripMenuItem tsmFileReadDir;
        private System.Windows.Forms.ToolStripMenuItem tsmReadDirRec;
        private System.Windows.Forms.ToolStripMenuItem tsmFileExit;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmHelpAbout;
        private System.Windows.Forms.ToolStripSeparator tsmFileSeparator;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpOutput;
        private System.Windows.Forms.ProgressBar pbFiles;
        private System.Windows.Forms.Button bPause;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.ProgressBar pbFile;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.OpenFileDialog ofGame;
        private System.Windows.Forms.FolderBrowserDialog fbGame;
        private System.Windows.Forms.SaveFileDialog sfOutput;
        private System.Windows.Forms.ToolStripMenuItem tsmSave;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveToClipboard;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveToFile;
        private System.Windows.Forms.ToolStripMenuItem tsmEngine;
        private System.Windows.Forms.ToolStripMenuItem tsmEngineAutodetect;
        private System.Windows.Forms.TabPage tpLog;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveToCSV;
        private System.Windows.Forms.ToolStripMenuItem tsmEngineHandheld;
        private System.Windows.Forms.ToolStripMenuItem tsmEngineGBA;
        private System.Windows.Forms.ToolStripMenuItem tsmEngineGBC;
        private System.Windows.Forms.ToolStripMenuItem tsmEngineGB;
        private System.Windows.Forms.ToolStripMenuItem tsmEngineNDS;
        private System.Windows.Forms.ToolStripMenuItem tsmEngineConsole;
        private System.Windows.Forms.ToolStripMenuItem tsmEngineFDS;
        private System.Windows.Forms.ToolStripMenuItem tsmEngineN64;
        private System.Windows.Forms.ToolStripMenuItem tsmEngineNES;
        private System.Windows.Forms.ToolStripMenuItem tsmEngineSNES;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabel;
        private System.Windows.Forms.ToolStripMenuItem tsmEngineNGC;
    }
}


using GameHeader.Abstract;
using GameHeader.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameHeader
{
    public partial class GForm : Form
    {
        private FileBackgroundWorker __file_worker;

        public GForm(string[] files)
        {
            base.SuspendLayout();
            this.InitializeComponent();
            this.__file_worker = new FileBackgroundWorker();
            this.UpdateGUIConfig();
            this.SetLoadBar(false);
            this.__file_worker.Site = this.Site;
            this.__file_worker.SetSevenZipPath("7z.dll");
            if (this.__file_worker.SevenZipEnabled)
                this.__file_worker.SetSevenZipExtensions((".rar,.7z,.zip").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
            this.__file_worker.FileStarted = (FileStartedEventHandler)Delegate.Combine(this.__file_worker.FileStarted, new FileStartedEventHandler(this.__file_worker_FileStarted));
            this.__file_worker.BlockRead = (BlockReadEventHandler)Delegate.Combine(this.__file_worker.BlockRead, new BlockReadEventHandler(this.__file_worker_BlockRead));
            this.__file_worker.ProgressUpdate = (ProgressUpdateEventHandler)Delegate.Combine(this.__file_worker.ProgressUpdate, new ProgressUpdateEventHandler(this.__file_worker_ProgressUpdate));
            this.__file_worker.FileCompleted = (FileCompletedEventHandler)Delegate.Combine(this.__file_worker.FileCompleted, new FileCompletedEventHandler(this.__file_worker_FileCompleted));
            this.__file_worker.ZipEntryCompleted = (ZipEntryCompletedEventHandler)Delegate.Combine(this.__file_worker.ZipEntryCompleted, new ZipEntryCompletedEventHandler(this.__file_worker_ZipEntryCompleted));
            this.__file_worker.ZipEntryStarted = (ZipEntryStartedEventHandler)Delegate.Combine(this.__file_worker.ZipEntryStarted, new ZipEntryStartedEventHandler(this.__file_worker_ZipEntryStarted));
            this.__file_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.__file_worker_RunWorkerCompleted);
            this.__file_worker.UpdateLog = (UpdateLogEventHandler)Delegate.Combine(this.__file_worker.UpdateLog, new UpdateLogEventHandler(this.__file_worker_UpdateLog));
            base.ResumeLayout(false);
        }

        // ****************************************************************************************
        // FORM UPDATE
        // ****************************************************************************************

        private void GForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.__file_worker.CancelAsync();
            while (this.__file_worker.IsBusy)
                Application.DoEvents();
        }

        private void GForm_Resize(object sender, EventArgs e)
        {
            if (base.WindowState == FormWindowState.Maximized)
                Program.Config.SetValue("GUI", "DefaultState", "Maximized");
            else if (base.WindowState == FormWindowState.Normal)
                Program.Config.SetValue("GUI", "DefaultState", "Normal");
            Program.Config.SetValue("GUI", "Width", base.Width.ToString());
            Program.Config.SetValue("GUI", "Height", base.Height.ToString());
        }

        public void SetLoadBar(bool open)
        {
            this.splitContainer2.Panel2Collapsed = !open;
            this.Refresh();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
         //   this.Refresh();
        }

        // ****************************************************************************************
        // FILE WORKER
        // ****************************************************************************************

        // after the job is completed, show some more output on GUI
        private void afterCompletedJob()
        {
            // hide LoadBar
            this.SetLoadBar(false);
            // update StatusStrip
            if (this.statusStripLabel.BackColor != Color.Red)
            {
                // show read files
                this.statusStripLabel.Text = "Read " + DebugReadFiles.ToString() + " file(s).";
                // skipped files?
                if (DebugTotalFiles != 0)
                {
                    this.statusStripLabel.BackColor = Color.Red;
                    this.statusStripLabel.Text = "WARNING: Check Log for errors.";
                }
            }
            // update log
            this.tbLog.Text += "\r\n";
            this.tbLog.Text += "-------------------------------------------------------------------\r\n";
            this.tbLog.Text += DateTime.Now.ToString() + " Job completed!";
            // move main TextBox's scrollbars to start
            this.tbOutput.Select(0, 0);
            this.tbOutput.ScrollToCaret();
        }

        // variables to count files, checking for skipped files
        private int DebugInputFiles; // total files requested for a scan
        private int DebugTotalFiles;
        private int DebugReadFiles;

        private void ExecuteWorker(FileInfo[] files)
        {
            if ((files.Length != 0) && !this.__file_worker.IsBusy)
            {
                DebugInputFiles = files.Length;
                DebugTotalFiles = 0;
                DebugReadFiles = 0;
                this.statusStripLabel.BackColor = SystemColors.Control;
                this.statusStripLabel.Text = "";
                this.tbOutput.Text = "";
                this.tbLog.Text += "File(s) to read: " + DebugInputFiles.ToString() + ".\r\n";
                this.tbLog.Text += "-------------------------------------------------------------------\r\n";
                this.pbFiles.Value = this.pbFiles.Minimum;
                this.pbFiles.Maximum = files.Length;
                this.pbFile.Value = this.pbFile.Minimum;
                this.SetLoadBar(true);
                string initialDirectory = Program.Config.GetValue("GUI", "LastScanPath");
                this.__file_worker.SetInitialDirectory(initialDirectory);
                this.__file_worker.SetFiles(files);
                this.__file_worker.RunWorkerAsync(files);
            }
        }

        private void __file_worker_BlockRead(object sender)
        {
            this.pbFile.Value++;
            int maximum = this.pbFile.Maximum;
            int num2 = this.pbFile.Value;
        }

        private void __file_worker_FileCompleted(object sender, FileCompletedEventArgs e)
        {
            this.pbFiles.Value++;
            if (e.Game != null)
            {
                this.tbOutput.AppendText(e.Game.Dump() + "\r\n");
                DebugReadFiles++;
            }
        }

        private void __file_worker_FileStarted(object sender, FileStartedEventArgs e)
        {
            FileWorkerInfo info = e.Info;
            this.statusStripLabel.Text = string.Concat(new object[] { this.pbFiles.Value + 1, " of ", this.pbFiles.Maximum, ": ", info.FileName });
            this.pbFile.Value = this.pbFile.Minimum;
            this.pbFile.Maximum = info.Blocks;
        }

        private void __file_worker_ProgressUpdate(object sender, ProgressUpdateEventArgs e)
        {
            this.pbFile.Value = e.Info.PercentDone;
            int maximum = this.pbFile.Maximum;
            int num2 = this.pbFile.Value;
        }

        private void __file_worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            afterCompletedJob();
        }

        private void __file_worker_WorkCompleted(object sender)
        {
            afterCompletedJob();
        }

        private void __file_worker_UpdateLog(object sender, UpdateLogEventArgs e)
        {
            if (e.Info.S == "$FILEINFO")
            {
                this.tbLog.AppendText("\r\n");
                this.tbLog.AppendText("File #" + ( DebugReadFiles + 1) + ":\r\n");
            }
            else if (e.Info.S == "$ARCHIVEDFILEINFO")
            {
                this.tbLog.AppendText("[ File #" + (DebugReadFiles + 1) + ": ");
            }
            else if (e.Info.S == "$WARNING")
            {
                this.tbLog.AppendText("WARNING!\r\n");
                this.statusStripLabel.BackColor = Color.Red;
                this.statusStripLabel.Text = "WARNING: Check Log for errors.";
            }
            else if (e.Info.S == "$CANCEL")
            {
                this.tbLog.AppendText("interrupted!\r\n");
                this.statusStripLabel.BackColor = Color.Red;
                this.statusStripLabel.Text = "JOB CANCELLED.";
            }
            else if (e.Info.S == "+1")
            {
                DebugTotalFiles++;
            }
            else if (e.Info.S == "-1")
            {
                DebugTotalFiles--;
            }
            else
            {
                this.tbLog.AppendText(e.Info.S);
            }
        }

        // ****************************************************************************************
        // ZIP WORKER
        // ****************************************************************************************

        private void __file_worker_ZipEntryCompleted(object sender, ZipEntryCompletedEventArgs e)
        {
            this.tbOutput.AppendText(e.Game.Dump() + "\r\n");
            DebugReadFiles++;
        }

        private void __file_worker_ZipEntryStarted(object sender, ZipEntryStartedEventArgs e)
        {
            ZipWorkerInfo info = e.Info;
            this.statusStripLabel.Text = string.Concat(new object[] { this.pbFiles.Value + 1, " of ", this.pbFiles.Maximum, ": ", info.FileName, " (", info.Number, " of ", info.Count, ": ", info.EntryName, ")" });
            this.pbFile.Value = this.pbFile.Minimum;
            this.pbFile.Maximum = info.Blocks;
            //this.statusStripLabel.Refresh();
        }

        // ****************************************************************************************
        // DRAG & DROP
        // ****************************************************************************************

        private void GForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll;
                string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);
                FileInfo[] files = new FileInfo[data.Length];
                for (int i = 0; i < data.Length; i++)
                    files[i] = new FileInfo(data[i]);
                Program.Config.SetValue("GUI", "LastScanPath", files[0].DirectoryName);
                Program.Config.WriteValues();
                this.tbLog.Text = DateTime.Now.ToString() +  " Job: Read dropped file(s).\r\n";
                this.ExecuteWorker(files);
            }
        }

        private void GForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll;
        }

        // ****************************************************************************************
        // MENU
        // ****************************************************************************************

        // used to get last path
        private string LastPath()
        {
            string dir = Program.Config.GetValue("GUI", "LastScanPath");
            if (dir == "")
                dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            while (!Directory.Exists(dir) || dir == null)
            {
                DirectoryInfo r = Directory.GetParent(dir);
                if (r == null)
                    dir = null;
                else
                    dir = r.FullName;
            }
            // set path
            if (dir != null)
                return dir;
            else
                return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        // MENU FILE / READ FILE(S)
        private void openRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ofGame.InitialDirectory = LastPath();
            this.ofGame.FileName = "";
            // open dialog
            if (this.ofGame.ShowDialog() == DialogResult.OK)
            {
                FileInfo[] files = new FileInfo[this.ofGame.FileNames.Length];
                for (int i = 0; i < this.ofGame.FileNames.Length; i++)
                    files[i] = new FileInfo(this.ofGame.FileNames[i]);
                Program.Config.SetValue("GUI", "LastScanPath", Path.GetDirectoryName(this.ofGame.FileNames[0]));
                Program.Config.WriteValues();
                this.tbLog.Text = DateTime.Now.ToString() + " Job: Read file(s).\r\n";
                this.ExecuteWorker(files);
            }
        }

        // used for recursive scan of directory
        private void scanDirectory(DirectoryInfo directory, List<FileInfo> files)
        {
            if (directory.Exists)
            {
                files.AddRange(directory.GetFiles());
                foreach (DirectoryInfo info in directory.GetDirectories())
                    this.scanDirectory(info, files);
            }
        }

        // MENU FILE / READ DIRECTORY
        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fbGame.SelectedPath = LastPath();
            // open dialog
            if (this.fbGame.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo info = new DirectoryInfo(this.fbGame.SelectedPath);
                if (info.Exists)
                {
                    Program.Config.SetValue("GUI", "LastScanPath", this.fbGame.SelectedPath);
                    Program.Config.WriteValues();
                    this.tbLog.Text = DateTime.Now.ToString() + " Job: Read directory.\r\n";
                    FileInfo[] files = info.GetFiles();
                    if (Program.Config.GetValue("GUI", "IgnoreHiddenFiles") == "True")
                        files = files.Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden)).ToArray();
                    this.ExecuteWorker(files);
                }
            }
        }

        // MENU FILE / READ DIRECTORY (RECURSIVE)
        private void openDirectoryRecursiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fbGame.SelectedPath = LastPath();
            // open dialog
            if (this.fbGame.ShowDialog() == DialogResult.OK)
            {
                List<FileInfo> fileList = new List<FileInfo>();
                DirectoryInfo directory = new DirectoryInfo(this.fbGame.SelectedPath);
                if (directory.Exists)
                {
                    Program.Config.SetValue("GUI", "LastScanPath", this.fbGame.SelectedPath);
                    Program.Config.WriteValues();
                    this.tbLog.Text = DateTime.Now.ToString() + " Job: Read directory recursively.\r\n";
                    this.scanDirectory(directory, fileList);
                    FileInfo[] files = fileList.ToArray();
                    if (Program.Config.GetValue("GUI", "IgnoreHiddenFiles") == "True")
                        files = files.Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden)).ToArray();
                    this.ExecuteWorker(files);
                }
            }
        }

        // MENU FILE / EXIT
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        // MENU SAVE / TO CLIPBOARD
        private void toClipboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.tbOutput.Text != "")
                Clipboard.SetText(this.tbOutput.Text);
        }

        // MENU SAVE / TO CSV
        private void tsmSaveToCSV_Click(object sender, EventArgs e)
        {
            this.sfOutput.DefaultExt = "csv";
            this.sfOutput.Filter = "Comma - separated values files (.csv)|*.csv";
            if (this.sfOutput.ShowDialog() == DialogResult.OK)
                using (StreamWriter writer = new StreamWriter(this.sfOutput.FileName, false, new UnicodeEncoding()))
                    writer.Write(CSVExport.Export(this.tbOutput.Text));
        }

        // MENU SAVE / TO FILE
        private void toFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.sfOutput.DefaultExt = "txt";
            this.sfOutput.Filter = "Text documents (.txt)|*.txt";
            if (this.sfOutput.ShowDialog() == DialogResult.OK)
                using (StreamWriter writer = new StreamWriter(this.sfOutput.FileName, false, new UnicodeEncoding()))
                    writer.Write(this.tbOutput.Text);
        }

        // MENU ENGINE / AUTODETECT
        private void onlyHashCaclToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Config.SetValue("ENGINE", "Type", "Autodetect");
            this.UpdateGUIConfig();
        }

        // MENU ENGINE / GBA
        private void tsmEngineGBA_Click(object sender, EventArgs e)
        {
            Program.Config.SetValue("ENGINE", "Type", "GBA");
            this.UpdateGUIConfig();
        }

        // MENU ENGINE / GBC
        private void tsmEngineGBC_Click(object sender, EventArgs e)
        {
            Program.Config.SetValue("ENGINE", "Type", "GBC");
            this.UpdateGUIConfig();
        }

        // MENU ENGINE / GB
        private void tsmEngineGB_Click(object sender, EventArgs e)
        {
            Program.Config.SetValue("ENGINE", "Type", "GB");
            this.UpdateGUIConfig();
        }

        // MENU ENGINE / NDS
        private void tsmEngineNDS_Click(object sender, EventArgs e)
        {
            Program.Config.SetValue("ENGINE", "Type", "NDS");
            this.UpdateGUIConfig();
        }

        // MENU ENGINE / FDS
        private void tsmEngineFDS_Click(object sender, EventArgs e)
        {
            Program.Config.SetValue("ENGINE", "Type", "FDS");
            this.UpdateGUIConfig();
        }

        // MENU ENGINE / N64
        private void tsmEngineN64_Click(object sender, EventArgs e)
        {
            Program.Config.SetValue("ENGINE", "Type", "N64");
            this.UpdateGUIConfig();
        }

        // MENU ENGINE / NES
        private void tsmEngineNES_Click(object sender, EventArgs e)
        {
            Program.Config.SetValue("ENGINE", "Type", "NES");
            this.UpdateGUIConfig();
        }

        // MENU ENGINE / NGC
        private void tsmEngineNGC_Click(object sender, EventArgs e)
        {
            Program.Config.SetValue("ENGINE", "Type", "NGC");
            this.UpdateGUIConfig();
        }

        // MENU ENGINE / SNES
        private void tsmEngineSNES_Click(object sender, EventArgs e)
        {
            Program.Config.SetValue("ENGINE", "Type", "SNES");
            this.UpdateGUIConfig();
        }

        // MENU HELP / ABOUT
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ABox().ShowDialog(this);
        }

        // SCAN BUTTON: CANCEL
        private void bCancel_Click(object sender, EventArgs e)
        {
            this.__file_worker.CancelAsync();
        }

        // SCAN BUTTON: PAUSE
        private void bPause_Click(object sender, EventArgs e)
        {
            this.__file_worker.CancelAsync();
        }

        // ****************************************************************************************
        // GUI CONFIG
        // ****************************************************************************************
        public void UpdateGUIConfig()
        {
            this.tcMain.SelectedTab = this.tpOutput;
            string str = Program.Config.GetValue("ENGINE", "Type", "Autodetect");
            // check menus
            this.tsmEngineAutodetect.Checked = (str == "Autodetect") ? true : false;
            this.tsmEngineGBA.Checked = (str == "GBA") ? true : false;
            this.tsmEngineGBC.Checked = (str == "GBC") ? true : false;
            this.tsmEngineGB.Checked = (str == "GB") ? true : false;
            this.tsmEngineNDS.Checked = (str == "NDS") ? true : false;
            this.tsmEngineFDS.Checked = (str == "FDS") ? true : false;
            this.tsmEngineN64.Checked = (str == "N64") ? true : false;
            this.tsmEngineNES.Checked = (str == "NES") ? true : false;
            this.tsmEngineNGC.Checked = (str == "NGC") ? true : false;
            this.tsmEngineSNES.Checked = (str == "SNES") ? true : false;
            // update StripLabel
            statusStripLabel.Text = "Engine: " + str;
            if (str == "Autodetect")
            {
                bool unk = Program.Config.GetFlag("ENGINE", "ParseUnknownFiles");
                if (unk)
                {
                    statusStripLabel.Text = statusStripLabel.Text + " (+ parse unknown files)";
                }
                else
                {
                    statusStripLabel.Text = statusStripLabel.Text + " (+ don't parse unknown files)";
                }
            }
            // window size
            switch (Program.Config.GetValue("GUI", "DefaultState", "Windowed"))
            {
                case "Maximized":
                    base.WindowState = FormWindowState.Maximized;
                    break;
                case "Normal":
                    base.WindowState = FormWindowState.Normal;
                    base.Width = Int32.Parse(Program.Config.GetValue("GUI", "Width", "500"));
                    base.Height = Int32.Parse(Program.Config.GetValue("GUI", "Height", "500"));
                    this.Refresh();
                    break;
            }
        }
    }
}
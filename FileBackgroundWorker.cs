namespace GameHeader
{
    using GameHeader.Abstract;
    using GameHeader.Systems;
    // zip is handled by 7zip
    // using ICSharpCode.SharpZipLib.Zip;
    using SevenZip;
    // https://archive.codeplex.com/?p=sevenzipsharp
    // https://github.com/tomap/SevenZipSharp
    // https://github.com/squid-box/SevenZipSharp
    // alternative: https://github.com/adoconnection/SevenZipExtractor
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    public class FileBackgroundWorker : BackgroundWorker
    {
        private FileInfo[] __files;
        private string __InitialDirectory;
        private MemoryStream __ms;
        private Processor __processor;
        private Processor[] __processors;
        private bool __sevenZipEnabled;
        private List<string> __sevenZipExtensions;
        private Stream __stream;
        public BlockReadEventHandler BlockRead;
        public const int BlockSize = 0x1400000;
        public FileCompletedEventHandler FileCompleted;
        public FileStartedEventHandler FileStarted;
        public ProgressUpdateEventHandler ProgressUpdate;
        public WorkCompletedEventHandler WorkCompleted;
        public ZipEntryCompletedEventHandler ZipEntryCompleted;
        public ZipEntryStartedEventHandler ZipEntryStarted;
        public UpdateLogEventHandler UpdateLog;

        public FileBackgroundWorker()
        {
            base.DoWork += new DoWorkEventHandler(this.__DoWork);
            base.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.__RunWorkerCompleted);
            base.ProgressChanged += new ProgressChangedEventHandler(this.__ProgressChanged);
            base.WorkerReportsProgress = true;
            base.WorkerSupportsCancellation = true;
            this.__processors = new Processor[] {
                new GBAProcessor(), new GBCProcessor(), new GBProcessor(), new NDSProcessor(),
                new FDSProcessor(), new N64Processor(), new NESProcessor(), new NGCProcessor(), new SNESProcessor(),
                new GeneralProcessor()
            };
            this.__sevenZipExtensions = new List<string>();
            this.__stream = null;
        }

        private Processor __DetectExtension(string ext)
        {
            string str = Program.Config.GetValue("ENGINE", "Type", "Autodetect");
            // autodetect
            if (str == "Autodetect")
            {
                for (int i = 0; i < this.__processors.Length; i++)
                {
                    if (this.__processors[i].DetectExtension(ext))
                    {
                        return this.__processors[i];
                    }
                }
            }
            // hashcalc only
            string[] extensions = Program.Config.GetValue("HASHCALC_ONLY", "Extensions", ".txt,.nfo,.sfv").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in extensions)
            {
                if (s == ext)
                {
                    // latest processor is Generic
                    return this.__processors[this.__processors.Length - 1];
                }
            }
            // forced
            for (int i = 0; i < this.__processors.Length; i++)
            {
                if (this.__processors[i].SystemType.ToString() == str)
                {
                    return this.__processors[i];
                }
            }
            return null;
        }

        private void __DoWork(object sender, DoWorkEventArgs e)
        {
            this.__files = (FileInfo[]) e.Argument;
            foreach (FileInfo info in this.__files)
            {
                if (base.CancellationPending)
                {
                    base.ReportProgress(7, new UpdateLogInfo("$CANCEL"));
                    break;
                }
                this.__ProcessFile(info);
            }
        }

        private void __ProcessFile(FileInfo file)
        {
            base.ReportProgress(1, new FileWorkerInfo(((int)(file.Length / 0x1400000L)) + (((file.Length % 0x1400000L) == 0L) ? 0 : 1), file.Name));
            base.ReportProgress(7, new UpdateLogInfo("$FILEINFO"));
            base.ReportProgress(7, new UpdateLogInfo(file.FullName + "\r\n"));
            Stream baseStream = null;
            try
            {
                baseStream = new StreamReader(file.FullName).BaseStream;
            }
            catch (Exception exc)
            {
                base.ReportProgress(7, new UpdateLogInfo("$WARNING"));
                base.ReportProgress(7, new UpdateLogInfo("{ERR01} " + exc.Message + "\r\n"));
            }
            // log next file
            if (this.__sevenZipEnabled && this.__sevenZipExtensions.Contains(file.Extension.ToLower()) || file.Extension.ToLower() == ".zip")
            {
                base.ReportProgress(7, new UpdateLogInfo("+1"));
            }
            if(baseStream != null)
            {
                // use 7zip
                if (this.__sevenZipEnabled && this.__sevenZipExtensions.Contains(file.Extension.ToLower()))
                {
                    using (SevenZipExtractor extractor = new SevenZipExtractor(file.FullName))
                    {
                        try
                        {
                            ReadOnlyCollection<ArchiveFileInfo> archiveFileData = extractor.ArchiveFileData;
                        }
                        catch (Exception exc)
                        {
                            base.ReportProgress(7, new UpdateLogInfo("$WARNING"));
                            base.ReportProgress(7, new UpdateLogInfo("{ERR02} " + exc.Message + "\r\n"));
                        }
                        int extracted = 0;
                        for (int i = 0; i < extractor.ArchiveFileData.Count; i++)
                        {
                            ArchiveFileInfo info = extractor.ArchiveFileData[i];
                            if (!info.IsDirectory)
                            {
                                extracted++;
                                base.ReportProgress(7, new UpdateLogInfo("+1"));
                                base.ReportProgress(7, new UpdateLogInfo("$ARCHIVEDFILEINFO"));
                                base.ReportProgress(7, new UpdateLogInfo(info.FileName + " ... "));
                                this.__processor = this.__DetectExtension(Path.GetExtension(info.FileName).ToLower());
                                if (this.__processor == null)
                                {
                                    return;
                                }
                                // file.Directory.Name = parent directory
                                // file.Directory.FullName = full path
                                string path = file.Directory.FullName;
                                if (!Program.GetConfig().GetFlag("OUTPUT", "FullPath"))
                                {
                                    if (path == __InitialDirectory)
                                        path = file.Directory.Name;
                                    else
                                        path = path.Replace(__InitialDirectory + "\\", "");
                                }
                                this.__processor.Initialize(new GameFileInfo(file.Name, path, info.FileName, (long)info.Size, true));
                                base.ReportProgress(4, new ZipWorkerInfo(100, file.Name, info.FileName, i + 1, extractor.ArchiveFileData.Count));
                                if (base.CancellationPending)
                                {
                                    this.__files = null;
                                    return;
                                }
                                if (this.__ms != null)
                                {
                                    this.__ms.Dispose();
                                }
                                this.__ms = new MemoryStream();
                                extractor.Extracting += new EventHandler<ProgressEventArgs>(this.tmp_Extracting);
                                try
                                {
                                    extractor.ExtractFile(i, this.__ms);
                                }
                                catch (Exception exc)
                                {
                                    base.ReportProgress(7, new UpdateLogInfo("$WARNING"));
                                    base.ReportProgress(7, new UpdateLogInfo("{ERR03} " + exc.Message + "\r\n"));
                                }
                                byte[] array = this.__ms.GetBuffer();
                                try
                                {
                                    Array.Resize<byte>(ref array, (int)info.Size);
                                }
                                catch (Exception exc)
                                {
                                    base.ReportProgress(7, new UpdateLogInfo("$WARNING"));
                                    base.ReportProgress(7, new UpdateLogInfo("{ERR04} " + exc.Message + "\r\n"));
                                }
                                this.__processor.ProcessBlock(array, 0L);
                                base.ReportProgress(5, this.__processor.Finalize());
                                Thread.Sleep(TimeSpan.FromMilliseconds(50.0));
                                base.ReportProgress(7, new UpdateLogInfo("-1"));
                                base.ReportProgress(7, new UpdateLogInfo("ok! ]\r\n"));
                            }
                        }
                        if (extracted > 0)
                        {
                            base.ReportProgress(7, new UpdateLogInfo("-1"));
                            base.ReportProgress(7, new UpdateLogInfo("... ok!\r\n"));
                        }
                        else
                        {
                            base.ReportProgress(7, new UpdateLogInfo("... EMPTY ARCHIVE!\r\n"));
                        }
                    }
                    base.ReportProgress(0, null);
                }
                // uncompressed file
                else
                {
                    using (this.__stream = baseStream)
                    {
                        base.ReportProgress(7, new UpdateLogInfo("... "));
                        base.ReportProgress(7, new UpdateLogInfo("+1"));
                        this.__processor = this.__DetectExtension(file.Extension.ToLower());
                        if (this.__processor == null)
                        {
                            return;
                        }
                        // file.Directory.Name = parent directory
                        // file.Directory.FullName = full path
                        string path = file.Directory.FullName;
                        if (!Program.GetConfig().GetFlag("OUTPUT", "FullPath"))
                        {
                            if (path == __InitialDirectory)
                                path = file.Directory.Name;
                            else
                                path = path.Replace(__InitialDirectory + "\\", "");
                        }
                        this.__processor.Initialize(new GameFileInfo(file.Name, path, file.Name, file.Length, false));
                        int num5 = 0;
                        long start = 0L;
                        byte[] buffer3 = new byte[0x1400000];
                        while (start < file.Length)
                        {
                            if (base.CancellationPending)
                            {
                                baseStream.Close();
                                this.__files = null;
                                return;
                            }
                            num5 = this.__stream.Read(buffer3, 0, 0x1400000);
                            if (num5 != 0x1400000)
                            {
                                Array.Resize<byte>(ref buffer3, num5);
                            }
                            this.__processor.ProcessBlock(buffer3, start);
                            start += 0x1400000L;
                            base.ReportProgress(2, null);
                        }
                    }
                    base.ReportProgress(7, new UpdateLogInfo("-1"));
                    base.ReportProgress(7, new UpdateLogInfo("ok!\r\n"));
                    base.ReportProgress(0, this.__processor.Finalize());
                }
                baseStream.Close();
            }
            base.ReportProgress(6, null);
        }

        private void __ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FileWorkerState progressPercentage = (FileWorkerState) e.ProgressPercentage;
            object userState = e.UserState;
            switch (progressPercentage)
            {
                case FileWorkerState.FileCompleted:
                    this.FileCompleted(this, new FileCompletedEventArgs((Game) userState));
                    return;

                case FileWorkerState.FileStarted:
                    this.FileStarted(this, new FileStartedEventArgs((FileWorkerInfo) userState));
                    return;

                case FileWorkerState.BlockRead:
                    this.BlockRead(this);
                    return;

                case FileWorkerState.ProgressUpdate:
                    this.ProgressUpdate(this, new ProgressUpdateEventArgs((ProgressUpdateInfo) userState));
                    return;

                case FileWorkerState.UpdateLog:
                    this.UpdateLog(this, new UpdateLogEventArgs((UpdateLogInfo)userState));
                    return;

                case FileWorkerState.ZipEntryStarted:
                    this.ZipEntryStarted(this, new ZipEntryStartedEventArgs((ZipWorkerInfo) userState));
                    return;

                case FileWorkerState.ZipEntryCompleted:
                    this.ZipEntryCompleted(this, new ZipEntryCompletedEventArgs((Game) userState));
                    return;
            }
        }

        private void __RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        public void SetFiles(FileInfo[] files)
        {
            this.__files = files;
        }

        public void SetInitialDirectory(string initialDirectory)
        {
            this.__InitialDirectory = initialDirectory;
        }

        public void SetSevenZipExtensions(string[] extensions)
        {
            foreach (string str in extensions)
            {
                this.__sevenZipExtensions.Add(str);
            }
        }

        public bool SetSevenZipPath(string path)
        {
            try
            {
                if (!Path.IsPathRooted(path))
                {
                    path = Application.StartupPath + "/" + path;
                }
                SevenZipExtractor.SetLibraryPath(Path.GetFullPath(path));
                SevenZipCompressor.SetLibraryPath(Path.GetFullPath(path));
                this.__sevenZipEnabled = true;
            }
            catch (Exception)
            {
                this.__sevenZipEnabled = false;
            }
            return this.__sevenZipEnabled;
        }

        public void tmp_Extracting(object sender, ProgressEventArgs e)
        {
            if (base.CancellationPending)
            {
                // ******************
                // e.Cancel = true;
            }
            else
            {
                base.ReportProgress(3, new ProgressUpdateInfo(e.PercentDelta, e.PercentDone));
            }
        }

        public bool SevenZipEnabled
        {
            get
            {
                return this.__sevenZipEnabled;
            }
        }

        public List<string> SevenZipExtensions
        {
            get
            {
                return this.__sevenZipExtensions;
            }
        }

        public enum FileWorkerState
        {
            FileCompleted,
            FileStarted,
            BlockRead,
            ProgressUpdate,
            ZipEntryStarted,
            ZipEntryCompleted,
            WorkCompleted,
            UpdateLog
        }
    }
}
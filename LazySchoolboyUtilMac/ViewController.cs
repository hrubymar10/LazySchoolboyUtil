using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AppKit;
using Foundation;

using LazySchoolboyUtil;

namespace LazySchoolboyUtilMac
{
    public partial class ViewController : NSViewController
    {
        public List<Format> formats = Util.GetListOfFormats();
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewWillAppear()
        {
            View.Window.Title = NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleName") + " " + NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            FileTypeBox.AddItem("Select File Type");
            foreach (Format format in formats)
            {
                FileTypeBox.AddItem(format.GetFormatName());
            }

            ProgressBar.MinValue = 0;
            ProgressBar.MaxValue = 100;
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        partial void GenerateButtonAsync(NSObject sender)
        {
            if (FileTypeBox.IndexOfSelectedItem == 0)
            {
                ShowAlert("You must select File Type");
            }
            else
            {
                int fileSize = FileSizeField.IntValue;
                if (fileSize <= 0)
                {
                    ShowAlert("File Size must be bigger than 0");
                }
                else
                {
                    //TODO: Enable floats
                    if (FileSizeField.FloatValue != fileSize)
                    {
                        ShowAlert("Sorry but this version doesn't support decimal numbers. Please enter integer.");
                    }
                    else
                    {
                        switch (FileSizeUnitSegment.SelectedSegment)
                        {
                            case 0:
                                fileSize *= 1000000;
                                break;
                            case 1:
                                fileSize *= 1000;
                                break;
                        }
                        int requiredSize = formats[Convert.ToInt32(FileTypeBox.IndexOfSelectedItem) - 1].GetOffset() + formats[Convert.ToInt32(FileTypeBox.IndexOfSelectedItem) - 1].GetMagicNumbers().Length;
                        if (fileSize < requiredSize)
                        {
                            ShowAlert("For selected File Type you must enter size bigger than " + requiredSize + "b");
                        }
                        else
                        {
                            if (fileSize >= 200000000)
                            {
                                ShowAlert("For good reasons please enter size smaller than 200Mb...");
                            }
                            else
                            {
                                using (var dlg = new NSSavePanel())
                                {
                                    dlg.Title = "Save File";
                                    dlg.AllowedFileTypes = new string[] { formats[Convert.ToInt32(FileTypeBox.IndexOfSelectedItem) - 1].GetFormatName() };

                                    if (dlg.RunModal() == 1)
                                    {
                                        string path = dlg.Url.Path;
                                        Format format = formats[Convert.ToInt32(FileTypeBox.IndexOfSelectedItem) - 1];

                                        //TODO: Error handling
                                        Task[] tasks = new Task[2];
                                        tasks[0] = Task.Factory.StartNew(() => SaveFile(path, fileSize, format));
                                        tasks[1] = Task.Factory.StartNew(ShowProgress);
                                        Task.WaitAll(tasks);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        async Task ShowProgress()
        {
            await Task.Run(() =>
            {
                InvokeOnMainThread(() => { ProgressBar.DoubleValue = 0; });
                while (Util.progress != 100)
                {
                    InvokeOnMainThread(() => { ProgressBar.DoubleValue = Util.progress; });
                    System.Threading.Thread.Sleep(20);
                }
                InvokeOnMainThread(() => { ProgressBar.DoubleValue = 100; });
            });
        }

        async Task SaveFile(string path, int fileSize, Format format)
        {
            await Task.Run(() => Util.GenerateFile(path, fileSize, format));
            InvokeOnMainThread(() => { ShowAlert("Done! Output file is: " + path); });
        }

        partial void FileTypeBoxChanged(NSObject sender)
        {
            FileTypeBox.Title = formats[Convert.ToInt32(FileTypeBox.IndexOfSelectedItem)-1].GetFormatName();
            FileMimeTypeLabel.StringValue = formats[Convert.ToInt32(FileTypeBox.IndexOfSelectedItem)-1].GetMime();
        }

        void ShowAlert(string message)
        {
            using (NSAlert alert = new NSAlert())
            {
                alert.MessageText = message;
                alert.AlertStyle = NSAlertStyle.Critical;
                alert.RunModal();
            }
        }
    }
}

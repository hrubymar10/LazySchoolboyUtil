// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace LazySchoolboyUtilMac
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextField FileMimeTypeLabel { get; set; }

		[Outlet]
		AppKit.NSTextField FileSizeField { get; set; }

		[Outlet]
		AppKit.NSSegmentedControl FileSizeUnitSegment { get; set; }

		[Outlet]
		AppKit.NSPopUpButton FileTypeBox { get; set; }

		[Outlet]
		AppKit.NSProgressIndicator ProgressBar { get; set; }

		[Action ("FileTypeBoxChanged:")]
		partial void FileTypeBoxChanged (Foundation.NSObject sender);

		[Action ("GenerateButton:")]
		partial void GenerateButtonAsync (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (FileMimeTypeLabel != null) {
				FileMimeTypeLabel.Dispose ();
				FileMimeTypeLabel = null;
			}

			if (FileSizeField != null) {
				FileSizeField.Dispose ();
				FileSizeField = null;
			}

			if (FileSizeUnitSegment != null) {
				FileSizeUnitSegment.Dispose ();
				FileSizeUnitSegment = null;
			}

			if (FileTypeBox != null) {
				FileTypeBox.Dispose ();
				FileTypeBox = null;
			}

			if (ProgressBar != null) {
				ProgressBar.Dispose ();
				ProgressBar = null;
			}
		}
	}
}

// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Orbi.iOS.TableSources
{
    [Register ("AllVideosViewCell")]
    partial class AllVideosViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel FilenameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ImageView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FilenameLabel != null) {
                FilenameLabel.Dispose ();
                FilenameLabel = null;
            }

            if (ImageView != null) {
                ImageView.Dispose ();
                ImageView = null;
            }
        }
    }
}
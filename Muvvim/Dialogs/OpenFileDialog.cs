using System.Collections.Generic;
using System.IO;
using System.Windows;

using Muvvim.Util;

namespace Muvvim.Dialogs
{
    public abstract class OpenFileDialog : Dialog
    {

        public static DependencyProperty InitialDirectoryProperty =
            DependencyProperty.Register(nameof(InitialDirectory), typeof(DirectoryInfo), typeof(OpenFileDialog));

        public static DependencyProperty FilterProperty =
            DependencyProperty.Register(nameof(Filter), typeof(string), typeof(OpenFileDialog));

        public DirectoryInfo InitialDirectory
        {
            get { return (DirectoryInfo)GetValue(InitialDirectoryProperty); }
            set { SetValue(InitialDirectoryProperty, value); }
        }

        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

    }
}

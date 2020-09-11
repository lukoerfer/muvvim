using System.IO;
using System.Windows;

using Muvvim.Util;

namespace Muvvim.Dialogs
{
    public class OpenSingleFileDialog : OpenFileDialog
    {
        public static DependencyProperty FileProperty =
            DependencyProperty.Register(nameof(File), typeof(FileInfo), typeof(OpenSingleFileDialog));

        public FileInfo File
        {
            get { return (FileInfo)GetValue(FileProperty); }
            set { SetValue(FileProperty, value); }
        }

        protected override void Run()
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog()
            {
                Title = Title,
                Multiselect = false,
                InitialDirectory = InitialDirectory?.FullName ?? File?.DirectoryName,
                Filter = Filter
            };
            bool? result = dialog.ShowDialog(Window.GetWindow(this));
            Reset();
            if (result.GetValueOrDefault())
            {
                File = new FileInfo(dialog.FileName);
                OnResult?.ExecuteIfPossible(File);
            }
            else
            {
                OnCancel?.ExecuteIfPossible(null);
            }
        }

    }
}

using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

using Muvvim.Util;

namespace Muvvim.Dialogs
{
    public class OpenFilesDialog : OpenFileDialog
    {
        public static DependencyProperty FilesProperty =
            DependencyProperty.Register(nameof(File), typeof(ObservableCollection<FileInfo>), typeof(OpenFilesDialog));

        public ObservableCollection<FileInfo> Files
        {
            get { return (ObservableCollection<FileInfo>)GetValue(FilesProperty); }
            set { SetValue(FilesProperty, value); }
        }

        protected override void Run()
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog()
            {
                Title = Title,
                Multiselect = true,
                InitialDirectory = InitialDirectory?.FullName,
                Filter = Filter
            };
            bool? result = dialog.ShowDialog(Window.GetWindow(this));
            Reset();
            if (result.GetValueOrDefault())
            {
                Files = dialog.FileNames.Select(name => new FileInfo(name)).ToObservableCollection();
                OnResult?.ExecuteIfPossible(Files);
            }
            else
            {
                OnCancel?.ExecuteIfPossible(null);
            }
        }

    }
}

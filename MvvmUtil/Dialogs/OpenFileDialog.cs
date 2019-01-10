using System.Collections.Generic;
using System.IO;
using System.Windows;

using MvvmUtil.Util;

namespace MvvmUtil.Dialogs
{
    public class OpenFileDialog : Dialog
    {
        public static DependencyProperty MultiselectProperty =
            DependencyProperty.Register(nameof(Multiselect), typeof(bool), typeof(OpenFileDialog));

        public static DependencyProperty InitialDirectoryProperty =
            DependencyProperty.Register(nameof(InitialDirectory), typeof(DirectoryInfo), typeof(OpenFileDialog));

        public static DependencyProperty FilterProperty =
            DependencyProperty.Register(nameof(Filter), typeof(List<ExtensionFilter>), typeof(OpenFileDialog));

        public bool Multiselect
        {
            get { return (bool)GetValue(MultiselectProperty); }
            set { SetValue(MultiselectProperty, value); }
        }

        public DirectoryInfo InitialDirectory
        {
            get { return (DirectoryInfo)GetValue(InitialDirectoryProperty); }
            set { SetValue(InitialDirectoryProperty, value); }
        }

        public List<ExtensionFilter> Filter
        {
            get { return (List<ExtensionFilter>)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        protected override void Run()
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog()
            {
                Title = Title,
                Multiselect = Multiselect,
                InitialDirectory = InitialDirectory?.FullName
            };
            bool? result = dialog.ShowDialog(Window.GetWindow(this));
            Reset();
            if (result.GetValueOrDefault())
            {
                if (Multiselect)
                {
                    OnResult?.ExecuteIfPossible(dialog.FileNames);
                }
                else
                {
                    OnResult?.ExecuteIfPossible(dialog.FileName);
                }
            }
            else
            {
                OnCancel?.ExecuteIfPossible(null);
            }
        }
    }
}

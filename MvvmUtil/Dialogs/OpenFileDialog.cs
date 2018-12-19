using System.Windows;

namespace MvvmUtil.Dialogs
{
    public class OpenFileDialog : BaseDialog
    {
        public static readonly DependencyProperty MultiselectProperty =
            DependencyProperty.Register(nameof(Multiselect), typeof(bool), typeof(BaseDialog),
                new FrameworkPropertyMetadata(false));

        public bool Multiselect
        {
            get { return (bool)GetValue(MultiselectProperty); }
            set { SetValue(MultiselectProperty, value); }
        }

        protected override void Run()
        {
            
        }
    }
}

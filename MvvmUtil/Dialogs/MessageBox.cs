using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace MvvmUtil.Dialogs
{
    [DesignTimeVisible(false)]
    public class MessageBox : BaseDialog
    {
        public static readonly DependencyProperty MessageProperty = 
            DependencyProperty.Register(nameof(Message), typeof(string), typeof(MessageBox), 
                new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty ImageProperty = 
            DependencyProperty.Register(nameof(Image), typeof(object), typeof(MessageBox), 
                new FrameworkPropertyMetadata(MessageBoxImage.None));

        public static readonly DependencyProperty ButtonsProperty = 
            DependencyProperty.Register(nameof(Buttons), typeof(object), typeof(MessageBox), 
                new FrameworkPropertyMetadata(MessageBoxButton.OK));

        public static readonly DependencyProperty DefaultButtonProperty = 
            DependencyProperty.Register(nameof(DefaultButton), typeof(object), typeof(MessageBox), 
                new FrameworkPropertyMetadata(MessageBoxResult.None));

        public static readonly DependencyProperty OkCommandProperty = 
            DependencyProperty.Register(nameof(OkCommand), typeof(ICommand), typeof(MessageBox));

        public static readonly DependencyProperty CancelCommandProperty = 
            DependencyProperty.Register(nameof(CancelCommand), typeof(ICommand), typeof(MessageBox));

        public static readonly DependencyProperty YesCommandProperty = 
            DependencyProperty.Register(nameof(YesCommand), typeof(ICommand), typeof(MessageBox));

        public static readonly DependencyProperty NoCommandProperty = 
            DependencyProperty.Register(nameof(NoCommand), typeof(ICommand), typeof(MessageBox));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public object Image
        {
            get { return GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public object Buttons
        {
            get { return GetValue(ButtonsProperty); }
            set { SetValue(ButtonsProperty, value); }
        }

        public object DefaultButton
        {
            get { return GetValue(DefaultButtonProperty); }
            set { SetValue(DefaultButtonProperty, value); }
        }

        public ICommand OkCommand
        {
            get { return (ICommand)GetValue(OkCommandProperty); }
            set { SetValue(OkCommandProperty, value); }
        }

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public ICommand YesCommand
        {
            get { return (ICommand)GetValue(YesCommandProperty); }
            set { SetValue(YesCommandProperty, value); }
        }

        public ICommand NoCommand
        {
            get { return (ICommand)GetValue(NoCommandProperty); }
            set { SetValue(NoCommandProperty, value); }
        }

        protected override void Run()
        {
            MessageBoxResult result = System.Windows.MessageBox.Show(Message, Title, (MessageBoxButton)Buttons, (MessageBoxImage)Image, (MessageBoxResult)DefaultButton);
        }

    }
}

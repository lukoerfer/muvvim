using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using PropertyChanged;

using MvvmUtil.Command;
using MvvmUtil.Windows;

namespace MvvmUtilTest
{
    [ImplementPropertyChanged]
    public partial class MainWindow : Window
    {

        public bool Valid { get; set; }

        public ICommand OpenGeneric { get; set; }
        public ICommand OpenBase { get; set; }

        public ICommand Close { get; set; }

        private object SubVM;

        public MainWindow()
        {
            this.InitializeComponent();
            this.OpenGeneric = new RelayCommand(() => this.SubVM = new Windowed<object>());
            this.OpenBase = new RelayCommand(() => this.SubVM = new MyVM());
            this.Close = new RelayCommand(() => (this.SubVM as IDisposable).Dispose());
            this.DataContext = this;
        }

    }

    internal class MyVM : Windowed
    {
        public MyVM() : base()
        {

        }
    }
}

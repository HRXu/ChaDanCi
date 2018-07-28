using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml;
using mshtml;

namespace query
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Data.window = this;
            
        }

        private void webbrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {

            //IHTMLElement inputSearch = (IHTMLElement)s.all.item("left-result-container");
            //var s1=inputSearch.innerHTML;
        }

        public void a()
        {
            Data.inputSearch = (IHTMLElement)((webbrowser.Document as HTMLDocument).all.item("left-result-container"));
        }

        private  async void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.Read();
            Manager.Query();
            OutputWindow ow = new OutputWindow();
            ow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.Save();
        }

        private void OpenSettingWindow(object sender, RoutedEventArgs e)
        {
        }
    }
}

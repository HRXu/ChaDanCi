using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace query
{
    /// <summary>
    /// OutputWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OutputWindow : Window
    {
        public OutputWindow()
        {
            InitializeComponent();
            listview1.ItemsSource =Data.LogList;
        }
    }
    public partial class Data
    {
        static ReaderWriterLockSlim logLock = new ReaderWriterLockSlim();
        static public ObservableCollection<log> LogList = new ObservableCollection<log>();
        static public void LogOutput(string s)
        {
            logLock.EnterWriteLock();
            LogList.Add(new log { content = s, time = DateTime.Now.ToShortTimeString() });
            logLock.ExitWriteLock();
        }
    }

    public class log
    {
        public string content { get; set; }
        public string time { get; set; }
    }
}

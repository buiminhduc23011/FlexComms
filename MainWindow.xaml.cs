using System;
using System.Windows;
using System.Windows.Threading;
using EasyModbus;

namespace FlexComms
{
    public partial class MainWindow : Window
    {
        //private ModbusClient modbusClient;
        //private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
        }

        
    protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

        }
    }
}

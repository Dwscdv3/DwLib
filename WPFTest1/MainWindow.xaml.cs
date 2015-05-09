using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Dwscdv3.WPF.UserControls;

namespace WPFTest1 {
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window {
		DispatcherTimer t = new DispatcherTimer();
		CircularProgress cp = new CircularProgress();

		public MainWindow() {
			InitializeComponent();
			cp.Width = 80; cp.Height = 80;
			cp.StrokeThickness = 7.5;
			cp.Maximum = 1000;
			cp.LabelFontSize = 24;
			cp.AutoRender = true;
			MainGrid.Children.Add(cp);
			t.Interval = new TimeSpan(0, 0, 0, 0, 20);
			t.Tick += t_Tick;
			t.Start();
			//cp.Color
		}

		void t_Tick(object sender, EventArgs e) {
			if (cp.Value >= cp.Maximum) {
				cp.Value = 0;
			}
			cp.Text = cp.Value.ToString();
			cp.Value++;
		}
	}
}

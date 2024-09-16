using ControlPanel.Models;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ControlPanel.Windows
{
	/// <summary>
	/// Interaction logic for AddSensorAction.xaml
	/// </summary>
	public partial class AddSensorAction : Window
	{
		public SensorAction sensorAction;

		public AddSensorAction()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			sensorAction = new SensorAction();
			sensorAction.Name = tbName.Text;
			sensorAction.Code = tbCode.Text;
			sensorAction.Value = tbValue.Text;
			sensorAction.Message = tbMessage.Text;
			this.DialogResult = true;
			this.Close();
		}
	}
}

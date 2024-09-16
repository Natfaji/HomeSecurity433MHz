using ControlPanel.Models;
using ControlPanel.Models.Interfaces;
using ControlPanel.Models.SensorTypes;
using System.Windows;
using System.Windows.Controls;

namespace ControlPanel.Windows
{
	/// <summary>
	/// Interaction logic for AddSensor.xaml
	/// </summary>
	public partial class AddSensor : Window
	{
		private Sensor sensor;
		private List<ISensorAction> sensorActions = new List<ISensorAction>();

		public AddSensor()
		{
			InitializeComponent();

			foreach (SensorType sensorType in Enum.GetValues(typeof(SensorType)))
			{
				//cbSensorType.Items.Add(sensorType.ToString());
			}
		}

		private bool ValidateInput()
		{
			if (tbName.Text == string.Empty)
			{
				throw new Exception("Name can't be empty!");
			}

			//if (cbSensorType.Text == string.Empty)
			//{
			//	throw new Exception("Sensor must have a type!");
			//}

			return true;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			AddSensorAction addSensorAction = new AddSensorAction();
			addSensorAction.ShowDialog();

			if (addSensorAction.DialogResult == true)
			{
				SensorAction sensorAction = addSensorAction.sensorAction;
				sensorActions.Add(sensorAction);
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			ValidateInput();
			switch ((SensorType)((TabItem)tcSensorType.SelectedItem).Tag)
			{
				case SensorType.Door:
					sensor = new DoorSensor(tbName.Text, SensorType.Door, tbDescription.Text);
					break;

				case SensorType.Motion:
					sensor = new MotionSensor(tbName.Text, SensorType.Motion, tbDescription.Text, 1000);
					break;

				case SensorType.Humidity:
					//sensor = new HumiditySensor(tbName.Text, tbDescription.Text);
					throw new NotImplementedException();
					break;
				case SensorType.Light:
					//sensor = new LightSensor(tbName.Text, tbDescription.Text);
					throw new NotImplementedException();
					break;
				case SensorType.Temperature:
					//sensor = new TemperatureSensor(tbName.Text, tbDescription.Text);
					throw new NotImplementedException();
					break;
				default:
					break;
			}
			sensor.Actions = sensorActions;

			DialogResult = true;
			Close();
		}
	}
}

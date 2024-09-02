using ControlPanel.Models;
using ControlPanel.Models.Interfaces;
using ControlPanel.Models.SensorTypes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ControlPanel
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			LoadSensors();
		}

		private void Test()
		{
			// Get the sensor controller
			SensorController sensorController = SensorController.GetInstance();

			// Create a sensor
			ISensor sensor1 = new DoorSensor("Door", SensorType.Door, "Door sensor");

			// Create a sensor action
			sensor1.Actions.Add(new SensorAction("Open", "10101010", "Open", "Door opened"));

			// Create a sensor action
			sensor1.Actions.Add(new SensorAction("Closeed", "01010101", "Closed", "Door closed"));

			// Add the sensor to the controller
			sensorController.AddSensor(sensor1);

			// Create a sensor
			ISensor sensor2 = new MotionSensor("Motion", SensorType.Motion, "Motion sensor", 5);

			// Create a sensor action
			sensor2.Actions.Add(new SensorAction("Movement", "11001100", "Movement", "Movement detected"));

			// Add the sensor to the controller
			sensorController.AddSensor(sensor2);

			// Load the sensors
			LoadSensors();
		}

		private void trigger()
		{
			// Get the sensor controller
			SensorController sensorController = SensorController.GetInstance();

			// Trigger the action
			sensorController.Trigger("10101010");
		}

		private void LoadSensors()
		{
			// Get the sensor controller
			SensorController sensorController = SensorController.GetInstance();

			// Get all sensors
			//Dictionary<SensorType, List<ISensor>> sensors = sensorController.GetAllSensors();
			List<ISensor> sensors = sensorController.GetAllSensors();

			// Clear the list
			//dgSensorsList.Items.Clear();

			// Add sensors to the list
			//foreach (ISensor sensor in sensors.Values)
			foreach (ISensor sensor in sensors)
			{
				//dgSensorsList.Items.Add(sensor);
			}
		}

		private void btnTrigger_Click(object sender, RoutedEventArgs e)
		{
			trigger();
		}

		private void btnTest_Click(object sender, RoutedEventArgs e)
		{
			Test();
			//LoadSensors();
		}

		private void CreateItem()
		{
			Grid grid1 = new Grid();
			grid1.Height = 125;
			grid1.Background = new SolidColorBrush(Colors.White);
			grid1.Margin = new Thickness(10);
			grid1.RowDefinitions.Add(new RowDefinition());
			grid1.ColumnDefinitions.Add(new ColumnDefinition());
			grid1.ColumnDefinitions.Add(new ColumnDefinition());
			grid1.ColumnDefinitions[0].Width = GridLength.Auto;
			grid1.ColumnDefinitions[1].Width = GridLength.Auto;

			Grid grid2 = new Grid();
			grid2.Margin = new Thickness(5, 0, 0, 0);
			grid2.HorizontalAlignment = HorizontalAlignment.Left;
			grid2.RowDefinitions.Add(new RowDefinition());
			grid2.RowDefinitions.Add(new RowDefinition());

			grid1.Children.Add(grid2);
			Grid.SetRow(grid2, 0);
			Grid.SetColumn(grid2, 1);

			Image img = new Image();
			img.Source = new BitmapImage(new Uri("pack://application:,,,/logoAsset 4.png"));
			img.HorizontalAlignment = HorizontalAlignment.Left;
			grid1.Children.Add(img);
			Grid.SetRow(img, 0);
			Grid.SetColumn(img, 0);

			Label label1 = new Label();
			label1.Content = "Open";
			label1.FontSize = 30;
			label1.VerticalAlignment = VerticalAlignment.Center;
			label1.HorizontalAlignment = HorizontalAlignment.Left;
			grid2.Children.Add(label1);
			Grid.SetRow(label1, 0);
			Grid.SetColumn(label1, 0);

			StackPanel spItem = new StackPanel();
			grid2.Children.Add(spItem);
			Grid.SetRow(spItem, 1);
			Grid.SetColumn(spItem, 0);

			Label label2 = new Label();
			label2.Content = "Last Opend: XX:XX:XX";
			spItem.Children.Add(label2);

			Label label3 = new Label();
			label3.Content = "Last Closed: XX:XX:XX";
			spItem.Children.Add(label3);


			spItems.Children.Add(grid1);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			CreateItem();
		}
	}
}
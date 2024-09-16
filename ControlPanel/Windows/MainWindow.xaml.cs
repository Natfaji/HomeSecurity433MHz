using ControlPanel.Models;
using ControlPanel.Models.Interfaces;
using ControlPanel.Models.SensorTypes;
using Microsoft.Toolkit.Uwp.Notifications;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

//using System.Windows.Forms;
//using System.Drawing;

namespace ControlPanel.Windows
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private System.Windows.Forms.NotifyIcon _notifyIcon;

		public MainWindow()
		{
			InitializeComponent();
			//CreateTestSensors();
			LoadSensors();
			SetupTrayIcon();
		}

		private void SetupTrayIcon()
		{
			string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", "SystemTray.ico");

			_notifyIcon = new System.Windows.Forms.NotifyIcon
			{
				Icon = new System.Drawing.Icon(iconPath), // Add an icon to your project and reference it
				Visible = false,
				Text = "Home Security"
			};

			// Define the action when the icon is clicked
			//_notifyIcon.MouseClick += NotifyIcon_Click;
			_notifyIcon.DoubleClick += (s, e) => ShowWindow();

			// Optionally add a context menu to the tray icon
			_notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();

			_notifyIcon.ContextMenuStrip.Items.Add("Show", null, (s, e) => ShowWindow());
			_notifyIcon.ContextMenuStrip.Items.Add("Exit", null, (s, e) => CloseApp());
		}

		//private void NotifyIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
		//{
		//	if (e.Button == System.Windows.Forms.MouseButtons.Left)
		//	{

		//	}
		//	else if (e.Button == System.Windows.Forms.MouseButtons.Right)
		//	{
		//		_notifyIcon.ContextMenuStrip.Show();
		//	}
		//}

		private void ShowWindow()
		{
			Show();
			WindowState = WindowState.Normal;
			Activate();

			ToggleNotifyIcon(false);
		}

		private void CloseApp()
		{
			_notifyIcon.Dispose();
			Application.Current.Shutdown();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			ToggleNotifyIcon(true);
			this.Hide();
		}

		private void Window_StateChanged(object sender, EventArgs e)
		{
			if (WindowState == WindowState.Minimized)
			{
				this.Hide();
				ToggleNotifyIcon(true);
			}
		}

		private void ToggleNotifyIcon(bool state)
		{
			if (state == true)
			{
				_notifyIcon.Visible = true;
				ToastContentBuilder toastContentBuilder = new ToastContentBuilder();
				toastContentBuilder.AddText("Application running");
				toastContentBuilder.AddText("Home Security is still running in the background.");
				toastContentBuilder.AddText("Check your system tray for more options.");
				toastContentBuilder.Show();
			}
			else
			{
				_notifyIcon.Visible = false;
			}
		}

		private void CreateTestSensors()
		{
			// Get the sensor controller
			SensorController sensorController = SensorController.GetInstance();

			// Create a sensor
			ISensor sensor1 = new DoorSensor("Door", SensorType.Door, "Door sensor");

			// Create a sensor action
			sensor1.Actions.Add(new SensorAction() { Name = "Open", Code = "10101010", Value = "Open", Message = "Door opened", ImageName = "open.png" });

			// Create a sensor action
			sensor1.Actions.Add(new SensorAction() { Name = "Closed", Code = "01010101", Value = "Close", Message = "Door closed", ImageName = "close.png" });

			// Add the sensor to the controller
			sensorController.AddSensor(sensor1);

			// Create a sensor
			//ISensor sensor2 = new MotionSensor("Motion", SensorType.Motion, "Motion sensor", 5);

			//// Create a sensor action
			//sensor2.Actions.Add(new SensorAction("Movement", "11001100", "Movement", "Movement detected"));

			//// Add the sensor to the controller
			//sensorController.AddSensor(sensor2);
		}

		private void trigger()
		{
			// Get the sensor controller
			SensorController sensorController = SensorController.GetInstance();

			// Trigger the action
			if (yesorno)
			{
				sensorController.Trigger("01010101");
				yesorno = false;
			}
			else
			{
				sensorController.Trigger("10101010");
				yesorno = true;
			}

			sensorController.SaveSensors();
		}

		private void LoadSensors()
		{
			// Get the sensor controller
			SensorController sensorController = SensorController.GetInstance();

			// Get all sensors
			List<ISensor> sensors = sensorController.GetAllSensors();

			// Add sensors to the list
			foreach (ISensor sensor in sensors)
			{
				CreateItem(sensor);
			}
		}

		private void CreateItem(ISensor sensor)
		{
			Border border = new Border()
			{
				BorderBrush = Brushes.Black,
				BorderThickness = new Thickness(1),
				Margin = new Thickness(5),
				Padding = new Thickness(5),
				Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF47494A")),
				CornerRadius = new CornerRadius(30),
				Height = 150,
				DataContext = sensor,
			};

			Grid grid1 = new Grid();
			grid1.RowDefinitions.Add(new RowDefinition());
			grid1.ColumnDefinitions.Add(new ColumnDefinition());
			grid1.ColumnDefinitions.Add(new ColumnDefinition());
			grid1.ColumnDefinitions[0].Width = GridLength.Auto;
			grid1.ColumnDefinitions[1].Width = GridLength.Auto;
			border.Child = grid1;

			Grid grid2 = new Grid()
			{
				Margin = new Thickness(5, 0, 0, 0),
				HorizontalAlignment = HorizontalAlignment.Left,
			};
			grid2.RowDefinitions.Add(new RowDefinition());
			grid2.RowDefinitions.Add(new RowDefinition());

			grid1.Children.Add(grid2);
			Grid.SetRow(grid2, 0);
			Grid.SetColumn(grid2, 1);

			Image img = new Image()
			{
				HorizontalAlignment = HorizontalAlignment.Left,
			};
			img.SetBinding(Image.SourceProperty, new Binding("LastTriggeredAction.ImageName")
			{ Converter = new ImagePathToBitmapConverter() });

			grid1.Children.Add(img);
			Grid.SetRow(img, 0);
			Grid.SetColumn(img, 0);

			Label CurrentValueMessage = new Label()
			{
				FontSize = 30,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Left,
			};
			Binding CurrentValueMessageBinding = new Binding("LastTriggeredAction.Message");
			CurrentValueMessage.SetBinding(Label.ContentProperty, CurrentValueMessageBinding);
			grid2.Children.Add(CurrentValueMessage);

			Grid.SetRow(CurrentValueMessage, 0);
			Grid.SetColumn(CurrentValueMessage, 0);

			StackPanel spItem = new StackPanel();
			grid2.Children.Add(spItem);
			Grid.SetRow(spItem, 1);
			Grid.SetColumn(spItem, 0);

			TextBlock LastTriggeredTime = new TextBlock();
			Binding LastTriggeredTimeBinding = new Binding("LastTriggeredTime")
			{
				StringFormat = "Last triggered: {0:HH:mm:ss}"
			};
			LastTriggeredTime.SetBinding(TextBlock.TextProperty, LastTriggeredTimeBinding);
			spItem.Children.Add(LastTriggeredTime);

			spItems.Children.Add(border);
		}

		private void button_Click_1(object sender, RoutedEventArgs e)
		{
			//trigger();
			AddSensor addSensor = new AddSensor();
			addSensor.ShowDialog();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			trigger();
		}
		public bool yesorno = false;
	}
}
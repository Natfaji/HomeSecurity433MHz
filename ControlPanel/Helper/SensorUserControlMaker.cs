using ControlPanel.Models.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;

namespace ControlPanel.Helper
{
	public static class SensorUserControlMaker
	{
		public static UIElement MakeControl(ISensor sensor)
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

			return border;
		}
	}
}

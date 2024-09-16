using ControlPanel.Models.Interfaces;
using Microsoft.Toolkit.Uwp.Notifications;

namespace ControlPanel.Models
{
	public class SensorAction : ISensorAction
	{
		public string Name { get; set; }
		public string Code { get; set; }
		public string Value { get; set; }
		public string Message { get; set; }
		public string ImageName { get; set; }

		//public SensorAction(string name, string code, string value, string message)
		//{
		//	Name = name ?? throw new ArgumentNullException(nameof(name));
		//	Code = code ?? throw new ArgumentNullException(nameof(code));
		//	Value = value ?? throw new ArgumentNullException(nameof(value));
		//	Message = message ?? throw new ArgumentNullException(nameof(message));
		//}

		public SensorAction()
		{

		}

		//Add Trigger method
		public void Trigger()
		{
			SendNotification($"{Name} Triggered");
		}

		public void SendNotification(string message)
		{
			new ToastContentBuilder()
				.AddText("Sensor Triggered")
				.AddText(message)
				.Show();
		}
	}
}

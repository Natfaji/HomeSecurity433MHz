namespace ControlPanel.Models.Interfaces
{
	public interface ISensorAction
	{
		string Name { get; set; }
		string Code { get; set; }
		string Value { get; set; }
		string Message { get; set; }
		string ImageName { get; set; }

		public void Trigger();

		void SendNotification(string message);
	}
}

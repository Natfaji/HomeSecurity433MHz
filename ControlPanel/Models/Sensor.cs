using ControlPanel.Models.Interfaces;

namespace ControlPanel.Models
{
	public abstract class Sensor : ISensor
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string CurrentValue { get; set; }
		public List<ISensorAction> Actions { get; set; }
		public DateTime LastTriggered { get; set; }
		public int LastTriggeredId { get; set; }
		public SensorType Type { get; set; }
		public string Description { get; set; }

		public Sensor(string name, SensorType type, string description)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			//Type = type;
			Description = description ?? throw new ArgumentNullException(nameof(description));

			Actions = new List<ISensorAction>();
		}

		//Add Trigger method
		public virtual void Trigger(string code)
		{
			Actions.FirstOrDefault(a => a.Code == code)?.Trigger();
		}

		public void AddSensorAction(ISensorAction action)
		{
			Actions.Add(action);
		}

		public void RemoveSensorAction(ISensorAction action)
		{
			Actions.Remove(action);
		}

		public void UpdateSensorAction(ISensorAction action)
		{
			throw new NotImplementedException();
		}
	}
}

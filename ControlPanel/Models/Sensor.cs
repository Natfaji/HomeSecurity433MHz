using ControlPanel.Models.Interfaces;
using System.ComponentModel;

namespace ControlPanel.Models
{
	public abstract class Sensor : ISensor, INotifyPropertyChanged
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<ISensorAction> Actions { get; set; }
		private DateTime _lastTriggeredTime { get; set; }
		public DateTime LastTriggeredTime
		{
			get => _lastTriggeredTime;
			set
			{
				if (_lastTriggeredTime != value)
				{
					_lastTriggeredTime = value;
					OnPropertyChanged(nameof(LastTriggeredTime));
				}
			}
		}
		private ISensorAction _lastTriggeredAction { get; set; }
		public ISensorAction LastTriggeredAction
		{
			get => _lastTriggeredAction;
			set
			{
				if (_lastTriggeredAction != value)
				{
					_lastTriggeredAction = value;
					OnPropertyChanged(nameof(LastTriggeredAction));
				}
			}
		}
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
		public virtual bool Trigger(string code)
		{
			int index = Actions.FindIndex(a => a.Code == code);

			if (index != -1)
			{
				ISensorAction sensorAction = Actions[index];
				sensorAction.Trigger();
				LastTriggeredTime = DateTime.Now;
				LastTriggeredAction = Actions[index];
				return true;
			}
			return false;
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

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

﻿namespace ControlPanel.Models.Interfaces
{
	public interface ISensor
	{
		int Id { get; set; }
		string Name { get; set; }
		List<ISensorAction> Actions { get; set; }
		DateTime LastTriggeredTime { get; set; }
		ISensorAction LastTriggeredAction { get; set; }
		SensorType Type { get; set; }
		string Description { get; set; }

		bool Trigger(string code);

		void AddSensorAction(ISensorAction action);

		void RemoveSensorAction(ISensorAction action);

		void UpdateSensorAction(ISensorAction action);
	}
}

using System.Diagnostics;
using ControlPanel.Models;
using ControlPanel.Models.Interfaces;

namespace ControlPanel
{
	public class SensorController
	{
		private static SensorController _UniqueInstance;
		private List<ISensor> _Sensors;
		private Dictionary<string, ISensor> _actionsByCode;

		private SensorController()
		{
			_Sensors = new List<ISensor>();
			_actionsByCode = new Dictionary<string, ISensor>();

			AddSensor(SensorDataController.GetSensors(), false);
		}

		public static SensorController GetInstance()
		{
			if (_UniqueInstance == null)
			{
				_UniqueInstance = new SensorController();
			}

			return _UniqueInstance;
		}

		public void Trigger(string code)
		{
			if (_actionsByCode.TryGetValue(code, out ISensor sensor))
			{
				if (sensor.Trigger(code))
				{
					SensorLogController.Log(sensor.Id, code);
				}
			}
			else
			{
				Debug.WriteLine("No sensor with that code!");
			}
		}

		public void AddSensor(ISensor sensor, bool saveWhenDone = true)
		{
			_Sensors.Add(sensor);

			foreach (ISensorAction action in sensor.Actions)
			{
				if (!_actionsByCode.ContainsKey(action.Code))
				{
					_actionsByCode[action.Code] = sensor;
				}
			}

			if (saveWhenDone)
			{
				SaveSensors();
			}
		}

		public void AddSensor(List<ISensor> sensors, bool saveWhenDone = true)
		{
			foreach (ISensor sensor in sensors)
			{
				AddSensor(sensor, false);
			}

			if (saveWhenDone)
			{
				SaveSensors();
			}
		}

		public void RemoveSensor(string sensorName)
		{
			//_Sensors.Remove(_Sensors.FirstOrDefault(s => s.Value.Name == sensorName));
			throw new NotImplementedException();
		}

		public void UpdateSensor(string sensorName, ISensor sensor)
		{
			//var index = _Sensors.FindIndex(s => s.Name == sensorName);
			//_Sensors[index] = sensor;
			throw new NotImplementedException();
		}

		public ISensor GetSensor(string sensorName)
		{
			//return _Sensors.FirstOrDefault(s => s.Name == sensorName);
			throw new NotImplementedException();
		}

		public List<ISensor> GetAllSensors()
		{
			return _Sensors;
		}

		public void ClearSensors()
		{
			_Sensors.Clear();
			_actionsByCode.Clear();
		}

		public void SaveSensors()
		{
			SensorDataController.SaveSensors(_Sensors);
		}
	}
}

using ControlPanel.Models;
using ControlPanel.Models.Interfaces;
using System.Configuration;
using System.IO;
using System.Text.Json;

namespace ControlPanel
{
	public static class SensorDataController
	{
		private static string _filePath = ConfigurationManager.AppSettings["JsonFilePath"];
		private static readonly JsonSerializerOptions options = new JsonSerializerOptions
		{
			WriteIndented = true,
			Converters = {
				new SensorConverter(),
				new SensorActionConverter()
			}
		};

		public static void AddSensorData(ISensor sensor)
		{
			List<ISensor> SensorsList = GetSensors();
			SensorsList.Add(sensor);
			SaveSensors(SensorsList);
		}

		public static void AddSensorData(List<ISensor> sensors)
		{
			List<ISensor> SensorsList = GetSensors();
			SensorsList.AddRange(sensors);
			SaveSensors(SensorsList);
		}

		public static List<ISensor> GetSensors()
		{
			CheckFile();

			string json = File.ReadAllText(_filePath);
			return JsonSerializer.Deserialize<List<ISensor>>(json, options);
		}

		public static void SaveSensors(List<ISensor> sensors)
		{
			CheckFile();

			string json = JsonSerializer.Serialize(sensors, options);
			File.WriteAllText(_filePath, json);
		}

		public static void SetFilePath(string filePath)
		{
			_filePath = filePath;
			ConfigurationManager.AppSettings["JsonFilePath"] = filePath;
		}

		private static void CheckFile()
		{
			if (!File.Exists(_filePath))
			{
				File.WriteAllText(_filePath, "[]");
			}
		}
	}
}

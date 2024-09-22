using System.Configuration;
using System.IO;

namespace ControlPanel
{
	public static class SensorLogController
	{
		private static string _filePath = ConfigurationManager.AppSettings["LogFilePath"];

		public static void Log(int sensorId, string code)
		{
			string LogString = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss},{sensorId},{code}";
			File.AppendAllText(_filePath, LogString + Environment.NewLine);
		}

		public static List<string> ReadLog()
		{
			StreamReader reader = new StreamReader(_filePath);
			List<string> log = new List<string>();

			while (!reader.EndOfStream)
			{
				log.Add(reader.ReadLine());
			}

			reader.Close();

			return log;
		}
	}
}

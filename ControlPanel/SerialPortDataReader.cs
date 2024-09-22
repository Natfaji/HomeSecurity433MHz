using System;
using System.Diagnostics;
using System.IO.Ports;
using Windows.Devices.SerialCommunication;
using Windows.Networking;

namespace ControlPanel
{
	public class SerialPortDataReader
	{
		static SerialPort _serialPort;

		public delegate void DataReceived(string data);
		public event DataReceived OnDataReceived;

		private readonly TimeSpan CooldownPeriod = TimeSpan.FromSeconds(1);
		private Dictionary<string, DateTime> _lastTriggeredTimes = new Dictionary<string, DateTime>();

		public SerialPortDataReader()
		{
			// Initialize the serial port
			_serialPort = new SerialPort();

			ConfigureSerialPort("COM3", baudRate: 115200, newLine: "\r\n");

			// Subscribe to the DataReceived event
			_serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

			try
			{
				// Open the serial port
				_serialPort.Open();
			}
			catch (Exception ex)
			{
				throw new Exception("Error opening serial port: " + ex.Message);
			}
			finally
			{
				// Close the serial port when done
				//if (_serialPort.IsOpen)
				//	_serialPort.Close();
			}
		}

		public void ConfigureSerialPort(string portName, int baudRate = 9600, string newLine = "\n", Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One, Handshake handshake = Handshake.None, int readTimeout = 500,
			int writeTimeout = 500)
		{
			// Set your port name (e.g., COM3, COM4, etc.)
			_serialPort.PortName = portName; // Change to your Arduino port
			_serialPort.BaudRate = baudRate;   // Match the baud rate set in Arduino sketch
			_serialPort.NewLine = "\r\n"; // Set the newline character
			_serialPort.Parity = parity;
			_serialPort.DataBits = dataBits;
			_serialPort.StopBits = stopBits;
			_serialPort.Handshake = handshake;
			_serialPort.ReadTimeout = readTimeout; // Optional: Set a read timeout
			_serialPort.WriteTimeout = writeTimeout; // Optional: Set a write timeout
		}

		private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				// Read the data from the Arduino
				string data = _serialPort.ReadLine();

				DateTime currentTime = DateTime.Now;

				// Check if the code was triggered recently
				if (_lastTriggeredTimes.ContainsKey(data))
				{
					DateTime lastTriggered = _lastTriggeredTimes[data];
					if (currentTime - lastTriggered < CooldownPeriod)
					{
						// Skip processing if the cooldown period has not passed
						_lastTriggeredTimes[data] = currentTime;
						Debug.WriteLine($"Cooldown active for code: {data}");
						return;
					}
				}

				// Handle the received data (e.g., trigger sensor actions)
				ProcessReceivedData(data);

				_lastTriggeredTimes[data] = currentTime;
			}
			catch (TimeoutException ex)
			{
				throw new TimeoutException("Timeout exception " + ex.Message);
			}
		}

		private void ProcessReceivedData(string data)
		{
			OnDataReceived?.Invoke(data);  // Trigger the event with the received data
		}
	}
}

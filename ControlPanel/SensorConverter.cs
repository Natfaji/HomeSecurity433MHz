using ControlPanel.Models;
using ControlPanel.Models.Interfaces;
using ControlPanel.Models.SensorTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

public class SensorConverter : JsonConverter<ISensor>
{
	public override ISensor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
		{
			JsonElement root = doc.RootElement;

			SensorType type = (SensorType)root.GetProperty("Type").GetInt32();

			switch (type)
			{
				case SensorType.Door:
					return JsonSerializer.Deserialize<DoorSensor>(root.GetRawText(), options);
				case SensorType.Motion:
					return JsonSerializer.Deserialize<MotionSensor>(root.GetRawText(), options);
				default:
					throw new NotSupportedException($"Sensor type {type} is not supported");
			}
		}
	}

	public override void Write(Utf8JsonWriter writer, ISensor value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();

		foreach (var property in value.GetType().GetProperties())
		{
			var propertyValue = property.GetValue(value);
			writer.WritePropertyName(property.Name);
			JsonSerializer.Serialize(writer, propertyValue, propertyValue?.GetType() ?? typeof(object), options);
		}

		writer.WriteEndObject();
	}
}

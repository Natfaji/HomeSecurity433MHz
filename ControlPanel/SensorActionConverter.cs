using ControlPanel.Models.Interfaces;
using ControlPanel.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

public class SensorActionConverter : JsonConverter<ISensorAction>
{
	public override ISensorAction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return JsonSerializer.Deserialize<SensorAction>(ref reader, options);
	}

	public override void Write(Utf8JsonWriter writer, ISensorAction value, JsonSerializerOptions options)
	{
		JsonSerializer.Serialize(writer, (SensorAction)value, options);
	}
}

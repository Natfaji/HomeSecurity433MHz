namespace ControlPanel.Models.SensorTypes
{
    public class MotionSensor : Sensor
    {
        public int ResetAfterSeconds { get; set; }

        public MotionSensor(string name, SensorType type, string description, int resetAfterSeconds) : base(name, type, description)
        {
            ResetAfterSeconds = resetAfterSeconds;
        }

        // TODO: reset state after seconds
    }
}

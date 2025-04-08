using FanControl.Plugins;

namespace FanControl.DellPlugin
{
    public class ClevoFanManagementFanSensor : IPluginSensor
    {
        private readonly ClevoFanManagementControlSensor _fan;

        public ClevoFanManagementFanSensor(ClevoFanManagementControlSensor fan)
        {
            _fan = fan;
        }

        public string Identifier => $"Clevo/FanSensor/{_fan.FanIndex}";

        public float? Value { get; private set; }

        public string Name => $"Clevo Fan {_fan.FanIndex}";

        public string Origin => $"ClevoEcInfo";

        public string Id => $"Fan_{_fan.FanIndex}";

        public void Update()
        {
            Value = _fan.Value;
        }
    }
}

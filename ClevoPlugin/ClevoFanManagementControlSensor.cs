using FanControl.Plugins;

namespace FanControl.DellPlugin
{
    public class ClevoFanManagementControlSensor: IPluginControlSensor
    {
        private readonly FanController _fanControl;
        private readonly int _fanIndex;
        private float? _val;

        public ClevoFanManagementControlSensor(FanController fanControl, int fanIndex)
        {
            _fanControl = fanControl;
            _fanIndex = fanIndex;
        }

        public float? Value { get; private set; }

        public string Name => $"Clevo Control {_fanIndex}";

        public string Origin => $"ClevoEcInfo";

        public string Id => "Control_" + _fanIndex.ToString();

        public int FanIndex => _fanIndex;

        public void Reset()
        {
            _fanControl.SetFansAuto(_fanIndex);
        }

        public void Set(float val)
        {
            _val = val;
            _fanControl.SetFanSpeed(_fanIndex, (double)_val);
        }

        public void Update() => Value = _val;
    }
}

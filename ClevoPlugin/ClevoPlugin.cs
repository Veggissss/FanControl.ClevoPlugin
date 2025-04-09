using FanControl.Plugins;
using System;
using System.Collections.Generic;

namespace FanControl.DellPlugin
{
    public class ClevoPlugin : IPlugin, IDisposable
    {
        private FanController _fanControl;

        public string Name => "Clevo";

        public void Close()
        {
            _fanControl = null;
        }

        public void Initialize()
        {
            _fanControl = new FanController();
        }

        public void Load(IPluginSensorsContainer _container)
        {
            // Index 1 is CPU & 2 is GPU Fan
            ClevoFanManagementControlSensor cpuFan = new ClevoFanManagementControlSensor(_fanControl, 1);
            ClevoFanManagementControlSensor gpuFan = new ClevoFanManagementControlSensor(_fanControl, 2);
            IEnumerable<ClevoFanManagementControlSensor> fanControls = new[] { cpuFan, gpuFan };

            ClevoFanManagementFanSensor cpuFanSensor = new ClevoFanManagementFanSensor(cpuFan);
            ClevoFanManagementFanSensor gpuFanSensor = new ClevoFanManagementFanSensor(gpuFan);
            IEnumerable<ClevoFanManagementFanSensor> fanSensors = new[] { cpuFanSensor, gpuFanSensor };

            _container.ControlSensors.AddRange(fanControls);
            _container.FanSensors.AddRange(fanSensors);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed state (managed objects)
                _fanControl.Dispose();
            }
            Close();
        }

         ~ClevoPlugin()
         {
             // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
             Dispose(disposing: false);
         }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

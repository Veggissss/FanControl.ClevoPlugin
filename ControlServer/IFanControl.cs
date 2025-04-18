﻿using System;

namespace FanControl.Server
{
    public interface IFanControl : IDisposable {

        ECData2 GetECData(int fanNr);

        void SetFanSpeed(int fanNr, double fanSpeedPercentage);

        void SetFansAuto(int fanNr);
    }
}

﻿using System.Runtime.InteropServices;

namespace FanControl.Server
{
    public class FanControl : IFanControl {
        IntPtr pDll;
        IntPtr pInitIo;
        IntPtr pGetTempFanDuty;
        IntPtr pSetFanDuty;
        IntPtr pSetFanDutyAuto;
        InitIo initIo;
        GetTempFanDuty getTempFanDuty;
        SetFanDuty2 setFanDuty;
        SetFanDutyAuto setFanDutyAuto;

        public FanControl() {
            string pathDll = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\ClevoEcInfo.dll";
            pDll = NativeMethods.LoadLibrary(pathDll);
            if (pDll == IntPtr.Zero)
            {
                int err = Marshal.GetLastWin32Error();
                throw new Exception($"Can't load {pathDll}\nError code: {err}");
            }

            pInitIo = NativeMethods.GetProcAddress(pDll, "InitIo");
            pGetTempFanDuty = NativeMethods.GetProcAddress(pDll, "GetTempFanDuty");
            pSetFanDuty = NativeMethods.GetProcAddress(pDll, "SetFanDuty");
            pSetFanDutyAuto = NativeMethods.GetProcAddress(pDll, "SetFanDutyAuto");
            if (pInitIo == IntPtr.Zero || pGetTempFanDuty == IntPtr.Zero || pSetFanDuty == IntPtr.Zero || pSetFanDutyAuto == IntPtr.Zero) throw new Exception("Can't find methods");


            initIo = (InitIo)Marshal.GetDelegateForFunctionPointer(pInitIo, typeof(InitIo));
            getTempFanDuty = (GetTempFanDuty)Marshal.GetDelegateForFunctionPointer(pGetTempFanDuty, typeof(GetTempFanDuty));
            setFanDuty = (SetFanDuty2)Marshal.GetDelegateForFunctionPointer(pSetFanDuty, typeof(SetFanDuty2));
            setFanDutyAuto = (SetFanDutyAuto)Marshal.GetDelegateForFunctionPointer(pSetFanDutyAuto, typeof(SetFanDutyAuto));

            bool ioResult = initIo();
            if (!ioResult) throw new Exception("Error initializing");
        }

        public ECData2 GetECData(int fanNr) {
            return getTempFanDuty(fanNr);
        }

        public void SetFanSpeed(int fanNr, double fanSpeedPercentage) {

            if (fanSpeedPercentage > 100)
                fanSpeedPercentage = 100;

            if (fanSpeedPercentage < 0)
                fanSpeedPercentage = 0;

            double fanSpeed = fanSpeedPercentage * 255.0 / 100.0;
            setFanDuty(fanNr, (int)fanSpeed);
        }

        public void SetFansAuto(int fanNr) {
            setFanDutyAuto(fanNr);
        }

        public void Dispose() {
            this.setFanDutyAuto(0);
            this.setFanDutyAuto(1);
            this.setFanDutyAuto(2);
            this.setFanDutyAuto(3);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool InitIo();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ECData2 GetTempFanDuty(int fanNr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SetFanDuty2(int p1, int p2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SetFanDutyAuto(int p1);

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ECData2 {
        public byte Remote;
        public byte Local;
        public byte FanDuty;
        public byte Reserve;
    };
}

# FanControl.ClevoPlugin

A plugin for [FanControl](https://github.com/Rem0o/FanControl.Releases) that provides fan control support for Clevo laptops by using `ClevoEcInfo.dll`.

## Credits

- **Fan Control Code**: Credit to [oleuzop's OptimizedClevoFan](https://github.com/oleuzop/OptimizedClevoFan) for the fan control logic.
  - This work is based on [djsubtronic's ClevoFanControl](https://github.com/djsubtronic/ClevoFanControl).
- **Plugin Template**: Based on the [DellPlugin](https://github.com/Rem0o/FanControl.DellPlugin).

## Installation

Follow these steps to install the ClevoPlugin:

1. **Install .NET Core**  
   Ensure you have the required .NET runtime by installing it from [here](https://aka.ms/dotnet-core-applaunch?missing_runtime=true&arch=x86&rid=win10-x86&apphost_version=6.0.8).

2. **Install the NTPort Driver**  
   Download and install the NTPort driver from the `OptimizedClevoFan` repository:  
   [NTPortDrvSetup.exe](https://github.com/djsubtronic/ClevoFanControl/blob/master/ClevoFanControl/NTPortDrvSetup.exe).

3. **Download Latest Plugin Release**  
   Download the latest release zip from the [Releases page](https://github.com/Veggissss/FanControl.ClevoPlugin/releases).

5. **Install Plugin**  
   - Option 1: Use the **"Install Plugin"** button in FanControl to install the plugin zip.
   - Option 2: Manually copy all the contents from the plugin’s zip into the **"plugins"** folder inside FanControl’s directory.

## Building

The `ClevoEcInfo.dll` is 32-bit, so make sure to compile the `ControlServer` as a `x86`.
The `ControlServer` output files should be placed in a `Server/` subfolder in the `ClevoPlugin` output.

`FanControl` does not support 32-bit plugins, so the `ClevoPlugin` should be compiled as `Any Cpu` or `x64`. 
This is why an IPC approach was needed to communicate with the `ClevoEcInfo.dll`. 
More info on 32-bit/64-bit dll IPC can be found [here](https://blog.mattmags.com/2007/06/30/accessing-32-bit-dlls-from-64-bit-code/)

## License

This project is licensed under the MIT License.

# FanControl.ClevoPlugin

A plugin for [FanControl](https://github.com/Rem0o/FanControl.Releases) that provides support for Clevo laptops using `ClevoEcInfo.dll`.

## Credits

- **Plugin Template**: Based on the [DellPlugin](https://github.com/Rem0o/FanControl.DellPlugin).
- **Fan Control Code**: Credit to [oleuzop](https://github.com/oleuzop/OptimizedClevoFan) for the fan control logic.
  - This work is based on [djsubtronic's ClevoFanControl](https://github.com/djsubtronic/ClevoFanControl).

## Installation

Follow these steps to install the ClevoPlugin:

1. **Install .NET Core**  
   Ensure you have the required .NET runtime by installing it from [here](https://aka.ms/dotnet-core-applaunch?missing_runtime=true&arch=x86&rid=win10-x86&apphost_version=6.0.8).

2. **Install the NTPort Driver**  
   Download and install the NTPort driver from the `OptimizedClevoFan` repository:  
   [NTPortDrvSetup.exe](https://github.com/djsubtronic/ClevoFanControl/blob/master/ClevoFanControl/NTPortDrvSetup.exe).

3. **Download Latest Release**  
   Download the latest release zip from the [Releases page](https://github.com/Rem0o/FanControl.Releases).

5. **Install Plugin**  
   - Option 1: Use the **"Install Plugin"** button in FanControl to install the plugin.
   - Option 2: Manually copy all the contents from the plugin’s zip into the **"plugins"** folder inside FanControl’s directory.

## Building

Since the `ClevoEcInfo.dll` is 32-bit, so make sure to compile the Server as a x86.
The server output files should be placed in a `Server/` folder.

## License

This project is licensed under the MIT License.

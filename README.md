# PSOMapRNGSimulation

A program to simulate the maps RNG generation alogrhythm of PSO on GameCube

## System Requirements
The only requirement is to have the .NET runtime installed. You can install the latest version of .NET by following [this link](https://dotnet.microsoft.com/download) (note, you ideally want to install .NET, not .NET Core or .NET Framework). For Linux, refer to your distribution's documentation for proper installation of the runtime.

## Usage
```Syntax: PSOMapRNGSimulation episode1|episode2 SeedCount [startingSeed]```

Outputs the resulting maps variant and objset for SeedCount amount of consecutive seeds starting at StartingSeed with
the maps of the corresponding episode. This only works with the NTSC-U version revision 0

Seed count must be a number above 0.

StartingSeed must be a 32 bit hexadecimal number with or without the "0x" prefix. If no StartingSeed is specified,
0x00000001 will be used which is the first seed on boot.

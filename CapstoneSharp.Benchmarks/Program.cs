using System.Reflection;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Running;
using CapstoneSharp;

if (Environment.GetEnvironmentVariable("CAPSTONE_PATH") is { } capstonePath)
{
    NativeLibrary.SetDllImportResolver(typeof(CapstoneDisassembler<,,>).Assembly, (name, _, _) =>
    {
        if (name == "capstone")
        {
            return NativeLibrary.Load(capstonePath);
        }

        return default;
    });
}

var dataSet = Environment.GetEnvironmentVariable("DATASET");

while (dataSet == null)
{
    Console.WriteLine("Which data set to use? (small/il2cpp)");
    dataSet = Console.ReadLine();
}

if (dataSet != "small" && dataSet != "il2cpp") throw new ArgumentException($"Data set {dataSet}, not found");

Environment.SetEnvironmentVariable("DATASET", dataSet);

BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(args);

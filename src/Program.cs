using System.Globalization;
using System.Reflection;
using System.Text;

namespace PSOMapRNGSimulation;

public static class Program
{
  private struct ProgArgs
  {
    public Episode Episode = default;
    public uint StartSeed = 1; // 1 is the first seed on boot
    public long SeedCount = 0;
    public bool Help = false;

    public ProgArgs()
    {
    }
  }

  public static void Main(string[] args)
  {
    ProgArgs progArgs;
    try
    {
      progArgs = ParseArgs(args);
      if (progArgs.Help)
      {
        PrintHelp();
        return;
      }
    }
    catch (ArgumentException e)
    {
      Console.WriteLine($"Error: {e.Message}");
      PrintHelp();
      return;
    }

    PrintSimulation(progArgs);
  }

  private static ProgArgs ParseArgs(string[] args)
  {
    if (args.Length == 2 && args[1].ToLower() == "help")
      return new ProgArgs { Help = true };

    if (args.Length < 3)
      throw new ArgumentException("Not enough arguments, review the syntax below");

    ProgArgs progArgs = new ProgArgs();
    if (!Enum.TryParse(args[1], true, out progArgs.Episode))
    {
      throw new ArgumentException($"Invalid episode, specify either \"{Enum.GetName(Episode.Episode1)!.ToLower()}\"" +
                                  $" or \"{Enum.GetName(Episode.Episode2)!.ToLower()}\"");
    }

    if (!long.TryParse(args[2], out progArgs.SeedCount))
      throw new ArgumentException("Invalid seed count");
    if (progArgs.SeedCount < 1)
      throw new ArgumentException($"The seed count has to be at least 1");

    if (args.Length > 3)
    {
      if (!uint.TryParse(args[3].ToUpper().Replace("0X", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture,
            out progArgs.StartSeed))
        throw new ArgumentException("Invalid starting seed, it must be in hexadecimal");
    }

    return progArgs;
  }

  private static void PrintHelp()
  {
    AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
    Console.WriteLine($"{assemblyName.Name} v{assemblyName.Version!.ToString(3)}");
    Console.WriteLine($"Syntax: {assemblyName.Name} episode1|episode2 SeedCount [startingSeed]");
    Console.WriteLine("Outputs the resulting maps variant and objset for SeedCount amount of consecutive seeds " +
                      "starting at StartingSeed with the maps of the corresponding episode. " +
                      "This only works with the NTSC-U version revision 0");
    Console.WriteLine("Seed count must be a number above 0.");
    Console.WriteLine("StartingSeed must be a 32 bit hexadecimal number with or without the \"0x\" prefix. If no " +
                      "StartingSeed is specified, 0x00000001 will be used which is the first seed on boot.");
  }

  private static void PrintHeader(Map[] episodeMaps)
  {
    StringBuilder sb = new StringBuilder("Seed");
    foreach (var map in episodeMaps)
    {
      sb.Append($";{Enum.GetName(map)} variant");
      sb.Append($";{Enum.GetName(map)} objset");
    }

    Console.WriteLine(sb.ToString());
  }

  private static void PrintFormattedFloorMaps(uint seed, List<FloorMap> floorMaps)
  {
    StringBuilder sb = new StringBuilder(seed.ToString("X8"));
    foreach (var floorMap in floorMaps)
    {
      sb.Append($";{floorMap.Variant}");
      sb.Append($";{floorMap.ObjSet}");
    }

    Console.WriteLine(sb.ToString());
  }

  private static void PrintSimulation(ProgArgs progArgs)
  {
    Map[] episodeMaps = Array.Empty<Map>();
    switch (progArgs.Episode)
    {
      case Episode.Episode1:
        episodeMaps = RomData.Ep1Maps;
        break;
      case Episode.Episode2:
        episodeMaps = RomData.Ep2Maps;
        break;
    }

    PrintHeader(episodeMaps);
    uint currentSeed = progArgs.StartSeed;
    for (int i = 0; i < progArgs.SeedCount; i++)
    {
      uint workingSeed = currentSeed;
      List<FloorMap> generatedMaps = new List<FloorMap>();
      foreach (var map in episodeMaps)
        generatedMaps.Add(PsoRng.GenerateFloorMap(ref workingSeed, map));
      PrintFormattedFloorMaps(currentSeed, generatedMaps);
      PsoRng.Prng(ref currentSeed);
    }
  }
}
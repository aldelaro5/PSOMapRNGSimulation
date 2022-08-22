namespace PSOMapRNGSimulation;

public enum Episode
{
  Episode1,
  Episode2
}

public struct FloorMap
{
  public int Variant;
  public int ObjSet;
}

public static class PsoRng
{
  public static uint Prng(ref uint seed)
  {
    seed = seed * 0x41c64e6d + 0x3039;
    return seed >> 0x10 & 0x7fff;
  }

  public static float RngToFloat(ref uint seed)
  {
    return Prng(ref seed) / (float)0x8000;
  }

  public static FloorMap GenerateFloorMap(ref uint seed, Map map)
  {
    float rngVariant = RngToFloat(ref seed);
    float rngObjSet = RngToFloat(ref seed);
    return new FloorMap()
    {
      Variant = (int)(RomData.AllMapinfo[map].NbrVariants * rngVariant),
      ObjSet = (int)(RomData.AllMapinfo[map].NbrObjSet * rngObjSet)
    };
  }
}
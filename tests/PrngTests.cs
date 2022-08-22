using Xunit;

namespace PSOMapRNGSimulation.Tests;

public class PrngTests
{
  private const float floatErrorMargin = 0.000001f;
  
  [Theory]
  [CsvData("./PrngTestData.csv")]
  public void ReturnsCorrectValues(uint seed, uint expectedSeed, uint expectedOutput)
  {
    uint output = PsoRng.Prng(ref seed);
    Assert.Equal(expectedSeed, seed);
    Assert.Equal(expectedOutput, output);
  }

  [Theory]
  [CsvData("./PrngFloatTestData.csv")]
  public void ReturnsCorrectFloatValue(uint seed, float expectedFloat)
  {
    float output = PsoRng.RngToFloat(ref seed);
    Assert.InRange(output, 0,  1 - float.Epsilon);
    Assert.InRange(output, expectedFloat - floatErrorMargin, expectedFloat + floatErrorMargin);
  }

  [Theory]
  [CsvData("./PrngMapTestData.csv")]
  public void GenerateCorrectMap(uint seed, int mapId, int expectedVariant, int expectedObjSet)
  {
    FloorMap floorMap = PsoRng.GenerateFloorMap(ref seed, (Map)mapId);
    Assert.Equal(expectedVariant, floorMap.Variant);
    Assert.Equal(expectedObjSet, floorMap.ObjSet);
  }
}
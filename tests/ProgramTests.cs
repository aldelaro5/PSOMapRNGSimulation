using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace PSOMapRNGSimulation.Tests;

public class ProgramTests
{
  private readonly TextWriter _writer = new StringWriter();

  private readonly string _psoAssemblyName =
    Assembly.GetAssembly(typeof(Program))?.GetName().Name ?? throw new InvalidOperationException();

  public ProgramTests()
  {
    Console.SetOut(_writer);
  }

  // Since the help message will have the assembly name, it's a good enough heuristic
  private bool HasHelpMessage(string? msg)
  {
    return msg != null && msg.Contains(_psoAssemblyName);
  }

  [Fact]
  public void PrintsHelpWhenAsked()
  {
    string[] args = { "help" };
    Program.Main(args);
    string? output = _writer.ToString();
    Assert.True(HasHelpMessage(output));
  }

  [Theory]
  [InlineData("")]
  [InlineData("", "episode1")]
  [InlineData("", "episode2")]
  [InlineData("", "episode1 test")]
  [InlineData("", "episode2 deadbeef")]
  [InlineData("", "episode1 0")]
  [InlineData("", "episode2 -1")]
  [InlineData("", "episode1 10 test")]
  [InlineData("", "episode2 10 0xNope")]
  public void PrintsErrorAndHelpForInvalidArguments(params string[] args)
  {
    Program.Main(args);
    string? output = _writer.ToString();
    Assert.StartsWith("error", output?.ToUpper() ?? string.Empty);
    Assert.True(HasHelpMessage(output));
  }

  [Fact]
  public void PrintsCorrectMapNamesInHeaderForEpisode1()
  {
    Program.Main(new[] { "", "episode1", "10" });
    string? output = _writer.ToString();
    string firstLine = output?.Split(Environment.NewLine).FirstOrDefault() ?? string.Empty;
    Assert.Collection(RomData.Ep1Maps, x => Assert.Contains(Enum.GetName(x), firstLine));
  }

  [Fact]
  public void PrintsCorrectMapNamesInHeaderForEpisode2()
  {
    Program.Main(new[] { "", "episode2", "10" });
    string? output = _writer.ToString();
    string firstLine = output?.Split(Environment.NewLine).FirstOrDefault() ?? string.Empty;
    Assert.Collection(RomData.Ep2Maps, x => Assert.Contains(Enum.GetName(x), firstLine));
  }

  [Fact]
  public void PrintsCorrectAmountOfLines()
  {
    int seedCount = 10;
    Program.Main(new[] { "", "episode1", $"{seedCount}" });
    string? output = _writer.ToString();
    string[] lines = output?.Split(Environment.NewLine) ?? Array.Empty<string>();
    Assert.Equal(seedCount + 1, lines.Length);
  }

  [Fact]
  public void PrintsCorrectAmountOfColumns()
  {
    Program.Main(new[] { "", "episode1", "10" });
    string? output = _writer.ToString();
    string[] lines = output?.Split(Environment.NewLine) ?? Array.Empty<string>();
    Assert.Collection(lines, x => Assert.Equal(RomData.Ep1Maps.Length * 2 + 1, x.Split(';').Length));
  }

  [Fact]
  public void DefaultStartingSeedIsCorrect()
  {
    Program.Main(new[] { "", "episode1", "10" });
    string? output = _writer.ToString();
    string firstLine = output?.Split(Environment.NewLine).FirstOrDefault() ?? string.Empty;
    Assert.StartsWith(1.ToString("X8"), firstLine);
  }

  [Fact]
  public void HonorCustomStartingSeed()
  {
    uint startingSeed = 555;
    Program.Main(new[] { "", "episode1", "10", $"{startingSeed}" });
    string? output = _writer.ToString();
    string firstLine = output?.Split(Environment.NewLine).FirstOrDefault() ?? string.Empty;
    Assert.StartsWith(startingSeed.ToString("X8"), firstLine);
  }

  [Fact]
  public void SeedsAreRolledLogically()
  {
    uint seed = 555;
    Program.Main(new[] { "", "episode1", $"{seed}" });
    string? output = _writer.ToString();
    var lines = output?.Split(Environment.NewLine).Skip(1) ?? Array.Empty<string>();
    Assert.Collection(lines, x =>
    {
      Assert.StartsWith(seed.ToString("X8"), x);
      PsoRng.Prng(ref seed);
    });
  }
}
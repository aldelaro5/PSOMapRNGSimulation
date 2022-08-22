namespace PSOMapRNGSimulation;

public enum Map
{
  Invalid = -1,
  Pioneer2Ep1 = 0,
  Forest1 = 1,
  Forest2 = 2,
  Cave1 = 3,
  Cave2 = 4,
  Cave3 = 5,
  Mines1 = 6,
  Mines2 = 7,
  Ruins1 = 8,
  Ruins2 = 9,
  Ruins3 = 10,
  BossDragon = 11,
  BossDerolle = 12,
  BossVolopt = 13,
  BossDarkfalz = 14,
  Lobby = 15,
  BattleSpaceship = 16,
  BattleRuins = 17,
  Pioneer2Ep2 = 18,
  TempleA = 19,
  TempleB = 20,
  SpaceshipA = 21,
  SpaceshipB = 22,
  Cca = 23,
  JungleEast = 24,
  JungleNorth = 25,
  Mountain = 26,
  Seaside = 27,
  SeabedUpper = 28,
  SeabedLower = 29,
  BossGalgryphon = 30,
  BossOlgaflow = 31,
  BossBarbaray = 32,
  BossGoldragon = 33,
  SeasideNight = 34,
  Tower = 35
}

public struct MapInfo
{
  public int NbrVariants;
  public int NbrObjSet;
}

public static class RomData
{
  public static readonly Map[] Ep1Maps = new Map[18]
  {
    Map.Pioneer2Ep1,
    Map.Forest1,
    Map.Forest2,
    Map.Cave1,
    Map.Cave2,
    Map.Cave3,
    Map.Mines1,
    Map.Mines2,
    Map.Ruins1,
    Map.Ruins2,
    Map.Ruins3,
    Map.BossDragon,
    Map.BossDerolle,
    Map.BossVolopt,
    Map.BossDarkfalz,
    Map.Lobby,
    Map.BattleSpaceship,
    Map.BattleRuins
  };

  public static readonly Map[] Ep2Maps = new Map[18]
  {
    Map.Pioneer2Ep2,
    Map.TempleA,
    Map.TempleB,
    Map.SpaceshipA,
    Map.SpaceshipB,
    Map.Cca,
    Map.JungleEast,
    Map.JungleNorth,
    Map.Mountain,
    Map.Seaside,
    Map.SeabedUpper,
    Map.SeabedLower,
    Map.BossGalgryphon,
    Map.BossOlgaflow,
    Map.BossBarbaray,
    Map.BossGoldragon,
    Map.SeasideNight,
    Map.Tower,
  };

  public static readonly Dictionary<Map, MapInfo> AllMapinfo = new Dictionary<Map, MapInfo>()
  {
    { Map.Pioneer2Ep1, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } },
    { Map.Forest1, new MapInfo() { NbrVariants = 1, NbrObjSet = 3 } },
    { Map.Forest2, new MapInfo() { NbrVariants = 1, NbrObjSet = 3 } },
    { Map.Cave1, new MapInfo() { NbrVariants = 3, NbrObjSet = 1 } },
    { Map.Cave2, new MapInfo() { NbrVariants = 3, NbrObjSet = 1 } },
    { Map.Cave3, new MapInfo() { NbrVariants = 3, NbrObjSet = 1 } },
    { Map.Mines1, new MapInfo() { NbrVariants = 3, NbrObjSet = 2 } },
    { Map.Mines2, new MapInfo() { NbrVariants = 3, NbrObjSet = 2 } },
    { Map.Ruins1, new MapInfo() { NbrVariants = 3, NbrObjSet = 2 } },
    { Map.Ruins2, new MapInfo() { NbrVariants = 3, NbrObjSet = 2 } },
    { Map.Ruins3, new MapInfo() { NbrVariants = 3, NbrObjSet = 2 } },
    { Map.BossDragon, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } },
    { Map.BossDerolle, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } },
    { Map.BossVolopt, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } },
    { Map.BossDarkfalz, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } },
    { Map.Lobby, new MapInfo() { NbrVariants = 10, NbrObjSet = 1 } },
    { Map.BattleSpaceship, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } },
    { Map.BattleRuins, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } },
    { Map.Pioneer2Ep2, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } },
    { Map.TempleA, new MapInfo() { NbrVariants = 2, NbrObjSet = 1 } },
    { Map.TempleB, new MapInfo() { NbrVariants = 2, NbrObjSet = 1 } },
    { Map.SpaceshipA, new MapInfo() { NbrVariants = 2, NbrObjSet = 1 } },
    { Map.SpaceshipB, new MapInfo() { NbrVariants = 2, NbrObjSet = 1 } },
    { Map.Cca, new MapInfo() { NbrVariants = 1, NbrObjSet = 3 } },
    { Map.JungleEast, new MapInfo() { NbrVariants = 1, NbrObjSet = 3 } },
    { Map.JungleNorth, new MapInfo() { NbrVariants = 1, NbrObjSet = 3 } },
    { Map.Mountain, new MapInfo() { NbrVariants = 2, NbrObjSet = 2 } },
    { Map.Seaside, new MapInfo() { NbrVariants = 1, NbrObjSet = 3 } },
    { Map.SeabedUpper, new MapInfo() { NbrVariants = 2, NbrObjSet = 1 } },
    { Map.SeabedLower, new MapInfo() { NbrVariants = 2, NbrObjSet = 1 } },
    { Map.BossGalgryphon, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } },
    { Map.BossOlgaflow, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } },
    { Map.BossBarbaray, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } },
    { Map.BossGoldragon, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } },
    { Map.SeasideNight, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } },
    { Map.Tower, new MapInfo() { NbrVariants = 1, NbrObjSet = 1 } }
  };
}
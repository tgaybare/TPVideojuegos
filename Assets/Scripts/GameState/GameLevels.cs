using System.Collections.Generic;


public sealed class GameLevels
{
    public static Levels MAX_LEVEL = Levels.LEVEL_2;

    public enum Levels
    {
        LEVEL_1 = 1,
        LEVEL_2
    }

    public static readonly Dictionary<Levels, string> LevelNames = new()
    {
        { Levels.LEVEL_1, "Level 1" },
        { Levels.LEVEL_2, "Level 2" }
    };


}

using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class DungeonCrawlerController
{
    public static LastAddedHashSet<Vector2Int> positionsVisited = new LastAddedHashSet<Vector2Int>();

    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        { Direction.Up, Vector2Int.up },
        { Direction.Down, Vector2Int.down },
        { Direction.Left, Vector2Int.left },
        { Direction.Right, Vector2Int.right }
    };

    public static LastAddedHashSet<Vector2Int> GenerateDungeon(DungeonGenerationData data)
    {
        List<DungeonCrawler> crawlers = new List<DungeonCrawler>();

        for (int i = 0; i < data.NumberOfCrawlers; i++)
        {
            crawlers.Add(new DungeonCrawler(i, Vector2Int.zero));
        }

        for (int i = 0; i < Random.Range(data.IterationMin, data.IterationMax); i++)
        {
            foreach (DungeonCrawler crawler in crawlers)
            {
                Vector2Int newPosition = crawler.Move(directionMovementMap);
                positionsVisited.Add(newPosition);
            }
        }

        return positionsVisited;
    }

}

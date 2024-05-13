using System.Collections;
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
    public static HashSet<Vector2Int> positionsVisited = new HashSet<Vector2Int>();

    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        { Direction.Up, Vector2Int.up },
        { Direction.Down, Vector2Int.down },
        { Direction.Left, Vector2Int.left },
        { Direction.Right, Vector2Int.right }
    };

    public static HashSet<Vector2Int> GenerateDungeon(DungeonGenerationData data) {
        List<DungeonCrawler> crawlers = new List<DungeonCrawler>();

        Debug.Log($"[DungeonCrawlerController] Number Of Crawlers: {data.NumberOfCrawlers}");
        Debug.Log($"[DungeonCrawlerController] Iteration Min: {data.IterationMin}");
        Debug.Log($"[DungeonCrawlerController] Iteration Max: {data.IterationMax}");

        for (int i = 0; i < data.NumberOfCrawlers; i++)
        {
            crawlers.Add(new DungeonCrawler(i, Vector2Int.zero));
            Debug.Log($"[DungeonCrawlerController] Added Crawler {i} (Total = {crawlers.Count})");
        }

        for (int i = 0; i < Random.Range(data.IterationMin, data.IterationMax); i++)
        {
            foreach (DungeonCrawler crawler in crawlers)
            {
                Vector2Int newPosition = crawler.Move(directionMovementMap);
                Debug.Log($"[DungeonCrawlerController] Crawler {crawler.Id} moved to {newPosition}");
                positionsVisited.Add(newPosition);
            }
        }

        return positionsVisited;
    }

}

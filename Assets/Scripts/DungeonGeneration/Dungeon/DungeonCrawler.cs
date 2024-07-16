using System.Collections.Generic;
using UnityEngine;


public class DungeonCrawler
{
    private int _id;
    private Vector2Int _position;

    public int Id { get => _id; }
    public Vector2Int Position { get => _position; }

    public DungeonCrawler(int id, Vector2Int startPosition)
    {
        _id = id;
        _position = startPosition;
    }

    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    {
        Direction toMove = (Direction)Random.Range(0, directionMovementMap.Count);
        _position += directionMovementMap[toMove];

        //"Jump" over the start room when moving
        if (_position == Vector2Int.zero)
        {
            _position += directionMovementMap[toMove];
        }

        return _position;
    }
}

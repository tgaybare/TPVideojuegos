using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private DungeonGenerationData _dungeonGenerationData;
    private HashSet<Vector2Int> _dungeonRooms;

    public DungeonGenerationData Data { get => _dungeonGenerationData;}

    private void Start()
    {
        _dungeonRooms = DungeonCrawlerController.GenerateDungeon(Data);

        // We manually remove the first room since it's the starting room
        _dungeonRooms.Remove(Vector2Int.zero);

        SpawnRooms(_dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> dungeonRooms)
    {
        // Load first room
        // TODO: Define first room
        RoomController.instance.LoadRoom("Room1", 0, 0);
        Debug.Log("Loaded first room");

        foreach (Vector2Int roomLocation in dungeonRooms)
        {
            int roomNumber = Random.Range(1, 4);
            Debug.Log($"Loading Room{roomNumber}");
            RoomController.instance.LoadRoom($"Room{roomNumber}", roomLocation.x, roomLocation.y);            
        }
    }
    
}
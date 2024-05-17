using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// TODO: Cambiar a patron Manager
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

        foreach (Vector2Int roomLocation in dungeonRooms)
        {
            int roomNumber = Random.Range(0, RoomController.RoomNames.Count);
            string roomName = RoomController.RoomNames[roomNumber];

            RoomController.instance.LoadRoom(roomName, roomLocation.x, roomLocation.y);            
        }
    }
    
}
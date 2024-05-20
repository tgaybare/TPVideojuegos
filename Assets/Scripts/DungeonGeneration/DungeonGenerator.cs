using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// TODO: Cambiar a patron Manager
public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private DungeonGenerationData _dungeonGenerationData;
    private LastAddedHashSet<Vector2Int> _dungeonRooms;

    public DungeonGenerationData Data { get => _dungeonGenerationData;}

    private void Start()
    {
        _dungeonRooms = DungeonCrawlerController.GenerateDungeon(Data);

        SpawnStartRoom();
        SpawnBossRoom();
        SpawnRooms();
    }

    private void SpawnRooms()
    {
        foreach (Vector2Int roomLocation in _dungeonRooms)
        {
            int roomNumber = Random.Range(0, RoomController.RoomNames.Count);
            string roomName = RoomController.RoomNames[roomNumber];

            RoomController.instance.LoadRoom(roomName, roomLocation);            
        }
    }

    private void SpawnStartRoom() {
        // Load first room
        RoomController.instance.LoadRoom(RoomController.START_ROOM_NAME, Vector2Int.zero);

        // We manually remove the first room since it's the starting room
        _dungeonRooms.Remove(Vector2Int.zero);
    }
    
    private void SpawnBossRoom() {
        // We want to spawn the boss room in the last added room
        Vector2Int furthestPosition = _dungeonRooms.LastAdded;
        RoomController.instance.LoadRoom(RoomController.BOSS_ROOM_NAME, furthestPosition);

        _dungeonRooms.Remove(furthestPosition);
    }
    
}
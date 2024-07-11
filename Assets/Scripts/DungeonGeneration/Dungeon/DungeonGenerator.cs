using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameLevels;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private DungeonGenerationData _dungeonGenerationData;
    private LastAddedHashSet<Vector2Int> _dungeonRooms;
    private bool _spawnedItemRoom = false;
    
    private Levels _currentLevel;
    private List<string> _currentLevelRooms;
    private string _startRoom;
    private string _bossRoom;

    public DungeonGenerationData Data { get => _dungeonGenerationData;}

    private void Awake()
    {
        _currentLevel = GameStateManager.instance.CurrentLevel();
        _currentLevelRooms = RoomController.RoomNamesByLevel[_currentLevel];
        _startRoom = RoomController.StartRoomNamesByLevel[_currentLevel];
        _bossRoom = RoomController.BossRoomNamesByLevel[_currentLevel];
    }

    private void Start()
    {
        _dungeonRooms = DungeonCrawlerController.GenerateDungeon(Data);

        SpawnStartRoom();
        SpawnBossRoom();
        SpawnRooms();
    }

    private void SpawnRooms()
    {
        int counter = 0;
        foreach (Vector2Int roomLocation in _dungeonRooms)
        {
            counter++;
            if(_spawnedItemRoom)
            {
                SpawnRandomRoom(roomLocation);
            } else {
                // 33% chance to spawn an item room, unless we've already spawned one
                // or we're at the last room
                if(counter == _dungeonRooms.Count || Random.Range(0, 2) == 0)
                {
                    SpawnItemRoom(roomLocation);
                    _spawnedItemRoom = true;
                } 
                else
                {
                    SpawnRandomRoom(roomLocation);
                }
            }

        }
    }

    private void SpawnRandomRoom(Vector2Int location) {
        int roomNumber = Random.Range(0, _currentLevelRooms.Count);
        string roomName = _currentLevelRooms[roomNumber];

        RoomController.instance.LoadRoom(roomName, location);
    }

    private void SpawnItemRoom(Vector2Int location)
    {
        RoomController.instance.LoadRoom(RoomController.ITEM_ROOM_NAME, location);
    }

    private void SpawnStartRoom() {
        // Load first room
        RoomController.instance.LoadRoom(_startRoom, Vector2Int.zero);

        // We manually remove the first room since it's the starting room
        _dungeonRooms.Remove(Vector2Int.zero);
    }
    
    private void SpawnBossRoom() {
        // We want to spawn the boss room in the last added room
        Vector2Int furthestPosition = _dungeonRooms.LastAdded;
        RoomController.instance.LoadRoom(_bossRoom, furthestPosition);

        _dungeonRooms.Remove(furthestPosition);
    }
    
}
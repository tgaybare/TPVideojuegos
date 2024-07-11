using Managers;
using Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameLevels;

public class RoomInfo
{   
    private string _name;
    private int _x;
    private int _z;

    public string Name { get => _name; set => _name = value;}
    public int X { get => _x; set => _x = value;}
    public int Z { get => _z; set => _z = value;}

    public RoomInfo(string name, int x, int z)
    {
        _name = name;
        _x = x;
        _z = z;
    }
}


public class RoomController : MonoBehaviour
{
    public static readonly Dictionary<Levels, List<string>> RoomNamesByLevel = new()
    {
        { Levels.LEVEL_1, new List<string> { "Room1", "Room2", "Room3" } },
        { Levels.LEVEL_2, new List<string> { "Room4", "Room5", "Room6" } }
    };
    public static readonly Dictionary<Levels, string> BossRoomNamesByLevel = new() 
    { 
        { Levels.LEVEL_1, "BossRoom" },
        { Levels.LEVEL_2, "FinalBossRoom" } 
    };
    public static readonly Dictionary<Levels, string> StartRoomNamesByLevel = new()
    {
        { Levels.LEVEL_1, "StartRoom" },
        { Levels.LEVEL_2, "StartRoom2" }
    };
    public const string ITEM_ROOM_NAME = "ItemRoom";


    private Levels _currentLevel;
    private RoomInfo _currentLoadRoomData;
    private Queue<RoomInfo> _loadRoomQueue = new();
    private bool _isLoadingRoom = false;
    private bool _finishedRoomsSetup = false;
    private bool _checkedCompletion = false;
    

    [SerializeField] private Room _currentRoom;

    [SerializeField] private List<Room> _loadedRooms = new();

    #region SINGLETON
    public static RoomController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    private void Start()
    {
        ActionManager.instance.OnEnemyKilled += OnEnemyKilled;
        ActionManager.instance.OnPlayerEnterRoom += OnPlayerEnterRoom;
        ActionManager.instance.OnPlayerExitRoom += OnPlayerExitRoom;

        _currentLevel = GameStateManager.instance.CurrentLevel();
    }

    private void Update()
    {
        // Room Generation
        UpdateRoomQueue();

        // Room Management
        if (_currentRoom != null && _currentRoom.IsCleared())
        {
            _currentRoom.OpenDoors();
        }

        // Check Level Completion
        if(!_checkedCompletion && _currentRoom is BossRoom && _currentRoom.IsCleared())
        {
            _checkedCompletion = true;
            if (GameStateManager.instance.CurrentLevel() == GameLevels.MAX_LEVEL)
            {
                ActionManager.instance.ActionGameOver(true);
            }
            else
            {
                ActionManager.instance.ActionBossDefeated();
            }
        }
    }


    public bool DoesRoomExist(int x, int z)
    {
        return FindRoom(x,z) != null;
    }

    public Room FindRoom(int x, int z)
    {
        return _loadedRooms.Find(room => room.X == x && room.Z == z);
    }

    public void LoadRoom(string name, int x, int z)
    {
        RoomInfo newRoomData = new RoomInfo(name, x, z);

        _loadRoomQueue.Enqueue(newRoomData);
    }

    public void LoadRoom(string name, Vector2Int position) {
        LoadRoom(name, position.x, position.y);
    }

    public IEnumerator LoadRoomRoutine(RoomInfo info)
    {   
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(info.Name, LoadSceneMode.Additive);

        while (!loadRoom.isDone)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {

        // No other room should exist at the same position
        if (DoesRoomExist(_currentLoadRoomData.X, _currentLoadRoomData.Z))
        {
            Debug.LogError($"Trying to register a room that already exists: {room}");
            Destroy(room.gameObject);
            _isLoadingRoom = false;
            return;
        }

        // Set the room's actual information
        room.SetPosition(_currentLoadRoomData.X * room.Width, _currentLoadRoomData.Z * room.Depth);

        room.X = _currentLoadRoomData.X;
        room.Z = _currentLoadRoomData.Z;
        room.name = $"{LevelNames[_currentLevel]} : {_currentLoadRoomData.Name} ({room.X};{room.Z})";
        room.transform.parent = transform;

        // If this is the first room, set it as the current room
        if(_currentRoom == null && _loadedRooms.Count == 0)
        {
            _currentRoom = room;
        }


        _loadedRooms.Add(room);
        _isLoadingRoom = false;
    }

    private void UpdateRoomQueue()
    {
        // If we are already loading a room, do nothing
        if (_isLoadingRoom)
        {
            return;
        }

        // If we have loaded all rooms, remove unconnected doors and spawn the boss room
        // If the queue is empty, we have loaded all rooms
        if (_loadRoomQueue.Count == 0) {
            if(!_finishedRoomsSetup)
            {
                FinishRoomSetup();
            }
            return;
        }

        // Load the next room
        _currentLoadRoomData = _loadRoomQueue.Dequeue();
        _isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(_currentLoadRoomData));
    }

    private void FinishRoomSetup()
    {
        foreach (Room room in _loadedRooms)
        {
            room.RemoveUnconnectedDoors();
            room.OpenDoors();
            // First room is always visible
            if(room != _currentRoom)
            {
                room.SetInvisible();
            }
        }
        _finishedRoomsSetup = true;
    }

    private void OnPlayerEnterRoom(Room room)
    {
        _currentRoom = room;

        UpdateCurrentRoomDoors();
        room.SetVisible();
        room.UnpauseEnemies();
    }

    private void OnPlayerExitRoom(Room room)
    {
        room.PauseEnemies();
        room.SetInvisible();
    }

    private void UpdateCurrentRoomDoors() 
    {
        if(_currentRoom.IsCleared())
        {
            _currentRoom.OpenDoors();
        }
        else
        {
            _currentRoom.CloseDoors();
        }
    }

    private void OnEnemyKilled(GameObject enemy)
    {
        _currentRoom.RemoveEnemy(enemy);
    }



   
} 
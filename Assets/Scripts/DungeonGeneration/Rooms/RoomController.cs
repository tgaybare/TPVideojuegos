using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static readonly List<String> LevelNames = new List<String> { "Level 1" };
    public static readonly List<String> RoomNames = new List<String> { "Room1", "Room2", "Room3" };
    public const string BOSS_ROOM_NAME = "BossRoom";
    public const string START_ROOM_NAME = "StartRoom";

    public static RoomController instance;

    private string _currentWorldName = LevelNames[0];
    private RoomInfo _currentLoadRoomData;
    private Queue<RoomInfo> _loadRoomQueue = new Queue<RoomInfo>();
    private bool _isLoadingRoom = false;
    private bool _finishedRoomsSetup = false;
    [SerializeField] private Room _currentRoom;

    [SerializeField] private List<Room> _loadedRooms = new List<Room>();


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

    private void Start()
    {
        ActionManager.instance.OnEnemyKilled += OnEnemyKilled;
        ActionManager.instance.OnPlayerEnterRoom += OnPlayerEnterRoom;
        ActionManager.instance.OnPlayerExitRoom += OnPlayerExitRoom;
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
        if(_currentRoom is BossRoom && _currentRoom.IsCleared())
        {
            ActionManager.instance.ActionGameOver(true);
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
        room.name = $"{_currentWorldName} : {_currentLoadRoomData.Name} ({room.X};{room.Z})";
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
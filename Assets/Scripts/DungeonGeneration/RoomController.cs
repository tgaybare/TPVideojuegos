using System;
using System.Collections;
using System.Collections.Generic;
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
    public static RoomController instance;

    [SerializeField] private List<Room> _loadedRooms = new List<Room>();

    private string _currentWorldName = "Level 1";

    private RoomInfo _currentLoadRoomData;

    private Queue<RoomInfo> _loadRoomQueue = new Queue<RoomInfo>();

    private bool _isLoadingRoom = false;

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

    private void Update()
    {
        UpdateRoomQueue();
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

        if (DoesRoomExist(_currentLoadRoomData.X, _currentLoadRoomData.Z))
        {
            Destroy(room.gameObject);
            _isLoadingRoom = false;
            return;
        }

        room.SetPosition(
            _currentLoadRoomData.X * room.Width,
            _currentLoadRoomData.Z * room.Depth
            );

        room.X = _currentLoadRoomData.X;
        room.Z = _currentLoadRoomData.Z;
        room.name = $"{_currentWorldName} : {_currentLoadRoomData.Name} ({room.X};{room.Z})";
        room.transform.parent = transform;

        room.RemoveUnconnectedDoors();
        Debug.Log($"Room loaded at: ({room.X}, {room.Z})");
        _loadedRooms.Add(room);

        _isLoadingRoom = false;
    }

    private void UpdateRoomQueue()
    {
        if (_isLoadingRoom || _loadRoomQueue.Count == 0)
        {
            return;
        }


        _currentLoadRoomData = _loadRoomQueue.Dequeue();
        _isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(_currentLoadRoomData));
    }

   
} 
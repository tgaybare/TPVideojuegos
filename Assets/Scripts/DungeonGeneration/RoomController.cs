using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{   
    public string Name;
    public int X;
    public int Y;
    
}


public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    private string _currentWorldName = "Level 1";

    private RoomInfo _currentLoadRoomData;

    private Queue<RoomInfo> _loadRoomQueue = new Queue<RoomInfo>();

    private List<Room> _loadedRooms = new List<Room>();

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

    public bool DoesRoomExist(int x, int y)
    {
        return _loadedRooms.Find(room => room.X == x && room.Y == y) != null;
    }
}
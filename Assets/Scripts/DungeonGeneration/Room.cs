using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private int _depth;
    [SerializeField] private int _x;
    [SerializeField] private int _y;
    [SerializeField] private int _z;

    private Doorway _topRightDoorway;
    private Doorway _topLeftDoorway;
    private Doorway _bottomRightDoorway;
    private Doorway _bottomLeftDoorway;
    private List<Doorway> _doorways = new List<Doorway>();

    private int _enemyCount = 3; // TODO: Update this variable when an enemy is spawned or defeated
    public int EnemyCount { get => _enemyCount; set => _enemyCount = value; }

    public int Width => _width;
    public int Height => _height;
    public int Depth => _depth;
    public int X { get => _x; set => _x = value; }
    public int Y => _y;
    public int Z { get => _z; set => _z = value; }

    protected virtual void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.LogError("RoomController instance does not exist.");
            return;
        }

        // Get all doors in the room. Should be 4.
        Doorway[] doorways = GetComponentsInChildren<Doorway>();
        foreach (Doorway d in doorways)
        {
            AssignDoorToLocalVariable(d);
            _doorways.Add(d);
        }


        RoomController.instance.RegisterRoom(this);
    }

   

  
    public Vector3 GetRoomCenter()
    {
        return new Vector3(_x * _width, _y * _height, _z * _depth);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        Gizmos.DrawWireCube(position, new Vector3(_width, _height, _depth));
    }

    public void SetPosition(int x, int z)
    {
        _x = x;
        _z = z;

        transform.position = new Vector3(_x, _y, _z);
    }

    private void AssignDoorToLocalVariable(Doorway d)
    {
        switch (d.Type)
        {
            case Doorway.DoorwayType.topRight:
                _topRightDoorway = d;
                break;
            case Doorway.DoorwayType.topLeft:
                _topLeftDoorway = d;
                break;
            case Doorway.DoorwayType.bottomRight:
                _bottomRightDoorway = d;
                break;
            case Doorway.DoorwayType.bottomLeft:
                _bottomLeftDoorway = d;
                break;
        }
    }

    // If exists, returns the adjacent room at (x + deltaX, z + deltaZ)
    private Room GetAdjacentRoomAt(int deltaX, int DeltaZ) {
        if(RoomController.instance.DoesRoomExist(_x + deltaX, _z + DeltaZ))
        {
            return RoomController.instance.FindRoom(_x + deltaX, _z + DeltaZ);
        }
        return null;
    }

    private Room GetTopRightRoom()
    {
        return GetAdjacentRoomAt(0, 1);
    }

    private Room GetTopLeftRoom()
    {
        return GetAdjacentRoomAt(-1, 0);
    }

    private Room GetBottomRightRoom()
    {
        return GetAdjacentRoomAt(1, 0);
    }

    private Room GetBottomLeftRoom()
    {
        return GetAdjacentRoomAt(0, -1);
    }

    public void RemoveUnconnectedDoors() {
        if(GetTopRightRoom() == null)
        {
            _topRightDoorway.gameObject.SetActive(false);
            _topRightDoorway.ReplacementWall.SetActive(true);
        }

        if(GetTopLeftRoom() == null)
        {
            _topLeftDoorway.gameObject.SetActive(false);
            _topLeftDoorway.ReplacementWall.SetActive(true);
        }

        if (GetBottomRightRoom() == null)
        {
            _bottomRightDoorway.gameObject.SetActive(false);
            _bottomRightDoorway.ReplacementWall.SetActive(true);
        }

        if(GetBottomLeftRoom() == null)
        {
            _bottomLeftDoorway.gameObject.SetActive(false);
            _bottomLeftDoorway.ReplacementWall.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }

    public override string ToString()
    {
        return $"Room '{name}' at ({_x}, {_z})";
    }

    public void OpenDoors() 
    {
        foreach (Doorway d in _doorways)
        {
            d.OpenDoor();
        }
    }

    // TODO
    public void CloseDoors() 
    {
        foreach (Doorway d in _doorways)
        {
            d.CloseDoor();
        }
    }

    // TODO
    public void PauseEnemies() {
        /*Enemy[] enemies = GetComponentsInChildren<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            enemy.Pause();
            enemy.gameObject.SetActive(false);
        }*/
    }

    // TODO
    public void UnpauseEnemies()
    {
        /*Enemy[] enemies = GetComponentsInChildren<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            enemy.Unpause();
            enemy.gameObject.SetActive(false);
        }*/
    }

    public bool IsCleared()
    {
        return _enemyCount == 0;
    }
}
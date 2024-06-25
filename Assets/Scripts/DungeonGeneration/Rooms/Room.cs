using Assets.Scripts.DungeonGeneration;
using Managers;
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
    private List<GameObject> _roomLights = new List<GameObject>();

    //[SerializeField] private RoomGameObjectsSpawner _roomObjectSpawner;
    [SerializeField] private RoomEnemiesSpawner _roomEnemiesSpawner;
    public int EnemyCount { get => _roomEnemiesSpawner.EnemiesInRoom.Count; }

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

        _roomEnemiesSpawner = GetComponent<RoomEnemiesSpawner>();
        if(_roomEnemiesSpawner == null)
        {
            Debug.LogError("RoomEnemiesSpawner is not set.");
            return;
        }

        foreach (Light light in GetComponentsInChildren<Light>())
        {
            _roomLights.Add(light.gameObject);
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ActionManager.instance.ActionPlayerEnterRoom(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActionManager.instance.ActionPlayerExitRoom(this);
        }
    }

    public void SetVisible() {
        SetChildrenVisibility(true);
    }

    public void SetInvisible()
    {
        SetChildrenVisibility(false);
    }

    // TODO: Lights?
    private void SetChildrenVisibility(bool status)
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = status;
        }

        foreach (GameObject light in _roomLights)
        {
            light.SetActive(status);
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

    public void CloseDoors() 
    {
        foreach (Doorway d in _doorways)
        {
            d.CloseDoor();
        }
    }

    public void PauseEnemies() {
        if (_roomEnemiesSpawner == null)
        {
            return;
        }

        List<GameObject> enemies = _roomEnemiesSpawner.EnemiesInRoom;

        foreach (GameObject enemy in enemies)
        {
            //TODO: enemy.Pause();  <-- Implement this method in the Enemy class
            if(enemy != null)
            {
                enemy.SetActive(false);
            }
        }
    }

    public void UnpauseEnemies()
    {
        if(_roomEnemiesSpawner == null)
        {
            return;
        }

        List<GameObject> enemies = _roomEnemiesSpawner.EnemiesInRoom;

        foreach (GameObject enemy in enemies)
        {
            //TODO: enemy.Unpause();  <-- Implement this method in the Enemy class
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }
    }

    public bool IsCleared()
    {
        return EnemyCount == 0;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        _roomEnemiesSpawner.RemoveEnemy(enemy);
    }
}
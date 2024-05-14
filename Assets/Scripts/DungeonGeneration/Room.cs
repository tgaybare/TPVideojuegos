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

    private Door _topRightDoor;
    private Door _topLeftDoor;
    private Door _bottomRightDoor;
    private Door _bottomLeftDoor;
    private List<Door> _doors = new List<Door>();


    public int Width => _width;
    public int Height => _height;
    public int Depth => _depth;
    public int X { get => _x; set => _x = value; }
    public int Y => _y;
    public int Z { get => _z; set => _z = value; }

    private void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.LogError("RoomController instance does not exist.");
            return;
        }

        // Get all doors in the room. Should be 4.
        Door[] doors = GetComponentsInChildren<Door>();
        foreach (Door door in doors)
        {
            AssignDoorToLocalVariable(door);
            _doors.Add(door);
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

    private void AssignDoorToLocalVariable(Door door)
    {
        switch (door.Type)
        {
            case Door.DoorType.topRight:
                _topRightDoor = door;
                break;
            case Door.DoorType.topLeft:
                _topLeftDoor = door;
                break;
            case Door.DoorType.bottomRight:
                _bottomRightDoor = door;
                break;
            case Door.DoorType.bottomLeft:
                _bottomLeftDoor = door;
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
            _topRightDoor.gameObject.SetActive(false);
            _topRightDoor.ReplacementWall.SetActive(true);
        }

        if(GetTopLeftRoom() == null)
        {
            _topLeftDoor.gameObject.SetActive(false);
            _topLeftDoor.ReplacementWall.SetActive(true);
        }

        if (GetBottomRightRoom() == null)
        {
            _bottomRightDoor.gameObject.SetActive(false);
            _bottomRightDoor.ReplacementWall.SetActive(true);
        }

        if(GetBottomLeftRoom() == null)
        {
            _bottomLeftDoor.gameObject.SetActive(false);
            _bottomLeftDoor.ReplacementWall.SetActive(true);
        }
    }
}
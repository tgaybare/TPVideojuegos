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

        RoomController.instance.RegisterRoom(this);
    }

  
    public Vector3 GetRoomCenter()
    {
        return new Vector3(_x * _width, _y * _height, _z * _depth);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), new Vector3(_width, _height, _depth));
    }

    public void SetPosition(int x, int z)
    {
        _x = x;
        _z = z;

        transform.position = new Vector3(_x, _y, _z);
    }
}
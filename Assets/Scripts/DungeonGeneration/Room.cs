using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private int _depth;
    [SerializeField] private int _x;
    [SerializeField] private int _y;
    [SerializeField] private int _z;

    public int Width => _width;
    public int Height => _height;
    public int Depth => _depth;
    public int X => _x;
    public int Y => _y;
    public int Z => _z;

    private void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.LogError("RoomController instance does not exist.");
            return;
        }
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(_x * _width, _y * _height, _z * _depth);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Room is rotated 45 degrees
        Quaternion rotation = Quaternion.Euler(0, 45, 0);
        Gizmos.matrix = Matrix4x4.TRS(transform.position, rotation, Vector3.one);
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), new Vector3(_width, _height, _depth));
    }
}
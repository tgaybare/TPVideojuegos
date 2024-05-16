using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Flyweight pattern?
public class GridController : MonoBehaviour
{
    [System.Serializable]
    public struct Grid {
        public int columns, rows;
        public float depthOffset, horizontalOffset;
    }

    [SerializeField] public Grid _grid;
    [SerializeField] public Room _room;
    [SerializeField] public GameObject _gridTile;
    [SerializeField] public int _widthAndDepthOffset;

    private List<Vector3> _availablePoints = new List<Vector3>();

    private void Awake()
    {
        Debug.Log("GridController Start");
        _room = GetComponentInParent<Room>();
        Debug.Log("Room: " + _room);
        _grid.columns = (_room.Width - _widthAndDepthOffset)/4;
        _grid.rows = (_room.Depth - _widthAndDepthOffset)/4;

        GenerateGrid();
    }

    public void GenerateGrid() 
    {
        _grid.depthOffset += _room.transform.localPosition.z;
        _grid.horizontalOffset += _room.transform.localPosition.x;

        for(int z = 0; z < _grid.rows; z++)
        {
            for(int x = 0; x < _grid.columns; x++)
            {
                GameObject tile = Instantiate(_gridTile, transform);

                float actualX = x - (_grid.columns - _grid.horizontalOffset/4);
                float actualZ = z - (_grid.rows - _grid.depthOffset/4);

                tile.transform.position = new Vector3(actualX * 4, 0.05f, actualZ * 4);
                tile.name = $"Tile ({x}, {z})";

                _availablePoints.Add(tile.transform.position);
            }
        }
    }

}

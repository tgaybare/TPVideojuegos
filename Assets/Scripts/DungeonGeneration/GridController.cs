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

    [SerializeField] private Grid _grid;
    [SerializeField] private Room _room;
    [SerializeField] private GameObject _gridTile;
    [SerializeField] private int _widthAndDepthOffset;

    private List<Vector3> _availablePoints = new List<Vector3>();
    public List<Vector3> AvailablePoints => _availablePoints;

    private void Awake()
    {
        _room = GetComponentInParent<Room>();
        _grid.columns = (_room.Width - _widthAndDepthOffset)/4;
        _grid.rows = (_room.Depth - _widthAndDepthOffset)/4;

        GenerateGrid();
    }

    private void GenerateGrid() 
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
                tile.SetActive(false);
            }
        }
    }

    //TODO: Capaz es mejor un Set
    public Vector3 AllocateRandomTile()
    {
        int randomPosition = Random.Range(0, _availablePoints.Count - 1);
        Vector3 toReturn = _availablePoints[randomPosition];

        //Debug.Log($"Allocating tile {toReturn}");

        _availablePoints.RemoveAt(randomPosition);

        return toReturn;
    }

}

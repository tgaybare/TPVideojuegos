using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    public enum DoorwayType
    {
        topRight, topLeft, bottomRight, bottomLeft
    }

    [SerializeField] private GameObject _replacementWall;
    public GameObject ReplacementWall { get => _replacementWall; }

    [SerializeField] private DoorwayType _doorwayType;
    public DoorwayType Type { get => _doorwayType; }

    [SerializeField] private GameObject _door;

    private void Start()
    {
        if (_door == null)
        {
            _door = transform.Find("Door_Wooden_Round_Left").gameObject;
        }
    }


    public void OpenDoor()
    {
        _door.transform.localRotation = Quaternion.Euler(0, 90, 0);
    }

    public void CloseDoor()
    {
        _door.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}

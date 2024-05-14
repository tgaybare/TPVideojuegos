using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        topRight, topLeft, bottomRight, bottomLeft
    }

    [SerializeField] private GameObject _replacementWall;
    public GameObject ReplacementWall { get => _replacementWall; }

    [SerializeField] private DoorType _doorType;
    public DoorType Type { get => _doorType; }
}

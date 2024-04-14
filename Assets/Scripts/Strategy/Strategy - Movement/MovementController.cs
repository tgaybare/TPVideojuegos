using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class MovementController : MonoBehaviour, IMoveable
{
    #region IMOVEABLE_PROPERTIES
    public float Speed => _speed;
    private float _speed = 10;
    #endregion

    #region IMOVEABLE_METHODS
    public void Move(Vector3 direction)
    {
        transform.position +=  Time.deltaTime * Speed * direction;
    }
    #endregion
}
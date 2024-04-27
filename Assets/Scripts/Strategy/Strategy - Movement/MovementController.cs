using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

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

    // Rotates towards the direction
    public void RotateTowards(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.LookAt(new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z));
        }
    }
    #endregion
}
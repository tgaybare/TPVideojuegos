using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Strategy.Strategy___Movement;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class MovementController : MonoBehaviour, IMoveable
{
    
    #region IMOVEABLE_PROPERTIES

    public ActorStats Stats => stats;
    [SerializeField] private ActorStats stats;
    
    public float Speed => stats.Speed;
    
    #endregion
    

    #region IMOVEABLE_METHODS
    public void Move(Vector3 direction)
    {
        Debug.Log(direction);
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

    public void Dodge(int duration)
    {
        transform.position += 25 * Time.deltaTime * Speed * transform.forward;
        StartCoroutine(SetInvulnerable(duration));

    }

    private IEnumerator SetInvulnerable(int duration)
    {
        int layer = gameObject.layer;
        gameObject.layer = 2;
        yield return new WaitForSeconds(duration / 1000); //ms to s
        Debug.Log("FUI INVULNERABLE Y YA ME LEVANTE");
        gameObject.layer = layer;
    }

    #endregion
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Strategy.Strategy___Movement;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class MovementController : MonoBehaviour, IMoveable
{

    private Vector3 _lastDirection;
    private bool _isDodging;
    
    #region IMOVEABLE_PROPERTIES

    public ActorStats Stats => _stats;
    [SerializeField] private ActorStats _stats;

    private Rigidbody _rigidbody;
    
    public float Speed => _stats.Speed;

    #endregion

    private void Start()
    {
        _rigidbody = transform.GetComponent<Rigidbody>();
        _lastDirection = Vector3.zero;
        _isDodging = false;
    }

    #region IMOVEABLE_METHODS

    public void Move(Vector3 direction)
    {
        if (!_isDodging){
            _rigidbody.MovePosition(_rigidbody.position + Time.deltaTime * Speed * direction);
            _lastDirection = direction;
            //transform.position +=  Time.deltaTime * Speed * direction;
        }
    }

    // Rotates towards the direction
    public void RotateTowards(Ray ray)
    {
        if (!_isDodging)
        {
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                transform.LookAt(new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z));
            }
        }

        
    }

    public void Dodge(int duration)
    {
        _isDodging = true;
        gameObject.GetComponent<Collider>().enabled = false;
        if (_lastDirection == Vector3.zero)
        {
            _lastDirection = transform.forward;
        }
        transform.position += _lastDirection + Vector3.up;
        // transform.rotation = Quaternion.Euler(180, 0, 0);
        StartCoroutine(SetInvulnerable(duration));

    }

    private IEnumerator SetInvulnerable(int duration)
    {
        yield return new WaitForSeconds(duration/1000f); //ms to s
        transform.position += _lastDirection + Vector3.down;
        transform.rotation = Quaternion.Euler(-180, 0, 0);
        gameObject.GetComponent<Collider>().enabled = true;
        _isDodging = false;
    }

    #endregion
}
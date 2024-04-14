using System.Collections.Generic;
using Commands;
using UnityEngine;

public class EventQueueManager : MonoBehaviour
{
    public static EventQueueManager instance;
    
    private Queue<ICommand> _eventQueue = new Queue<ICommand>();
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    private void Update()
    {
        if (_eventQueue.Count > 0)
        {
            ICommand command = _eventQueue.Dequeue();
            command.Do();
        }
    }

    public void AddEventToQueue(ICommand command)
    {
        instance._eventQueue.Enqueue(command);
    }
}

using Managers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DungeonGeneration.Rooms
{
    public class ItemRoom : Room
    {
        public bool AlreadyVisited => _alreadyVisited;
        private bool _alreadyVisited = false;


        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.CompareTag("Player"))
            {
                ActionManager.instance.ActionPlayerEnterItemRoom(_alreadyVisited);
                _alreadyVisited = true;
            }
        }
    }
}
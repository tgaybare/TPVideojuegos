using Managers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DungeonGeneration.Rooms
{
    public class BossRoomLadder : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ActionManager.instance.ActionLevelComplete();
            }
        }
    }
}
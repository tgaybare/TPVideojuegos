using Managers;
using UnityEngine;

public class BossRoom : Room
{

    [SerializeField] private GameObject _jailDoor;
    private bool bossDefeated = false;

    protected override void Start()
    {
        base.Start();

        if(this.EnemyCount == 0)
        {
            RoomEnemiesSpawner.InitializeObjectSpawning();
        }

        // Quick fix for the spider boss
        if (this.EnemyCount == 1)
        {
            GameObject spider = RoomEnemiesSpawner.EnemiesInRoom[0].gameObject;
            spider.transform.position = GetRoomCenter();
        }

        ActionManager.instance.OnBossDefeated += OnBossDefeated;
    }

    private void OnBossDefeated()
    {
        bossDefeated = true;
        OpenJailDoor();
    }

    private void OpenJailDoor()
    {
        _jailDoor.transform.rotation = Quaternion.Euler(0, 260, 0);
    }

    
}
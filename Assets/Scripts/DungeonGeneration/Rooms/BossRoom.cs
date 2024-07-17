using Managers;
using UnityEngine;

public class BossRoom : Room
{

    [SerializeField] private GameObject _jailDoor;
    private bool bossDefeated = false;
    private bool bossMoved = false;

    protected override void Start()
    {
        base.Start();

        Debug.Log($"Enemy Count: {EnemyCount}");

        /*if(this.EnemyCount == 0)
        {
            RoomEnemiesSpawner.InitializeObjectSpawning();
        }*/

        // Quick fix for the spider boss
        if (this.EnemyCount == 1)
        {
            GameObject boss = RoomEnemiesSpawner.EnemiesInRoom[0].gameObject;
            boss.transform.position = GetRoomCenter();
        }

        ActionManager.instance.OnBossDefeated += OnBossDefeated;
    }

    private void Update()
    {
        if (!bossMoved && this.EnemyCount == 1)
        {
            GameObject boss = RoomEnemiesSpawner.EnemiesInRoom[0].gameObject;
            boss.transform.position = GetRoomCenter();
            bossMoved = true;
        }
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
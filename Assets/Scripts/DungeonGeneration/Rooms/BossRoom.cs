using Managers;
using UnityEngine;

public class BossRoom : Room
{

    [SerializeField] private GameObject _jailDoor;

    protected override void Start()
    {
        base.Start();

        ActionManager.instance.OnBossDefeated += OpenJailDoor;
    }

    private void OpenJailDoor()
    {
        _jailDoor.transform.rotation = Quaternion.Euler(0, 260, 0);
    }
}

public class BossRoom : Room {

    // Since the boss room spawns last, we must remove unconnected doors
    protected override void Start()
    {
        base.Start();
        RemoveUnconnectedDoors();
        OpenDoors();
    }
}
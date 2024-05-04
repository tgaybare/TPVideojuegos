using UnityEngine;

namespace Weapons
{
    public class MagicStaff : DistanceWeapon
    {
        
        public override void Attack()
        {
            Vector3 staffBarrelPosition = GameObject.FindWithTag("MagicStaff").GetComponent<Collider>().transform.position;
            Vector3 position = staffBarrelPosition;
            Instantiate(
                ProjectilePrefab, 
                position, 
                transform.rotation);
        }
        
    }
}
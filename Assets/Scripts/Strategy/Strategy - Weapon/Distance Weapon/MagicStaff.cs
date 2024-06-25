using Animations;
using TMPro;
using UnityEngine;


    public class MagicStaff : DistanceWeapon
    {
        
        private PlagueDoctorAnimController _animator;
        
        private void Start()
        {
            _animator = GetComponent<PlagueDoctorAnimController>();
        }
        
        public override void Attack()
        {
            _animator.StartAttack();

        }

        protected void generateProjectile()
        {
            Vector3 staffBarrelPosition = GameObject.FindWithTag("MagicStaff").GetComponent<Collider>().bounds.center;
            GameObject player = GameObject.FindWithTag("Player");
            Vector3 playerPosition = player.transform.position + new Vector3(0, 1, 0);
            Vector3 position = staffBarrelPosition;
            
            // Calculate direction from spawn point to target position
            Vector3 direction = playerPosition - staffBarrelPosition;

            Quaternion rotation = Quaternion.LookRotation(direction);
            
            Instantiate(
                ProjectilePrefab, 
                position, 
                rotation);
            
            _animator.FinishAttack();
        }

    }

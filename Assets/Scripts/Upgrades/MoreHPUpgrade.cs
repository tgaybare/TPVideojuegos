using Strategy.Strategy___Weapon;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class MoreHPUpgrade : MonoBehaviour, IUpgrade
    {
        public static MoreHPUpgrade instance;

        [SerializeField] private float _extraHealthMultiplier = 1.5f;
        [SerializeField] private LifeController _playerLifeController;

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

        public void Start()
        {
            _playerLifeController = GameObject.Find("MainCharacter").GetComponent<LifeController>();
            if(_playerLifeController == null)
            {
                Debug.LogError("Player LifeController not found");
            }
        }

        public void applyUpgrade()
        {
            _playerLifeController.incrementMaxLife(_extraHealthMultiplier);
        }

        

    }
}
using UnityEngine;

public abstract class DistanceWeapon : MonoBehaviour, IDistanceWeapon
{
    #region DISTANCE_WEAPON_PROPERTIES
    private int _damage = 10;
    private int _maxProjectileCount = 10;
    [SerializeField] private int _currentProjectileCount;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private GameObject _explosiveProjectilePrefab;

    public int ProjectilesPerAttack { get; set; } = 1;

    public bool ExplosiveShot { get; set; } = false;
    public float ProjectileDelay { get; } = 0.2f;
    #endregion

    #region I_DISTANCE_WEAPON_PROPERTIES
    public int Damage => _damage;
    public int MaxProjectileCount => _maxProjectileCount;
    public GameObject ProjectilePrefab => _projectilePrefab;
    public GameObject ExplosiveProjectilePrefab => _explosiveProjectilePrefab;
    #endregion

    #region I_DISTANCE_WEAPON_PROPERTIES
    public virtual void Attack() => Instantiate(
                                        _projectilePrefab,
                                        transform.position,
                                        transform.rotation);

    public virtual void Reload() => _currentProjectileCount = _maxProjectileCount;
    #endregion
}

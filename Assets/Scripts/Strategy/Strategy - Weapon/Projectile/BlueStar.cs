using Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlueStar : MonoBehaviour, IProjectile
{

    [SerializeField] private int _damage = 20;
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _lifetime = 5;
    [SerializeField] private List<int> _layerMasks;

    private FixedSoundPlayer _soundPlayer;

    public int Damage { get => _damage; set => _damage = value; }
    public float Speed => _speed;
    public float LifeTime => _lifetime;

    private Vector3 _direction;

    private void Awake()
    {
        transform.position += transform.forward * 4f;
        _soundPlayer = gameObject.GetComponent<FixedSoundPlayer>();
    }

    private void Update()
    {
        Travel();

        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0) Destroy(this.gameObject);
    }

    public void Travel()
    {
        transform.position += transform.forward * Time.deltaTime * Speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (_layerMasks.Contains(other.gameObject.layer))
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(Damage);
            // ActionManager.instance.BoltHit();

            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (_layerMasks.Contains(collision.gameObject.layer))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(Damage);
            _soundPlayer.Play();
            StartCoroutine(WaitForSound());
        }
    }


    IEnumerator WaitForSound()
    {
        //Disables all the components of the object
        foreach (ParticleSystem ps in gameObject.GetComponentsInChildren<ParticleSystem>())
        {
            ps.Stop();
            ps.Clear();
        }
        gameObject.GetComponent<Collider>().enabled = false;
        while (_soundPlayer.IsPlaying())
        {
            yield return null;
        }

        Destroy(gameObject);
    }

}

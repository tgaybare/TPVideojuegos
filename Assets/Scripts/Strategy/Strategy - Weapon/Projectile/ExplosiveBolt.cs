
using System.Collections;
using Sound;
using UnityEngine;

public class ExplosiveBolt : Bolt
{
    
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private int explosionDamage = 30;
    [SerializeField] private int explosionForce = 1000;
    
    [SerializeField] private GameObject explosionPrefab;
    
    private VariableSoundPlayer _variableSoundPlayer;
    
    protected void Awake()
    {
        base.Awake();
        _variableSoundPlayer = gameObject.GetComponent<VariableSoundPlayer>();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (_layerMasks.Contains(collision.gameObject.layer))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(Damage);
            _soundPlayer.Play();
            StartCoroutine(ExplodeAndWaitForSound());
        }
    }
    
    IEnumerator ExplodeAndWaitForSound()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        
        //1. Instantiate de explosion
         GameObject explosionGameObject = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        _variableSoundPlayer.PlayOneShot(explosionSound);
        
        //2.1 Get all the colliders in the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        
        //2.2 Damage all the damageables in the explosion radius
        foreach (var col in colliders)
        {
            IDamageable damageable = col.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(explosionDamage);
            
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
        
        while (_soundPlayer.IsPlaying() || _variableSoundPlayer.IsPlaying())
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        Destroy(explosionGameObject);
        Destroy(this.gameObject);
    }
}

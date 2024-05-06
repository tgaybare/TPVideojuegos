using System;
using Managers;
using UnityEngine;

namespace Sound
{
    public class SoundFxController : MonoBehaviour, IVariableSoundPlayer
    {
        private AudioSource _audioSource;
        
        public AudioSource AudioSource => _audioSource;
        
        //Sounds
        [SerializeField] private AudioClip boltHit;
        [SerializeField] private AudioClip crossbowShot;
        [SerializeField] private AudioClip swordSlash;


        private void Start()
        {
            InitAudioSource();
            
            
            ActionManager.instance.OnBoltHit += OnBoltHit;
            ActionManager.instance.OnCrossbowShot += OnCrossbowShot;
            
            ActionManager.instance.OnSwordSlash += OnSwordSlash;
        }
        
        private void OnBoltHit()
        {
            PlayOneShot(boltHit);
            Debug.Log("se llamo a bolt hit");
        }
        
        private void OnCrossbowShot()
        {
            PlayOneShot(crossbowShot);
        }
        
        private void OnSwordSlash()
        {
            PlayOneShot(swordSlash);
        }

        public void InitAudioSource()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        

        public void PlayOneShot(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }
}
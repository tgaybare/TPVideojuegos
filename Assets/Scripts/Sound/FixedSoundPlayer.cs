using UnityEngine;
using UnityEngine.UIElements;

namespace Sound
{
    public class FixedSoundPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;

        [SerializeField] private AudioClip audioClip;

        void Start()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();   
            _audioSource.clip = audioClip;
            _audioSource.maxDistance = 5000000000000;
        }

        public void Play()
        {
            _audioSource.Play();
        }
        
        public void Stop()
        {
            _audioSource.Stop();
        }

        public bool IsPlaying()
        {
            return _audioSource.isPlaying;
        }
    }
}
using UnityEngine;

namespace Sound
{
    public class VariableSoundPlayer : MonoBehaviour
    {
        private AudioSource AudioSource { get; set; }

        void Start()
        {
            AudioSource = gameObject.GetComponent<AudioSource>();
        }

        public void PlayOneShot(AudioClip clip)
        {
            AudioSource.PlayOneShot(clip);
        }

        public bool IsPlaying()
        {
            return AudioSource.isPlaying;
        }
    }
}
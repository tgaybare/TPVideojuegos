using UnityEngine;

namespace Sound
{
    public interface IVariableSoundPlayer
    {
        
        AudioSource AudioSource { get; }
        
        void InitAudioSource();
        
        void PlayOneShot(AudioClip clip);
    }
}
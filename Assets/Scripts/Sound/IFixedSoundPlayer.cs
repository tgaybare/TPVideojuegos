using UnityEngine;

namespace Sound
{
    public interface IFixedSoundPlayer
    {
        AudioClip AudioClip { get; }
        
        AudioSource AudioSource { get; }
        
        void InitAudioSource();
        
        void Play();
        void Stop();
    }
}
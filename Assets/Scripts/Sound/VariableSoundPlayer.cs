﻿using UnityEngine;

namespace Sound
{
    public class VariableSoundPlayer: MonoBehaviour
    {
        private AudioSource AudioSource { get; set; }

        void Start()
        {
            AudioSource = gameObject.GetComponent<AudioSource>();   
        }

        public void PlayOneShot(AudioClip clip)
        {
            PlayOneShot(clip);
        }
    }
}
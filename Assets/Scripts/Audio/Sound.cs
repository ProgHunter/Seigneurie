using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [Serializable]
    public class Sound
    {
        public string Nom;
        public AudioClip Clip;
        public bool JouerSurAwake;
        public bool Loop;
        [Range(0f,1f)]
        public float Volume = 1;
        [Range(0f,1f)]
        public float Vitesse = 1;

        public AudioMixerGroup AudioMixerGroupe;

        [HideInInspector] public AudioSource Source;
    }
}
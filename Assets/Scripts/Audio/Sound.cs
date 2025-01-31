using System;
using UnityEngine;

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
        public float Volume;
        [Range(0f,1f)]
        public float Hauteur;

        [HideInInspector] public AudioSource Source;
    }
}
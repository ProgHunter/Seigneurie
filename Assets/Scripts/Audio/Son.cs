using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [Serializable]
    public class Son
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

        public void Initialiser(AudioSource source)
        {
            Source = source;
            Source.clip = Clip;
            Source.volume = Volume;
            Source.pitch = Vitesse;
            Source.outputAudioMixerGroup = AudioMixerGroupe;
            Source.playOnAwake = JouerSurAwake;
            Source.loop = Loop;
        }
    }
}
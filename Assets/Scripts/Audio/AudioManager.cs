using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private readonly Dictionary<string, Sound> _sonsDict = new();
        [SerializeField] private Sound[] _sons;

        public static AudioManager Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else 
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
            
            foreach (var son in _sons)
            {
                son.Source = gameObject.AddComponent<AudioSource>();
                son.Source.clip = son.Clip;

                son.Source.volume = son.Volume;
                son.Source.pitch = son.Hauteur;
                son.Source.playOnAwake = son.JouerSurAwake;
                son.Source.loop = son.Loop;
                
                _sonsDict.Add(son.Nom, son);
            }
        }

        public void Play(string nom)
        {
            var son = _sonsDict[nom];
            if (son == null)
            {
                Debug.LogWarning($"Avertissement, son {nom} pas trouvé");
                return;
            }
            son.Source.Play();
        }
    }
}
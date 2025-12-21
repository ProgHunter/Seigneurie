using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private readonly Dictionary<string, Son> _sonsDict = new();
        [SerializeField] private Son[] _sons;

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
                var audioSource = gameObject.AddComponent<AudioSource>();
                son.Initialiser(audioSource);
                
                _sonsDict.Add(son.Nom, son);
            }
        }

        public void Start()
        {
            //Play background music
            Play("Musique principale");
        }

        public void Play(string nom)
        {
            var son = _sonsDict[nom];
            if (son == null)
            {
                Debug.LogWarning($"Avertissement, son {nom} pas trouvé");
                return;
            }
            son.Jouer();
        }
    }
}
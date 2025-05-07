using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Audio
{
    public class BarreDeVolume : MonoBehaviour
    {
        [SerializeField] private Slider _glisseurVolumeMaitre;

        [SerializeField] private AudioMixer _mixer;
        private const string _volumeMaitre = "masterVolume";
        private const string _mixerMaitrevolume = "MasterVolume";

        // Start is called before the first frame update
        private void Start()
        {
            _glisseurVolumeMaitre.onValueChanged.AddListener(OnMasterVolumeChanged);

            if (PlayerPrefs.HasKey(_volumeMaitre))
            {
                LoadVolume();
            }
            else
            {
                OnMasterVolumeChanged(_glisseurVolumeMaitre.value); 
            }
        }

        private void OnMasterVolumeChanged(float volume)
        {
            _mixer.SetFloat(_mixerMaitrevolume, Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat(_volumeMaitre, volume);
        }

        private void LoadVolume()
        {
            _glisseurVolumeMaitre.value = PlayerPrefs.GetFloat(_volumeMaitre);
        }
    }
}

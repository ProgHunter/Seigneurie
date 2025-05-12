using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Audio
{
    public class BarreDeVolume : MonoBehaviour
    {
        [SerializeField] private Slider _barreDeGlissementMaitre;

        [SerializeField] private AudioMixer _mixer;
        private const string _volumeMaitre = "masterVolume";
        private const string _mixerMaitrevolume = "MasterVolume";

        // Start is called before the first frame update
        private void Start()
        {
            _barreDeGlissementMaitre.onValueChanged.AddListener(OnMasterVolumeChanged);

            if (PlayerPrefs.HasKey(_volumeMaitre))
            {
                ChargerParamètreVolume();
            }
            else
            {
                OnMasterVolumeChanged(_barreDeGlissementMaitre.value); 
            }
        }

        private void OnMasterVolumeChanged(float volume)
        {
            _mixer.SetFloat(_mixerMaitrevolume, Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat(_volumeMaitre, volume);
        }

        private void ChargerParamètreVolume()
        {
            _barreDeGlissementMaitre.value = PlayerPrefs.GetFloat(_volumeMaitre);
        }
    }
}

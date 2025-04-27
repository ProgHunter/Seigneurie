using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Audio
{
    public class VolumeSettings : MonoBehaviour
    {
        [SerializeField] private Slider _masterVolumeSlider;

        [SerializeField] private AudioMixer _mixer;
        private const string _mastervolume = "masterVolume";
        private const string _mixerMastervolume = "MasterVolume";

        // Start is called before the first frame update
        private void Start()
        {
            _masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);

            if (PlayerPrefs.HasKey(_mastervolume))
            {
                LoadVolume();
            }
            else
            {
                OnMasterVolumeChanged(_masterVolumeSlider.value); 
            }
        }

        private void OnMasterVolumeChanged(float volume)
        {
            _mixer.SetFloat(_mixerMastervolume, Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat(_mastervolume, volume);
        }

        private void LoadVolume()
        {
            _masterVolumeSlider.value = PlayerPrefs.GetFloat(_mastervolume);
        }
    }
}

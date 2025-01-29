using UnityEngine;
namespace UI.Components
{
    public class CustomToggleGroup : MonoBehaviour
    {
        private CustomToggle _toggleActif;
        [SerializeField] private CustomToggle[] _toggles;

        public void Awake()
        {
            if (_toggles.Length == 0)
            {
                Debug.LogWarning($"The custom toggle {gameObject.name} has no toggles");
                return;
            }

            foreach (var toggle in _toggles)
            {
                toggle.QuandAppuyé = SélectionnerUnOnglet;
            }
        }

        private void SélectionnerUnOnglet(CustomToggle customToggle)
        {
            _toggleActif = customToggle;
            foreach (var toggle in _toggles)
            {
                if(toggle != _toggleActif)
                    toggle.Déselectionner();
            }
        }
    }
}
using System;
using Profession;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Gère les pourcentages de population assignés à chaque profession
    /// </summary>
    public class AssignationProfessionsUI : MonoBehaviour, IDisposable
    {
        private ProfessionEnum _profession;
        [SerializeField] private Button _boutonMoins;
        [SerializeField] private Button _boutonPlus;

        [SerializeField] private TextMeshProUGUI _titre;
        [SerializeField] private TextMeshProUGUI _pourcentage;

        [SerializeField] private Slider _slider;
        private Action _pourcentageEstModifie;

        private int _pourcentageActuel;
        private const int _pcMax = 100;
        private const int _pcMin = 0;

        public int AccesPourcentageActuel()
        {
            return _pourcentageActuel;
        }
        public void Awake()
        {
            _boutonMoins.onClick.AddListener(ClicBoutonMoins);
            _boutonPlus.onClick.AddListener(ClicBoutonPlus);
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        public void InitProfession(ProfessionEnum profession, Action professionEstModifie)
        {
                _profession = profession;
                _titre.text = GestionnaireProfessions.Instance.ProfessionDictConfig[_profession].Nom;
                _pourcentageActuel = (int)GestionnaireProfessions.Instance.AccederPourcent(_profession);
                _slider.SetValueWithoutNotify(_pourcentageActuel);
                
                //Debug.Log($"Pourcentage pour {_profession}: {_pourcentageActuel}");
                _pourcentageEstModifie = professionEstModifie;
                UpdatePourcentage();
        }

        private void ClicBoutonMoins()
        {
            if (_pourcentageActuel <= _pcMin)
                return;
            
            _pourcentageActuel--;
            
            _slider.value = _pourcentageActuel;
            UpdatePourcentage();
            //Debug.Log($"Pourcentage pour {_profession}: {GestionnaireProfessions.Instance.AccederPourcent(_profession)}");
        }
        
        private void ClicBoutonPlus()
        {
            /*if (GestionnaireProfessions.Instance.PourcentPopLibre() < 1)
                return;*/
            if (_pourcentageActuel >= _pcMax)
            {
                return;
            }
            _pourcentageActuel++;
            UpdatePourcentage();
            _slider.value = _pourcentageActuel;
            //Debug.Log($"Pourcentage pour {_profession}: {GestionnaireProfessions.Instance.AccederPourcent(_profession)}");
        }

        private void OnSliderValueChanged(float value)
        {
            //si on essaie de augmenter
            /*if (value > _pourcentageActuel && GestionnaireProfessions.Instance.PourcentPopLibre() < 1)
            {
                _slider.value = _pourcentageActuel;
                Debug.LogWarning("Tried to add more profession");
                return;
            }

            if (value <_pourcentageActuel && _pourcentageActuel < 1)
            {
                
                _slider.value = _pourcentageActuel;
                Debug.LogWarning("Tried to remove profession");
                return;
            }*/
            
            _pourcentageActuel = (int)value;
            UpdatePourcentage();
            //Debug.Log("Slider "+ value);
        }

        /// <summary>
        /// Met � jour le texte de pourcentage, ainsi que toutes les autres vues qui sont concern�es.
        /// </summary>
        private void UpdatePourcentage()
        {
            _pourcentage.text = _pourcentageActuel + "%";

            _pourcentageEstModifie?.Invoke();
            //Debug.Log($"Pourcentage actuel: {_pourcentageActuel}");
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
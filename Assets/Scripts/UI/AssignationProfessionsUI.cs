using System;
using Profession;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AssignationProfessionsUI : MonoBehaviour
    {
        private ProfessionEnum _profession;
        [SerializeField] private Button _boutonMoins;
        [SerializeField] private Button _boutonPlus;

        [SerializeField] private TextMeshProUGUI _titre;
        [SerializeField] private TextMeshProUGUI _pourcentage;

        [SerializeField] private Slider _slider;
        private Action _updatePourcentageTotal;
        private int _pourcentageActuel;
        private const int _pcMax = 100;
        private const int _pcMin = 0;

        public int GetPourcentageActuel()
        {
            return _pourcentageActuel;
        }
        public void Awake()
        {
            _boutonMoins.onClick.AddListener(ClicBoutonMoins);
            _boutonPlus.onClick.AddListener(ClicBoutonPlus);
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        public void InitProfession(ProfessionEnum profession, Action pourcentageTotal)
        {
                _profession = profession;
                _titre.text = GestionnaireProfessions.Instance.ProfessionDictConfig[_profession].Nom;
                _pourcentageActuel = (int)(GestionnaireProfessions.Instance.AccederPourcent(_profession) * 100);
                _slider.SetValueWithoutNotify(_pourcentageActuel);
                UpdatePourcentage();
                Debug.Log($"Pourcentage pour {_profession}: {_pourcentageActuel}");
                _updatePourcentageTotal = pourcentageTotal;
        }

        public ProfessionEnum GetProfession()
        {
            return _profession;
        }

        private void ClicBoutonMoins()
        {
            if (_pourcentageActuel <= _pcMin)
                return;
            
            _pourcentageActuel--;
            UpdatePourcentage();
            _updatePourcentageTotal.Invoke();
            _slider.value = _pourcentageActuel;
            Debug.Log($"Pourcentage pour {_profession}: {GestionnaireProfessions.Instance.AccederPourcent(_profession)}");
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
            _updatePourcentageTotal.Invoke();
            _slider.value = _pourcentageActuel;
            Debug.Log($"Pourcentage pour {_profession}: {GestionnaireProfessions.Instance.AccederPourcent(_profession)}");
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
            _updatePourcentageTotal.Invoke();
            Debug.Log("Slider "+ value);
        }

        private void UpdatePourcentage()
        {
            _pourcentage.text = _pourcentageActuel + "%";
        }
    }
}
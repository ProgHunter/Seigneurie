using System;
using System.Text.RegularExpressions;
using Profession;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Gère les pourcentages de population assignés à chaque profession
    /// </summary>
    public sealed class VueAssignationProfessions : MonoBehaviour, IDisposable
    {
        private ProfessionEnum _profession;
        [SerializeField] private Button _boutonMoins;
        [SerializeField] private Button _boutonPlus;

        [SerializeField] private TMP_InputField _inputPourcentage;
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
            _inputPourcentage.onValueChanged.AddListener(OnInputPourcentageValueChanged);
            _boutonMoins.onClick.AddListener(ClicBoutonMoins);
            _boutonPlus.onClick.AddListener(ClicBoutonPlus);
            _slider.onValueChanged.AddListener(PourcentageModifié);
        }

        public void InitProfession(ProfessionEnum profession, Action professionEstModifie)
        {
                _profession = profession;
                _titre.text = GestionnaireProfessions.Instance.ProfessionDictConfig[_profession].Nom;
                _pourcentageActuel = (int)GestionnaireProfessions.Instance.AccederPourcent(_profession);
                _slider.SetValueWithoutNotify(_pourcentageActuel);
                
                //Debug.Log($"Pourcentage pour {_profession}: {_pourcentageActuel}");
                _pourcentageEstModifie = professionEstModifie;
                MetAJourPourcentage();
        }

        private void OnInputPourcentageValueChanged(string inputText)
        {
            string pattern = @"^(100|[0-9][0-9]?)%?$";
            bool isValid = Regex.IsMatch(inputText, pattern);
            if (isValid)
            {
                _pourcentageActuel  = int.Parse(inputText.TrimEnd('%')); 
                _slider.value = _pourcentageActuel;
            }
            else
            {
                MetAJourPourcentage();
            }
        }

        private void ClicBoutonMoins()
        {
            if (_pourcentageActuel <= _pcMin)
                return;
            
            _pourcentageActuel--;
            
            _slider.value = _pourcentageActuel;
            MetAJourPourcentage();
            //Debug.Log($"Pourcentage pour {_profession}: {GestionnaireProfessions.Instance.AccederPourcent(_profession)}");
        }
        
        private void ClicBoutonPlus()
        {
            if (_pourcentageActuel >= _pcMax)
            {
                return;
            }
            _pourcentageActuel++;
            MetAJourPourcentage();
            _slider.value = _pourcentageActuel;
            //Debug.Log($"Pourcentage pour {_profession}: {GestionnaireProfessions.Instance.AccederPourcent(_profession)}");
        }

        private void PourcentageModifié(float value)
        {
            _pourcentageActuel = (int)value;
            MetAJourPourcentage();
            //Debug.Log("Slider "+ value);
        }

        /// <summary>
        /// Met à jour le texte de pourcentage, ainsi que toutes les autres vues qui sont concernées.
        /// </summary>
        private void MetAJourPourcentage()
        {
            string pourcentageActuel = _pourcentageActuel + "%";
            _pourcentage.text = pourcentageActuel;
            _slider.SetValueWithoutNotify(_pourcentageActuel);
            _inputPourcentage.text = pourcentageActuel;
            _pourcentageEstModifie?.Invoke();
            //Debug.Log($"Pourcentage actuel: {_pourcentageActuel}");
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
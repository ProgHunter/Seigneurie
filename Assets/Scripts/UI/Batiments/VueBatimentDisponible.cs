using Batiment;
using Production;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Batiments
{
    /// <summary>
    /// Ligne de la description d'un bâtiment qui est disponible à la construction 
    /// </summary>
    public class VueBatimentDisponible : MonoBehaviour, IDisposable
    {
        [SerializeField] private Image _icone;
        [SerializeField] private TextMeshProUGUI _nomBatiment;
        [SerializeField] private TextMeshProUGUI _coutRessources;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _coutTemps;
        [SerializeField] private Button _btnDebutConstruction;

        private BatimentEnum _batimentEnum;
        private const string _aucunMaçon = "Aucun maçon";
        private const string _ticksString = " ticks";
        private const string _msgConstructionCommencé = "Construction de ";
        private const string _msgErreurConstruction = "Ne peut pas construire. Le bouton ne devrait pas être clickable";
        private Action MettreAJourBatimentsEnCours;

        public void Init(BatimentEnum batimentEnum, Sprite icone, string nomBatiment, string coutRessources,
            string description, Action mettreAJourBatimentsEnCours)
        {
            _batimentEnum = batimentEnum;
            //TODO icone
            //_icone.sprite = icone;
            _nomBatiment.text = nomBatiment;
            _coutRessources.text = coutRessources;
            _description.text = description;
            UpdateValeurs();
            _btnDebutConstruction.onClick.AddListener(DémarrerConstruction);
            MettreAJourBatimentsEnCours += mettreAJourBatimentsEnCours;
        }
        
        private void MetAJourCoutTicks(long nb)
        {
            string texte = nb == -1 ? _aucunMaçon : nb + _ticksString;
            _coutTemps.text = texte;
        }

        private void DémarrerConstruction()
        {
            if (GestionnaireBatiments.Instance.DemarrerConstruction(_batimentEnum))
            {
                Debug.Log(_msgConstructionCommencé + _nomBatiment.text);
                MettreAJourBatimentsEnCours?.Invoke();
            }
            else
            {
                Debug.LogError(_msgErreurConstruction);
            }
            
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public void UpdateValeurs()
        {
            long nbTicksConstruction = GestionnaireProductions.Instance.NbTicksRestantsConstruction(-1, _batimentEnum);
            MetAJourCoutTicks(nbTicksConstruction);

            _btnDebutConstruction.interactable =
                GestionnaireBatiments.Instance.CoutConstructionEstDisponible(_batimentEnum);
        }
    }
}

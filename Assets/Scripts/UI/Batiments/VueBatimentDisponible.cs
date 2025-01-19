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
        private Action MettreAJourBatimentsEnCours;

        private const string _aucunMaçon = "Aucun maçon";
        private const string _ticksTexte = " tick(s)";
        private const string _msgConstructionCommencé = "Construction de ";
        private const string _msgErreurConstruction = "Ne peut pas construire. Le bouton ne devrait pas être clickable";

        public void Init(BatimentEnum batimentEnum, Sprite icone, string nomBatiment, string coutRessources,
            string description, Action mettreAJourBatimentsEnCours)
        {
            _batimentEnum = batimentEnum;
            //TODO icone
            //_icone.sprite = icone;
            _nomBatiment.text = nomBatiment;
            _coutRessources.text = coutRessources;
            _description.text = description;
            MetAjourValeurs();
            _btnDebutConstruction.onClick.AddListener(DémarrerConstruction);
            MettreAJourBatimentsEnCours += mettreAJourBatimentsEnCours;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public void MetAjourValeurs()
        {
            long nbTicksConstruction = GestionnaireProductions.Instance.NbTicksRestantsConstruction(-1, _batimentEnum);
            MetAJourCoutTicks(nbTicksConstruction);

            _btnDebutConstruction.interactable =
                GestionnaireBatiments.Instance.CoutConstructionEstDisponible(_batimentEnum);
        }

        private void MetAJourCoutTicks(long nbTicks)
        {
            string nbTicksTexte = nbTicks == -1 ? _aucunMaçon : nbTicks + _ticksTexte;
            _coutTemps.text = nbTicksTexte;
        }

        private void DémarrerConstruction()
        {
            if (GestionnaireBatiments.Instance.DemarrerConstruction(_batimentEnum))
            {
                //Debug.Log(_msgConstructionCommencé + _nomBatiment.text);
                MettreAJourBatimentsEnCours?.Invoke();
            }
            else
            {
                Debug.LogError(_msgErreurConstruction);
            }
        }
    }
}

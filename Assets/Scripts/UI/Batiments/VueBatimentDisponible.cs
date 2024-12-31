using Batiment;
using Production;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Batiments
{
    public class VueBatimentDisponible : MonoBehaviour, IDisposable
    {
        [SerializeField] private Image _icone;
        [SerializeField] private TextMeshProUGUI _nomBatiment;
        [SerializeField] private TextMeshProUGUI _coutRessources;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _coutTemps;
        [SerializeField] private Button _btnDebutConstruction;

        private BatimentEnum _batimentEnum;

        public void Init(BatimentEnum batimentEnum, Sprite icone, string nomBatiment, string coutRessources,
            string description, string coutTemps)
        {
            _batimentEnum = batimentEnum;
            //_icone.sprite = icone;
            _nomBatiment.text = nomBatiment;
            _coutRessources.text = coutRessources;
            _description.text = description;
            MetAJourCoutTicks(coutTemps);
            //TODO Initialiser le bouton
            _btnDebutConstruction.onClick.AddListener(DémarrerConstruction);
        }
        
        private void MetAJourCoutTicks(string texte)
        {
            _coutTemps.text = texte + " ticks";
        }

        private void DémarrerConstruction()
        {
            if (GestionnaireBatiments.Instance.DemarrerConstruction(_batimentEnum))
            {
                Debug.Log("Construction de " + _nomBatiment);
            }
            else
            {
                Debug.LogError("Ne peut pas construire");
                //TODO: Afficher à l'utilisateur que la construction n'a pas pu être démarré.
                // ou simplement griser le bouton "Construire".
            }
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public void UpdateCoutConstruction()
        {
            var nbTicksConstruction = GestionnaireProductions.Instance.NbTicksRestantsConstruction(-1, false, _batimentEnum);
            MetAJourCoutTicks(nbTicksConstruction.ToString());
        }
    }
}

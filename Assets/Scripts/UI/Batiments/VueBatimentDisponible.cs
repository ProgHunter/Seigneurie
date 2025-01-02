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
            string description, long coutTemps)
        {
            _batimentEnum = batimentEnum;
            //TODO icone
            //_icone.sprite = icone;
            _nomBatiment.text = nomBatiment;
            _coutRessources.text = coutRessources;
            _description.text = description;
            UpdateVue();
            _btnDebutConstruction.onClick.AddListener(DémarrerConstruction);
        }
        
        private void MetAJourCoutTicks(long nb)
        {
            string texte = nb == -1 ? "Aucun maçon" : nb + " ticks";
            _coutTemps.text = texte;
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

        public void UpdateVue()
        {
            var nbTicksConstruction = GestionnaireProductions.Instance.NbTicksRestantsConstruction(-1, false, _batimentEnum);
            MetAJourCoutTicks(nbTicksConstruction);

            _btnDebutConstruction.interactable =
                GestionnaireBatiments.Instance.CoutConstructionEstDisponible(_batimentEnum);
        }
    }
}

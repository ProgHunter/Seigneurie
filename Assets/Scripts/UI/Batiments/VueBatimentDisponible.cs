using Batiment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Batiments
{
    public class VueBatimentDisponible : MonoBehaviour
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
            SetCoutTemps(coutTemps);
            //TODO Initialiser le bouton
            _btnDebutConstruction.onClick.AddListener(DémarrerConstruction);
        }
        
        private void SetCoutTemps(string texte)
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
            }
            //Montrer une erreur si ça n'a pas marché
        }
    }
}

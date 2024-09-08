using Ressource;
using TMPro;
using UnityEngine;

namespace UI
{
    public class VueProductionRessource : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nom;
        [SerializeField] private TextMeshProUGUI _production;
        
        private RessourceEnum _ressourceRepresentee;

        public void InitRessourceRepresentee(RessourceEnum value)
        {
            _ressourceRepresentee = value;
            _nom.text = InventaireRessources.Instance.ressourceConfigDict[_ressourceRepresentee].Nom;
        }
        public void UpdateValeur()
        {
            //_production.text = GestionnaireProductions.Instance.;
        }
    }
}
using Ressource;
using UnityEngine;
using TMPro;

namespace UI
{
    public class IndicateurRessource : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nom;
        [SerializeField] private TextMeshProUGUI _nombre;

        private RessourceEnum _ressourceRepresentee;

        public void  InitRessourceRepresentee(RessourceEnum value)
        {
                _ressourceRepresentee = value;
                _nom.text = InventaireRessources.Instance.RessourceConfigDict[_ressourceRepresentee].Nom;
        }

        public void UpdateValeur()
        {
            _nombre.text = InventaireRessources.Instance.AccesQteRessource(_ressourceRepresentee).ToString();
        }
    }
}

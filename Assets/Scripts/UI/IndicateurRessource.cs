using System.Globalization;
using Ressource;
using UnityEngine;
using TMPro;

namespace UI
{
    /// <summary>
    /// Indique le nom et la quantité d'une ressource
    /// </summary>
    public class IndicateurRessource : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nom;
        [SerializeField] private TextMeshProUGUI _nombre;

        private RessourceEnum _ressourceRepresentee;

        public void InitRessourceRepresentee(RessourceEnum value)
        {
                _ressourceRepresentee = value;
                _nom.text = GestionnaireRessources.Instance.RessourceConfigDict[_ressourceRepresentee].Nom;
        }

        public void UpdateValeur()
        {
            _nombre.text = GestionnaireRessources.Instance.AccesQteRessource(_ressourceRepresentee).ToString("#,0", CultureInfo.CurrentCulture);
        }
    }
}

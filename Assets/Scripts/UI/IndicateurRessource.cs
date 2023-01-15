using Ressources;
using TMPro;
using UnityEngine;

namespace UI
{
    public class IndicateurRessource : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nom;
        [SerializeField] private TextMeshProUGUI _nombre;

        private AbstractRessource _ressource;

        public AbstractRessource Ressource
        {
            set
            {
                _ressource = value;
                _nom.text = _ressource.Nom;
            }
        }

        public void UpdateValeur()
        {
            _nombre.text = _ressource.Quantite.ToString();
        }
    }
}

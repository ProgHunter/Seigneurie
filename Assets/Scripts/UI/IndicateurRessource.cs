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

        public TextMeshProUGUI Nombre
        {
            get => _nombre;
            set => _nombre = value;
        }

        public AbstractRessource Ressource
        {
            set
            {
                _ressource = value;
                _nom.text = _ressource.Nom;
            }
        }
    }
}

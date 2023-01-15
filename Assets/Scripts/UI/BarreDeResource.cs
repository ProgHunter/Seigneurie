using Ressources;
using UnityEngine;

namespace UI
{
    public class BarreDeResource : MonoBehaviour
    {
        //[SerializeField] private List<IndicateurRessource> _listeIndicateurRessources;
        [SerializeField] private IndicateurRessource _duBois;
        [SerializeField] private IndicateurRessource _cailloux;
        [SerializeField] private IndicateurRessource _bouffe;
        
        private void Start()
        {
            _duBois.Ressource = new Nourriture();
            _cailloux.Ressource = new Nourriture();
            _bouffe.Ressource = new Nourriture();
        }
    }
}

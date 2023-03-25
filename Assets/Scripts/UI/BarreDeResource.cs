using System.Collections.Generic;
using Ressource;
using UnityEngine;

namespace UI
{
    public class BarreDeResource : MonoBehaviour
    {
        [SerializeField] private List<IndicateurRessource> _listeIndicateurRessources;

        public void Setup(List<AbstractRessource> ressources)
        {
            for (int i = 0; i < ressources.Count; i++)
            {
                if (_listeIndicateurRessources != null)
                {
                    _listeIndicateurRessources[i].Ressource = ressources[i];
                }
            }
        }

        public void UpdateRessources()
        {
            foreach (IndicateurRessource ressource in _listeIndicateurRessources)
            {
                ressource.UpdateValeur();
            }
        }
    }
}

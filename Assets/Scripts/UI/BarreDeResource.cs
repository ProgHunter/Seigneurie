using System.Collections.Generic;
using Ressource;
using UnityEngine;
using Utils;

namespace UI
{
    public class BarreDeResource : MonoBehaviour
    {
        [SerializeField] private List<IndicateurRessource> _listeIndicateurRessources;

        public void Init()
        {
            int i = 0;
            foreach (var ressource in EnumUtils.GetEnumValues<RessourceEnum>())
            {
                _listeIndicateurRessources?[i].InitRessourceRepresentee(ressource);

                i++;
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

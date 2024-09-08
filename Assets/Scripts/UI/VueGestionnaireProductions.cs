using System.Collections.Generic;
using Production;
using Ressource;
using UnityEngine;
using Utils;

namespace UI
{
    public class VueGestionnaireProductions : MonoBehaviour
    {
        [SerializeField] private List<VueProductionRessource> _listeProductions;

        public void Init()
        {
            GestionnaireProductions.Instance.Production();
            int i = 0;
            foreach (var ressource in EnumUtils.GetEnumValues<RessourceEnum>())
            {
                if (_listeProductions != null)
                {
                    _listeProductions[i]?.InitRessourceRepresentee(ressource);
                }

                i++;
            }
        }
    }
}
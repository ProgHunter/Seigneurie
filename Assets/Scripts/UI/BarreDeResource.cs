using System.Collections.Generic;
using Ressource;
using UnityEngine;
using Utils;

namespace UI
{
    public class BarreDeResource : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private IndicateurRessource _vueInstancier;
        private readonly Dictionary<RessourceEnum, IndicateurRessource> _dictRessources = new();

        public void Init()
        {
            var ressources = EnumUtils.GetEnumValues<RessourceEnum>();
            foreach (var ressource in ressources)
            {
                if (!InventaireRessources.Instance.EstDeverrouille(ressource))
                    continue;

                AjouterIndicateurRessource(ressource);
            }
        }

        public void UpdateRessources()
        {
            if (!gameObject.activeInHierarchy)
                return;

            var ressources = EnumUtils.GetEnumValues<RessourceEnum>();
            foreach (var ressource in ressources)
            {
                if (!InventaireRessources.Instance.EstDeverrouille(ressource))
                    continue;

                if(!_dictRessources.ContainsKey(ressource))
                    AjouterIndicateurRessource(ressource);

                _dictRessources[ressource].UpdateValeur();
            }
        }

        private void AjouterIndicateurRessource(RessourceEnum ressource)
        {
            IndicateurRessource indicateur = Instantiate(_vueInstancier, _parent);
            var config = InventaireRessources.Instance.RessourceConfigDict[ressource];

            indicateur.InitRessourceRepresentee(ressource);
            _dictRessources.Add(ressource, indicateur);
        }
    }
}

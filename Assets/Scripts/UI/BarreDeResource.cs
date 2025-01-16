using System.Collections.Generic;
using System.Linq;
using Ressource;
using UnityEngine;
using Utils;

namespace UI
{
    /// <summary>
    /// Liste des ressources du joueur
    /// </summary>
    public class BarreDeResource : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private IndicateurRessource _vueInstancier;
        private readonly Dictionary<RessourceEnum, IndicateurRessource> _dictRessources = new();

        public void Init()
        {
            List<RessourceEnum> ressources = InventaireRessources.Instance.RessourceConfigDict.Keys.ToList();
            foreach (var ressource in ressources)
            {
                if (!InventaireRessources.Instance.EstDeverrouille(ressource))
                    continue;

                AjouterIndicateurRessource(ressource);
            }
        }

        public void MetAJourListeDeRessources()
        {
            if (!gameObject.activeInHierarchy)
                return;

            List<RessourceEnum> ressources = InventaireRessources.Instance.RessourceConfigDict.Keys.ToList();
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

using Ressource;
using Batiment;
using System.Collections.Generic;
using UnityEngine;

namespace Production
{
    public sealed class GestionnaireProductions
    {
        private static readonly GestionnaireProductions _instance = new();
        private Dictionary<RessourceEnum, AbstraitProduction> _productionRessourceDict;
        private ProductionEffortConstruction _productionEffortConstruction;

        public GestionnaireProductions()
        {
            _productionRessourceDict = new Dictionary<RessourceEnum, AbstraitProduction>
            {
                { RessourceEnum.POPULATION, new ProductionPopulation() },
                { RessourceEnum.NOURRITURE, new ProductionNouriture()  },
                { RessourceEnum.BOIS,       new ProductionBois()       },
                { RessourceEnum.MINERAUX,   new ProductionMineraux()   }
            };

            _productionEffortConstruction = new ProductionEffortConstruction();
        }

        public static GestionnaireProductions Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Exécute un tick de production pour le jeu.
        /// Comprend les types de ressources dans l'inventaire et la construction de batiment.
        /// </summary>
        public void Production()
        {
            // Production des ressources
            LotRessources ressourcesProduites = new();
            foreach (KeyValuePair<RessourceEnum, AbstraitProduction> ressource in _productionRessourceDict)
            {
                int production = ressource.Value.CalculerProduction();
                Debug.Log($"Production de {production} pour la ressource {ressource.Key}.");
                ressourcesProduites.AttribuerQteRessource(ressource.Key, production);
            }
            InventaireRessources.Instance.ModifierQteRessource(ressourcesProduites);

            // Progression de la construction
            int effort = _productionEffortConstruction.CalculerProduction();
            GestionnaireBatiments.Instance.AvancerConstruction(effort);
        }
    }
}

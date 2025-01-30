using Ressource;
using Batiment;
using System.Collections.Generic;
using Profession;

namespace Production
{
    public sealed class GestionnaireProductions
    {
        #region members
        private static readonly GestionnaireProductions _instance = new();
        private Dictionary<RessourceEnum, AbstraitProduction> _productionRessourceDict;
        private ProductionEffortConstruction _productionEffortConstruction;
        #endregion members

        private GestionnaireProductions()
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

        public static GestionnaireProductions Instance => _instance;

        /// <summary>
        /// Exécute un tick de production pour le jeu.
        /// Comprend les types de ressources dans l'inventaire et la construction de batiment.
        /// </summary>
        public void Production()
        {
            LotProfessions professions = GestionnaireProfessions.Instance.Professions;
            
            // Production des ressources
            LotRessources ressourcesProduites = new();
            foreach (KeyValuePair<RessourceEnum, AbstraitProduction> ressource in _productionRessourceDict)
            {
                long production = ressource.Value.CalculerProduction(professions);
                //Debug.Log($"Production de {production} pour la ressource {ressource.Key}.");
                ressourcesProduites.AttribuerQteRessource(ressource.Key, production);
            }
            GestionnaireRessources.Instance.AjouterQteRessourceAvecLimites(ressourcesProduites);

            // Progression de la construction
            long effort = _productionEffortConstruction.CalculerProduction(professions);
            GestionnaireBatiments.Instance.AvancerConstruction(effort);
        }

        /// <summary>
        /// Evalue la production d'une ressource selon un lot de profession.
        /// </summary>
        /// <param name="ressource">La ressource dont on veut connaitre la production.</param>
        /// <param name="professions">Un lot de professions, si null on prend celui du gestionnaire
        /// de professions pour évaluer la production.</param>
        /// <returns>La quantité de ressource produite selon le lot de professions.</returns>
        public long EvaluerProduction(RessourceEnum ressource, LotProfessions professions = null)
        {
            professions ??= GestionnaireProfessions.Instance.Professions;

            return _productionRessourceDict[ressource].CalculerProduction(professions);
        }

        /// <summary>
        /// Evalue le nombre de ticks restants à une construction selon le pourcent de maçons qui seront attribués.
        /// </summary>
        /// <param name="pourcentMacon">Le pourcentage de la population qui travaillera sur la profession de maçon. [0, 100]
        /// Laisser -1 si on veut évaluer une construction avec le pourcentage de maçons actuel.</param>
        /// <param name="batiment">Le type de bâtiment dont on veut le nombre de ticks que prendrait sa contruction total.
        /// Laisser NUL si on veut l'information de la construction en cours, sinon on effectue le calcul sur l'effort total
        /// du bâtiment spécifié.</param>
        /// <returns>Le nombre de ticks estimés pour la complétion du bâtiment.
        /// -1 si aucun effort de construction n'est produit.</returns>
        public long NbTicksRestantsConstruction(long pourcentMacon = -1, BatimentEnum batiment = BatimentEnum.NUL)
        {
            return _productionEffortConstruction.EstimeNombreTicksRestants(pourcentMacon, batiment);
        }
    }
}

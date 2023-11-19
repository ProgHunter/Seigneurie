using System;
using System.Collections.Generic;
using Utils;

namespace Ressource
{
    public sealed class InventaireRessources
    {
        private static readonly InventaireRessources _instance = new();
        private LotRessources _quantiteRessources;
        public Dictionary<RessourceEnum, AbstraitRessourceConfig> ressourceConfigDict;

        private InventaireRessources()
        {
            ressourceConfigDict = new Dictionary<RessourceEnum, AbstraitRessourceConfig>
            {
                { RessourceEnum.POPULATION, new PopulationConfig() },
                { RessourceEnum.NOURRITURE, new NourritureConfig() },
                { RessourceEnum.BOIS,       new BoisConfig()       },
                { RessourceEnum.MINERAUX,   new MinerauxConfig()   }
            };

            int qtePopulation = ressourceConfigDict[RessourceEnum.POPULATION].qteBase;
            int qteMaxPopulation = ressourceConfigDict[RessourceEnum.POPULATION].qteMax;

            int qteNourriture = ressourceConfigDict[RessourceEnum.NOURRITURE].qteBase;
            int qteMaxNourriture = ressourceConfigDict[RessourceEnum.NOURRITURE].qteMax;

            int qteBois = ressourceConfigDict[RessourceEnum.BOIS].qteBase;
            int qteMaxBois = ressourceConfigDict[RessourceEnum.BOIS].qteMax;

            int qteMineraux = ressourceConfigDict[RessourceEnum.MINERAUX].qteBase;
            int qteMaxMineraux = ressourceConfigDict[RessourceEnum.MINERAUX].qteMax;

            _quantiteRessources = new LotRessources(new Qte(qtePopulation, qteMaxPopulation),
                                                    new Qte(qteNourriture, qteMaxNourriture),
                                                    new Qte(qteBois, qteMaxBois),
                                                    new Qte(qteMineraux, qteMaxMineraux));
        }

        public static InventaireRessources Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Donne accès à la quantité d'une ressource de l'inventaire.
        /// </summary>
        /// <param name="ressource">La ressource dont on veut avoir la quantité</param>
        /// <returns>La quantité de la ressource en inventaire</returns>
        public int AccesQteRessource(RessourceEnum ressource)
        {
            return _quantiteRessources.AccesQteRessource(ressource);
        }

        /// <summary>
        /// Donne accès à la quantité minimale d'une ressource de l'inventaire.
        /// </summary>
        /// <param name="ressource">La ressource dont on veut avoir la quantité minimale</param>
        /// <returns>La quantité minimale de la ressource</returns>
        public int AccesQteMinRessource(RessourceEnum ressource)
        {
            return _quantiteRessources.AccesQteMinRessource(ressource);
        }

        /// <summary>
        /// Donne accès à la quantité maximale d'une ressource de l'inventaire.
        /// </summary>
        /// <param name="ressource">La ressource dont on veut avoir la quantité maximale</param>
        /// <returns>La quantité maximale de la ressource</returns>
        public int AccesQteMaxRessource(RessourceEnum ressource)
        {
            return _quantiteRessources.AccesQteMaxRessource(ressource);
        }

        /// <summary>
        /// Attribue une valeur spécifique à une ressource dans l'inventaire.
        /// </summary>
        /// <param name="ressource">La ressource à attribuer</param>
        /// <param name="quantite">La quantité à attribuer</param>
        public void AttribuerQteRessource(RessourceEnum ressource, int quantite)
        {
            _quantiteRessources.AttribuerQteRessource(ressource, quantite);
        }

        public void AttribuerQteRessource(LotRessources ressources)
        {
            foreach (RessourceEnum ressource in Enum.GetValues(typeof(RessourceEnum)))
                AttribuerQteRessource(ressource, ressources.AccesQteRessource(ressource));
        }

        /// <summary>
        /// Attribue la valeur maximale d'un type de ressource dans l'inventaire.
        /// </summary>
        /// <param name="ressource">Le type de ressource</param>
        /// <param name="quantite">La quantité maximale</param>
        public void AttribuerQteMaxRessource(RessourceEnum ressource, int quantite)
        {
            _quantiteRessources.AttribuerQteMaxRessource(ressource, quantite);
        }

        /// <summary>
        /// Permet d'additionner ou soustraire une quantité à une ressource.
        /// La valeur minimale et maximale de la ressource sera validé.
        /// Si les limites ne sont pas respectés, la valeur min (ou max) sera mise dans l'inventaire.
        /// </summary>
        /// <param name="ressource">La ressource à modifier</param>
        /// <param name="quantite">La quantité à additionner ou soustraire</param>
        /// <returns>Retourne vrai si la valeur résultant respectait les limite inférieures et suppérieures de l'inventaire</returns>
        public bool ModifierQteRessource(RessourceEnum ressource, int quantite)
        {
            int qteMax = _quantiteRessources.AccesQteMaxRessource(ressource);
            int qteMin = _quantiteRessources.AccesQteMinRessource(ressource);
            int inventaire = _quantiteRessources.AccesQteRessource(ressource);

            int resultat = inventaire + quantite;
            bool respecteLimites = true;
            if (resultat < qteMin)
            {
                resultat = qteMin;
                respecteLimites = false;
            }
            else if (resultat > qteMax)
            {
                resultat = qteMax;
                respecteLimites = false;
            }

            _quantiteRessources.AttribuerQteRessource(ressource, resultat);

            return respecteLimites;
        }

        /// <summary>
        /// Permet d'additionner ou soustraire une quantité à une ressource à partir d'un lot de ressources
        /// </summary>
        /// <param name="ressources">Le lot de ressources contenant les quantités</param>
        /// <returns>Vrai si toutes les ressources ont pu être modifiées</returns>
        public bool ModifierQteRessource(LotRessources ressources)
        {
            bool resultat = true;
            foreach (RessourceEnum ressource in Enum.GetValues(typeof(RessourceEnum)))
            {
                if (ModifierQteRessource(ressource, ressources.AccesQteRessource(ressource)))
                    resultat = false;
            }

            return resultat;
        }
    }
}

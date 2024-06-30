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

            long qtePopulation = ressourceConfigDict[RessourceEnum.POPULATION].qteBase;
            long qteMinPopulation = ressourceConfigDict[RessourceEnum.POPULATION].qteMin;
            long qteMaxPopulation = ressourceConfigDict[RessourceEnum.POPULATION].qteMax;

            long qteNourriture = ressourceConfigDict[RessourceEnum.NOURRITURE].qteBase;
            long qteMinNourriture = ressourceConfigDict[RessourceEnum.NOURRITURE].qteMin;
            long qteMaxNourriture = ressourceConfigDict[RessourceEnum.NOURRITURE].qteMax;

            long qteBois = ressourceConfigDict[RessourceEnum.BOIS].qteBase;
            long qteMinBois = ressourceConfigDict[RessourceEnum.BOIS].qteMin;
            long qteMaxBois = ressourceConfigDict[RessourceEnum.BOIS].qteMax;

            long qteMineraux = ressourceConfigDict[RessourceEnum.MINERAUX].qteBase;
            long qteMinMineraux = ressourceConfigDict[RessourceEnum.MINERAUX].qteMin;
            long qteMaxMineraux = ressourceConfigDict[RessourceEnum.MINERAUX].qteMax;

            _quantiteRessources = new LotRessources(new Qte(qtePopulation, qteMaxPopulation, qteMinPopulation),
                                                    new Qte(qteNourriture, qteMaxNourriture, qteMinNourriture),
                                                    new Qte(qteBois, qteMaxBois, qteMinBois),
                                                    new Qte(qteMineraux, qteMaxMineraux, qteMinMineraux));
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
        public long AccesQteRessource(RessourceEnum ressource)
        {
            return _quantiteRessources.AccesQteRessource(ressource);
        }

        /// <summary>
        /// Donne accès à la quantité minimale d'une ressource de l'inventaire.
        /// </summary>
        /// <param name="ressource">La ressource dont on veut avoir la quantité minimale</param>
        /// <returns>La quantité minimale de la ressource</returns>
        public long AccesQteMinRessource(RessourceEnum ressource)
        {
            return _quantiteRessources.AccesQteMinRessource(ressource);
        }

        /// <summary>
        /// Donne accès à la quantité maximale d'une ressource de l'inventaire.
        /// </summary>
        /// <param name="ressource">La ressource dont on veut avoir la quantité maximale</param>
        /// <returns>La quantité maximale de la ressource</returns>
        public long AccesQteMaxRessource(RessourceEnum ressource)
        {
            return _quantiteRessources.AccesQteMaxRessource(ressource);
        }

        /// <summary>
        /// Attribue une valeur spécifique à une ressource dans l'inventaire.
        /// Aucune validation!
        /// </summary>
        /// <param name="ressource">La ressource à attribuer</param>
        /// <param name="quantite">La quantité à attribuer</param>
        public void AttribuerQteRessource(RessourceEnum ressource, long quantite)
        {
            _quantiteRessources.AttribuerQteRessource(ressource, quantite);
        }

        /// <summary>
        /// Attribue le lot de ressources à l'inventaire.
        /// Aucune validation!
        /// </summary>
        /// <param name="ressources">Le lot de ressources à attribuer</param>
        public void AttribuerQteRessource(LotRessources ressources)
        {
            foreach (RessourceEnum ressource in Enum.GetValues(typeof(RessourceEnum)))
                AttribuerQteRessource(ressource, ressources.AccesQteRessource(ressource));
        }

        /// <summary>
        /// Attribue la valeur minimal à chaque quantité de ressource de l'inventaire.
        /// </summary>
        public void AttribuerQteMinRessources()
        {
            foreach (RessourceEnum ressource in Enum.GetValues(typeof(RessourceEnum)))
                AttribuerQteRessource(ressource, _quantiteRessources.AccesQteMinRessource(ressource));
        }

        /// <summary>
        /// Attribue une limite maximale au type de ressource dans l'inventaire.
        /// </summary>
        /// <param name="ressource">Le type de ressource</param>
        /// <param name="limiteMax">La quantité maximale</param>
        public void ModifierLimiteMaxRessource(RessourceEnum ressource, long limiteMax)
        {
            _quantiteRessources.ModifierLimiteMaxRessource(ressource, limiteMax);
        }

        /// <summary>
        /// Permet d'additionner ou soustraire une quantité à une ressource.
        /// La valeur minimale et maximale de la ressource sera validé.
        /// 
        /// Si les limites ne sont pas respectés, la valeur min (ou max) sera mise dans l'inventaire (limitesBloquantes == faux).
        /// limitesBloquantes == vrai : La transaction ne sera pas complétée dès qu'une limite est dépassée.
        /// </summary>
        /// <param name="ressource">La ressource à modifier</param>
        /// <param name="quantite">La quantité à additionner ou soustraire</param>
        /// <param name="limitesBloquantes">La transaction ne sera pas complété si une limite est dépassée</param>
        /// <returns>Retourne vrai si la valeur résultant respectait les limite inférieures et suppérieures de l'inventaire</returns>
        public bool AjouterQteRessourceAvecLimites(RessourceEnum ressource, long quantite, bool limitesBloquantes = false)
        {
            long inventaire = _quantiteRessources.AccesQteRessource(ressource);
            long resultat = inventaire + quantite;
            bool respecteLimites = ValideLimitesMinMaxAttributionRessource(ressource, ref resultat);

            if (limitesBloquantes && !respecteLimites)
                return false;
            
            _quantiteRessources.AttribuerQteRessource(ressource, resultat);

            return respecteLimites;
        }

        /// <summary>
        /// Permet d'additionner ou soustraire une quantité à une ressource à partir d'un lot de ressources.
        /// Si limitesBloquantes, la transaction complète est annulé lorsqu'une des limites est dépassée.
        /// </summary>
        /// <param name="ressources">Le lot de ressources contenant les quantités</param>
        /// <returns>Vrai si toutes les ressources ont pu être modifiées sans atteindre une limite</returns>
        public bool AjouterQteRessourceAvecLimites(LotRessources ressources, bool limitesBloquantes = false)
        {
            if (limitesBloquantes)
            {
                long resultat;
                foreach (RessourceEnum ressource in Enum.GetValues(typeof(RessourceEnum)))
                {
                    resultat = ressources.AccesQteRessource(ressource) + _quantiteRessources.AccesQteRessource(ressource);
                    if (!ValideLimitesMinMaxAttributionRessource(ressource, ref resultat))
                        return false;
                }
            }

            bool respecteLimites = true;
            foreach (RessourceEnum ressource in Enum.GetValues(typeof(RessourceEnum)))
            {
                respecteLimites &= AjouterQteRessourceAvecLimites(ressource, ressources.AccesQteRessource(ressource));
            }

            return respecteLimites;
        }

        /// <summary>
        /// Valide si la quantité de ressources attribuée respectera les limites min et max de celle-ci.
        /// Corrige qtAttribuee si ce n'est pas le cas
        /// </summary>
        /// <param name="ressource">La ressource à modifier</param>
        /// <param name="qtAttribuee">Ref, la quantité à attribuer dans l'inventaire. Sera modifier pour respecter les limites.</param>
        /// <returns>Vrai si la quantité respecte les limites min et max de la ressource.</returns>
        public bool ValideLimitesMinMaxAttributionRessource(RessourceEnum ressource, ref long qtAttribuee)
        {
            long qteMax = _quantiteRessources.AccesQteMaxRessource(ressource);
            long qteMin = _quantiteRessources.AccesQteMinRessource(ressource);
            //long inventaire = _quantiteRessources.AccesQteRessource(ressource);

            if (qtAttribuee < qteMin)
            {
                qtAttribuee = qteMin;
                return false;
            }
            if (qtAttribuee > qteMax)
            {
                qtAttribuee = qteMax;
                return false;
            }

            return true;
        }
    }
}

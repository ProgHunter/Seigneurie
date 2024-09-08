using UnityEngine;
using Batiment;
using System;
using System.Collections.Generic;

namespace Ressource
{
    public sealed class InventaireRessources
    {
        #region members
        private static readonly InventaireRessources _instance = new();
        private LotRessources _quantiteRessources;
        public Dictionary<RessourceEnum, AbstraitRessourceConfig> RessourceConfigDict;
        #endregion members

        private InventaireRessources()
        {
            RessourceConfigDict = new Dictionary<RessourceEnum, AbstraitRessourceConfig>
            {
                { RessourceEnum.POPULATION, new PopulationConfig() },
                { RessourceEnum.NOURRITURE, new NourritureConfig() },
                { RessourceEnum.BOIS,       new BoisConfig()       },
                { RessourceEnum.MINERAUX,   new MinerauxConfig()   }
            };

            _quantiteRessources = new LotRessources(RessourceConfigDict[RessourceEnum.POPULATION].Qte,
                                                    RessourceConfigDict[RessourceEnum.NOURRITURE].Qte,
                                                    RessourceConfigDict[RessourceEnum.BOIS].Qte,
                                                    RessourceConfigDict[RessourceEnum.MINERAUX].Qte);
        }

        public static InventaireRessources Instance
        {
            get
            {
                return _instance;
            }
        }

        #region accesseurs_mutateurs
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
            _quantiteRessources.AttribuerQteRessource(ressources);
        }

        /// <summary>
        /// Attribue la valeur minimal à chaque quantité de ressource de l'inventaire.
        /// </summary>
        public void AttribuerQteMinRessource()
        {
            _quantiteRessources.AttribuerQteMinRessource();
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
        #endregion accesseurs_mutateurs

        /// <summary>
        /// Permet d'additionner ou soustraire une quantité à une ressource.
        /// La valeur minimale et maximale de la ressource sera validé.
        /// 
        /// Si la ressource est verrouiller, on ne peut pas en ajouter (sauf si on ajoute 0).
        /// Si les limites ne sont pas respectés, la valeur min (ou max) sera mise dans l'inventaire (limitesBloquantes == faux).
        /// limitesBloquantes == vrai : La transaction ne sera pas complétée dès qu'une limite est dépassée.
        /// </summary>
        /// <param name="ressource">La ressource à modifier</param>
        /// <param name="quantite">La quantité à additionner ou soustraire</param>
        /// <param name="limitesBloquantes">La transaction ne sera pas complété si une limite est dépassée</param>
        /// <returns>Retourne vrai si la valeur résultant respectait les limite inférieures et suppérieures de l'inventaire</returns>
        public bool AjouterQteRessourceAvecLimites(RessourceEnum ressource, long quantite, bool limitesBloquantes = false)
        {
            // Si la _qte à ajouter est 0, on évite les validations et on retourne dirrectement OK.
            if (quantite == 0)
                return true;

            if (!EstDeverrouille(ressource))
                return false;

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
        /// Si la ressource est verrouiller, on ne peut pas en ajouter (sauf si on ajoute 0).
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
                    if (ressources.AccesQteRessource(ressource) == 0)
                        continue;

                    if (!EstDeverrouille(ressource))
                        return false;

                    resultat = ressources.AccesQteRessource(ressource) + _quantiteRessources.AccesQteRessource(ressource);
                    if (!ValideLimitesMinMaxAttributionRessource(ressource, ref resultat))
                        return false;
                }
            }

            bool respecteLimites = true;
            foreach (RessourceEnum ressource in Enum.GetValues(typeof(RessourceEnum)))
            {
                if (ressources.AccesQteRessource(ressource) == 0)
                    continue;

                if (!EstDeverrouille(ressource))
                {
                    respecteLimites = false;
                    continue;
                }

                respecteLimites &= AjouterQteRessourceAvecLimites(ressource, ressources.AccesQteRessource(ressource));
            }

            return respecteLimites;
        }

        /// <summary>
        /// Valide si la quantité de ressources attribuée respectera les limites min et max de celle-ci.
        /// Valide aussi si la ressource est déverrouillée, sinon retourne la valeur min de la ressource.
        /// Corrige qtAttribuee si ce n'est pas le cas.
        /// </summary>
        /// <param name="ressource">La ressource à modifier</param>
        /// <param name="qtAttribuee">Ref, la quantité à attribuer dans l'inventaire. Sera modifier pour respecter les limites.</param>
        /// <returns>Vrai si la quantité respecte les limites min et max de la ressource.</returns>
        public bool ValideLimitesMinMaxAttributionRessource(RessourceEnum ressource, ref long qtAttribuee)
        {
            long qteMax = _quantiteRessources.AccesQteMaxRessource(ressource);
            long qteMin = _quantiteRessources.AccesQteMinRessource(ressource);

            if (!EstDeverrouille(ressource) || qtAttribuee < qteMin)
            {
                qtAttribuee = qteMin;
                return false;
            }
            else if (qtAttribuee > qteMax)
            {
                qtAttribuee = qteMax;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Indique si la ressource est déverrouillé.
        /// On valide avec le prérequis de la ressource.
        /// Si la ressource est verouillé, on ne doit pas pouvoir la voir (et la produire).
        /// </summary>
        /// <returns>Vrai si la ressource est disponible</returns>
        public bool EstDeverrouille(RessourceEnum ressource)
        {
            LotBatiments prerequis;
            try
            {
                prerequis = RessourceConfigDict[ressource].Prerequis;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
                return false;
            }

            return GestionnaireBatiments.Instance.PrerequisEstRespecte(prerequis);
        }

        /// <summary>
        /// Valide si on a assez de ressources pour le coût.
        /// </summary>
        /// <param name="cout">Le coût à valider. Qte positives.</param>
        /// <returns>Vrai si l'inventaire a assez de ressources pour le coût comparé.</returns>
        public bool QteRessourcesSuffisantes(LotRessources cout)
        {
            return _quantiteRessources >= cout;
        }
    }
}

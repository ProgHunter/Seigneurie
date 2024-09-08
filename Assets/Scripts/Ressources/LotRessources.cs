using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Ressource
{
    public class LotRessources
    {
        public Dictionary<RessourceEnum, Qte> RessourcesDict;

        public LotRessources(Qte qtePopulation, Qte qteNourriture, Qte qteBois, Qte qteMineraux)
        {
            RessourcesDict = new Dictionary<RessourceEnum, Qte>
            {
                { RessourceEnum.POPULATION, qtePopulation },
                { RessourceEnum.NOURRITURE, qteNourriture },
                { RessourceEnum.BOIS,       qteBois       },
                { RessourceEnum.MINERAUX,   qteMineraux   }
            };
        }

        /// <summary>
        /// Constructeur pour un lot de ressources qui exprime seulement une quantité.
        /// Pas de min ni de max spécifié.
        /// </summary>
        public LotRessources(long qtePopulation = 0, long qteNourriture= 0, long qteBois = 0, long qteMineraux = 0)
        {
            RessourcesDict = new Dictionary<RessourceEnum, Qte>
            {
                { RessourceEnum.POPULATION, new Qte(qtePopulation) },
                { RessourceEnum.NOURRITURE, new Qte(qteNourriture) },
                { RessourceEnum.BOIS,       new Qte(qteBois)       },
                { RessourceEnum.MINERAUX,   new Qte(qteMineraux)   }
            };
        }

        /// <summary>
        /// Donne accès à la quantité d'une ressource.
        /// </summary>
        /// <param name="ressource">La ressource dont on veut avoir la quantité</param>
        /// <returns>La quantité de la ressource</returns>
        public long AccesQteRessource(RessourceEnum ressource)
        {
            long qte = 0;

            try
            {
                qte = RessourcesDict[ressource].qte;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }

            return qte;
        }

        /// <summary>
        /// Attribue une valeur spécifique à une ressource.
        /// </summary>
        /// <param name="ressource">La ressource à attribuer</param>
        /// <param name="qte">La quantité à attribuer</param>
        public void AttribuerQteRessource(RessourceEnum ressource, long qte)
        {
            try
            {
                RessourcesDict[ressource].qte = qte;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }
        }

        /// <summary>
        /// Donne accès à la limite minimal d'une ressource.
        /// </summary>
        /// <param name="ressource">Le type de ressource</param>
        /// <returns>La quantité minimal de la ressource</returns>
        public long AccesQteMinRessource(RessourceEnum ressource)
        {
            long qteMin = 0;

            try
            {
                qteMin = RessourcesDict[ressource].qteMin;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }

            return qteMin;
        }

        /// <summary>
        /// Donne accès à la limite maximal d'une ressource.
        /// </summary>
        /// <param name="ressource">Le type de ressource</param>
        /// <returns>La quantité maximale de la ressource</returns>
        public long AccesQteMaxRessource(RessourceEnum ressource)
        {
            long qteMax = 0;

            try
            {
                qteMax = RessourcesDict[ressource].qteMax;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }

            return qteMax;
        }

        /// <summary>
        /// Modifie la limite maximale d'un type de ressource.
        /// </summary>
        /// <param name="ressource">Le type de ressource</param>
        /// <param name="qteMax">La quantité maximale</param>
        public void ModifierLimiteMaxRessource(RessourceEnum ressource, long qteMax)
        {
            try
            {
                RessourcesDict[ressource].qteMax = qteMax;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }
        }
    }
}

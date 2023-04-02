using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Ressource
{
    public class LotRessources
    {
        public Dictionary<RessourceEnum, Qte> ressourcesDict;

        public LotRessources(Qte qtePopulation, Qte qteNourriture, Qte qteBois, Qte qteMineraux)
        {
            ressourcesDict = new Dictionary<RessourceEnum, Qte>
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
        public LotRessources(int qtePopulation, int qteNourriture, int qteBois, int qteMineraux)
        {
            ressourcesDict = new Dictionary<RessourceEnum, Qte>
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
        public int AccesQteRessource(RessourceEnum ressource)
        {
            int qte = 0;

            try
            {
                qte = ressourcesDict[ressource].qte;
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
        public void AttribuerQteRessource(RessourceEnum ressource, int qte)
        {
            try
            {
                ressourcesDict[ressource].qte = qte;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }
        }

        /// <summary>
        /// Donne accès à la quantité maximal d'une ressource.
        /// </summary>
        /// <param name="ressource">Le type de ressource</param>
        /// <returns>La quantité maximale de la ressource</returns>
        public int AccesQteMaxRessource(RessourceEnum ressource)
        {
            int qteMax = 0;

            try
            {
                qteMax = ressourcesDict[ressource].qteMax;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }

            return qteMax;
        }

        /// <summary>
        /// Attribue la quantité maximale d'un type de ressource.
        /// </summary>
        /// <param name="ressource">Le type de ressource</param>
        /// <param name="qteMax">La quantité maximale</param>
        public void AttribuerQteMaxRessource(RessourceEnum ressource, int qteMax)
        {
            try
            {
                ressourcesDict[ressource].qteMax = qteMax;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }
        }
    }
}

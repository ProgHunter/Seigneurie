using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ressource
{
    public abstract class AbstractRessource
    {
        #region members
        public string ID;
        public string Nom;
        public Sprite Icone;
        public string Description;
        public int Quantite;
        #endregion members
    }

    public enum RessourceEnum
    {
        POPULATION = 0,
        NOURRITURE = 1,
        BOIS = 2,
        MINERAUX = 3,
    }

    public struct LotRessources
    {
        public LotRessources(int qtPopulation = 0, int qtNourriture = 0, int qtBois = 0, int qtMineraux = 0)
        {
            RessourcesDic = new Dictionary<RessourceEnum, int>();
            RessourcesDic.Add(RessourceEnum.POPULATION, qtPopulation);
            RessourcesDic.Add(RessourceEnum.NOURRITURE, qtNourriture);
            RessourcesDic.Add(RessourceEnum.BOIS, qtBois);
            RessourcesDic.Add(RessourceEnum.MINERAUX, qtMineraux);
        }

        public Dictionary<RessourceEnum, int> RessourcesDic;

        /// <summary>
        /// Donne acc�s � la quantit� d'une ressource.
        /// </summary>
        /// <param name="ressource">La ressource dont on veut avoir la quantit�</param>
        /// <returns>La quantit� de la ressource</returns>
        public int AccesRessource(RessourceEnum ressource)
        {
            int quantite = 0;

            try
            {
                quantite = RessourcesDic[ressource];
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }

            return quantite;
        }

        /// <summary>
        /// Attribue une valeur sp�cifique � une ressource.
        /// </summary>
        /// <param name="ressource">La ressource � attribuer</param>
        /// <param name="quantite">La quantit� � attribuer</param>
        public void AttribuerRessource(RessourceEnum ressource, int quantite)
        {
            try
            {
                RessourcesDic[ressource] = quantite;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ressource
{
    public sealed class InventaireRessources
    {
        private static readonly InventaireRessources _instance = new InventaireRessources();
        public Dictionary<RessourceEnum, int> QuantiteRessources;

        private InventaireRessources()
        {
            QuantiteRessources = new Dictionary<RessourceEnum, int>();
            Array ressourcesArray = Enum.GetValues(typeof(RessourceEnum));
            foreach (RessourceEnum ressource in ressourcesArray)
                QuantiteRessources.Add(ressource, 100);
            // TODO: Remplacer la valeur placeholder 100 par une fonction pour récupérer la valeur
            //       de base du jeu et éventuellement de la sauvegarde.
        }

        public static InventaireRessources Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Donne accès à la quantité d'une ressource de l'inventaire
        /// </summary>
        /// <param name="ressource">La ressource dont on veut avoir la quantité</param>
        /// <returns>La quantité de la ressource en inventaire</returns>
        public int AccesQuantiteRessource(RessourceEnum ressource)
        {
            int quantite = 0;

            try
            {
                quantite = QuantiteRessources[ressource];
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }

            return quantite;
        }

        /// <summary>
        /// Permet d'additionner ou soustraire une quantité à une ressource.
        /// La valeur minimale et maximale de la ressource sera validé. 
        /// L'opération sera refusé si le résultat est négatif.
        /// </summary>
        /// <param name="ressource">La ressource à modifier</param>
        /// <param name="quantite">La quantité à additionner ou soustraire</param>
        /// <returns>Retourne vrai si la valeur a été modifié</returns>
        public bool ModifierRessource(RessourceEnum ressource, int quantite)
        {
            int result;
            int inventaire;

            try
            {
                inventaire = QuantiteRessources[ressource];
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
                return false;
            }

            result = inventaire + quantite;
            if (result < 0)
                return false;
            else if (result > 1000)
                result = 1000;
            // TODO: Remplacer la valeur placeholder 1000 par la valeur maximale de cette ressource
            QuantiteRessources[ressource] = result;

            return true;
        }

        /// <summary>
        /// Attribue une valeur spécifique à une ressource dans l'inventaire.
        /// </summary>
        /// <param name="ressource">La ressource à attribuer</param>
        /// <param name="quantite">La quantité à attribuer</param>
        public void AttribuerRessource(RessourceEnum ressource, int quantite)
        {
            try
            {
                QuantiteRessources[ressource] = quantite;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }
        }
    }
}

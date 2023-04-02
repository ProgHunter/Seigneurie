using System.Collections.Generic;

namespace Ressource
{
    public sealed class InventaireRessources
    {
        private static readonly InventaireRessources _instance = new();
        public LotRessources QuantiteRessources;

        private InventaireRessources(int qtPopulation = 100, int qtNourriture = 100, int qtBois = 100, int qtMineraux = 100)
        {
            QuantiteRessources = new LotRessources(qtPopulation, qtNourriture, qtBois, qtMineraux);
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
            return QuantiteRessources.AccesRessource(ressource);
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
                inventaire = QuantiteRessources.AccesRessource(ressource);
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            result = inventaire + quantite;
            if (result < 0)
                return false;
            else if (result > 1000)
                result = 1000;
            // TODO: Remplacer la valeur placeholder 1000 par la valeur maximale de cette ressource
            QuantiteRessources.AttribuerRessource(ressource, result);

            return true;
        }

        /// <summary>
        /// Attribue une valeur spécifique à une ressource dans l'inventaire.
        /// </summary>
        /// <param name="ressource">La ressource à attribuer</param>
        /// <param name="quantite">La quantité à attribuer</param>
        public void AttribuerRessource(RessourceEnum ressource, int quantite)
        {
            QuantiteRessources.AttribuerRessource(ressource, quantite);
        }
    }
}

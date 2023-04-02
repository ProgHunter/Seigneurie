using System.Collections.Generic;
using Utils;

namespace Ressource
{
    public sealed class InventaireRessources
    {
        private static readonly InventaireRessources _instance = new();
        private LotRessources _quantiteRessources;
        public Dictionary<RessourceEnum, AbstraitRessourceConfig> ressourceConfigDict;

        private InventaireRessources(int qtePopulation = 100, int qteMaxPopulation = 100000,
                                     int qteNourriture = 100, int qteMaxNourriture = 10000, 
                                     int qteBois = 100,       int qteMaxBois = 10000, 
                                     int qteMineraux = 100,   int qteMaxMineraux = 10000)
        {
            _quantiteRessources = new LotRessources(new Qte(qtePopulation, qteMaxPopulation),
                                                    new Qte(qteNourriture, qteMaxNourriture),
                                                    new Qte(qteBois, qteMaxBois),
                                                    new Qte(qteMineraux, qteMaxMineraux));

            ressourceConfigDict = new Dictionary<RessourceEnum, AbstraitRessourceConfig>
            {
                { RessourceEnum.POPULATION, new PopulationConfig() },
                { RessourceEnum.NOURRITURE, new NourritureConfig() },
                { RessourceEnum.BOIS,       new BoisConfig()       },
                { RessourceEnum.MINERAUX,   new MinerauxConfig()   }
            };
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
        /// L'opération sera refusé si le résultat est négatif.
        /// </summary>
        /// <param name="ressource">La ressource à modifier</param>
        /// <param name="quantite">La quantité à additionner ou soustraire</param>
        /// <returns>Retourne vrai si la valeur a été modifié</returns>
        public bool ModifierQteRessource(RessourceEnum ressource, int quantite)
        {
            int result;
            int inventaire;

            inventaire = _quantiteRessources.AccesQteRessource(ressource);

            int qteMax = _quantiteRessources.AccesQteMaxRessource(ressource);
            result = inventaire + quantite;
            if (result < 0)
                return false;
            else if (result > qteMax)
                result = qteMax;

            _quantiteRessources.AttribuerQteRessource(ressource, result);

            return true;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Batiment
{
    public sealed class GestionnaireBatiments
    {
        #region members
        private static readonly GestionnaireBatiments _instance = new();
        private LotBatiments _batiments;
        public Dictionary<BatimentEnum, AbstraitBatimentConfig> batimentConfigDict;
        /// <summary>
        /// <see cref="BatimentEnum"/> Constitue le bâtiment en contruction
        /// <see cref="int"/> Effort restant pour compléter la construction
        /// </summary>
        public Paire<BatimentEnum, int> enConstruction = null;
        #endregion members

        private GestionnaireBatiments(int qteMaison = 1,       int qteMaxMaison = 1000, 
                                      int qteFerme = 0,        int qteMaxFerme = 1000, 
                                      int qteScierie = 0,      int qteMaxScierie = 100, 
                                      int qteMine = 0,         int qteMaxMine = 100,
                                      int qteHotelDeVille = 0, int qteMaxHotelDeVille = 1)
        {
            _batiments = new LotBatiments(new Qte(qteMaison, qteMaxMaison),
                                          new Qte(qteFerme, qteMaxFerme),
                                          new Qte(qteScierie, qteMaxScierie),
                                          new Qte(qteMine, qteMaxMine),
                                          new Qte(qteHotelDeVille, qteMaxHotelDeVille));

            batimentConfigDict = new Dictionary<BatimentEnum, AbstraitBatimentConfig>
            {
                { BatimentEnum.MAISON,       new MaisonConfig()       },
                { BatimentEnum.FERME,        new FermeConfig()        },
                { BatimentEnum.SCIERIE,      new ScierieConfig()      },
                { BatimentEnum.MINE,         new MineConfig()         },
                { BatimentEnum.HOTELDEVILLE, new HotelDeVilleConfig() }
            };
        }

        public static GestionnaireBatiments Instance
        {
            get
            {
                return _instance;
            }
        }

        public int AccesQteBatiment(BatimentEnum batiment)
        {
            return _batiments.AccesQteBatiment(batiment);
        }

        public int AccesQteMaxBatiment(BatimentEnum batiment)
        {
            return _batiments.AccesQteMaxBatiment(batiment);
        }

        public void AttribuerQteBatiment(BatimentEnum batiment, int qte)
        {
            _batiments.AttribuerQteBatiment(batiment, qte);
        }

        public void AttribuerQteMaxBatiment(BatimentEnum batiment, int qte)
        {
            _batiments.AttribuerQteMaxBatiment(batiment, qte);
        }

        /// <summary>
        /// Démarrer une nouvelle construction.
        /// Une seul construction à la fois. Refusé si déjà au max de ce type de bâtiment.
        /// </summary>
        /// <param name="batiment">Le bâtiment à construire</param>
        /// <returns>Vrai si la construction peut être démarrée</returns>
        public bool DemarrerConstruction(BatimentEnum batiment)
        {
            // Un seule construction à la fois
            if (enConstruction != null)
                return false;

            // Si on est à la limite max, on ne commance pas la nouvelle construction
            if (_batiments.AccesQteBatiment(batiment) >= _batiments.AccesQteMaxBatiment(batiment))
                return false;

            try
            {
                enConstruction = new Paire<BatimentEnum, int>();
                enConstruction.Item1 = batiment;
                enConstruction.Item2 = batimentConfigDict[batiment].effortConstruction;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le bâtiment {batiment} n'est pas dans le dictionnaire.");
                enConstruction = null;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Permet de faire progresser la construction d'un bâtiment en indiquant l'effort fournis
        /// </summary>
        /// <param name="effort">L'effort à soustraire de la construction</param>
        /// <returns>Vrai quand la contruction est complétée</returns>
        public bool AvancerConstruction(int effort)
        {
            // Aucune construction en cours
            if (enConstruction == null)
                return false;

            enConstruction.Item2 -= effort;

            // S'il reste de l'effort à fournir pour la construction, elle reste en cours
            if (enConstruction.Item2 > 0)
                return false;

            // La construction est complétée
            _batiments.AjouterUnBatiment(enConstruction.Item1);
            enConstruction = null;
            return true;
        }
    }
}

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
        public Dictionary<BatimentEnum, AbstraitBatimentConfig> BatimentConfigDict;
        /// <summary>
        /// <see cref="BatimentEnum"/> Constitue le bâtiment en contruction
        /// <see cref="int"/> Effort restant pour compléter la construction
        /// </summary>
        public Paire<BatimentEnum, long> EnConstruction = null;
        #endregion members

        private GestionnaireBatiments(long qteMaison = 1,       long qteMaxMaison = 1000, 
                                      long qteFerme = 0,        long qteMaxFerme = 1000, 
                                      long qteScierie = 0,      long qteMaxScierie = 100, 
                                      long qteMine = 0,         long qteMaxMine = 100,
                                      long qteHotelDeVille = 0, long qteMaxHotelDeVille = 1)
        {
            _batiments = new LotBatiments(new Qte(qteMaison, qteMaxMaison),
                                          new Qte(qteFerme, qteMaxFerme),
                                          new Qte(qteScierie, qteMaxScierie),
                                          new Qte(qteMine, qteMaxMine),
                                          new Qte(qteHotelDeVille, qteMaxHotelDeVille));

            BatimentConfigDict = new Dictionary<BatimentEnum, AbstraitBatimentConfig>
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

        public long AccesQteBatiment(BatimentEnum batiment)
        {
            return _batiments.AccesQteBatiment(batiment);
        }

        public long AccesQteMaxBatiment(BatimentEnum batiment)
        {
            return _batiments.AccesQteMaxBatiment(batiment);
        }

        public void AttribuerQteBatiment(BatimentEnum batiment, long qte)
        {
            _batiments.AttribuerQteBatiment(batiment, qte);
        }

        public void ModifierLimiteMaxBatiment(BatimentEnum batiment, long qte)
        {
            _batiments.ModifierLimiteMaxBatiment(batiment, qte);
        }

        public LotBatiments AccesPrerequis(BatimentEnum batiment)
        {
            LotBatiments prerequis = null;

            try
            {
                prerequis = BatimentConfigDict[batiment].Prerequis;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le bâtiment {batiment} n'est pas dans le dictionnaire.");
                return new LotBatiments();
            }

            return prerequis;
        }

        /// <summary>
        /// Valide si les bâtiments mentionnés dans le lot sont construits et en quantité suffisante.
        /// </summary>
        /// <param name="prerequis">Le lot de bâtiments qui constitu le prérequis</param>
        /// <returns>Vrai si on a au moins la même quantité que spécifiée dans le lot</returns>
        public bool PrerequisEstRespecte(LotBatiments prerequis)
        {
            return _batiments >= prerequis;
        }

        /// <summary>
        /// Indique si le bâtiment est déverrouillé.
        /// On valide avec le prérequis du bâtiment.
        /// Si le bâtiment est verouillé, on ne doit pas pouvoir le construire.
        /// </summary>
        /// <returns>Vrai si le batiment est disponible pour être construit</returns>
        public bool EstDeverrouille(BatimentEnum batiment)
        {
            return PrerequisEstRespecte(AccesPrerequis(batiment));
        }

        public bool ConstructionEstEnCours()
        {
            return EnConstruction != null;
        }

        public long AccesEffortConstructionTotal(BatimentEnum batiment)
        {
            int EffortConstructionTotal = 0;

            try
            {
                EffortConstructionTotal = BatimentConfigDict[batiment].EffortConstruction;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le bâtiment {batiment} n'est pas dans le dictionnaire.");
                return 0;
            }

            return EffortConstructionTotal;
        }

        public long AccesEffortConstructionTotalEnCours()
        {
            if (!ConstructionEstEnCours())
                return 0;

            return AccesEffortConstructionTotal(EnConstruction.Item1);
        }

        public long AccesEffortConstructionRestant()
        {
            if (!ConstructionEstEnCours())
                return 0;

            return EnConstruction.Item2;
        }

        /// <summary>
        /// Démarrer une nouvelle construction.
        /// Une seul construction à la fois. Refusé si déjà au max de ce type de bâtiment.
        /// </summary>
        /// <param name="batiment">Le bâtiment à construire</param>
        /// <returns>Vrai si la construction peut être démarrée</returns>
        public bool DemarrerConstruction(BatimentEnum batiment)
        {
            // S'il y a déjà une construction en cours, ou
            // Si le bâtiment n'est pas encore disponible, ou
            // Si on est à la limite max, on ne commance pas la nouvelle construction
            if (ConstructionEstEnCours() ||
                !EstDeverrouille(batiment) ||
                (_batiments.AccesQteBatiment(batiment) >= _batiments.AccesQteMaxBatiment(batiment)))
                return false;

            try
            {
                EnConstruction = new Paire<BatimentEnum, long>();
                EnConstruction.Item1 = batiment;
                EnConstruction.Item2 = BatimentConfigDict[batiment].EffortConstruction;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le bâtiment {batiment} n'est pas dans le dictionnaire.");
                EnConstruction = null;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Permet de faire progresser la construction d'un bâtiment en indiquant l'effort fournis
        /// </summary>
        /// <param name="effort">L'effort à soustraire de la construction</param>
        /// <returns>Vrai quand la contruction est complétée</returns>
        public bool AvancerConstruction(long effort)
        {
            // Aucune construction en cours
            if (EnConstruction == null)
                return false;

            EnConstruction.Item2 -= effort;

            // S'il reste de l'effort à fournir pour la construction, elle reste en cours
            if (EnConstruction.Item2 > 0)
                return false;

            // La construction est complétée
            _batiments.AjouterUnBatiment(EnConstruction.Item1);
            EnConstruction = null;
            return true;
        }

        /// <summary>
        /// Annuler une construction en cours.
        /// Le coût ne sera pas remboursé et la progression ne sera pas sauvegardé.
        /// </summary>
        public void AnnulerConstruction()
        {
            EnConstruction = null;
        }
    }
}

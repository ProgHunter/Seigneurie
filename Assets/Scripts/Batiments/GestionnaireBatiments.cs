using Ressource;
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
        /// <see cref="long"/> Effort restant pour compléter la construction
        /// </summary>
        public Paire<BatimentEnum, long> EnConstruction = null;
        #endregion members

        private GestionnaireBatiments()
        {
            BatimentConfigDict = new Dictionary<BatimentEnum, AbstraitBatimentConfig>
            {
                { BatimentEnum.MAISON,       new MaisonConfig()       },
                { BatimentEnum.FERME,        new FermeConfig()        },
                { BatimentEnum.SCIERIE,      new ScierieConfig()      },
                { BatimentEnum.MINE,         new MineConfig()         },
                { BatimentEnum.HOTELDEVILLE, new HotelDeVilleConfig() }
            };

            _batiments = new LotBatiments(BatimentConfigDict[BatimentEnum.MAISON].Qte,
                                          BatimentConfigDict[BatimentEnum.FERME].Qte,
                                          BatimentConfigDict[BatimentEnum.SCIERIE].Qte,
                                          BatimentConfigDict[BatimentEnum.MINE].Qte,
                                          BatimentConfigDict[BatimentEnum.HOTELDEVILLE].Qte);
        }

        public static GestionnaireBatiments Instance => _instance;

        #region accesseurs_mutateurs
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
        #endregion accesseurs_mutateurs

        /// <summary>
        /// Valide si les bâtiments mentionnés dans le lot sont construits et en quantité suffisante.
        /// </summary>
        /// <param name="prerequis">Le lot de bâtiments qui constitue le prérequis</param>
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

        /// <summary>
        /// Accès au coût de contruction du bâtiment
        /// </summary>
        /// <param name="batiment">Le bâtiment dont on veut avoir le coût.</param>
        /// <returns>Le lot de ressource avec les quantitées correspondant au coût du bâtiment</returns>
        public LotRessources AccesCoutBatiment(BatimentEnum batiment)
        {
            LotRessources Cout;

            try
            {
                Cout = BatimentConfigDict[batiment].CoutConstruction;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le bâtiment {batiment} n'est pas dans le dictionnaire.");
                return null;
            }

            return Cout;
        }

        /// <summary>
        /// Valide si les ressources sont disponibles pour le coût de construction du bâtiment.
        /// </summary>
        /// <param name="batiment">Le batiment à valider le coût</param>
        /// <returns>Vrai si les ressources sont disponibles</returns>
        public bool CoutConstructionEstDisponible(BatimentEnum batiment)
        {
            LotRessources coutConstruction = AccesCoutBatiment(batiment);
            return GestionnaireRessources.Instance.QteRessourcesSuffisantes(coutConstruction);
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

            // On essaie d'effectuer la transaction avec l'inventaire.
            // Si le coût est trop élevé pour le nombre de ressources dans l'inventaire, 
            // les ressources ne sont pas retirées et la construction n'est pas démarrée.
            if (!GestionnaireRessources.Instance.AjouterQteRessourceAvecLimites(-AccesCoutBatiment(batiment), true))
                return false;

            Paire<BatimentEnum, long> Chantier = new Paire<BatimentEnum, long>();
            try
            {
                Chantier.Item1 = batiment;
                Chantier.Item2 = BatimentConfigDict[batiment].EffortConstruction;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le bâtiment {batiment} n'est pas dans le dictionnaire.");
                return false;
            }

            EnConstruction = Chantier;
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

        /// <summary>
        /// Rétablie les valeurs (Qte) de la config pour les bâtiments.
        /// Annule la construction en cours.
        /// </summary>
        public void Reinitialiser()
        {
            _batiments = new LotBatiments(BatimentConfigDict[BatimentEnum.MAISON].Qte,
                                          BatimentConfigDict[BatimentEnum.FERME].Qte,
                                          BatimentConfigDict[BatimentEnum.SCIERIE].Qte,
                                          BatimentConfigDict[BatimentEnum.MINE].Qte,
                                          BatimentConfigDict[BatimentEnum.HOTELDEVILLE].Qte);

            AnnulerConstruction();
        }
    }
}

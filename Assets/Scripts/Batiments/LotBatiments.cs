using Ressource;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Batiment
{
    public class LotBatiments
    {
        public Dictionary<BatimentEnum, Qte> BatimentsDic;

        public LotBatiments(Qte qteMaison, Qte qteFerme, Qte qteScierie, Qte qteMine, Qte qteHotelDeVille)
        {
            BatimentsDic = new Dictionary<BatimentEnum, Qte>
            {
                { BatimentEnum.MAISON,       qteMaison       },
                { BatimentEnum.FERME,        qteFerme        },
                { BatimentEnum.SCIERIE,      qteScierie      },
                { BatimentEnum.MINE,         qteMine         },
                { BatimentEnum.HOTELDEVILLE, qteHotelDeVille }
            };
        }

        /// <summary>
        /// Constructeur pour un lot de bâtiments qui exprime seulement une quantité.
        /// Pas de min ni de max spécifié.
        /// </summary>
        public LotBatiments(long qteMaison = 0, long qteFerme = 0, long qteScierie = 0, long qteMine = 0, long qteHotelDeVille = 0)
        {
            BatimentsDic = new Dictionary<BatimentEnum, Qte>
            {
                { BatimentEnum.MAISON,       new Qte(qteMaison)       },
                { BatimentEnum.FERME,        new Qte(qteFerme)        },
                { BatimentEnum.SCIERIE,      new Qte(qteScierie)      },
                { BatimentEnum.MINE,         new Qte(qteMine)         },
                { BatimentEnum.HOTELDEVILLE, new Qte(qteHotelDeVille) }
            };
        }

        /// <summary>
        /// Ajoute un bâtiment du type voulu au lot
        /// Valide la quantité maximale.
        /// </summary>
        /// <param name="batiment">Le batiment que l'on veut un de plus</param>
        /// <returns>Vrai si le nombre a été incrémenté</returns>
        public bool AjouterUnBatiment(BatimentEnum batiment)
        {
            long qte;

            try
            {
                qte = BatimentsDic[batiment].qte;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            if (++qte > BatimentsDic[batiment].qteMax)
                return false;

            BatimentsDic[batiment].qte = qte;
            return true;
        }

        /// <summary>
        /// Donne accès à la quantité d'un type de bâtiment.
        /// </summary>
        /// <param name="batiment">Le bâtiment dont on veut avoir la quantité</param>
        /// <returns>La quantité de ce bâtiment</returns>
        public long AccesQteBatiment(BatimentEnum batiment)
        {
            long quantite = 0;

            try
            {
                quantite = BatimentsDic[batiment].qte;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le bâtiment {batiment} n'est pas dans le dictionnaire.");
            }

            return quantite;
        }

        /// <summary>
        /// Attribue une quantité spécifique d'un bâtiment.
        /// Ne valide pas avec le min et le max de cette quantité.
        /// Cette responsabilité est lessée à l'appelant.
        /// </summary>
        /// <param name="batiment">Le bâtiment</param>
        /// <param name="qte">La quantité à attribuer</param>
        public void AttribuerQteBatiment(BatimentEnum batiment, long qte)
        {
            try
            {
                BatimentsDic[batiment].qte = qte;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le bâtiment {batiment} n'est pas dans le dictionnaire.");
            }
        }

        /// <summary>
        /// Donne accès à la quantité maximal d'un type de bâtiment.
        /// </summary>
        /// <param name="batiment">Le bâtiment dont on veut avoir la quantité maximal</param>
        /// <returns>La quantité de ce bâtiment</returns>
        public long AccesQteMaxBatiment(BatimentEnum batiment)
        {
            long qteMax = 0;

            try
            {
                qteMax = BatimentsDic[batiment].qteMax;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le bâtiment {batiment} n'est pas dans le dictionnaire.");
            }

            return qteMax;
        }

        /// <summary>
        /// Attribue la quantité maximal d'un bâtiment.
        /// </summary>
        /// <param name="batiment">Le bâtiment</param>
        /// <param name="qteMax">La quantité à attribuer</param>
        public void ModifierLimiteMaxBatiment(BatimentEnum batiment, long qteMax)
        {
            try
            {
                BatimentsDic[batiment].qteMax = qteMax;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le bâtiment {batiment} n'est pas dans le dictionnaire.");
            }
        }

        /// <summary>
        /// Compare si le premier lot de batiments est plus petit ou égale au deuxième.
        /// On vient comparer la quantité pour chaque type de batiment.
        /// </summary>
        /// <param name="lot1">Premier lot de batiments</param>
        /// <param name="lot2">Deuxième lot de batiments</param>
        /// <returns>Vrai si la quantité de chaque type de batiment est plus petit ou égale</returns>
        public static bool operator <=(LotBatiments lot1, LotBatiments lot2)
        {
            foreach (KeyValuePair<BatimentEnum, Qte> qteBatiment in lot1.BatimentsDic)
            {
                var qteBatimentLot1 = qteBatiment.Value.qte;
                var qteBatimentLot2 = lot2.BatimentsDic[qteBatiment.Key].qte;

                if (qteBatimentLot1 > qteBatimentLot2)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Compare si le premier lot de batiments est plus grand ou égale au deuxième.
        /// On vient comparer la quantité pour chaque type de batiment.
        /// </summary>
        /// <param name="lot1">Premier lot de batiments</param>
        /// <param name="lot2">Deuxième lot de batiments</param>
        /// <returns>Vrai si la quantité de chaque type de batiment est plus grand ou égale</returns>
        public static bool operator >=(LotBatiments lot1, LotBatiments lot2)
        {
            foreach (KeyValuePair<BatimentEnum, Qte> qteBatiment in lot1.BatimentsDic)
            {
                var qteBatimentLot1 = qteBatiment.Value.qte;
                var qteBatimentLot2 = lot2.BatimentsDic[qteBatiment.Key].qte;

                if (qteBatimentLot1 < qteBatimentLot2)
                    return false;
            }

            return true;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Batiment
{
    public class LotBatiments
    {
        public Dictionary<BatimentEnum, Qte> batimentsDic;

        public LotBatiments(Qte qteMaison, Qte qteFerme, Qte qteScierie, Qte qteMine, Qte qteHotelDeVille)
        {
            batimentsDic = new Dictionary<BatimentEnum, Qte>
            {
                { BatimentEnum.MAISON,       qteMaison       },
                { BatimentEnum.FERME,        qteFerme        },
                { BatimentEnum.SCIERIE,      qteScierie      },
                { BatimentEnum.MINE,         qteMine         },
                { BatimentEnum.HOTELDEVILLE, qteHotelDeVille }
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
            int qte;

            try
            {
                qte = batimentsDic[batiment].qte;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            if (++qte > batimentsDic[batiment].qteMax)
                return false;

            batimentsDic[batiment].qte = qte;
            return true;
        }

        /// <summary>
        /// Donne accès à la quantité d'un type de bâtiment.
        /// </summary>
        /// <param name="batiment">Le bâtiment dont on veut avoir la quantité</param>
        /// <returns>La quantité de ce bâtiment</returns>
        public int AccesQteBatiment(BatimentEnum batiment)
        {
            int quantite = 0;

            try
            {
                quantite = batimentsDic[batiment].qte;
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
        public void AttribuerQteBatiment(BatimentEnum batiment, int qte)
        {
            try
            {
                batimentsDic[batiment].qte = qte;
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
        public int AccesQteMaxBatiment(BatimentEnum batiment)
        {
            int qteMax = 0;

            try
            {
                qteMax = batimentsDic[batiment].qteMax;
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
        public void AttribuerQteMaxBatiment(BatimentEnum batiment, int qteMax)
        {
            try
            {
                batimentsDic[batiment].qteMax = qteMax;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le bâtiment {batiment} n'est pas dans le dictionnaire.");
            }
        }
    }
}
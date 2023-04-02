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
        /// Ajoute un b�timent du type voulu au lot
        /// Valide la quantit� maximale.
        /// </summary>
        /// <param name="batiment">Le batiment que l'on veut un de plus</param>
        /// <returns>Vrai si le nombre a �t� incr�ment�</returns>
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
        /// Donne acc�s � la quantit� d'un type de b�timent.
        /// </summary>
        /// <param name="batiment">Le b�timent dont on veut avoir la quantit�</param>
        /// <returns>La quantit� de ce b�timent</returns>
        public int AccesQteBatiment(BatimentEnum batiment)
        {
            int quantite = 0;

            try
            {
                quantite = batimentsDic[batiment].qte;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le b�timent {batiment} n'est pas dans le dictionnaire.");
            }

            return quantite;
        }

        /// <summary>
        /// Attribue une quantit� sp�cifique d'un b�timent.
        /// Ne valide pas avec le min et le max de cette quantit�.
        /// Cette responsabilit� est less�e � l'appelant.
        /// </summary>
        /// <param name="batiment">Le b�timent</param>
        /// <param name="qte">La quantit� � attribuer</param>
        public void AttribuerQteBatiment(BatimentEnum batiment, int qte)
        {
            try
            {
                batimentsDic[batiment].qte = qte;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le b�timent {batiment} n'est pas dans le dictionnaire.");
            }
        }

        /// <summary>
        /// Donne acc�s � la quantit� maximal d'un type de b�timent.
        /// </summary>
        /// <param name="batiment">Le b�timent dont on veut avoir la quantit� maximal</param>
        /// <returns>La quantit� de ce b�timent</returns>
        public int AccesQteMaxBatiment(BatimentEnum batiment)
        {
            int qteMax = 0;

            try
            {
                qteMax = batimentsDic[batiment].qteMax;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le b�timent {batiment} n'est pas dans le dictionnaire.");
            }

            return qteMax;
        }

        /// <summary>
        /// Attribue la quantit� maximal d'un b�timent.
        /// </summary>
        /// <param name="batiment">Le b�timent</param>
        /// <param name="qteMax">La quantit� � attribuer</param>
        public void AttribuerQteMaxBatiment(BatimentEnum batiment, int qteMax)
        {
            try
            {
                batimentsDic[batiment].qteMax = qteMax;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le b�timent {batiment} n'est pas dans le dictionnaire.");
            }
        }
    }
}
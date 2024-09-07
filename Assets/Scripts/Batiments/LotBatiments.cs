using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Batiment
{
    public class LotBatiments
    {
        private Dictionary<BatimentEnum, Qte> _batimentsDict;

        public LotBatiments(Qte qteMaison, Qte qteFerme, Qte qteScierie, Qte qteMine, Qte qteHotelDeVille)
        {
            _batimentsDict = new Dictionary<BatimentEnum, Qte>
            {
                { BatimentEnum.MAISON,       qteMaison.Clone()       },
                { BatimentEnum.FERME,        qteFerme.Clone()        },
                { BatimentEnum.SCIERIE,      qteScierie.Clone()      },
                { BatimentEnum.MINE,         qteMine.Clone()         },
                { BatimentEnum.HOTELDEVILLE, qteHotelDeVille.Clone() }
            };
        }

        /// <summary>
        /// Constructeur pour un lot de bâtiments qui exprime seulement une quantité.
        /// Pas de min ni de max spécifié.
        /// </summary>
        public LotBatiments(long qteMaison = 0, long qteFerme = 0, long qteScierie = 0, long qteMine = 0, long qteHotelDeVille = 0)
        {
            _batimentsDict = new Dictionary<BatimentEnum, Qte>
            {
                { BatimentEnum.MAISON,       new Qte(qteMaison)       },
                { BatimentEnum.FERME,        new Qte(qteFerme)        },
                { BatimentEnum.SCIERIE,      new Qte(qteScierie)      },
                { BatimentEnum.MINE,         new Qte(qteMine)         },
                { BatimentEnum.HOTELDEVILLE, new Qte(qteHotelDeVille) }
            };
        }

        /// <summary>
        /// Crée un clone profond du lot de bâtiments
        /// </summary>
        /// <returns>Nouvel objet LotBatiments avec les mêmes valeurs</returns>
        public LotBatiments Clone()
        {
            return new LotBatiments(_batimentsDict[BatimentEnum.MAISON].Clone(),
                                    _batimentsDict[BatimentEnum.FERME].Clone(),
                                    _batimentsDict[BatimentEnum.SCIERIE].Clone(),
                                    _batimentsDict[BatimentEnum.MINE].Clone(),
                                    _batimentsDict[BatimentEnum.HOTELDEVILLE].Clone());
        }

        #region accesseurs_mutateurs
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
                quantite = _batimentsDict[batiment].qte;
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
                _batimentsDict[batiment].qte = qte;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le bâtiment {batiment} n'est pas dans le dictionnaire.");
            }
        }

        /// <summary>
        /// Attribue la valeur minimale (qteMin) aux _qte des bâtiments du lot.
        /// </summary>
        public void AttribuerQteMinBatiment()
        {
            foreach (BatimentEnum batiment in _batimentsDict.Keys)
                _batimentsDict[batiment].qte = _batimentsDict[batiment].qteMin;
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
                qteMax = _batimentsDict[batiment].qteMax;
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
                _batimentsDict[batiment].qteMax = qteMax;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Le bâtiment {batiment} n'est pas dans le dictionnaire.");
            }
        }
        #endregion accesseurs_mutateurs

        #region operateurs
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
                qte = _batimentsDict[batiment].qte;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            if (++qte > _batimentsDict[batiment].qteMax)
                return false;

            _batimentsDict[batiment].qte = qte;
            return true;
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
            foreach ((BatimentEnum batiment, Qte qteBatiment) in lot1._batimentsDict)
            {
                var qteBatimentLot1 = qteBatiment.qte;
                var qteBatimentLot2 = lot2._batimentsDict[batiment].qte;

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
            foreach ((BatimentEnum batiment, Qte qteBatiment) in lot1._batimentsDict)
            {
                var qteBatimentLot1 = qteBatiment.qte;
                var qteBatimentLot2 = lot2._batimentsDict[batiment].qte;

                if (qteBatimentLot1 < qteBatimentLot2)
                    return false;
            }

            return true;
        }
        #endregion operateurs
    }
}

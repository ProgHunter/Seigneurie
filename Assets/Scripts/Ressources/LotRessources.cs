using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Utils;

namespace Ressource
{
    public class LotRessources
    {
        private Dictionary<RessourceEnum, Qte> _ressourcesDict;

        public LotRessources(Qte qtePopulation, Qte qteNourriture, Qte qteBois, Qte qteMineraux)
        {
            _ressourcesDict = new Dictionary<RessourceEnum, Qte>
            {
                { RessourceEnum.POPULATION, qtePopulation.Clone() },
                { RessourceEnum.NOURRITURE, qteNourriture.Clone() },
                { RessourceEnum.BOIS,       qteBois.Clone()       },
                { RessourceEnum.MINERAUX,   qteMineraux.Clone()   }
            };
        }

        /// <summary>
        /// Constructeur pour un lot de ressources qui exprime seulement une quantité.
        /// Pas de min ni de max spécifié.
        /// </summary>
        public LotRessources(long qtePopulation = 0, long qteNourriture = 0, long qteBois = 0, long qteMineraux = 0)
        {
            _ressourcesDict = new Dictionary<RessourceEnum, Qte>
            {
                { RessourceEnum.POPULATION, new Qte(qtePopulation) },
                { RessourceEnum.NOURRITURE, new Qte(qteNourriture) },
                { RessourceEnum.BOIS,       new Qte(qteBois)       },
                { RessourceEnum.MINERAUX,   new Qte(qteMineraux)   }
            };
        }

        /// <summary>
        /// Crée un clone profond du lot de ressources
        /// </summary>
        /// <returns>Nouvel objet LotRessources avec les mêmes valeurs</returns>
        public LotRessources Clone()
        {
            return new LotRessources(_ressourcesDict[RessourceEnum.POPULATION].Clone(),
                                     _ressourcesDict[RessourceEnum.NOURRITURE].Clone(),
                                     _ressourcesDict[RessourceEnum.BOIS].Clone(),
                                     _ressourcesDict[RessourceEnum.MINERAUX].Clone());
        }

        public override string ToString()
        {
            List<string> listeRessources = new();
            foreach ((RessourceEnum ressource, Qte quantité) in _ressourcesDict)
            {
                if (quantité.qte > 0)
                {
                    string texte = $"{quantité.qte.ToString("#,0", CultureInfo.CurrentCulture)} {GestionnaireRessources.Instance.RessourceConfigDict[ressource].Nom}";
                    listeRessources.Add(texte);
                }
            }

            var listeRessourcesString = String.Join("\n", listeRessources.ToArray());
            return listeRessourcesString;
        }

        #region accesseurs_mutateurs
        /// <summary>
        /// Donne accès à la quantité d'une ressource.
        /// </summary>
        /// <param name="ressource">La ressource dont on veut avoir la quantité</param>
        /// <returns>La quantité de la ressource</returns>
        public long AccesQteRessource(RessourceEnum ressource)
        {
            long qte = 0;

            try
            {
                qte = _ressourcesDict[ressource].qte;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }

            return qte;
        }

        /// <summary>
        /// Attribue une valeur spécifique à la _qte d'une ressource.
        /// </summary>
        /// <param name="ressource">La ressource à attribuer</param>
        /// <param name="qte">La quantité à attribuer</param>
        public void AttribuerQteRessource(RessourceEnum ressource, long qte)
        {
            try
            {
                _ressourcesDict[ressource].qte = qte;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }
        }

        /// <summary>
        /// Attribue une valeur spécifique aux _qte des ressources du lot.
        /// </summary>
        /// <param name="lotRessource">Le lot de ressources avec les _qte à attribuer à celui-ci</param>
        public void AttribuerQteRessource(LotRessources lotRessource)
        {
            foreach (RessourceEnum ressource in _ressourcesDict.Keys)
                _ressourcesDict[ressource].qte = lotRessource._ressourcesDict[ressource].qte;
        }

        /// <summary>
        /// Donne accès à la limite minimal d'une ressource.
        /// </summary>
        /// <param name="ressource">Le type de ressource</param>
        /// <returns>La quantité minimal de la ressource</returns>
        public long AccesQteMinRessource(RessourceEnum ressource)
        {
            long qteMin = 0;

            try
            {
                qteMin = _ressourcesDict[ressource].qteMin;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }

            return qteMin;
        }

        /// <summary>
        /// Attribue la valeur minimale (qteMin) aux _qte des ressources du lot.
        /// </summary>
        public void AttribuerQteMinRessource()
        {
            foreach (RessourceEnum ressource in _ressourcesDict.Keys)
                _ressourcesDict[ressource].qte = _ressourcesDict[ressource].qteMin;
        }

        /// <summary>
        /// Donne accès à la limite maximal d'une ressource.
        /// </summary>
        /// <param name="ressource">Le type de ressource</param>
        /// <returns>La quantité maximale de la ressource</returns>
        public long AccesQteMaxRessource(RessourceEnum ressource)
        {
            long qteMax = 0;

            try
            {
                qteMax = _ressourcesDict[ressource].qteMax;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }

            return qteMax;
        }

        /// <summary>
        /// Modifie la limite maximale d'un type de ressource.
        /// </summary>
        /// <param name="ressource">Le type de ressource</param>
        /// <param name="qteMax">La quantité maximale</param>
        public void ModifierLimiteMaxRessource(RessourceEnum ressource, long qteMax)
        {
            try
            {
                _ressourcesDict[ressource].qteMax = qteMax;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La ressource {ressource} n'est pas dans le dictionnaire.");
            }
        }
        #endregion accesseurs_mutateurs

        #region operateurs
        /// <summary>
        /// Additionne les _qte de ressources du lot en entrée à notre lot.
        /// </summary>
        /// <param name="lot">Le lot de _qte à ajouter</param>
        public void AdditionnerQteRessources(LotRessources lot)
        {
            foreach (RessourceEnum ressource in _ressourcesDict.Keys)
            {
                _ressourcesDict[ressource].qte += lot._ressourcesDict[ressource].qte;
            }
        }

        /// <summary>
        /// Crée l'opposé du lot actuel.
        /// Exemple, l'opposé de la _qte 5 est -5.
        /// Util pour transformer un lot de ressource en coût que l'on veut additionner à un autre lot par la suite.
        /// </summary>
        /// <param name="lot">Le lot dont on veut l'opposé</param>
        /// <returns>Un lot de ressource dont les _qte sont opposées de celles du lot actuel</returns>
        public static LotRessources operator -(LotRessources lot)
        {
            LotRessources lotResultant = new LotRessources();
            foreach (RessourceEnum ressource in lot._ressourcesDict.Keys)
                lotResultant._ressourcesDict[ressource].qte = -lot._ressourcesDict[ressource].qte;

            return lotResultant;
        }

        /// <summary>
        /// Compare si le premier lot de ressources est plus petit ou égale au deuxième.
        /// On vient comparer la quantité pour chaque type de ressource.
        /// </summary>
        /// <param name="lot1">Premier lot de ressources</param>
        /// <param name="lot2">Deuxième lot de ressources</param>
        /// <returns>Vrai si la quantité de chaque type de ressource est plus petit ou égale</returns>
        public static bool operator <=(LotRessources lot1, LotRessources lot2)
        {
        foreach ((RessourceEnum batiment, Qte qteRessource) in lot1._ressourcesDict)
            {
            var qteRessourceLot1 = qteRessource.qte;
            var qteResssourceLot2 = lot2._ressourcesDict[batiment].qte;

            if (qteRessourceLot1 > qteResssourceLot2)
                return false;
            }

        return true;
        }

        /// <summary>
        /// Compare si le premier lot de ressources est plus grand ou égale au deuxième.
        /// On vient comparer la quantité pour chaque type de ressource.
        /// </summary>
        /// <param name="lot1">Premier lot de ressources.</param>
        /// <param name="lot2">Deuxième lot de ressources.</param>
        /// <returns>Vrai si la quantité de chaque type de ressource est plus grand ou égale</returns>
        public static bool operator >=(LotRessources lot1, LotRessources lot2)
        {
            foreach ((RessourceEnum batiment, Qte qteRessource) in lot1._ressourcesDict)
            {
                var qteRessourceLot1 = qteRessource.qte;
                var qteResssourceLot2 = lot2._ressourcesDict[batiment].qte;

                if (qteRessourceLot1 < qteResssourceLot2)
                    return false;
            }

            return true;
        }
        #endregion operateurs
    }
}

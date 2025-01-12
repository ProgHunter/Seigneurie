using Batiment;
using System.Collections.Generic;
using UnityEngine;

namespace Profession
{
    public sealed class GestionnaireProfessions
    {
        #region members
        private static readonly GestionnaireProfessions _instance = new();
        private LotProfessions _professions;
        public Dictionary<ProfessionEnum, AbstraitProfessionConfig> ProfessionDictConfig;

        private const long _pcTotalMax = 100;
        private const long _pcTotalMin = 0;
        #endregion members

        public GestionnaireProfessions()
        {
            ProfessionDictConfig = new Dictionary<ProfessionEnum, AbstraitProfessionConfig>
            {
                { ProfessionEnum.NATALITE, new NataliteConfig() },
                { ProfessionEnum.FERMIER,  new FermierConfig()  },
                { ProfessionEnum.BUCHERON, new BucheronConfig() },
                { ProfessionEnum.MINEUR,   new MineurConfig()   },
                { ProfessionEnum.MACON,    new MaconConfig()    }
            };

            _professions = new LotProfessions(ProfessionDictConfig[ProfessionEnum.NATALITE].ProfessionPourcent,
                                              ProfessionDictConfig[ProfessionEnum.FERMIER].ProfessionPourcent,
                                              ProfessionDictConfig[ProfessionEnum.BUCHERON].ProfessionPourcent,
                                              ProfessionDictConfig[ProfessionEnum.MINEUR].ProfessionPourcent,
                                              ProfessionDictConfig[ProfessionEnum.MACON].ProfessionPourcent);
        }

        public static GestionnaireProfessions Instance
        {
            get
            {
                return _instance;
            }
        }

        #region accesseurs_mutateurs
        public LotProfessions Professions => _professions;
        
        /// <summary>
        /// Retourne le pourcentage attribu� � la profession
        /// </summary>
        /// <param name="profession"></param>
        /// <returns></returns>
        public long AccederPourcent(ProfessionEnum profession)
        {
            return _professions.AccesPourcentProfession(profession);
        }

        /// <summary>
        /// Retourne le pourcentage attribu� � la profession
        /// </summary>
        /// <param name="profession"></param>
        /// <returns></returns>
        public float AccederPourcentFraction(ProfessionEnum profession)
        {
            return _professions.AccesPourcentProfessionFraction(profession);
        }

        /// <summary>
        /// Attribut un pourcentage � la profession.
        /// Aucune validation!
        /// </summary>
        /// <param name="profession">La profession que l'ont veut attribuer un pourcentage</param>
        /// <param name="pourcent">Le pourcentage � attribuer</param>
        public void AttribuerPourcent(ProfessionEnum profession, long pourcent)
        {
            _professions.AttribuerPourcentProfession(profession, pourcent);
        }

        /// <summary>
        /// Attribue le pourcentage de chaque profession du lot � celui-ci.
        /// Aucune validation.
        /// </summary>
        /// <param name="pcProfessions">Le lot de professions avec les pourcentages</param>
        public void AttribuerPourcent(LotProfessions pcProfessions)
        {
            _professions.AttribuerPourcentProfession(pcProfessions);
        }

        /// <summary>
        /// Attribuer un pourcentage de population � une profession.
        /// La modification est ajust�e si le pourcentage total de toutes les professions
        /// d�passerait 100% ensemble. Elle l'est aussi si le pourcentage est plus petit que 0.
        /// On ne peut attribuer un pourcentage � une profession verrouill�e.
        /// </summary>
        /// <param name="profession">La profession</param>
        /// <param name="pourcent">Le pourcentage [0,100]</param>
        /// <returns>Vrai si la modification respecte les limites</returns>
        public bool AttribuerPourcentValide(ProfessionEnum profession, long pourcent)
        {
            long pourcentLibre = PourcentPopLibre();
            long pcMin = _professions.AccesPourcentMin(profession);
            bool estValide = true;
            
            if (!EstDeverrouille(profession) || pourcent < pcMin)
            {
                pourcent = pcMin;
                estValide = false;
            } else if ((pourcent - AccederPourcent(profession)) > pourcentLibre)
            {
                pourcent = AccederPourcent(profession) + PourcentPopLibre();
                estValide = false;
            }

            AttribuerPourcent(profession, pourcent);
            return estValide;
        }

        /// <summary>
        /// Attribuer le pourcentage de population d�crit par le lot pour les professions.
        /// La modification est ajust�e si le pourcentage total de toutes les professions
        /// d�passerait 100% ensemble. Elle l'est aussi si le pourcentage est plus petit que 0.
        /// On ne peut attribuer un pourcentage � une profession verrouill�e.
        /// </summary>
        /// <param name="pcProfessions"></param>
        /// <returns>Vrai si les modifications respectent les limites</returns>
        public bool AttribuerPourcentValide(LotProfessions pcProfessions)
        {
            bool estValide = true;
            // Lib�rer la population avant de la r�assigner
            _professions.AttribuerPourcentMin();

            foreach (ProfessionEnum profession in ProfessionDictConfig.Keys)
                estValide &= AttribuerPourcentValide(profession, pcProfessions.AccesPourcentProfession(profession));

            return estValide;
        }
        #endregion accesseurs_mutateurs

        /// <summary>
        /// Incr�mente le pourcentage de 1, max 100%.
        /// </summary>
        /// <param name="profession">La profession � incr�menter le poucentage.</param>
        /// <returns>Vrai si le pourcentage a �t� incr�ment� de 1%.</returns>
        public bool IncrementerPourcent(ProfessionEnum profession)
        {
            long pourcent = AccederPourcent(profession);
            return AttribuerPourcentValide(profession, ++pourcent);
        }

        /// <summary>
        /// D�cr�mente le pourcentage de 1, min 0%.
        /// </summary>
        /// <param name="profession">La profession � incr�menter le poucentage.</param>
        /// <returns>Vrai si le pourcentage a �t� d�cr�ment� de 1%.</returns>
        public bool DecrementerPourcent(ProfessionEnum profession)
        {
            long pourcent = AccederPourcent(profession);
            return AttribuerPourcentValide(profession, --pourcent);
        }

        /// <summary>
        /// Le pourcentage de population sans profession
        /// </summary>
        /// <returns>Le pourcentage de population libre</returns>
        public long PourcentPopLibre()
        {
            long pourcentPopLibre = 100;
            foreach (ProfessionEnum profession in ProfessionDictConfig.Keys)
                pourcentPopLibre -= _professions.AccesPourcentProfession(profession);

            if (pourcentPopLibre > _pcTotalMax || pourcentPopLibre < _pcTotalMin)
                Debug.LogError($"Le pourcentage de population libre est {pourcentPopLibre}, alors qu'il devrait �tre dans [0,100].");

            return pourcentPopLibre;
        }

        /// <summary>
        /// Indique si la profession est d�verrouill�e.
        /// Si le pr�requis de la profession n'est pas rencontr�, celle-ci est verrouill�e.
        /// </summary>
        /// <param name="profession">La profession dont on valide le pr�requis.</param>
        /// <returns>Vrai si la profession est d�verrouill�e.</returns>
        public bool EstDeverrouille(ProfessionEnum profession)
        {
            LotBatiments prerequis;
            try
            {
                prerequis = ProfessionDictConfig[profession].Prerequis;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La profession {profession} n'est pas dans le dictionnaire.");
                return false;
            }

            return GestionnaireBatiments.Instance.PrerequisEstRespecte(prerequis);
        }

        /// <summary>
        /// R�tablie les valeurs (Qte) de la config pour les professions.
        /// </summary>
        public void Reinitialiser()
        {
            _professions = new LotProfessions(ProfessionDictConfig[ProfessionEnum.NATALITE].ProfessionPourcent,
                                              ProfessionDictConfig[ProfessionEnum.FERMIER].ProfessionPourcent,
                                              ProfessionDictConfig[ProfessionEnum.BUCHERON].ProfessionPourcent,
                                              ProfessionDictConfig[ProfessionEnum.MINEUR].ProfessionPourcent,
                                              ProfessionDictConfig[ProfessionEnum.MACON].ProfessionPourcent);
        }
    }
}

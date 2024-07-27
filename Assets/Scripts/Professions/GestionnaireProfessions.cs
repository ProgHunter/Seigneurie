using System;
using System.Collections.Generic;
using UnityEngine;

namespace Profession
{
    public sealed class GestionnaireProfessions
    {
        private static readonly GestionnaireProfessions _instance = new();
        public Dictionary<ProfessionEnum, AbstraitProfessionConfig> professionDict;

        public GestionnaireProfessions()
        {
            professionDict = new Dictionary<ProfessionEnum, AbstraitProfessionConfig>
            {
                { ProfessionEnum.NATALITE, new Natalite() },
                { ProfessionEnum.FERMIER,  new Fermier()  },
                { ProfessionEnum.BUCHERON, new Bucheron() },
                { ProfessionEnum.MINEUR,   new Mineur()   },
                { ProfessionEnum.MACON,    new Macon()    }
            };
        }

        public static GestionnaireProfessions Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Retourne le pourcentage attribué à la profession
        /// </summary>
        /// <param name="profession"></param>
        /// <returns></returns>
        public int AccederPourcent(ProfessionEnum profession)
        {
            int pourcent = 0;

            try
            {
                pourcent = professionDict[profession].professionPourcent;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La profession {profession} n'est pas dans le dictionnaire.");
            }

            return pourcent;
        }

        /// <summary>
        /// Attribut un pourcentage à la profession.
        /// Aucune validation!
        /// </summary>
        /// <param name="profession">La profession que l'ont veut attribuer un pourcentage</param>
        /// <param name="pourcent">Le pourcentage à attribuer</param>
        public void AttribuerPourcent(ProfessionEnum profession, int pourcent)
        {
            try
            {
                professionDict[profession].professionPourcent = pourcent;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La profession {profession} n'est pas dans le dictionnaire.");
            }
        }

        /// <summary>
        /// Attribuer un pourcentage de population à une profession
        /// La modification est ajustée si le pourcentage total de toutes les professions
        /// dépasserait 100% ensemble. Elle l'est aussi si le pourcentage est plus petit que 0.
        /// </summary>
        /// <param name="profession">La profession</param>
        /// <param name="pourcent">Le pourcentage</param>
        /// <returns>Vrai si la modification respecte les lmites</returns>
        public bool AttribuerPourcentValide(ProfessionEnum profession, int pourcent)
        {
            int pourcentLibre = PourcentPopLibre();
            bool estValide = true;
            
            if (pourcent < 0)
            {
                pourcent = 0;
                estValide = false;
            } else if ((pourcent - AccederPourcent(profession)) > pourcentLibre)
            {
                pourcent = PourcentPopLibre();
                estValide = false;
            }

            AttribuerPourcent(profession, pourcent);
            return estValide;
        }

        /// <summary>
        /// Le pourcentage de population sans profession
        /// </summary>
        /// <returns>Le pourcentage de population libre</returns>
        public int PourcentPopLibre()
        {
            int pourcentPopLibre = 100;
            foreach (ProfessionEnum profession in Enum.GetValues(typeof(ProfessionEnum)))
            {
                pourcentPopLibre -= professionDict[profession].professionPourcent;
            }

            return pourcentPopLibre;
        }
    }
}

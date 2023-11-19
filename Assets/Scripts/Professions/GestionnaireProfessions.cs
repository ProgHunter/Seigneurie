using Ressource;
using System;
using System.Collections.Generic;

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
        /// Attribuer un pourcentage de population à une profession
        /// La modification est refusée si le pourcentage total de toutes les professions
        /// dépasserait 100% ensemble. Elle l'est aussi si le pourcentage est plus petit que 0.
        /// </summary>
        /// <param name="profession">La profession</param>
        /// <param name="pourcent">Le pourcentage</param>
        /// <returns>Vrai si la modification est accepté</returns>
        public bool AttribuerPourcent(ProfessionEnum profession, int pourcent)
        {
            int delta = pourcent - professionDict[profession].professionPourcent;
            if (pourcent < 0 || delta > PourcentPopLibre())
                return false;

            professionDict[profession].professionPourcent = pourcent;
            return true;
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

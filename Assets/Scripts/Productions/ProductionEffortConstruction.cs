using UnityEngine;
using Batiment;
using Profession;
using Ressource;

namespace Production
{
    public class ProductionEffortConstruction : AbstraitProduction
    {
        /// <summary>
        /// E(p) = p^e
        /// p : Le nombre de population participant à générer l'effort
        /// e : L'exposant calculé à partir de l'indice de parallélisation
        /// </summary>
        /// <returns>E(p) soit l'effort produit par un nombre de population</returns>
        public override long CalculerProduction()
        {
            if (!GestionnaireBatiments.Instance.ConstructionEstEnCours())
                return 0;

            float professionPourcent = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            long popActuelle = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            long nbPopTravaille = (long)(popActuelle * professionPourcent);
            if (nbPopTravaille <= 0)
                return 0;

            float exposant = CalculerExposantEffortConstruction();
            return (long)(Mathf.Pow(nbPopTravaille, exposant) * EfficacitePourcent / 100);
        }

        /// <summary>
        /// Estime le nombre de ticks restants avant de compléter la construction 
        /// basé sur le nombre de population y travaillant actuellement
        /// </summary>
        /// <returns>
        /// L'estimé du nombre de ticks restants
        /// "-1" si "infini", ex: on ne fournira pas d'effort pour compléter la contruction
        /// </returns>
        public long EstimeNombreTicksRestants()
        {
            long effortRestant = GestionnaireBatiments.Instance.AccesEffortConstructionRestant();
            if (effortRestant <= 0)
                return 0;

            long effortProduit = CalculerProduction();
            if (effortProduit <= 0)
                return -1;

            return Mathf.CeilToInt((float)effortRestant / CalculerProduction());
        }

        /// <summary>
        /// exp = (ln(p) / ln(eff)) + 1
        /// p : est l'indice de parallelisation. 
        /// Exemple si p = 80%, l'exposant retourné permet de générer 80 d'effort avec 100 personnes pour une construction de 100 d'effort.
        /// eff : l'effort total pour une construction
        /// </summary>
        /// <returns>exp soit l'exposant calculé à partir d'un indice de parallelisation pour un effort de construction</returns>
        private float CalculerExposantEffortConstruction()
        {
            float parallelisable = ((Macon)GestionnaireProfessions.Instance.ProfessionDict[ProfessionEnum.MACON]).ParallelisablePourcent;
            long effotConstructionTotal = GestionnaireBatiments.Instance.AccesEffortConstructionTotalEnCours();

            return (Mathf.Log(parallelisable) / Mathf.Log(effotConstructionTotal)) + 1;
        }
    }
}

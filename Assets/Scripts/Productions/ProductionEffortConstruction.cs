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
        public override int CalculerProduction()
        {
            if (!GestionnaireBatiments.Instance.ConstructionEnCours())
                return 0;

            int professionPourcent = GestionnaireProfessions.Instance.professionDict[ProfessionEnum.MACON].professionPourcent;
            int popActuelle = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            int nbPopTravaille = popActuelle * professionPourcent / 100;
            if (nbPopTravaille <= 0)
                return 0;

            float exposant = CalculerExposantEffortConstruction();
            return (int)(Mathf.Pow(nbPopTravaille, exposant) * efficacitePourcent / 100);
        }

        /// <summary>
        /// Estime le nombre de ticks restants avant de compléter la construction 
        /// basé sur le nombre de population y travaillant actuellement
        /// </summary>
        /// <returns>L'estimé du nombre de ticks restants</returns>
        public int EstimeNombreTicksRestants()
        {
            int effortRestant = GestionnaireBatiments.Instance.AccesEffortConstructionRestant();
            if (effortRestant <= 0)
                return 0;

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
            float parallelisable = (float)((Macon)GestionnaireProfessions.Instance.professionDict[ProfessionEnum.MACON]).parallelisablePourcent / 100;
            int effotConstructionTotal = GestionnaireBatiments.Instance.AccesEffortConstructionTotal();

            return (Mathf.Log(parallelisable) / Mathf.Log(effotConstructionTotal)) + 1;
        }
    }
}

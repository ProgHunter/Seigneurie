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
        /// p : Le nombre de population participant � g�n�rer l'effort
        /// e : L'exposant calcul� � partir de l'indice de parall�lisation
        /// </summary>
        /// <param name="professions">Lot de professions à partir duquel on calcule la production.</param>
        /// <returns>E(p) soit l'effort produit par un nombre de population</returns>
        public override long CalculerProduction(LotProfessions professions)
        {
            if (!GestionnaireBatiments.Instance.ConstructionEstEnCours())
                return 0;

            float professionPourcent = professions.AccesPourcentProfessionFraction(ProfessionEnum.MACON);
            long popActuelle = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            long nbPopTravaille = (long)(popActuelle * professionPourcent);
            if (nbPopTravaille <= 0)
                return 0;

            float exposant = CalculerExposantEffortConstruction();
            long result = (long)(Mathf.Pow(nbPopTravaille, exposant) * EfficacitePourcent);
            return result;
        }

        /// <summary>
        /// Estime le nombre de ticks restants avant de compl�ter la construction 
        /// bas� sur le nombre de population y travaillant actuellement
        /// </summary>
        /// <param name="pourcentMacon">Le nombre de maçons qui travailleraient sur le batiment.</param>
        /// <returns>
        /// L'estim� du nombre de ticks restants
        /// "-1" si "infini", ex: on ne fournira pas d'effort pour compl�ter la contruction
        /// </returns>
        public long EstimeNombreTicksRestants(long pourcentMacon = -1)
        {
            // Si le pourcentage de macons n'est pas explicitement fourni, on prend celui actuel dans
            // le gestionnaire de professions.
            LotProfessions professions = pourcentMacon == -1 ? 
                GestionnaireProfessions.Instance.Professions : 
                new LotProfessions(0, 0, 0, 0, pourcentMacon);
                    
            long effortRestant = GestionnaireBatiments.Instance.AccesEffortConstructionRestant();
            if (effortRestant <= 0)
                return 0;

            long effortProduit = CalculerProduction(professions);
            if (effortProduit <= 0)
                return -1;

            return Mathf.CeilToInt((float)effortRestant / effortProduit);
        }

        /// <summary>
        /// exp = (ln(p) / ln(eff)) + 1
        /// p : est l'indice de parallelisation. 
        /// Exemple si p = 80%, l'exposant retourn� permet de g�n�rer 80 d'effort avec 100 personnes pour une construction de 100 d'effort.
        /// eff : l'effort total pour une construction
        /// </summary>
        /// <returns>exp soit l'exposant calcul� � partir d'un indice de parallelisation pour un effort de construction</returns>
        private float CalculerExposantEffortConstruction()
        {
            float parallelisable = ((MaconConfig)GestionnaireProfessions.Instance.ProfessionDictConfig[ProfessionEnum.MACON]).ParallelisablePourcent;
            long effotConstructionTotal = GestionnaireBatiments.Instance.AccesEffortConstructionTotalEnCours();

            return (Mathf.Log(parallelisable) / Mathf.Log(effotConstructionTotal)) + 1;
        }
    }
}

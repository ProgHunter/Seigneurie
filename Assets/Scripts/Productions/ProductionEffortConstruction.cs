using UnityEngine;
using Batiment;
using Profession;
using Ressource;

namespace Production
{
    public class ProductionEffortConstruction
    {
        /// <summary>
        /// <see cref="EfficacitePourcent"/> 
        /// Efficacité de la production.
        /// Sert purement de configuration pour balancer la production dans le jeu. Set à 1 par défaut.
        /// </summary>
        private float _efficacitePourcent = 1.0f;

        public float EfficacitePourcent { get => _efficacitePourcent; protected set => _efficacitePourcent = value; }

        /// <summary>
        /// E(p) = p^e
        /// p : Le nombre de population participant é générer l'effort
        /// e : L'exposant calculé é partir de l'indice de parallélisation
        /// </summary>
        /// <param name="professions">Lot de professions à partir duquel on calcule la production.</param>
        /// <param name="batiment">Le type de bâtiment pour lequel on veut l'effort de contruction produit.
        /// Laisser NUL si on veut le calcul pour la construction en cours, sinon on effectue le calcul pour
        /// le bâtiment spécifié.</param>
        /// <returns>E(p) soit l'effort produit par un nombre de population</returns>
        public long CalculerProduction(LotProfessions professions, BatimentEnum batiment = BatimentEnum.NUL)
        {
            if (batiment == BatimentEnum.NUL && !GestionnaireBatiments.Instance.ConstructionEstEnCours())
                return 0;

            float professionPourcent = professions.AccesPourcentProfessionFraction(ProfessionEnum.MACON);
            long popActuelle = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            long nbPopTravaille = (long)(popActuelle * professionPourcent);
            if (nbPopTravaille <= 0)
                return 0;

            float exposant = CalculerExposantEffortConstruction(batiment);
            long result = (long)(Mathf.Pow(nbPopTravaille, exposant) * EfficacitePourcent);
            return result;
        }

        /// <summary>
        /// Evalue le nombre de ticks restants à une construction selon le pourcent de maçons qui seront attribués.
        /// </summary>
        /// <param name="pourcentMacon">Le pourcentage de la population qui travaillera sur la profession de maçon. [0, 100]
        /// Laisser -1 si on veut évaluer une construction avec le pourcentage de maçons actuel.</param>
        /// <param name="batiment">Le type de bâtiment dont on veut le nombre de ticks que prendrait sa contruction total.
        /// Laisser NUL si on veut l'information de la construction en cours, sinon on effectue le calcul sur l'effort total
        /// du bâtiment spécifié.</param>
        /// <returns>Le nombre de ticks estimés pour la complétion du bâtiment.
        /// "-1" si "infini", ex: on ne fournira pas d'effort pour compléter la contruction
        /// </returns>
        public long EstimeNombreTicksRestants(long pourcentMacon = -1, BatimentEnum batiment = BatimentEnum.NUL)
        {
            // Si le pourcentage de macons n'est pas explicitement fourni, on prend celui actuel dans
            // le gestionnaire de professions.
            LotProfessions professions = pourcentMacon == -1 ? 
                GestionnaireProfessions.Instance.Professions : 
                new LotProfessions(0, 0, 0, 0, pourcentMacon);

            // Si ConstructionEnCours, on retourne l'information pour la construction en cours, sinon
            // pour le bâtiment mentionné en param.
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            long effortRestant = batiment == BatimentEnum.NUL ? gestionnaireBatiments.AccesEffortConstructionRestant() :
               gestionnaireBatiments.AccesEffortConstructionTotal(batiment);
            if (effortRestant <= 0)
                return 0;

            long effortProduit = CalculerProduction(professions, batiment);
            if (effortProduit <= 0)
                return -1;

            return Mathf.CeilToInt((float)effortRestant / effortProduit);
        }

        /// <summary>
        /// exp = (ln(p) / ln(eff)) + 1
        /// p : est l'indice de parallelisation. 
        /// Exemple si p = 80%, l'exposant retourné permet de générer 80 d'effort avec 100 personnes pour une construction de 100 d'effort.
        /// eff : l'effort total pour une construction
        /// </summary>
        /// <param name="batiment">L'effort total pour ce type de bâtiment sera pris pour calculer l'exposant.
        /// Laisser NUL si on veut le calcul sur l'effort total du bâtiment en cours de construction, sinon on effectue le calcul pour
        /// le bâtiment spécifié.</param>
        /// <returns>exp soit l'exposant calculé à partir d'un indice de parallelisation pour un effort de construction</returns>
        private float CalculerExposantEffortConstruction(BatimentEnum batiment = BatimentEnum.NUL)
        {
            float parallelisable = ((MaconConfig)GestionnaireProfessions.Instance.ProfessionDictConfig[ProfessionEnum.MACON]).ParallelisablePourcent;
            long effotConstructionTotal = batiment == BatimentEnum.NUL ? GestionnaireBatiments.Instance.AccesEffortConstructionTotalEnCours() :
               GestionnaireBatiments.Instance.AccesEffortConstructionTotal(batiment);

            return (Mathf.Log(parallelisable) / Mathf.Log(effotConstructionTotal)) + 1;
        }
    }
}

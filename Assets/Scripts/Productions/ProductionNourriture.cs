using Batiment;
using Profession;
using Ressource;

namespace Production
{
    public class ProductionNouriture : AbstraitProduction
    {
        /// <summary>
        /// Production des fermiers avec le bonus des fermes moins la consommation de la population.
        /// Peut être néguative si la consommation de la population dépasse la production.
        /// </summary>
        /// <returns>Production net de nouriture</returns>
        public override int CalculerProduction()
        {
            return ProductionFermiers() - ConsommationPopulation();
        }

        private int ProductionFermiers()
        {
            // Calcul de l'effort des fermiers
            int professionPourcent = GestionnaireProfessions.Instance.professionDict[ProfessionEnum.FERMIER].professionPourcent;
            int popActuelle = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            int nbPopTravaille = popActuelle * professionPourcent / 100;
            if (nbPopTravaille <= 0)
                return 0;
            // Calcul du bonus de production des fermes
            float bonusFermes = (float)((FermeConfig)GestionnaireBatiments.Instance.batimentConfigDict[BatimentEnum.FERME]).bonusProductionPourcent / 100;
            int nbFermes = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.FERME);
            bonusFermes = bonusFermes * nbFermes + 1;

            return (int)(nbPopTravaille * efficacitePourcent / 100 * bonusFermes);
        }

        private int ConsommationPopulation()
        {
            int popActuelle = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            int indiceDeFaim = ((PopulationConfig)InventaireRessources.Instance.ressourceConfigDict[RessourceEnum.POPULATION]).faimPourcent;

            return popActuelle * indiceDeFaim / 100;
        }
    }
}

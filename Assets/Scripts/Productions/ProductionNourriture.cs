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
        public override long CalculerProduction()
        {
            return ProductionFermiers() - ConsommationPopulation();
        }

        /// <summary>
        /// Calcul le pourcentage de la consommation qui ne peut être satisfait par la nourriture disponible.
        /// Ex: On a 1000 de nourriture, on produit 500, et on consomme 2000 => Il manque 25 pourcent de la consommation.
        /// </summary>
        /// <returns>Le pourcentage de la consommation non couvert.</returns>
        public int CalculManqueNourriturePourcent()
        {
            long consommation = ConsommationPopulation();
            long nourritureDisponible = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE) + ProductionFermiers() - consommation;

            long nourritureManquante = nourritureDisponible >= 0 ? 0 : -nourritureDisponible;

            return (int)(nourritureManquante * 100 / consommation);
        }

        private long ProductionFermiers()
        {
            // Calcul de l'effort des fermiers
            long professionPourcent = GestionnaireProfessions.Instance.professionDict[ProfessionEnum.FERMIER].professionPourcent;
            long popActuelle = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            long nbPopTravaille = popActuelle * professionPourcent / 100;
            if (nbPopTravaille <= 0)
                return 0;
            // Calcul du bonus de production des fermes
            float bonusFermes = (float)((FermeConfig)GestionnaireBatiments.Instance.batimentConfigDict[BatimentEnum.FERME]).bonusProductionPourcent / 100;
            long nbFermes = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.FERME);
            bonusFermes = bonusFermes * nbFermes + 1;

            return (long)(nbPopTravaille * efficacitePourcent * bonusFermes / 100);
        }

        private long ConsommationPopulation()
        {
            long popActuelle = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            long indiceDeFaim = ((PopulationConfig)InventaireRessources.Instance.ressourceConfigDict[RessourceEnum.POPULATION]).faimPourcent;

            return popActuelle * indiceDeFaim / 100;
        }
    }
}

using Batiment;
using Profession;
using Ressource;

namespace Production
{
    public class ProductionBois : AbstraitProduction
    {
        /// <summary>
        /// Production des bucherons avec le bonus des scieries.
        /// </summary>
        /// <returns>Production de bois</returns>
        public override long CalculerProduction()
        {
            // Calcul de l'effort des bucherons
            float professionPourcent = GestionnaireProfessions.Instance.professionDict[ProfessionEnum.BUCHERON].professionPourcent;
            long popActuelle = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            long nbPopTravaille = (long)(popActuelle * professionPourcent);
            if (nbPopTravaille <= 0)
                return 0;
            // Calcul du bonus de production des scieries
            float bonusScieries = ((ScierieConfig)GestionnaireBatiments.Instance.batimentConfigDict[BatimentEnum.SCIERIE]).bonusProductionPourcent;
            long nbScieries = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.SCIERIE);
            bonusScieries = bonusScieries * nbScieries + 1;

            return (long)(nbPopTravaille * efficacitePourcent * bonusScieries);
        }
    }
}

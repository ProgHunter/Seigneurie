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
        public override int CalculerProduction()
        {
            // Calcul de l'effort des bucherons
            int professionPourcent = GestionnaireProfessions.Instance.professionDict[ProfessionEnum.BUCHERON].professionPourcent;
            int popActuelle = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            int nbPopTravaille = popActuelle * professionPourcent / 100;
            if (nbPopTravaille <= 0)
                return 0;
            // Calcul du bonus de production des scieries
            float bonusScieries = (float)((ScierieConfig)GestionnaireBatiments.Instance.batimentConfigDict[BatimentEnum.SCIERIE]).bonusProductionPourcent / 100;
            int nbFermes = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.SCIERIE);
            bonusScieries = bonusScieries * nbFermes + 1;

            return (int)(nbPopTravaille * efficacitePourcent / 100 * bonusScieries);
        }
    }
}

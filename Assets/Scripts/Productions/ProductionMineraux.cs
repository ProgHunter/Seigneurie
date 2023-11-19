using Batiment;
using Profession;
using Ressource;

namespace Production
{
    public class ProductionMineraux : AbstraitProduction
    {
        /// <summary>
        /// Production des mineurs avec le bonus des mines.
        /// </summary>
        /// <returns>Production de minéraux</returns>
        public override int CalculerProduction()
        {
            // Calcul de l'effort des mineurs
            int professionPourcent = GestionnaireProfessions.Instance.professionDict[ProfessionEnum.MINEUR].professionPourcent;
            int popActuelle = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            int nbPopTravaille = popActuelle * professionPourcent / 100;
            if (nbPopTravaille <= 0)
                return 0;
            // Calcul du bonus de production des mines
            float bonusMines = (float)((MineConfig)GestionnaireBatiments.Instance.batimentConfigDict[BatimentEnum.MINE]).bonusProductionPourcent / 100;
            int nbFermes = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MINE);
            bonusMines = bonusMines * nbFermes + 1;

            return (int)(nbPopTravaille * efficacitePourcent / 100 * bonusMines);
        }
    }
}

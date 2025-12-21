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
        /// <param name="professions">Lot de professions à partir duquel on calcule la production.</param>
        /// <returns>Production de minéraux</returns>
        public override long CalculerProduction(LotProfessions professions)
        {
            // Calcul de l'effort des mineurs
            float professionPourcent = professions.AccesPourcentProfessionFraction(ProfessionEnum.MINEUR);
            long popActuelle = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            long nbPopTravaille = (long)(popActuelle * professionPourcent);
            if (nbPopTravaille <= 0)
                return 0;
            // Calcul du bonus de production des mines
            float bonusMines = ((MineConfig)GestionnaireBatiments.Instance.BatimentConfigDict[BatimentEnum.MINE]).BonusProductionPourcent;
            long nbMines = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MINE);
            bonusMines = bonusMines * nbMines + 1;

            return (long)(nbPopTravaille * EfficacitePourcent * bonusMines);
        }
    }
}

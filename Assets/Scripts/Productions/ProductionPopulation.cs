using Batiment;
using Ressource;
using Utils;
using Profession;
using System;

namespace Production
{
    public class ProductionPopulation : AbstraitProduction
    {
        public override long CalculerProduction()
        {
            float professionPourcent = GestionnaireProfessions.Instance.professionDict[ProfessionEnum.NATALITE].professionPourcent;
            return (long)(CalculerCroissance() * efficacitePourcent - CalculerMortaliteFamine());
        }

        private long CalculerCroissance()
        {
            long capaciteMax = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MAISON) * ((MaisonConfig)GestionnaireBatiments.Instance.batimentConfigDict[BatimentEnum.MAISON]).capacite;
            long popActuelle = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            long popActive = (long)(popActuelle * GestionnaireProfessions.Instance.professionDict[ProfessionEnum.NATALITE].professionPourcent);

            // Valider si nos nombres sont positifs et si la population actuelle n'est pas déjà presqu'à notre capacité ou plus grand
            if (capaciteMax < 0 || popActive <= 0 || popActive + 1 >= capaciteMax)
                return 0;

            long popMin = InventaireRessources.Instance.ressourceConfigDict[RessourceEnum.POPULATION].QteMin;
            popMin = popMin < 1 ? 1 : popMin;

            float croissance = ((PopulationConfig)InventaireRessources.Instance.ressourceConfigDict[RessourceEnum.POPULATION]).croissancePourcent;
            if (croissance <= 0)
                return 0;

            // Calculer le "tick présent" selont la formule avec capaciteMax qui a peut-être changé (possiblement non entier).
            double tick = MathUtils.FonctionLogistiqueTickIsole(popActive, popMin, capaciteMax, croissance);

            long nbPopCroissance = (long)Math.Ceiling(MathUtils.FonctionLogistique(tick + 1, popMin, capaciteMax, croissance)) - popActive;

            return nbPopCroissance > 0 ? nbPopCroissance : 0;
        }

        /// <summary>
        /// Calcul la quantité de population qui meurt de famine.
        /// Formule: Pop qui meurt = pop actuelle * pourcent manque nourriture * taux de mortalité en cas de famine
        /// </summary>
        /// <returns>Nombre de population qui meurt.</returns>
        private long CalculerMortaliteFamine()
        {
            ProductionNouriture productionNouriture = new ProductionNouriture();
            float manqueNourriturePourcent = productionNouriture.CalculManqueNourriturePourcent();
            long nbPop = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            float mortaliteFaminePc = ((PopulationConfig)InventaireRessources.Instance.ressourceConfigDict[RessourceEnum.POPULATION]).mortaliteFaminePourcent;

            return (long)(nbPop * mortaliteFaminePc * manqueNourriturePourcent);
        }
    }
}

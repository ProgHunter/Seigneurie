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
            int professionPourcent = GestionnaireProfessions.Instance.professionDict[ProfessionEnum.NATALITE].professionPourcent;
            return CalculerCroissance() * professionPourcent * efficacitePourcent / 10000 - CalculerMortaliteFamine();
        }

        private long CalculerCroissance()
        {
            long capaciteMax = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MAISON) * ((MaisonConfig)GestionnaireBatiments.Instance.batimentConfigDict[BatimentEnum.MAISON]).capacite;
            long popActuelle = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            // Valider si nos nombres sont positifs et si la population actuelle n'est pas déjà presqu'à notre capacité ou plus grand
            if (capaciteMax < 0 || popActuelle < 0 || popActuelle + 1 >= capaciteMax)
                return 0;

            long popMin = InventaireRessources.Instance.ressourceConfigDict[RessourceEnum.POPULATION].qteMin;
            popMin = popMin < 1 ? 1 : popMin;

            int croissancePc = ((PopulationConfig)InventaireRessources.Instance.ressourceConfigDict[RessourceEnum.POPULATION]).croissancePourcent;
            if (croissancePc <= 0)
                return 0;

            float croissance = (float)croissancePc / 100;
            // Calculer le "tick présent" selont la formule avec capaciteMax qui a peut-être changé (possiblement non entier).
            double tick = MathUtils.FonctionLogistiqueTickIsole(popActuelle, popMin, capaciteMax, croissance);

            long nbPopCroissance = (long)Math.Ceiling(MathUtils.FonctionLogistique(tick + 1, popMin, capaciteMax, croissance)) - popActuelle;

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
            long manqueNourriturePourcent = productionNouriture.CalculManqueNourriturePourcent();

            return (InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION) * ((PopulationConfig)InventaireRessources.Instance.ressourceConfigDict[RessourceEnum.POPULATION]).mortaliteFaminePourcent * manqueNourriturePourcent) / 1000;
        }
    }
}

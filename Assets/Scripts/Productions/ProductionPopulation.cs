using System;
using Batiment;
using Production;
using Profession;
using Ressource;
using Utils;

namespace Productions
{
    public class ProductionPopulation : AbstraitProduction
    {
        public override long CalculerProduction(LotProfessions professions)
        {
            float professionPourcent = professions.AccesPourcentProfessionFraction(ProfessionEnum.NATALITE);
            float pourcentFermiers = professions.AccesPourcentProfessionFraction(ProfessionEnum.FERMIER);
            long croissance = (long)(CalculerCroissance(professionPourcent) * EfficacitePourcent);
            long mortalite = CalculerMortaliteFamine(pourcentFermiers);
            
            var resultat = croissance - mortalite;
            return resultat;
        }

        private long CalculerCroissance(float professionPourcent)
        {
            long capaciteMax = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MAISON) * ((MaisonConfig)GestionnaireBatiments.Instance.BatimentConfigDict[BatimentEnum.MAISON]).Capacite;
            long popActuelle = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            long popActive = (long)(popActuelle * professionPourcent);

            long popMin = GestionnaireRessources.Instance.AccesQteMinRessource(RessourceEnum.POPULATION);
            popMin = popMin < 1 ? 1 : popMin;

            // Valider si nos nombres sont positifs et si la population actuelle n'est pas déjà presqu'à notre capacité ou plus grand
            if (capaciteMax < 0 || popActive <= popMin || popActuelle >= capaciteMax)
                return 0;

            float croissance = ((PopulationConfig)GestionnaireRessources.Instance.RessourceConfigDict[RessourceEnum.POPULATION]).CroissancePourcent;
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
        /// <param name="pourcentFermiers">Pourcentage de la population attribué à la profession de
        /// fermier pour calculer la production de nouriture.</param>
        /// <returns>Nombre de population qui meurt.</returns>
        private long CalculerMortaliteFamine(float pourcentFermiers)
        {
            ProductionNouriture productionNouriture = new ProductionNouriture();
            float manqueNourriturePourcent = productionNouriture.CalculManqueNourriturePourcent(pourcentFermiers);
            long nbPop = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            float mortaliteFaminePc = ((PopulationConfig)GestionnaireRessources.Instance.RessourceConfigDict[RessourceEnum.POPULATION]).MortaliteFaminePourcent;

            long resulat = (long)(nbPop * mortaliteFaminePc * manqueNourriturePourcent);
            return resulat;
        }
    }
}

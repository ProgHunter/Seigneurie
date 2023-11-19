using UnityEngine;
using Batiment;
using Ressource;
using Utils;
using Profession;

namespace Production
{
    public class ProductionPopulation : AbstraitProduction
    {
        public override int CalculerProduction()
        {
            int professionPourcent = GestionnaireProfessions.Instance.professionDict[ProfessionEnum.NATALITE].professionPourcent;
            return CalculerCroissance() * professionPourcent * efficacitePourcent / 10000 - CalculerMortaliteFamine();
        }

        private int CalculerCroissance()
        {
            int capaciteMax = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MAISON) * ((MaisonConfig)GestionnaireBatiments.Instance.batimentConfigDict[BatimentEnum.MAISON]).capacite;
            int popActuelle = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            // Valider si nos nombres sont positifs et si la population actuelle n'est pas déjà presqu'à notre capacité ou plus grand
            if (capaciteMax < 0 || popActuelle < 0 || popActuelle + 1 >= capaciteMax)
                return 0;

            int nbPopBase = InventaireRessources.Instance.ressourceConfigDict[RessourceEnum.POPULATION].qteBase;
            float croissance = (float)((PopulationConfig)InventaireRessources.Instance.ressourceConfigDict[RessourceEnum.POPULATION]).croissancePourcent / 100;
            // Calculer le "tick présent" selont la formule avec capaciteMax qui a peut-être changé (possiblement non entier).
            double tick = MathUtils.FonctionLogistiqueTickIsole(popActuelle, nbPopBase, capaciteMax, croissance);

            return Mathf.Max(MathUtils.FonctionLogistique(tick + 1, nbPopBase, capaciteMax, croissance) - popActuelle, 0);
        }

        private int CalculerMortaliteFamine()
        {
            int besoinNourriture = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION) * ((PopulationConfig)InventaireRessources.Instance.ressourceConfigDict[RessourceEnum.POPULATION]).faimPourcent / 100;
            
            int manqueNourriturePourcent = 100 - (InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE) * 100 / besoinNourriture);
            manqueNourriturePourcent = manqueNourriturePourcent < 0 ? 0 : manqueNourriturePourcent;

            int pertePopFamine = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION) * ((PopulationConfig)InventaireRessources.Instance.ressourceConfigDict[RessourceEnum.POPULATION]).mortaliteFaminePourcent / 100;

            return (pertePopFamine * manqueNourriturePourcent) / 100;
        }
    }
}

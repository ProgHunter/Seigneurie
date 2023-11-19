using Ressource;

namespace Batiment
{
    public class MaisonConfig : AbstraitBatimentConfig
    {
        /// <summary>
        /// <see cref="capacite"/> Indique � quel point une maison contribue � la croissance de la population. 
        ///                        La population ne pourra jamais cro�tre et exc�der cette valeur multipli�e par le nombre de maisons.
        /// </summary>
        public int capacite;
        public MaisonConfig() : base()
        {
            id = "B0_Maison";
            nom = "Maison";
            icone = null;  // TODO
            description = "Permet d'augmenter la croissance de la population.";
            coutConstruction = new LotRessources(0, 0, 10, 5);  // TODO: Mettres les valeurs dans un fichier de config
            effortConstruction = 5;
            capacite = 10;
        }
    }
}

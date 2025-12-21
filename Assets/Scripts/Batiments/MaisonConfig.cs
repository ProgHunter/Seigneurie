using Ressource;
using Utils;

namespace Batiment
{
    public class MaisonConfig : AbstraitBatimentConfig
    {
        #region membres
        /// <summary>
        /// <see cref="Capacite"/> Indique de combien une maison contribue à la limite max de population, impactant ainsi sa croissance. 
        ///                        La population ne pourra jamais croître et excéder cette valeur multipliée par le nombre de maisons.
        /// </summary>
        private int _capacite;
        #endregion membres

        public MaisonConfig() : base()
        {
            Id = "B0_Maison";
            Nom = "Pâté de maisons";
            Icone = null;  // TODO
            Qte = new Qte(10 /*Qte de base*/, 1000 /*Max*/, 1 /*Min*/);
            CoutConstruction = new LotRessources(0, 0, 100, 50);  // TODO: Mettres les valeurs dans un fichier de config
            EffortConstruction = 100;
            Capacite = 100;
            Prerequis = new LotBatiments();  // Aucun prérequis
            Description = "Permet d'augmenter la croissance de la population. \n" +
                          $"Un {Nom} peut héberger jusqu'à {Capacite} personnes.";
        }

        public int Capacite { get => _capacite; private set => _capacite = value; }
    }
}

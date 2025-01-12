using Ressource;
using Utils;

namespace Batiment
{
    public class MaisonConfig : AbstraitBatimentConfig
    {
        #region membres
        /// <summary>
        /// <see cref="Capacite"/> Indique à quel point une maison contribue à la croissance de la population. 
        ///                        La population ne pourra jamais croître et excéder cette valeur multipliée par le nombre de maisons.
        /// </summary>
        private int _capacite;
        #endregion membres

        public MaisonConfig() : base()
        {
            Id = "B0_Maison";
            Nom = "Maison";
            Icone = null;  // TODO
            Qte = new Qte(10 /*Qte de base*/, 1000 /*Max*/, 1 /*Min*/);
            CoutConstruction = new LotRessources(0, 0, 10, 5);  // TODO: Mettres les valeurs dans un fichier de config
            EffortConstruction = 5;
            Capacite = 10;
            Prerequis = new LotBatiments();  // Aucun prérequis
            Description = "Permet d'augmenter la croissance de la population. \n" +
                          $"Une {Nom} peut héberger jusqu'à {Capacite} personnes.";
        }

        public int Capacite { get => _capacite; private set => _capacite = value; }
    }
}

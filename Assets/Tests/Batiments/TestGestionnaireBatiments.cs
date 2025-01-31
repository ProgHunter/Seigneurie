using NUnit.Framework;
using Batiment;
using Ressource;
using Profession;

namespace Test
{
    public class TestGestionnaireBatiments
    {
        [SetUp]
        public void SetUp()
        {
            // Attribuer les valeurs des configs
            GestionnaireProfessions.Instance.Reinitialiser();
            GestionnaireRessources.Instance.Reinitialiser();
            GestionnaireBatiments.Instance.Reinitialiser();
        }

        [Test]
        public void TestAttribuerEtAccesQteMaison()
        {
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var qteAttribuee = 2;
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.MAISON, qteAttribuee);

            var qteRetour = gestionnaireBatiments.AccesQteBatiment(BatimentEnum.MAISON);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteFerme()
        {
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var qteAttribuee = 2;
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.FERME, qteAttribuee);

            var qteRetour = gestionnaireBatiments.AccesQteBatiment(BatimentEnum.FERME);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteScierie()
        {
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var qteAttribuee = 2;
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.SCIERIE, qteAttribuee);

            var qteRetour = gestionnaireBatiments.AccesQteBatiment(BatimentEnum.SCIERIE);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteMine()
        {
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var qteAttribuee = 2;
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.MINE, qteAttribuee);

            var qteRetour = gestionnaireBatiments.AccesQteBatiment(BatimentEnum.MINE);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteHotelDeVille()
        {
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var qteAttribuee = 2;
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.HOTELDEVILLE, qteAttribuee);

            var qteRetour = gestionnaireBatiments.AccesQteBatiment(BatimentEnum.HOTELDEVILLE);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteMaxMaison()
        {
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var qteAttribuee = 3;
            gestionnaireBatiments.ModifierLimiteMaxBatiment(BatimentEnum.MAISON, qteAttribuee);

            var qteRetour = gestionnaireBatiments.AccesQteMaxBatiment(BatimentEnum.MAISON);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestConstruction3Maisons2Max()
        {
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var qteDebut = 0;
            var qteMax = 2;
            var coutConstruction = GestionnaireBatiments.Instance.AccesCoutBatiment(BatimentEnum.MAISON);
            coutConstruction.AdditionnerQteRessources(coutConstruction);
            coutConstruction.AdditionnerQteRessources(coutConstruction); // Coût de 3 maisons

            // Initialisation des données du test
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.MAISON, qteDebut);
            gestionnaireBatiments.ModifierLimiteMaxBatiment(BatimentEnum.MAISON, qteMax);
            GestionnaireRessources.Instance.AttribuerQteRessource(coutConstruction);
            gestionnaireBatiments.AnnulerConstruction();

            // Aucune construction en cours
            var effortRestant = gestionnaireBatiments.AccesEffortConstructionRestant();
            Assert.AreEqual(0, effortRestant);

            // Démarrer la première construction
            var retour = gestionnaireBatiments.DemarrerConstruction(BatimentEnum.MAISON);
            Assert.IsTrue(retour);

            // La construction n'est pas fini
            var effortTot = gestionnaireBatiments.AccesEffortConstructionTotal(BatimentEnum.MAISON);
            effortRestant = gestionnaireBatiments.AccesEffortConstructionRestant();
            Assert.AreEqual(effortTot, effortRestant);

            // Construction en cours avancée
            retour = gestionnaireBatiments.AvancerConstruction(effortTot / 2);
            effortRestant = gestionnaireBatiments.AccesEffortConstructionRestant();
            Assert.IsTrue(effortRestant <= ((effortTot / 2) + 1));
            Assert.IsTrue(!retour);

            // Une seul construction à la fois
            retour = gestionnaireBatiments.DemarrerConstruction(BatimentEnum.MAISON);
            Assert.IsFalse(retour);

            // Construction complétée
            retour = gestionnaireBatiments.AvancerConstruction((effortTot / 2) + 1);
            Assert.IsTrue(retour);

            // On devrait avoir une maison de plus
            var qte = gestionnaireBatiments.AccesQteBatiment(BatimentEnum.MAISON);
            Assert.AreEqual(qteDebut + 1, qte);

            // La première construction devrait être fini, une nouvelle peut être lancée
            retour = gestionnaireBatiments.DemarrerConstruction(BatimentEnum.MAISON);
            Assert.IsTrue(retour);

            // La 2e construction est complétée
            retour = gestionnaireBatiments.AvancerConstruction(effortTot);
            Assert.IsTrue(retour);

            // On est au Maximum de maison, le démarrage de la 3e construction est refusé
            retour = gestionnaireBatiments.DemarrerConstruction(BatimentEnum.MAISON);
            Assert.IsFalse(retour);

            // On ne peut avancer si aucune construction n'est en cours
            retour = gestionnaireBatiments.AvancerConstruction(effortTot);
            Assert.IsFalse(retour);

            // On est toujours au maximum de construction
            qte = gestionnaireBatiments.AccesQteBatiment(BatimentEnum.MAISON);
            Assert.AreEqual(qteMax, qte);
        }

        [Test]
        public void TestConstructionMaisonsAvecEtSansRessources()
        {
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var qteDebut = 0;
            var qteMax = 2;
            var coutConstruction = gestionnaireBatiments.AccesCoutBatiment(BatimentEnum.MAISON);

            // Initialisation des données du test
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.MAISON, qteDebut);
            gestionnaireBatiments.ModifierLimiteMaxBatiment(BatimentEnum.MAISON, qteMax);
            GestionnaireRessources.Instance.AttribuerQteRessource(coutConstruction);
            gestionnaireBatiments.AnnulerConstruction();

            // Démarrer la première construction
            var retour = gestionnaireBatiments.DemarrerConstruction(BatimentEnum.MAISON);
            Assert.IsTrue(retour);

            var effortTot = gestionnaireBatiments.AccesEffortConstructionTotal(BatimentEnum.MAISON);
            // Construction complétée
            retour = gestionnaireBatiments.AvancerConstruction(effortTot + 1);
            Assert.IsTrue(retour);

            // On devrait avoir une maison de plus
            var qte = gestionnaireBatiments.AccesQteBatiment(BatimentEnum.MAISON);
            Assert.AreEqual(qteDebut + 1, qte);

            // La première construction devrait être fini, une nouvelle peut être lancé,
            // mais on avait juste les ressource pour une.
            retour = gestionnaireBatiments.DemarrerConstruction(BatimentEnum.MAISON);
            Assert.IsFalse(retour);

            // On ne peut avancer si aucune construction n'est en cours
            retour = gestionnaireBatiments.AvancerConstruction(effortTot + 1);
            Assert.IsFalse(retour);

            // On est toujours à +1 maison et non +2
            qte = gestionnaireBatiments.AccesQteBatiment(BatimentEnum.MAISON);
            Assert.AreEqual(qteDebut + 1, qte);
        }

        [Test]
        public void TestConstructionBatimentVerrouille()
        {
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var qteMaisons = 0L;
            var qteFermes = 0L;
            var qteHDV = 0L;
            var coutConstruction = gestionnaireBatiments.AccesCoutBatiment(BatimentEnum.HOTELDEVILLE);
            // Initialisation des données du test avec 0 bâtiments
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.MAISON, qteMaisons);
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.FERME, qteFermes);
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.HOTELDEVILLE, qteHDV);
            GestionnaireRessources.Instance.AttribuerQteRessource(coutConstruction);
            gestionnaireBatiments.AnnulerConstruction();

            // Essayer de démarrer la contruction de l'hotel de ville alors qu'elle est verrouillée
            // Il faut minimalement 10 maisons et 1 ferme pour la déverrouiller
            var retour = gestionnaireBatiments.DemarrerConstruction(BatimentEnum.HOTELDEVILLE);
            Assert.IsFalse(retour);

            // Cas où il manque encore 1 maison
            var prerequisHDV = gestionnaireBatiments.AccesPrerequis(BatimentEnum.HOTELDEVILLE);
            qteMaisons = prerequisHDV.AccesQteBatiment(BatimentEnum.MAISON) - 1;
            qteFermes = prerequisHDV.AccesQteBatiment(BatimentEnum.FERME) + 1;
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.MAISON, qteMaisons);
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.FERME, qteFermes);

            retour = gestionnaireBatiments.DemarrerConstruction(BatimentEnum.HOTELDEVILLE);
            Assert.IsFalse(retour);

            // Cas où on a exactement le prérequis pour construire l'hotel de ville
            qteMaisons = prerequisHDV.AccesQteBatiment(BatimentEnum.MAISON);
            qteFermes = prerequisHDV.AccesQteBatiment(BatimentEnum.FERME);
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.MAISON, qteMaisons);
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.FERME, qteFermes);

            retour = gestionnaireBatiments.DemarrerConstruction(BatimentEnum.HOTELDEVILLE);
            Assert.IsTrue(retour);
        }
    }
}

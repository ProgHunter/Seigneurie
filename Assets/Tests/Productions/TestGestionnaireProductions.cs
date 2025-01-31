using Batiment;
using NUnit.Framework;
using Production;
using Profession;
using Ressource;

namespace Test
{
    public class TestGestionnaireProductions
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
        public void TestProfessions0Pourcent()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            // Mettre toutes les professions à 0%
            var lotProfessionInit = new LotProfessions();
            GestionnaireProfessions.Instance.AttribuerPourcentValide(lotProfessionInit);

            // Attribuer des ressources de base
            var qtePopBase = 1000;
            var qteNourritureBase = 1000;
            var qteBoisBase = 1000;
            var qteMinerauxBase = 1000;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            gestionnaireRessources.AttribuerQteRessource(ressourcesBase);

            // Production!
            GestionnaireProductions.Instance.Production();

            // Lecture des ressources après production 0%
            var qtePopApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourritureApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.NOURRITURE);
            var qteBoisApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.BOIS);
            var qteMinerauxApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.MINERAUX);

            // Aucune ressource produite. Nouriture consommée
            Assert.AreEqual(qtePopBase, qtePopApres);
            Assert.IsTrue(qteNourritureBase > qteNourritureApres);
            Assert.AreEqual(qteBoisBase, qteBoisApres);
            Assert.AreEqual(qteMinerauxBase, qteMinerauxApres);

            // Aucun maçon
            var nbTicksRestant = GestionnaireProductions.Instance.NbTicksRestantsConstruction();
            //Assert.AreEqual(-1, nbTicksRestant);  // TODO: Debug, et ajouter tests EvaluerProduction()
        }

        [Test]
        public void TestProfessions25Pourcent0Batiments()
        {
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var gestionnaireRessources = GestionnaireRessources.Instance;
            // Mettre toutes les productions de ressources à 25%
            var lotProfessionInit = new LotProfessions(25, 25, 25, 25, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(lotProfessionInit);

            // Aucun batiments
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.MAISON, 0);
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.FERME, 0);
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.SCIERIE, 0);
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.MINE, 0);
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.HOTELDEVILLE, 0);

            // Attribuer des ressources de base
            var qtePopBase = 1000;
            var qteNourritureBase = 1000;
            var qteBoisBase = 1000;
            var qteMinerauxBase = 1000;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            gestionnaireRessources.AttribuerQteRessource(ressourcesBase);

            // Production!
            GestionnaireProductions.Instance.Production();

            // Lecture des ressources après production 25%
            var qtePopApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourritureApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.NOURRITURE);
            var qteBoisApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.BOIS);
            var qteMinerauxApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.MINERAUX);

            // Population ne grandie pas puisqu'il n'y a pas de maisons
            Assert.AreEqual(qtePopBase, qtePopApres);
            // Nourriture consommée
            Assert.IsTrue(qteNourritureBase > qteNourritureApres);  // Peut échouer si on ajuste la production de nourriture ou la faim de la pop
            // Ressources produites
            Assert.IsTrue(qteBoisBase < qteBoisApres);
            Assert.IsTrue(qteMinerauxBase < qteMinerauxApres);
        }

        [Test]
        public void TestProfessions25PourcentAvecBatiments()
        {
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var gestionnaireRessources = GestionnaireRessources.Instance;
            // Mettre toutes les productions de ressources à 25%
            var lotProfessionInit = new LotProfessions(25, 25, 25, 25, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(lotProfessionInit);

            // Création de bâtiments
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.MAISON, 1000);
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.FERME, 100);
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.SCIERIE, 100);
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.MINE, 100);
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.HOTELDEVILLE, 1);

            // Attribuer des ressources de base
            var qtePopBase = 1000;
            var qteNourritureBase = 1000;
            var qteBoisBase = 1000;
            var qteMinerauxBase = 1000;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            gestionnaireRessources.AttribuerQteRessource(ressourcesBase);

            // Production!
            GestionnaireProductions.Instance.Production();

            // Lecture des ressources après production 25%
            var qtePopApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourritureApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.NOURRITURE);
            var qteBoisApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.BOIS);
            var qteMinerauxApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.MINERAUX);

            // Population grandie avec maisons
            Assert.IsTrue(qtePopBase < qtePopApres);
            // Nourriture net produite avec bonus fermes
            Assert.IsTrue(qteNourritureBase < qteNourritureApres);  // Peut échouer si on ajuste la production de nourriture ou la faim de la pop
            // Bois produit avec bonus scieries
            Assert.IsTrue(qteBoisBase < qteBoisApres);
            // Mineraux produits avec bonus mines
            Assert.IsTrue(qteMinerauxBase < qteMinerauxApres);
        }

        [Test]
        public void TestMaconConstructionMaison()
        {
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var gestionnaireProductions = GestionnaireProductions.Instance;
            // Attribuer des maçons
            var lotProfessionInit = new LotProfessions(0, 50, 0, 0, 1);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(lotProfessionInit);

            // Attribuer des ressources de base
            var qtePopBase = 1000;
            var qteNourritureBase = 1000;
            var qteBoisBase = 1000;
            var qteMinerauxBase = 1000;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            GestionnaireRessources.Instance.AttribuerQteRessource(ressourcesBase);

            // Initialiser le nombre de maisons à 0
            gestionnaireBatiments.AttribuerQteBatiment(BatimentEnum.MAISON, 0);

            // Aucune construction en cours
            gestionnaireBatiments.AnnulerConstruction();
            var nbTicksRestant = gestionnaireProductions.NbTicksRestantsConstruction();
            Assert.AreEqual(0, nbTicksRestant);

            // Démarer une construction
            var result = gestionnaireBatiments.DemarrerConstruction(BatimentEnum.MAISON);
            Assert.IsTrue(result);

            var nbTicksMax = gestionnaireBatiments.AccesEffortConstructionTotal(BatimentEnum.MAISON);
            nbTicksRestant = gestionnaireProductions.NbTicksRestantsConstruction();
            Assert.IsTrue(nbTicksRestant > 0);
            Assert.IsTrue(nbTicksRestant <= nbTicksMax);

            // Production! Une maison devrait être construite en nbTicksMax ou moins
            var nbTicks = 0;
            
            while (gestionnaireBatiments.ConstructionEstEnCours() && nbTicks < nbTicksMax)
            {
                gestionnaireProductions.Production();
                nbTicks++;
            }

            // Construction complétée
            result = gestionnaireBatiments.ConstructionEstEnCours();
            nbTicksRestant = gestionnaireProductions.NbTicksRestantsConstruction();
            Assert.IsFalse(result);
            Assert.AreEqual(0, nbTicksRestant);
        }

        [Test]
        public void TestFamine()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            // Mettre toutes les professions à 0%
            var lotProfessionInit = new LotProfessions();
            GestionnaireProfessions.Instance.AttribuerPourcentValide(lotProfessionInit);

            // Attribuer des ressources de base
            var qtePopBase = 10000;
            var qteNourritureBase = 0;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase);
            gestionnaireRessources.AttribuerQteRessource(ressourcesBase);

            // Production! (Famine)
            GestionnaireProductions.Instance.Production();

            // Lecture des ressources après production 0%
            var qtePopApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourritureApres = gestionnaireRessources.AccesQteRessource(RessourceEnum.NOURRITURE);

            // Aucune ressource produite. Population en déclin
            Assert.IsTrue(qtePopBase > qtePopApres);
            Assert.AreEqual(qteNourritureBase, qteNourritureApres);
        }
    }
}

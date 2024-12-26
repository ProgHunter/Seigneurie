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
            GestionnaireBatiments.Instance.Reinitialiser();
            GestionnaireProfessions.Instance.Reinitialiser();
            InventaireRessources.Instance.Reinitialiser();
        }

        [Test]
        public void TestProfessions0Pourcent()
        {
            // Mettre toutes les professions � 0%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, 0);

            // Attribuer des ressources de base
            var qtePopBase = 1000;
            var qteNourritureBase = 1000;
            var qteBoisBase = 1000;
            var qteMinerauxBase = 1000;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            InventaireRessources.Instance.AttribuerQteRessource(ressourcesBase);

            // Production!
            GestionnaireProductions.Instance.Production();

            // Lecture des ressources apr�s production 0%
            var qtePopApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourritureApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);
            var qteBoisApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);
            var qteMinerauxApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            // Aucune ressource produite. Nouriture consomm�e
            Assert.AreEqual(qtePopBase, qtePopApres);
            Assert.IsTrue(qteNourritureBase > qteNourritureApres);
            Assert.AreEqual(qteBoisBase, qteBoisApres);
            Assert.AreEqual(qteMinerauxBase, qteMinerauxApres);
        }

        [Test]
        public void TestProfessions25Pourcent0Batiments()
        {
            // Mettre toutes les productions de ressources � 25%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, 0);

            // Aucun batiments
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MAISON, 0);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.FERME, 0);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.SCIERIE, 0);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MINE, 0);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.HOTELDEVILLE, 0);

            // Attribuer des ressources de base
            var qtePopBase = 1000;
            var qteNourritureBase = 1000;
            var qteBoisBase = 1000;
            var qteMinerauxBase = 1000;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            InventaireRessources.Instance.AttribuerQteRessource(ressourcesBase);

            // Production!
            GestionnaireProductions.Instance.Production();

            // Lecture des ressources apr�s production 25%
            var qtePopApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourritureApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);
            var qteBoisApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);
            var qteMinerauxApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            // Population ne grandie pas puisqu'il n'y a pas de maisons
            Assert.AreEqual(qtePopBase, qtePopApres);
            // Nourriture consomm�e
            Assert.IsTrue(qteNourritureBase > qteNourritureApres);  // Peut �chouer si on ajuste la production de nourriture ou la faim de la pop
            // Ressources produites
            Assert.IsTrue(qteBoisBase < qteBoisApres);
            Assert.IsTrue(qteMinerauxBase < qteMinerauxApres);
        }

        [Test]
        public void TestProfessions25PourcentAvecBatiments()
        {
            // Mettre toutes les productions de ressources � 25%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, 0);

            // Cr�ation de b�timents
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MAISON, 1000);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.FERME, 100);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.SCIERIE, 100);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MINE, 100);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.HOTELDEVILLE, 1);

            // Attribuer des ressources de base
            var qtePopBase = 1000;
            var qteNourritureBase = 1000;
            var qteBoisBase = 1000;
            var qteMinerauxBase = 1000;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            InventaireRessources.Instance.AttribuerQteRessource(ressourcesBase);

            // Production!
            GestionnaireProductions.Instance.Production();

            // Lecture des ressources apr�s production 25%
            var qtePopApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourritureApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);
            var qteBoisApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);
            var qteMinerauxApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            // Population grandie avec maisons
            Assert.IsTrue(qtePopBase < qtePopApres);
            // Nourriture net produite avec bonus fermes
            Assert.IsTrue(qteNourritureBase < qteNourritureApres);  // Peut �chouer si on ajuste la production de nourriture ou la faim de la pop
            // Bois produit avec bonus scieries
            Assert.IsTrue(qteBoisBase < qteBoisApres);
            // Mineraux produits avec bonus mines
            Assert.IsTrue(qteMinerauxBase < qteMinerauxApres);
        }

        [Test]
        public void TestMaconConstructionMaison()
        {
            // Attribuer des ma�ons
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 50);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, 1);

            // Attribuer des ressources de base
            var qtePopBase = 1000;
            var qteNourritureBase = 1000;
            var qteBoisBase = 1000;
            var qteMinerauxBase = 1000;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            InventaireRessources.Instance.AttribuerQteRessource(ressourcesBase);

            // Initialiser le nombre de maisons � 0
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MAISON, 0);

            // D�marer une construction
            GestionnaireBatiments.Instance.AnnulerConstruction();
            Assert.IsTrue(GestionnaireBatiments.Instance.DemarrerConstruction(BatimentEnum.MAISON));

            // Production! Une maison devrait �tre construite en nbTicksMax ou moins
            var nbTicks = 0;
            var nbTicksMax = GestionnaireBatiments.Instance.AccesEffortConstructionTotal(BatimentEnum.MAISON);
            while (GestionnaireBatiments.Instance.ConstructionEstEnCours() && nbTicks < nbTicksMax)
            {
                GestionnaireProductions.Instance.Production();
                nbTicks++;
            }

            // Construction compl�t�e
            Assert.IsFalse(GestionnaireBatiments.Instance.ConstructionEstEnCours());
        }

        [Test]
        public void TestFamine()
        {
            // Mettre toutes les professions � 0%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, 0);

            // Attribuer des ressources de base
            var qtePopBase = 10000;
            var qteNourritureBase = 0;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase);
            InventaireRessources.Instance.AttribuerQteRessource(ressourcesBase);

            // Production! (Famine)
            GestionnaireProductions.Instance.Production();

            // Lecture des ressources apr�s production 0%
            var qtePopApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourritureApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            // Aucune ressource produite. Population en d�clin
            Assert.IsTrue(qtePopBase > qtePopApres);
            Assert.AreEqual(qteNourritureBase, qteNourritureApres);
        }
    }
}

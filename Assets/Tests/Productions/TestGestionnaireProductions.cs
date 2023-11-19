using Batiment;
using NUnit.Framework;
using Production;
using Profession;
using Ressource;

namespace Test
{
    public class TestGestionnaireProductions
    {
        [Test]
        public void TestProfessions0Pourcent()
        {
            // Mettre toutes les professions à 0%
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.NATALITE].AttribuerPourcentProfession(0);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.FERMIER].AttribuerPourcentProfession(0);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.BUCHERON].AttribuerPourcentProfession(0);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.MINEUR].AttribuerPourcentProfession(0);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.MACON].AttribuerPourcentProfession(0);

            // Attribuer des ressources de base
            int qtePopBase = 1000;
            int qteNourritureBase = 1000;
            int qteBoisBase = 1000;
            int qteMinerauxBase = 1000;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            InventaireRessources.Instance.AttribuerQteRessource(ressourcesBase);

            // Production!
            GestionnaireProductions.Instance.Production();

            // Lecture des ressources après production 0%
            int qtePopApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            int qteNourritureApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);
            int qteBoisApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);
            int qteMinerauxApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            // Aucune ressource produite. Nouriture consommée
            Assert.AreEqual(qtePopBase, qtePopApres);
            Assert.IsTrue(qteNourritureBase > qteNourritureApres);
            Assert.AreEqual(qteBoisBase, qteBoisApres);
            Assert.AreEqual(qteMinerauxBase, qteMinerauxApres);
        }

        [Test]
        public void TestProfessions25Pourcent0Batiments()
        {
            // Mettre toutes les productions de ressources à 25%
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.NATALITE].AttribuerPourcentProfession(25);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.FERMIER].AttribuerPourcentProfession(25);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.BUCHERON].AttribuerPourcentProfession(25);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.MINEUR].AttribuerPourcentProfession(25);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.MACON].AttribuerPourcentProfession(0);

            // Aucun batiments
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MAISON, 0);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.FERME, 0);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.SCIERIE, 0);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MINE, 0);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.HOTELDEVILLE, 0);

            // Attribuer des ressources de base
            int qtePopBase = 1000;
            int qteNourritureBase = 1000;
            int qteBoisBase = 1000;
            int qteMinerauxBase = 1000;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            InventaireRessources.Instance.AttribuerQteRessource(ressourcesBase);

            // Production!
            GestionnaireProductions.Instance.Production();

            // Lecture des ressources après production 25%
            int qtePopApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            int qteNourritureApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);
            int qteBoisApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);
            int qteMinerauxApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

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
            // Mettre toutes les productions de ressources à 25%
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.NATALITE].AttribuerPourcentProfession(25);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.FERMIER].AttribuerPourcentProfession(25);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.BUCHERON].AttribuerPourcentProfession(25);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.MINEUR].AttribuerPourcentProfession(25);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.MACON].AttribuerPourcentProfession(0);

            // Création de bâtiments
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MAISON, 1000);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.FERME, 100);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.SCIERIE, 100);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MINE, 100);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.HOTELDEVILLE, 1);

            // Attribuer des ressources de base
            int qtePopBase = 1000;
            int qteNourritureBase = 1000;
            int qteBoisBase = 1000;
            int qteMinerauxBase = 1000;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            InventaireRessources.Instance.AttribuerQteRessource(ressourcesBase);

            // Production!
            GestionnaireProductions.Instance.Production();

            // Lecture des ressources après production 25%
            int qtePopApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            int qteNourritureApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);
            int qteBoisApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);
            int qteMinerauxApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

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
        public void TestMaconConstruction()
        {
            // Attribuer des maçons
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.NATALITE].AttribuerPourcentProfession(0);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.FERMIER].AttribuerPourcentProfession(50);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.BUCHERON].AttribuerPourcentProfession(0);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.MINEUR].AttribuerPourcentProfession(0);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.MACON].AttribuerPourcentProfession(50);

            // Attribuer des ressources de base
            int qtePopBase = 1000;
            int qteNourritureBase = 1000;
            int qteBoisBase = 1000;
            int qteMinerauxBase = 1000;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            InventaireRessources.Instance.AttribuerQteRessource(ressourcesBase);

            // Démarer une construction
            GestionnaireBatiments.Instance.DemarrerConstruction(BatimentEnum.MAISON);

            // Production! Une maison devrait être construite en
            int nbTicks = 0;
            int nbTicksMax = GestionnaireBatiments.Instance.batimentConfigDict[BatimentEnum.MAISON].effortConstruction;
            while (GestionnaireBatiments.Instance.ConstructionEnCours() && nbTicks < nbTicksMax)
            {
                GestionnaireProductions.Instance.Production();
                nbTicks++;
            }

            // Construction complétée
            Assert.IsFalse(GestionnaireBatiments.Instance.ConstructionEnCours());
        }

        [Test]
        public void TestFamine()
        {
            // Mettre toutes le professions à 0%
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.NATALITE].AttribuerPourcentProfession(0);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.FERMIER].AttribuerPourcentProfession(0);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.BUCHERON].AttribuerPourcentProfession(0);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.MINEUR].AttribuerPourcentProfession(0);
            GestionnaireProfessions.Instance.professionDict[ProfessionEnum.MACON].AttribuerPourcentProfession(0);

            // Attribuer des ressources de base
            int qtePopBase = 10000;
            int qteNourritureBase = 0;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase);
            InventaireRessources.Instance.AttribuerQteRessource(ressourcesBase);

            // Production! (Famine)
            GestionnaireProductions.Instance.Production();

            // Lecture des ressources après production 0%
            int qtePopApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            int qteNourritureApres = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            // Aucune ressource produite. Population en déclin
            Assert.IsTrue(qtePopBase > qtePopApres);
            Assert.AreEqual(qteNourritureBase, qteNourritureApres);
        }
    }
}

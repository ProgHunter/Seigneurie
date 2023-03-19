using NUnit.Framework;
using Ressource;

namespace Tests
{
    public class TestInventaireRessources
    {
        [Test]
        public void TestAccesQuantiteBasePopulation()
        {
            var quantite = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.POPULATION);
            // TODO: Changer le placeholder 100 pour la valeur de base du jeu
            Assert.AreEqual(100, quantite);
        }

        [Test]
        public void TestAccesQuantiteBaseNouriture()
        {
            var quantite = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.NOURRITURE);
            // TODO: Changer le placeholder 100 pour la valeur de base du jeu
            Assert.AreEqual(100, quantite);
        }

        [Test]
        public void TestAccesQuantiteBaseBois()
        {
            var quantite = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.BOIS);
            // TODO: Changer le placeholder 100 pour la valeur de base du jeu
            Assert.AreEqual(100, quantite);
        }

        [Test]
        public void TestAccesQuantiteBaseMineraux()
        {
            var quantite = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.MINERAUX);
            // TODO: Changer le placeholder 100 pour la valeur de base du jeu
            Assert.AreEqual(100, quantite);
        }

        [Test]
        public void TestAttribuerQuantitePopulation()
        {
            var quantiteAttribuee = 2;
            InventaireRessources.Instance.AttribuerRessource(RessourceEnum.POPULATION, quantiteAttribuee);

            var quantiteRetour = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.POPULATION);

            Assert.AreEqual(quantiteAttribuee, quantiteRetour);
        }

        [Test]
        public void TestAttribuerQuantiteNouriture()
        {
            var quantiteAttribuee = 2;
            InventaireRessources.Instance.AttribuerRessource(RessourceEnum.NOURRITURE, quantiteAttribuee);

            var quantiteRetour = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(quantiteAttribuee, quantiteRetour);
        }

        [Test]
        public void TestAttribuerQuantiteBois()
        {
            var quantiteAttribuee = 2;
            InventaireRessources.Instance.AttribuerRessource(RessourceEnum.BOIS, quantiteAttribuee);

            var quantiteRetour = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.BOIS);

            Assert.AreEqual(quantiteAttribuee, quantiteRetour);
        }

        [Test]
        public void TestAttribuerQuantiteMineraux()
        {
            var quantiteAttribuee = 2;
            InventaireRessources.Instance.AttribuerRessource(RessourceEnum.MINERAUX, quantiteAttribuee);

            var quantiteRetour = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.MINERAUX);

            Assert.AreEqual(quantiteAttribuee, quantiteRetour);
        }

        [Test]
        public void TestModifierNouritureAjout()
        {
            var quantiteAttribuee = 100;
            var quantiteAjoutee = 100;

            InventaireRessources.Instance.AttribuerRessource(RessourceEnum.NOURRITURE, quantiteAttribuee);

            var resultOK = InventaireRessources.Instance.ModifierRessource(RessourceEnum.NOURRITURE, quantiteAjoutee);
            var quantiteRetour = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(true, resultOK);
            Assert.AreEqual(quantiteAttribuee + quantiteAjoutee, quantiteRetour);
        }

        [Test]
        public void TestModifierNouritureSoustraction()
        {
            var quantiteAttribuee = 300;
            var quantiteAjoutee = -100;

            InventaireRessources.Instance.AttribuerRessource(RessourceEnum.NOURRITURE, quantiteAttribuee);

            var resultOK = InventaireRessources.Instance.ModifierRessource(RessourceEnum.NOURRITURE, quantiteAjoutee);
            var quantiteRetour = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(true, resultOK);
            Assert.AreEqual(quantiteAttribuee + quantiteAjoutee, quantiteRetour);
        }

        [Test]
        public void TestModifierNouritureNegatif()
        {
            var quantiteAttribuee = 200;
            var quantiteAjoutee = -300;

            InventaireRessources.Instance.AttribuerRessource(RessourceEnum.NOURRITURE, quantiteAttribuee);

            var resultOK = InventaireRessources.Instance.ModifierRessource(RessourceEnum.NOURRITURE, quantiteAjoutee);
            var quantiteRetour = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(false, resultOK);
            Assert.AreEqual(quantiteAttribuee, quantiteRetour);
        }

        [Test]
        public void TestModifierPopulationMax()
        {
            var quantiteAttribuee = 100;
            var quantiteAjoutee = 1100;

            InventaireRessources.Instance.AttribuerRessource(RessourceEnum.POPULATION, quantiteAttribuee);

            var resultOK = InventaireRessources.Instance.ModifierRessource(RessourceEnum.POPULATION, quantiteAjoutee);
            var quantiteRetour = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.POPULATION);

            Assert.AreEqual(true, resultOK);
            // TODO: Changer le placeholder 1000 pour la valeur max du jeu
            Assert.AreEqual(1000, quantiteRetour);
        }

        [Test]
        public void TestModifierNouritureMax()
        {
            var quantiteAttribuee = 100;
            var quantiteAjoutee = 1100;

            InventaireRessources.Instance.AttribuerRessource(RessourceEnum.NOURRITURE, quantiteAttribuee);

            var resultOK = InventaireRessources.Instance.ModifierRessource(RessourceEnum.NOURRITURE, quantiteAjoutee);
            var quantiteRetour = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(true, resultOK);
            // TODO: Changer le placeholder 1000 pour la valeur max du jeu
            Assert.AreEqual(1000, quantiteRetour);
        }

        [Test]
        public void TestModifierBoisMax()
        {
            var quantiteAttribuee = 100;
            var quantiteAjoutee = 1100;

            InventaireRessources.Instance.AttribuerRessource(RessourceEnum.BOIS, quantiteAttribuee);

            var resultOK = InventaireRessources.Instance.ModifierRessource(RessourceEnum.BOIS, quantiteAjoutee);
            var quantiteRetour = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.BOIS);

            Assert.AreEqual(true, resultOK);
            // TODO: Changer le placeholder 1000 pour la valeur max du jeu
            Assert.AreEqual(1000, quantiteRetour);
        }

        [Test]
        public void TestModifierMinerauxMax()
        {
            var quantiteAttribuee = 100;
            var quantiteAjoutee = 1100;

            InventaireRessources.Instance.AttribuerRessource(RessourceEnum.MINERAUX, quantiteAttribuee);

            var resultOK = InventaireRessources.Instance.ModifierRessource(RessourceEnum.MINERAUX, quantiteAjoutee);
            var quantiteRetour = InventaireRessources.Instance.AccesQuantiteRessource(RessourceEnum.MINERAUX);

            Assert.AreEqual(true, resultOK);
            // TODO: Changer le placeholder 1000 pour la valeur max du jeu
            Assert.AreEqual(1000, quantiteRetour);
        }
    }
}

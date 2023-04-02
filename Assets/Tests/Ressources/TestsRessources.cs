using NUnit.Framework;
using Ressource;

namespace Tests
{
    public class TestInventaireRessources
    {
        [Test]
        public void TestAttribuerEtAccesQtePopulation()
        {
            var qteAttribuee = 2;
            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.POPULATION, qteAttribuee);

            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteNouriture()
        {
            var qteAttribuee = 2;
            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteBois()
        {
            var qteAttribuee = 2;
            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.BOIS, qteAttribuee);

            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteMineraux()
        {
            var qteAttribuee = 2;
            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.MINERAUX, qteAttribuee);

            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestModifierNouritureAjout()
        {
            var qteAttribuee = 100;
            var qteAjoutee = 100;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var resultOK = InventaireRessources.Instance.ModifierQteRessource(RessourceEnum.NOURRITURE, qteAjoutee);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(true, resultOK);
            Assert.AreEqual(qteAttribuee + qteAjoutee, qteRetour);
        }

        [Test]
        public void TestModifierNouritureSoustraction()
        {
            var qteAttribuee = 300;
            var qteAjoutee = -100;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var resultOK = InventaireRessources.Instance.ModifierQteRessource(RessourceEnum.NOURRITURE, qteAjoutee);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(true, resultOK);
            Assert.AreEqual(qteAttribuee + qteAjoutee, qteRetour);
        }

        [Test]
        public void TestModifierNouritureNegatif()
        {
            var qteAttribuee = 200;
            var qteAjoutee = -300;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var resultOK = InventaireRessources.Instance.ModifierQteRessource(RessourceEnum.NOURRITURE, qteAjoutee);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(false, resultOK);
            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestModifierPopulationMax()
        {
            var qteAttribuee = 100;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.POPULATION, qteAttribuee);

            var qteMax = InventaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.POPULATION);
            var resultOK = InventaireRessources.Instance.ModifierQteRessource(RessourceEnum.POPULATION, qteMax);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);

            Assert.AreEqual(true, resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierNouritureMax()
        {
            var qteAttribuee = 100;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var qteMax = InventaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.NOURRITURE);
            var resultOK = InventaireRessources.Instance.ModifierQteRessource(RessourceEnum.NOURRITURE, qteMax);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(true, resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierBoisMax()
        {
            var qteAttribuee = 100;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.BOIS, qteAttribuee);

            var qteMax = InventaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.BOIS);
            var resultOK = InventaireRessources.Instance.ModifierQteRessource(RessourceEnum.BOIS, qteMax);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);

            Assert.AreEqual(true, resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierMinerauxMax()
        {
            var qteAttribuee = 100;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.MINERAUX, qteAttribuee);

            var qteMax = InventaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.MINERAUX);
            var resultOK = InventaireRessources.Instance.ModifierQteRessource(RessourceEnum.MINERAUX, qteMax);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            Assert.AreEqual(true, resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }
    }
}

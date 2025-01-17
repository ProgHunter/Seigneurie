using NUnit.Framework;
using Ressource;
using Utils;

namespace Test
{
    public class TestGestionnaireRessources
    {
        [SetUp]
        public void SetUp()
        {
            GestionnaireRessources.Instance.Reinitialiser();
        }

        [Test]
        public void TestAttribuerEtAccesQtePopulation()
        {
            var qteAttribuee = 2;
            GestionnaireRessources.Instance.AttribuerQteRessource(RessourceEnum.POPULATION, qteAttribuee);

            var qteRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteNouriture()
        {
            var qteAttribuee = 2;
            GestionnaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var qteRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteBois()
        {
            var qteAttribuee = 2;
            GestionnaireRessources.Instance.AttribuerQteRessource(RessourceEnum.BOIS, qteAttribuee);

            var qteRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteMineraux()
        {
            var qteAttribuee = 2;
            GestionnaireRessources.Instance.AttribuerQteRessource(RessourceEnum.MINERAUX, qteAttribuee);

            var qteRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureAjout()
        {
            var qteAttribuee = 100;
            var qteAjoutee = 100;

            GestionnaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var resultOK = GestionnaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteAjoutee);
            var qteRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.IsTrue(resultOK);
            Assert.AreEqual(qteAttribuee + qteAjoutee, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureSoustraction()
        {
            var qteAttribuee = 300;
            var qteAjoutee = -100;

            GestionnaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var resultOK = GestionnaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteAjoutee);
            var qteRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.IsTrue(resultOK);
            Assert.AreEqual(qteAttribuee + qteAjoutee, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureDepasseMin()
        {
            var qteMin = GestionnaireRessources.Instance.AccesQteMinRessource(RessourceEnum.NOURRITURE);
            GestionnaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteMin);

            var qteAjoutee = -300;
            var resultOK = GestionnaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteAjoutee);
            var qteRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteMin, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureDepasseMinBloquant()
        {
            var qteMin = GestionnaireRessources.Instance.AccesQteMinRessource(RessourceEnum.NOURRITURE);
            GestionnaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteMin + 1);

            var qteAjoutee = -300;
            // La transaction n'est pas complétée puisque le résultat serait sous la limite min
            var resultOK = GestionnaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteAjoutee, true /*limitesBloquantes*/);
            var qteRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteMin + 1, qteRetour);
        }

        [Test]
        public void TestModifierQtePopulationDepasseMax()
        {
            var qteAttribuee = 100;

            GestionnaireRessources.Instance.AttribuerQteRessource(RessourceEnum.POPULATION, qteAttribuee);

            var qteMax = GestionnaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.POPULATION);
            var resultOK = GestionnaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.POPULATION, qteMax);
            var qteRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureDepasseMax()
        {
            var qteAttribuee = 100;

            GestionnaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var qteMax = GestionnaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.NOURRITURE);
            var resultOK = GestionnaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteMax);
            var qteRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierQteBoisDepasseMax()
        {
            var qteAttribuee = 100;

            GestionnaireRessources.Instance.AttribuerQteRessource(RessourceEnum.BOIS, qteAttribuee);

            var qteMax = GestionnaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.BOIS);
            var resultOK = GestionnaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.BOIS, qteMax);
            var qteRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierQteMinerauxDepasseMax()
        {
            var qteAttribuee = 100;

            GestionnaireRessources.Instance.AttribuerQteRessource(RessourceEnum.MINERAUX, qteAttribuee);

            var qteMax = GestionnaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.MINERAUX);
            var resultOK = GestionnaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.MINERAUX, qteMax);
            var qteRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierQteMinerauxDepasseMaxBloquant()
        {
            var qteAttribuee = 100;

            GestionnaireRessources.Instance.AttribuerQteRessource(RessourceEnum.MINERAUX, qteAttribuee);

            var qteMax = GestionnaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.MINERAUX);
            // La transaction n'est pas complétée puisque le résultat serait au dessus de la limite max
            var resultOK = GestionnaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.MINERAUX, qteMax, true /*limitesBloquantes*/);
            var qteRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestTransactionLotRessourcesBloquantAcceptee()
        {
            // Réinitialise les quantitées de ressources dans l'inventaire pour le minimum de chaque
            GestionnaireRessources.Instance.AttribuerQteMinRessource();

            var qtePopulationAjoutee = 2;
            var qteNourritureAjoutee = 3;
            var qteBoisAjoutee = 4;
            var qteMinerauxAjoutee = 5;

            LotRessources LotRessources = new LotRessources(new Qte(qtePopulationAjoutee),
                                                            new Qte(qteNourritureAjoutee),
                                                            new Qte(qteBoisAjoutee),
                                                            new Qte(qteMinerauxAjoutee));

            var resultOK = GestionnaireRessources.Instance.AjouterQteRessourceAvecLimites(LotRessources, true /*limitesBloquantes*/);

            var qtePopRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);
            var qteBoisRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);
            var qteMinerRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            var qtePopAttendu = GestionnaireRessources.Instance.AccesQteMinRessource(RessourceEnum.POPULATION) + qtePopulationAjoutee;
            var qteNourAttendu = GestionnaireRessources.Instance.AccesQteMinRessource(RessourceEnum.NOURRITURE) + qteNourritureAjoutee;
            var qteBoisAttendu = GestionnaireRessources.Instance.AccesQteMinRessource(RessourceEnum.BOIS) + qteBoisAjoutee;
            var qteMinerAttendu = GestionnaireRessources.Instance.AccesQteMinRessource(RessourceEnum.MINERAUX) + qteMinerauxAjoutee;

            Assert.IsTrue(resultOK);
            Assert.AreEqual(qtePopAttendu, qtePopRetour);
            Assert.AreEqual(qteNourAttendu, qteNourRetour);
            Assert.AreEqual(qteBoisAttendu, qteBoisRetour);
            Assert.AreEqual(qteMinerAttendu, qteMinerRetour);
        }

        [Test]
        public void TestTransactionLotRessourcesBloquantRefusé()
        {
            // Réinitialise les quantitées de ressources dans l'inventaire pour le minimum de chaque
            GestionnaireRessources.Instance.AttribuerQteMinRessource();

            var qtePopulationAjoutee = 2;
            var qteNourritureAjoutee = 3;
            var qteBoisAjoutee = 4;
            var qteMinerauxAjoutee = GestionnaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.MINERAUX) + 1;

            LotRessources LotRessources = new LotRessources(new Qte(qtePopulationAjoutee),
                                                            new Qte(qteNourritureAjoutee),
                                                            new Qte(qteBoisAjoutee),
                                                            new Qte(qteMinerauxAjoutee));

            var resultOK = GestionnaireRessources.Instance.AjouterQteRessourceAvecLimites(LotRessources, true /*limitesBloquantes*/);

            var qtePopRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);
            var qteBoisRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);
            var qteMinerRetour = GestionnaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            var qtePopAttendu = GestionnaireRessources.Instance.AccesQteMinRessource(RessourceEnum.POPULATION);
            var qteNourAttendu = GestionnaireRessources.Instance.AccesQteMinRessource(RessourceEnum.NOURRITURE);
            var qteBoisAttendu = GestionnaireRessources.Instance.AccesQteMinRessource(RessourceEnum.BOIS);
            var qteMinerAttendu = GestionnaireRessources.Instance.AccesQteMinRessource(RessourceEnum.MINERAUX);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qtePopAttendu, qtePopRetour);
            Assert.AreEqual(qteNourAttendu, qteNourRetour);
            Assert.AreEqual(qteBoisAttendu, qteBoisRetour);
            Assert.AreEqual(qteMinerAttendu, qteMinerRetour);
        }

        [Test]
        public void TestTransactionLotRessourcesVerrouille()
        {
            // TODO: Créer un test avec une ressource verrouillée lors qu'elle sera ajoutée au jeu
            Assert.IsTrue(true);
        }
    }
}

using NUnit.Framework;
using Ressource;
using Utils;

namespace Test
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
        public void TestModifierQteNouritureAjout()
        {
            var qteAttribuee = 100;
            var qteAjoutee = 100;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var resultOK = InventaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteAjoutee);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(true, resultOK);
            Assert.AreEqual(qteAttribuee + qteAjoutee, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureSoustraction()
        {
            var qteAttribuee = 300;
            var qteAjoutee = -100;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var resultOK = InventaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteAjoutee);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(true, resultOK);
            Assert.AreEqual(qteAttribuee + qteAjoutee, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureDepasseMin()
        {
            var qteMin = InventaireRessources.Instance.AccesQteMinRessource(RessourceEnum.NOURRITURE);
            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteMin);

            var qteAjoutee = -300;
            var resultOK = InventaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteAjoutee);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(false, resultOK);
            Assert.AreEqual(qteMin, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureDepasseMinBloquant()
        {
            var qteMin = InventaireRessources.Instance.AccesQteMinRessource(RessourceEnum.NOURRITURE);
            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteMin + 1);

            var qteAjoutee = -300;
            // La transaction n'est pas complétée puisque le résultat serait sous la limite min
            var resultOK = InventaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteAjoutee, true /*limitesBloquantes*/);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(false, resultOK);
            Assert.AreEqual(qteMin + 1, qteRetour);
        }

        [Test]
        public void TestModifierQtePopulationDepasseMax()
        {
            var qteAttribuee = 100;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.POPULATION, qteAttribuee);

            var qteMax = InventaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.POPULATION);
            var resultOK = InventaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.POPULATION, qteMax);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);

            Assert.AreEqual(false, resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureDepasseMax()
        {
            var qteAttribuee = 100;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var qteMax = InventaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.NOURRITURE);
            var resultOK = InventaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteMax);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(false, resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierQteBoisDepasseMax()
        {
            var qteAttribuee = 100;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.BOIS, qteAttribuee);

            var qteMax = InventaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.BOIS);
            var resultOK = InventaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.BOIS, qteMax);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);

            Assert.AreEqual(false, resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierQteMinerauxDepasseMax()
        {
            var qteAttribuee = 100;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.MINERAUX, qteAttribuee);

            var qteMax = InventaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.MINERAUX);
            var resultOK = InventaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.MINERAUX, qteMax);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            Assert.AreEqual(false, resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierQteMinerauxDepasseMaxBloquant()
        {
            var qteAttribuee = 100;

            InventaireRessources.Instance.AttribuerQteRessource(RessourceEnum.MINERAUX, qteAttribuee);

            var qteMax = InventaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.MINERAUX);
            // La transaction n'est pas complétée puisque le résultat serait au dessus de la limite max
            var resultOK = InventaireRessources.Instance.AjouterQteRessourceAvecLimites(RessourceEnum.MINERAUX, qteMax, true /*limitesBloquantes*/);
            var qteRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            Assert.AreEqual(false, resultOK);
            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestTransactionLotRessourcesBloquantAcceptee()
        {
            // Réinitialise les quantitées de ressources dans l'inventaire pour le minimum de chaque
            InventaireRessources.Instance.AttribuerQteMinRessources();

            var qtePopulationAjoutee = 2;
            var qteNourritureAjoutee = 3;
            var qteBoisAjoutee = 4;
            var qteMinerauxAjoutee = 5;

            LotRessources LotRessources = new LotRessources(new Qte(qtePopulationAjoutee),
                                                            new Qte(qteNourritureAjoutee),
                                                            new Qte(qteBoisAjoutee),
                                                            new Qte(qteMinerauxAjoutee));

            var resultOK = InventaireRessources.Instance.AjouterQteRessourceAvecLimites(LotRessources, true /*limitesBloquantes*/);

            var qtePopRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);
            var qteBoisRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);
            var qteMinerRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            var qtePopAttendu = InventaireRessources.Instance.AccesQteMinRessource(RessourceEnum.POPULATION) + qtePopulationAjoutee;
            var qteNourAttendu = InventaireRessources.Instance.AccesQteMinRessource(RessourceEnum.NOURRITURE) + qteNourritureAjoutee;
            var qteBoisAttendu = InventaireRessources.Instance.AccesQteMinRessource(RessourceEnum.BOIS) + qteBoisAjoutee;
            var qteMinerAttendu = InventaireRessources.Instance.AccesQteMinRessource(RessourceEnum.MINERAUX) + qteMinerauxAjoutee;

            Assert.AreEqual(true, resultOK);
            Assert.AreEqual(qtePopAttendu, qtePopRetour);
            Assert.AreEqual(qteNourAttendu, qteNourRetour);
            Assert.AreEqual(qteBoisAttendu, qteBoisRetour);
            Assert.AreEqual(qteMinerAttendu, qteMinerRetour);
        }

        [Test]
        public void TestTransactionLotRessourcesBloquantRefuse()
        {
            // Réinitialise les quantitées de ressources dans l'inventaire pour le minimum de chaque
            InventaireRessources.Instance.AttribuerQteMinRessources();

            var qtePopulationAjoutee = 2;
            var qteNourritureAjoutee = 3;
            var qteBoisAjoutee = 4;
            var qteMinerauxAjoutee = InventaireRessources.Instance.AccesQteMaxRessource(RessourceEnum.MINERAUX) + 1;

            LotRessources LotRessources = new LotRessources(new Qte(qtePopulationAjoutee),
                                                            new Qte(qteNourritureAjoutee),
                                                            new Qte(qteBoisAjoutee),
                                                            new Qte(qteMinerauxAjoutee));

            var resultOK = InventaireRessources.Instance.AjouterQteRessourceAvecLimites(LotRessources, true /*limitesBloquantes*/);

            var qtePopRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.NOURRITURE);
            var qteBoisRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.BOIS);
            var qteMinerRetour = InventaireRessources.Instance.AccesQteRessource(RessourceEnum.MINERAUX);

            var qtePopAttendu = InventaireRessources.Instance.AccesQteMinRessource(RessourceEnum.POPULATION);
            var qteNourAttendu = InventaireRessources.Instance.AccesQteMinRessource(RessourceEnum.NOURRITURE);
            var qteBoisAttendu = InventaireRessources.Instance.AccesQteMinRessource(RessourceEnum.BOIS);
            var qteMinerAttendu = InventaireRessources.Instance.AccesQteMinRessource(RessourceEnum.MINERAUX);

            Assert.AreEqual(false, resultOK);
            Assert.AreEqual(qtePopAttendu, qtePopRetour);
            Assert.AreEqual(qteNourAttendu, qteNourRetour);
            Assert.AreEqual(qteBoisAttendu, qteBoisRetour);
            Assert.AreEqual(qteMinerAttendu, qteMinerRetour);
        }

        [Test]
        public void TestTransactionLotRessourcesVerrouille()
        {
            // TODO: Créer un test avec une ressource verrouiller lors qu'elle sera ajouté au jeu
            Assert.IsTrue(true);
        }
    }
}

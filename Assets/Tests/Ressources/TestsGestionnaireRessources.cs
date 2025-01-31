using Batiment;
using NUnit.Framework;
using Profession;
using Ressource;
using Utils;

namespace Test
{
    public class TestGestionnaireRessources
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
        public void TestAttribuerEtAccesQtePopulation()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            var qteAttribuee = 2;
            gestionnaireRessources.AttribuerQteRessource(RessourceEnum.POPULATION, qteAttribuee);

            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.POPULATION);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteNouriture()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            var qteAttribuee = 2;
            gestionnaireRessources.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteBois()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            var qteAttribuee = 2;
            gestionnaireRessources.AttribuerQteRessource(RessourceEnum.BOIS, qteAttribuee);

            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.BOIS);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestAttribuerEtAccesQteMineraux()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            var qteAttribuee = 2;
            gestionnaireRessources.AttribuerQteRessource(RessourceEnum.MINERAUX, qteAttribuee);

            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.MINERAUX);

            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureAjout()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            var qteAttribuee = 100;
            var qteAjoutee = 100;

            gestionnaireRessources.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var resultOK = gestionnaireRessources.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteAjoutee);
            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.IsTrue(resultOK);
            Assert.AreEqual(qteAttribuee + qteAjoutee, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureSoustraction()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            var qteAttribuee = 300;
            var qteAjoutee = -100;

            gestionnaireRessources.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var resultOK = gestionnaireRessources.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteAjoutee);
            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.IsTrue(resultOK);
            Assert.AreEqual(qteAttribuee + qteAjoutee, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureDepasseMin()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            var qteMin = gestionnaireRessources.AccesQteMinRessource(RessourceEnum.NOURRITURE);
            gestionnaireRessources.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteMin);

            var qteAjoutee = -300;
            var resultOK = gestionnaireRessources.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteAjoutee);
            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteMin, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureDepasseMinBloquant()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            var qteMin = gestionnaireRessources.AccesQteMinRessource(RessourceEnum.NOURRITURE);
            gestionnaireRessources.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteMin + 1);

            var qteAjoutee = -300;
            // La transaction n'est pas complétée puisque le résultat serait sous la limite min
            var resultOK = gestionnaireRessources.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteAjoutee, true /*limitesBloquantes*/);
            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteMin + 1, qteRetour);
        }

        [Test]
        public void TestModifierQtePopulationDepasseMax()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            var qteAttribuee = 100;

            gestionnaireRessources.AttribuerQteRessource(RessourceEnum.POPULATION, qteAttribuee);

            var qteMax = gestionnaireRessources.AccesQteMaxRessource(RessourceEnum.POPULATION);
            var resultOK = gestionnaireRessources.AjouterQteRessourceAvecLimites(RessourceEnum.POPULATION, qteMax);
            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.POPULATION);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierQteNouritureDepasseMax()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            var qteAttribuee = 100;

            gestionnaireRessources.AttribuerQteRessource(RessourceEnum.NOURRITURE, qteAttribuee);

            var qteMax = gestionnaireRessources.AccesQteMaxRessource(RessourceEnum.NOURRITURE);
            var resultOK = gestionnaireRessources.AjouterQteRessourceAvecLimites(RessourceEnum.NOURRITURE, qteMax);
            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.NOURRITURE);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierQteBoisDepasseMax()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            var qteAttribuee = 100;

            gestionnaireRessources.AttribuerQteRessource(RessourceEnum.BOIS, qteAttribuee);

            var qteMax = gestionnaireRessources.AccesQteMaxRessource(RessourceEnum.BOIS);
            var resultOK = gestionnaireRessources.AjouterQteRessourceAvecLimites(RessourceEnum.BOIS, qteMax);
            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.BOIS);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierQteMinerauxDepasseMax()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            var qteAttribuee = 100;

            gestionnaireRessources.AttribuerQteRessource(RessourceEnum.MINERAUX, qteAttribuee);

            var qteMax = gestionnaireRessources.AccesQteMaxRessource(RessourceEnum.MINERAUX);
            var resultOK = gestionnaireRessources.AjouterQteRessourceAvecLimites(RessourceEnum.MINERAUX, qteMax);
            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.MINERAUX);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }

        [Test]
        public void TestModifierQteMinerauxDepasseMaxBloquant()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            var qteAttribuee = 100;

            gestionnaireRessources.AttribuerQteRessource(RessourceEnum.MINERAUX, qteAttribuee);

            var qteMax = gestionnaireRessources.AccesQteMaxRessource(RessourceEnum.MINERAUX);
            // La transaction n'est pas complétée puisque le résultat serait au dessus de la limite max
            var resultOK = gestionnaireRessources.AjouterQteRessourceAvecLimites(RessourceEnum.MINERAUX, qteMax, true /*limitesBloquantes*/);
            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.MINERAUX);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteAttribuee, qteRetour);
        }

        [Test]
        public void TestTransactionLotRessourcesBloquantAcceptee()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            // Réinitialise les quantitées de ressources dans l'inventaire pour le minimum de chaque
            gestionnaireRessources.AttribuerQteMinRessources();

            var qtePopulationAjoutee = 2;
            var qteNourritureAjoutee = 3;
            var qteBoisAjoutee = 4;
            var qteMinerauxAjoutee = 5;

            LotRessources LotRessources = new LotRessources(new Qte(qtePopulationAjoutee),
                                                            new Qte(qteNourritureAjoutee),
                                                            new Qte(qteBoisAjoutee),
                                                            new Qte(qteMinerauxAjoutee));

            var resultOK = gestionnaireRessources.AjouterQteRessourceAvecLimites(LotRessources, true /*limitesBloquantes*/);

            var qtePopRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.NOURRITURE);
            var qteBoisRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.BOIS);
            var qteMinerRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.MINERAUX);

            var qtePopAttendu = gestionnaireRessources.AccesQteMinRessource(RessourceEnum.POPULATION) + qtePopulationAjoutee;
            var qteNourAttendu = gestionnaireRessources.AccesQteMinRessource(RessourceEnum.NOURRITURE) + qteNourritureAjoutee;
            var qteBoisAttendu = gestionnaireRessources.AccesQteMinRessource(RessourceEnum.BOIS) + qteBoisAjoutee;
            var qteMinerAttendu = gestionnaireRessources.AccesQteMinRessource(RessourceEnum.MINERAUX) + qteMinerauxAjoutee;

            Assert.IsTrue(resultOK);
            Assert.AreEqual(qtePopAttendu, qtePopRetour);
            Assert.AreEqual(qteNourAttendu, qteNourRetour);
            Assert.AreEqual(qteBoisAttendu, qteBoisRetour);
            Assert.AreEqual(qteMinerAttendu, qteMinerRetour);
        }

        [Test]
        public void TestTransactionLotRessourcesBloquantRefuse()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            // Réinitialise les quantitées de ressources dans l'inventaire pour le minimum de chaque
            gestionnaireRessources.AttribuerQteMinRessources();

            var qtePopulationAjoutee = 2;
            var qteNourritureAjoutee = 3;
            var qteBoisAjoutee = 4;
            var qteMinerauxAjoutee = gestionnaireRessources.AccesQteMaxRessource(RessourceEnum.MINERAUX) + 1;

            LotRessources LotRessources = new LotRessources(new Qte(qtePopulationAjoutee),
                                                            new Qte(qteNourritureAjoutee),
                                                            new Qte(qteBoisAjoutee),
                                                            new Qte(qteMinerauxAjoutee));

            var resultOK = gestionnaireRessources.AjouterQteRessourceAvecLimites(LotRessources, true /*limitesBloquantes*/);

            var qtePopRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.POPULATION);
            var qteNourRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.NOURRITURE);
            var qteBoisRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.BOIS);
            var qteMinerRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.MINERAUX);

            var qtePopAttendu = gestionnaireRessources.AccesQteMinRessource(RessourceEnum.POPULATION);
            var qteNourAttendu = gestionnaireRessources.AccesQteMinRessource(RessourceEnum.NOURRITURE);
            var qteBoisAttendu = gestionnaireRessources.AccesQteMinRessource(RessourceEnum.BOIS);
            var qteMinerAttendu = gestionnaireRessources.AccesQteMinRessource(RessourceEnum.MINERAUX);

            Assert.IsFalse(resultOK);
            Assert.AreEqual(qtePopAttendu, qtePopRetour);
            Assert.AreEqual(qteNourAttendu, qteNourRetour);
            Assert.AreEqual(qteBoisAttendu, qteBoisRetour);
            Assert.AreEqual(qteMinerAttendu, qteMinerRetour);
        }

        [Test]
        public void TestTransactionModifierLimiteMaxRessource()
        {
            var gestionnaireRessources = GestionnaireRessources.Instance;
            gestionnaireRessources.AttribuerQteMinRessources();
            var qteMin = gestionnaireRessources.AccesQteRessource(RessourceEnum.POPULATION);
            var qteMax = gestionnaireRessources.AccesQteMaxRessource(RessourceEnum.POPULATION) + 1;
            // On modifie la limite à 2 au dessus celle actuelle
            gestionnaireRessources.ModifierLimiteMaxRessource(RessourceEnum.POPULATION, qteMax + 1);
            var resultOK = gestionnaireRessources.AjouterQteRessourceAvecLimites(RessourceEnum.POPULATION, qteMax - qteMin, true /*limitesBloquantes*/);
            var qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.POPULATION);

            Assert.IsTrue(resultOK);
            Assert.AreEqual(qteMax, qteRetour);

            // On remet la limite de départ
            gestionnaireRessources.ModifierLimiteMaxRessource(RessourceEnum.POPULATION, qteMax + 1);
            resultOK = gestionnaireRessources.AjouterQteRessourceAvecLimites(RessourceEnum.POPULATION, qteMax, true /*limitesBloquantes*/);
            qteRetour = gestionnaireRessources.AccesQteRessource(RessourceEnum.POPULATION);

            // On est déjà au dessus de la limite de +1
            Assert.IsFalse(resultOK);
            Assert.AreEqual(qteMax, qteRetour);
        }
    }
}

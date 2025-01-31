using Batiment;
using NUnit.Framework;
using Profession;
using Ressource;
using UnityEngine;

namespace Test
{
    public class TestGestionnaireProfessions
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
        public void TestProfessionAttribuerLot()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            // Assigner la config au lot de profession
            gestionnaireProfessions.Reinitialiser();

            // Attribuer 0% à toutes les professions
            var lotProfessionVide = new LotProfessions();
            gestionnaireProfessions.AttribuerPourcent(lotProfessionVide);

            var pourcentRetouNatalite = gestionnaireProfessions.AccederPourcent(ProfessionEnum.NATALITE);
            var pourcentRetouFermier = gestionnaireProfessions.AccederPourcent(ProfessionEnum.FERMIER);
            var pourcentRetouBucheron = gestionnaireProfessions.AccederPourcent(ProfessionEnum.BUCHERON);
            var pourcentRetouMineur = gestionnaireProfessions.AccederPourcent(ProfessionEnum.MINEUR);
            var pourcentRetouMacon = gestionnaireProfessions.AccederPourcent(ProfessionEnum.MACON);
            
            Assert.AreEqual(0, pourcentRetouNatalite);
            Assert.AreEqual(0, pourcentRetouFermier);
            Assert.AreEqual(0, pourcentRetouBucheron);
            Assert.AreEqual(0, pourcentRetouMineur);
            Assert.AreEqual(0, pourcentRetouMacon);
        }

        [Test]
        public void TestProfession50pc()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            var pourcentTest = 50;
            // Mettre toutes les professions à 0%, et Maçon à 50%
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.NATALITE, 0);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.FERMIER, 0);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 0);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MINEUR, 0);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MACON, pourcentTest);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);

            Assert.AreEqual(pourcentTest, pourcentRetour);
        }

        [Test]
        public void TestProfessionMoins10pc()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            // Mettre toutes les professions à 0%, et Maçon à -10%
            var lotProfessionVide = new LotProfessions();
            gestionnaireProfessions.AttribuerPourcent(lotProfessionVide);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MACON, -10);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 0, puisqu'on ne peut pas attribuer moins que 0%
            Assert.AreEqual(0, pourcentRetour);
        }

        [Test]
        public void TestProfession110pc()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            // Mettre toutes les professions à 0%, et Maçon à 110%
            var lotProfessionVide = new LotProfessions();
            gestionnaireProfessions.AttribuerPourcent(lotProfessionVide);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MACON, 110);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 100, puisqu'on ne peut pas attribuer plus de 100%
            Assert.AreEqual(100, pourcentRetour);
        }

        [Test]
        public void TestProfessionReste20pcTotal()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            var lotProfessionVide = new LotProfessions();
            gestionnaireProfessions.AttribuerPourcent(lotProfessionVide);
            // Mettre toutes les professions à 20%, et Maçon à 50%
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.NATALITE, 20);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.FERMIER, 20);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 20);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MINEUR, 20);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MACON, 50);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 20, puisque le total attribué aux professions ne peut cumuler plus de 100%
            Assert.AreEqual(20, pourcentRetour);
        }

        [Test]
        public void TestProfessionReste0pcTotal()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            var lotProfessionVide = new LotProfessions();
            gestionnaireProfessions.AttribuerPourcent(lotProfessionVide);
            // Mettre toutes les professions à 0%, et Maçon à 10%
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.NATALITE, 25);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.FERMIER, 25);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 25);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MINEUR, 25);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MACON, 10);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 0, puisque le total attribué aux professions ne peut cumuler plus de 100%
            Assert.AreEqual(0, pourcentRetour);
        }

        [Test]
        public void TestProfessionReste20pcTotalLot()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            var lotProfessionVide = new LotProfessions();
            gestionnaireProfessions.AttribuerPourcent(lotProfessionVide);
            // Mettre toutes les professions à 20%, et Maçon à 50%
            var lotProfession = new LotProfessions(20,20,20,20,50);
            gestionnaireProfessions.AttribuerPourcentValide(lotProfession);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 20, puisque le total attribué aux professions ne peut cumuler plus de 100%
            Assert.AreEqual(20, pourcentRetour);
        }

        [Test]
        public void TestProfessionAccederPourcentFractionSansValide()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            var pourcentTest = 110;
            gestionnaireProfessions.AttribuerPourcent(ProfessionEnum.MACON, pourcentTest);
            var pourcentRetour = gestionnaireProfessions.AccederPourcentFraction(ProfessionEnum.MACON);

            Assert.IsTrue(Mathf.Approximately(1.1f, pourcentRetour));
        }

        [Test]
        public void TestProfessionAttribuerPourcentLotSansValide()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            var pourcentTestNatalite = 10;
            var pourcentTestFermier = 20;
            var pourcentTestBucheron = 30;
            var pourcentTestMineur = 40;
            var pourcentTestMacon = 50;
            var lotProfession = new LotProfessions(pourcentTestNatalite, 
                                                   pourcentTestFermier, 
                                                   pourcentTestBucheron, 
                                                   pourcentTestMineur, 
                                                   pourcentTestMacon);

            gestionnaireProfessions.AttribuerPourcent(lotProfession);

            var pourcentRetouNatalite = gestionnaireProfessions.AccederPourcent(ProfessionEnum.NATALITE);
            var pourcentRetouFermier = gestionnaireProfessions.AccederPourcent(ProfessionEnum.FERMIER);
            var pourcentRetouBucheron = gestionnaireProfessions.AccederPourcent(ProfessionEnum.BUCHERON);
            var pourcentRetouMineur = gestionnaireProfessions.AccederPourcent(ProfessionEnum.MINEUR);
            var pourcentRetouMacon = gestionnaireProfessions.AccederPourcent(ProfessionEnum.MACON);

            Assert.AreEqual(pourcentTestNatalite, pourcentRetouNatalite);
            Assert.AreEqual(pourcentTestFermier, pourcentRetouFermier);
            Assert.AreEqual(pourcentTestBucheron, pourcentRetouBucheron);
            Assert.AreEqual(pourcentTestMineur, pourcentRetouMineur);
            Assert.AreEqual(pourcentTestMacon, pourcentRetouMacon);
        }

        [Test]
        public void TestProfessionIncrementerDecrementer()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            var lotProfessionVide = new LotProfessions(20,20,20,20,20);
            gestionnaireProfessions.AttribuerPourcent(lotProfessionVide);

            var succes = gestionnaireProfessions.DecrementerPourcent(ProfessionEnum.NATALITE);
            var pourcentRetouNatalite = gestionnaireProfessions.AccederPourcent(ProfessionEnum.NATALITE);

            Assert.IsTrue(succes);
            Assert.AreEqual(19, pourcentRetouNatalite);

            succes = gestionnaireProfessions.IncrementerPourcent(ProfessionEnum.NATALITE);
            pourcentRetouNatalite = gestionnaireProfessions.AccederPourcent(ProfessionEnum.NATALITE);

            Assert.IsTrue(succes);
            Assert.AreEqual(20, pourcentRetouNatalite);

            succes = gestionnaireProfessions.IncrementerPourcent(ProfessionEnum.NATALITE);
            pourcentRetouNatalite = gestionnaireProfessions.AccederPourcent(ProfessionEnum.NATALITE);
            // Max 100%
            Assert.IsFalse(succes);
            Assert.AreEqual(20, pourcentRetouNatalite);
        }
    }
}

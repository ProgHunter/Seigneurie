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
            // Attribuer 0% à toutes les professions
            var lotProfessionVide = new LotProfessions();
            gestionnaireProfessions.AttribuerPourcent(lotProfessionVide);

            var pourcentRetourNatalite = gestionnaireProfessions.AccederPourcent(ProfessionEnum.NATALITE);
            var pourcentRetourFermier = gestionnaireProfessions.AccederPourcent(ProfessionEnum.FERMIER);
            var pourcentRetourBucheron = gestionnaireProfessions.AccederPourcent(ProfessionEnum.BUCHERON);
            var pourcentRetourMineur = gestionnaireProfessions.AccederPourcent(ProfessionEnum.MINEUR);
            var pourcentRetourMacon = gestionnaireProfessions.AccederPourcent(ProfessionEnum.MACON);
            
            Assert.AreEqual(0, pourcentRetourNatalite);
            Assert.AreEqual(0, pourcentRetourFermier);
            Assert.AreEqual(0, pourcentRetourBucheron);
            Assert.AreEqual(0, pourcentRetourMineur);
            Assert.AreEqual(0, pourcentRetourMacon);
        }

        [Test]
        public void TestProfessionAttribuerPourcentValide()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            var pourcentTest = 50;
            // Mettre toutes les professions à 0%, et Maçon à 50%
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.NATALITE, 0);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.FERMIER, 0);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 0);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MINEUR, 0);
            var valide = gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MACON, pourcentTest);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);

            Assert.IsTrue(valide);
            Assert.AreEqual(pourcentTest, pourcentRetour);

            // Test d'égalité de lots
            var lotProfession = new LotProfessions(0, 0, 0, 0, pourcentTest);
            var result = gestionnaireProfessions.Professions.EstEgale(lotProfession);

            Assert.IsTrue(result);

            lotProfession.AttribuerPourcentProfession(ProfessionEnum.MINEUR, 1);
            result = gestionnaireProfessions.Professions.EstEgale(lotProfession);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestProfessionAttribuerPourcentPlusPetitQueMinInvalide()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            // Mettre toutes les professions à 0%, et Maçon à -10%
            var lotProfessionVide = new LotProfessions();
            gestionnaireProfessions.AttribuerPourcent(lotProfessionVide);
            var valide = gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MACON, -10);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 0, puisqu'on ne peut pas attribuer moins que 0%
            Assert.IsFalse(valide);
            Assert.AreEqual(0, pourcentRetour);
        }

        [Test]
        public void TestProfessionAttribuerPourcentPlusGrandQueMaxInvalide()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            // Mettre toutes les professions à 0%, et Maçon à 110%
            var lotProfessionVide = new LotProfessions();
            gestionnaireProfessions.AttribuerPourcent(lotProfessionVide);
            var valide = gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MACON, 110);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 100, puisqu'on ne peut pas attribuer plus de 100%
            Assert.IsFalse(valide);
            Assert.AreEqual(100, pourcentRetour);
        }

        [Test]
        public void TestProfessionPourcentTotalPlusGrandQue100Invalide()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            var lotProfessionVide = new LotProfessions();
            gestionnaireProfessions.AttribuerPourcent(lotProfessionVide);
            // Mettre toutes les professions à 20%, et Maçon à 50%
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.NATALITE, 20);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.FERMIER, 20);
            gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 20);
            var valideMineur = gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MINEUR, 20);
            var valideMacon = gestionnaireProfessions.AttribuerPourcentValide(ProfessionEnum.MACON, 50);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 20, puisque le total attribué aux professions ne peut cumuler plus de 100%
            Assert.IsTrue(valideMineur);
            Assert.IsFalse(valideMacon);
            Assert.AreEqual(20, pourcentRetour);
        }

        [Test]
        public void TestProfessionReste20pcTotalLot()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            var lotProfessionVide = new LotProfessions();
            gestionnaireProfessions.AttribuerPourcent(lotProfessionVide);
            // Mettre les professions à 20%, Mineurs à 30% et Maçon à 50%
            var lotProfession = new LotProfessions(25,25,25,30,50);
            var valide = gestionnaireProfessions.AttribuerPourcentValide(lotProfession);

            var pourcentRetourMineur = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MINEUR);
            var pourcentRetourMacon = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // Le total attribué aux professions ne peut cumuler plus de 100%
            // Il reste 25% qui peut être attribuer aux mineurs et 0% aux maçons
            Assert.IsFalse(valide);
            Assert.AreEqual(25, pourcentRetourMineur);
            Assert.AreEqual(0, pourcentRetourMacon);
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

            var pourcentRetourNatalite = gestionnaireProfessions.AccederPourcent(ProfessionEnum.NATALITE);
            var pourcentRetourFermier = gestionnaireProfessions.AccederPourcent(ProfessionEnum.FERMIER);
            var pourcentRetourBucheron = gestionnaireProfessions.AccederPourcent(ProfessionEnum.BUCHERON);
            var pourcentRetourMineur = gestionnaireProfessions.AccederPourcent(ProfessionEnum.MINEUR);
            var pourcentRetourMacon = gestionnaireProfessions.AccederPourcent(ProfessionEnum.MACON);

            Assert.AreEqual(pourcentTestNatalite, pourcentRetourNatalite);
            Assert.AreEqual(pourcentTestFermier, pourcentRetourFermier);
            Assert.AreEqual(pourcentTestBucheron, pourcentRetourBucheron);
            Assert.AreEqual(pourcentTestMineur, pourcentRetourMineur);
            Assert.AreEqual(pourcentTestMacon, pourcentRetourMacon);
        }

        [Test]
        public void TestProfessionIncrementerDecrementer()
        {
            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            var lotProfessionVide = new LotProfessions(20,20,20,20,20);
            gestionnaireProfessions.AttribuerPourcent(lotProfessionVide);

            var succes = gestionnaireProfessions.DecrementerPourcent(ProfessionEnum.NATALITE);
            var pourcentRetourNatalite = gestionnaireProfessions.AccederPourcent(ProfessionEnum.NATALITE);

            Assert.IsTrue(succes);
            Assert.AreEqual(19, pourcentRetourNatalite);

            succes = gestionnaireProfessions.IncrementerPourcent(ProfessionEnum.NATALITE);
            pourcentRetourNatalite = gestionnaireProfessions.AccederPourcent(ProfessionEnum.NATALITE);

            Assert.IsTrue(succes);
            Assert.AreEqual(20, pourcentRetourNatalite);

            succes = gestionnaireProfessions.IncrementerPourcent(ProfessionEnum.NATALITE);
            pourcentRetourNatalite = gestionnaireProfessions.AccederPourcent(ProfessionEnum.NATALITE);
            // Max 100%
            Assert.IsFalse(succes);
            Assert.AreEqual(20, pourcentRetourNatalite);
        }
    }
}

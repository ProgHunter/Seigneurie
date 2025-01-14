using NUnit.Framework;
using Profession;

namespace Test
{
    public class TestGestionnaireProfessions
    {
        [SetUp]
        public void SetUp()
        {
            GestionnaireProfessions.Instance.Reinitialiser();
        }

        [Test]
        public void TestProfession50pc()
        {
            var pourcentTest = 50;
            // Mettre toutes les professions à 0%, et Maçon à 50%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, pourcentTest);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);

            Assert.AreEqual(pourcentTest, pourcentRetour);
        }

        [Test]
        public void TestProfessionMoins10pc()
        {
            var pourcentTest = -10;
            // Mettre toutes les professions � 0%, et Ma�on � -10%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, pourcentTest);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 0, puisqu'on ne peut pas attribuer moins que 0%
            Assert.AreEqual(0, pourcentRetour);
        }

        [Test]
        public void TestProfession110pc()
        {
            var pourcentTest = 110;
            // Mettre toutes les professions � 0%, et Ma�on � 110%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 0);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, pourcentTest);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 100, puisqu'on ne peut pas attribuer plus de 100%
            Assert.AreEqual(100, pourcentRetour);
        }

        [Test]
        public void TestProfessionReste20pcTotal()
        {
            var pourcentTest = 50;
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, 0);
            // Mettre toutes les professions � 20%, et Ma�on � 50%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 20);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 20);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 20);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 20);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, pourcentTest);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 20, puisque le total attribu� aux professions ne peut cumuler plus de 100%
            Assert.AreEqual(20, pourcentRetour);
        }

        [Test]
        public void TestProfessionReste0pcTotal()
        {
            var pourcentTest = 10;
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, 0);
            // Mettre toutes les professions � 25%, et Ma�on � 10%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, pourcentTest);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 0, puisque le total attribu� aux professions ne peut cumuler plus de 100%
            Assert.AreEqual(0, pourcentRetour);
        }

        [Test]
        public void TestProfessionVerrouilleeAttribuerPc()
        {
            // TODO: Cr�er un test avec une profession verrouill�e lors qu'elle sera ajout�e au jeu
            Assert.IsTrue(true);
        }
    }
}

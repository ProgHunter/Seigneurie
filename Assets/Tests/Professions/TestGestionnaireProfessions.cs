using NUnit.Framework;
using Profession;
using UnityEngine;

namespace Test
{
    public class TestGestionnaireProfessions
    {
        [Test]
        public void TestProfession50pc()
        {
            var pourcentTest = 0.5f;
            // Mettre toutes les professions à 0%, et Maçon à 50%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 0f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 0f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 0f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 0f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, pourcentTest);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);

            Assert.IsTrue(Mathf.Approximately(pourcentTest, pourcentRetour));
        }

        [Test]
        public void TestProfessionMoins10pc()
        {
            var pourcentTest = -0.1f;
            // Mettre toutes les professions à 0%, et Maçon à -10%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 0f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 0f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 0f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 0f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, pourcentTest);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 0, puisqu'on ne peut pas attribuer moins que 0%
            Assert.IsTrue(Mathf.Approximately(0f, pourcentRetour));
        }

        [Test]
        public void TestProfession110pc()
        {
            var pourcentTest = 1.1f;
            // Mettre toutes les professions à 0%, et Maçon à 110%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 0f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 0f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 0f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 0f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, pourcentTest);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 100, puisqu'on ne peut pas attribuer plus de 100%
            Assert.IsTrue(Mathf.Approximately(1f, pourcentRetour));
        }

        [Test]
        public void TestProfessionReste20pcTotal()
        {
            var pourcentTest = 0.5f;
            // Mettre toutes les professions à 20%, et Maçon à 50%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 0.2f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 0.2f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 0.2f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 0.2f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, pourcentTest);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 20, puisque le total attribué aux professions ne peut cumuler plus de 100%
            Assert.IsTrue(Mathf.Approximately(0.2f, pourcentRetour));
        }

        [Test]
        public void TestProfessionReste0pcTotal()
        {
            var pourcentTest = 0.1f;
            // Mettre toutes les professions à 25%, et Maçon à 10%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 0.25f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 0.25f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 0.25f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 0.25f);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, pourcentTest);

            var pourcentRetour = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
            // 0, puisque le total attribué aux professions ne peut cumuler plus de 100%
            Assert.IsTrue(Mathf.Approximately(0f, pourcentRetour));
        }
    }
}

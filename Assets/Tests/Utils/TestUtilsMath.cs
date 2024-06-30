using NUnit.Framework;
using System;
using Utils;

namespace Test
{
    public class TestUtilsMath
    {
        [Test]
        public void TestFonctionLogistique_1Tick()
        {
            var tick = 1;
            var nbPopBase = 2;
            var capaciteMax = 100;
            var croissance = 1;

            var popAvecCroissance = MathUtils.FonctionLogistique(tick, nbPopBase, capaciteMax, croissance);

            Assert.AreEqual(5, (long)Math.Round(popAvecCroissance));
        }

        [Test]
        public void TestFonctionLogistique_6Ticks()
        {
            var tick = 6;
            var nbPopBase = 2;
            var capaciteMax = 100;
            var croissance = 1;

            var popAvecCroissance = MathUtils.FonctionLogistique(tick, nbPopBase, capaciteMax, croissance);

            Assert.AreEqual(89, (long)Math.Round(popAvecCroissance));
        }

        [Test]
        public void TestFonctionLogistique_6Ticks_50pcCroissance()
        {
            var tick = 6;
            var nbPopBase = 2;
            var capaciteMax = 100;
            var croissance = (float)0.5;

            var popAvecCroissance = MathUtils.FonctionLogistique(tick, nbPopBase, capaciteMax, croissance);

            Assert.AreEqual(28, (long)Math.Round(popAvecCroissance));
        }

        [Test]
        public void TestFonctionLogistique_6Ticks_0pcCroissance()
        {
            var tick = 6;
            var nbPopBase = 2;
            var capaciteMax = 100;
            var croissance = (float)0;

            var popAvecCroissance = MathUtils.FonctionLogistique(tick, nbPopBase, capaciteMax, croissance);

            Assert.AreEqual(nbPopBase, (long)Math.Round(popAvecCroissance));
        }

        [Test]
        public void TestFonctionLogistiqueTickIsole_50Pop()
        {
            var popActuelle = 50;
            var nbPopBase = 2;
            var capaciteMax = 100;
            var croissance = 1;

            var tick = MathUtils.FonctionLogistiqueTickIsole(popActuelle, nbPopBase, capaciteMax, croissance);

            Assert.AreEqual(4, (long)Math.Round(tick));
        }

        [Test]
        public void TestFonctionLogistiqueTickIsole_50Pop_1pcCroissance()
        {
            var popActuelle = 50;
            var nbPopBase = 2;
            var capaciteMax = 100;
            var croissance = (float)0.01;

            var tick = MathUtils.FonctionLogistiqueTickIsole(popActuelle, nbPopBase, capaciteMax, croissance);

            Assert.AreEqual(389, (long)Math.Round(tick));
        }

        [Test]
        public void TestFonctionLogistiqueTickIsole_10Pop()
        {
            var popActuelle = 10;
            var nbPopBase = 2;
            var capaciteMax = 100;
            var croissance = 1;

            var tick = MathUtils.FonctionLogistiqueTickIsole(popActuelle, nbPopBase, capaciteMax, croissance);

            Assert.AreEqual(2, (long)Math.Round(tick));
        }

        [Test]
        public void TestFonctionLogistiqueEtTickIsole_52Pop()
        {
            var popActuelle = 52;
            var nbPopBase = 2;
            var capaciteMax = 100;
            var croissance = 1;
            // TODO: Debug 52pop -> 3,97ticks
            var tick = MathUtils.FonctionLogistiqueTickIsole(popActuelle, nbPopBase, capaciteMax, croissance);
            // 4ticks -> 51,70pop
            var popAvecCroissance = MathUtils.FonctionLogistique(tick, nbPopBase, capaciteMax, croissance);

            Assert.AreEqual(popActuelle, (long)Math.Round(popAvecCroissance));
        }
    }
}

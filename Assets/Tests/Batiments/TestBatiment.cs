using NUnit.Framework;
using Batiment;

public class TestBatiment
{
    [Test]
    public void TestAccesQteMaison()
    {
        var qte = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MAISON);
        Assert.AreEqual(1, qte);
    }

    [Test]
    public void TestAccesQteFerme()
    {
        var qte = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.FERME);
        Assert.AreEqual(0, qte);
    }

    [Test]
    public void TestAccesQteScierie()
    {
        var qte = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.SCIERIE);
        Assert.AreEqual(0, qte);
    }

    [Test]
    public void TestAccesQteMine()
    {
        var qte = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MINE);
        Assert.AreEqual(0, qte);
    }

    [Test]
    public void TestAccesQteHotelDeVille()
    {
        var qte = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.HOTELDEVILLE);
        Assert.AreEqual(0, qte);
    }

    [Test]
    public void TestAccesQteMaxMaison()
    {
        var qteMax = GestionnaireBatiments.Instance.AccesQteMaxBatiment(BatimentEnum.MAISON);
        Assert.AreEqual(1000, qteMax);
    }

    [Test]
    public void TestAttribuerQteMaxMaison()
    {
        var qteMax = 3;
        GestionnaireBatiments.Instance.AttribuerQteMaxBatiment(BatimentEnum.MAISON, qteMax);
        var résultat = GestionnaireBatiments.Instance.AccesQteMaxBatiment(BatimentEnum.MAISON);
        Assert.AreEqual(qteMax, résultat);
    }

    [Test]
    public void TestConstructionMaisons()
    {
        // Démarrer la première construction
        var retour = GestionnaireBatiments.Instance.DemarrerConstruction(BatimentEnum.MAISON);
        Assert.IsTrue(retour);

        // La construction n'est pas fini
        int effortTot = GestionnaireBatiments.Instance.batimentConfigDic[BatimentEnum.MAISON].effortConstruction;
        retour = GestionnaireBatiments.Instance.AvancerConstruction(effortTot / 2);
        Assert.IsTrue(!retour);

        // Une seul construction à la fois
        retour = GestionnaireBatiments.Instance.DemarrerConstruction(BatimentEnum.MAISON);
        Assert.IsTrue(!retour);

        // Construction complétée
        retour = GestionnaireBatiments.Instance.AvancerConstruction((effortTot / 2) + 1);
        Assert.IsTrue(retour);

        // On devrait avoir une maison de plus
        var qte = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MAISON);
        Assert.AreEqual(2, qte);

        // La première construction devrait être fini, une nouvelle peut être lancé
        retour = GestionnaireBatiments.Instance.DemarrerConstruction(BatimentEnum.MAISON);
        Assert.IsTrue(retour);

        // La 2e construction est complétée
        retour = GestionnaireBatiments.Instance.AvancerConstruction(effortTot);
        Assert.IsTrue(retour);

        // On est au Maximum de maison, le démarrage de la construction est refusé
        retour = GestionnaireBatiments.Instance.DemarrerConstruction(BatimentEnum.MAISON);
        Assert.IsTrue(!retour);

        // On ne peut avancer si aucune construction n'est en cours
        retour = GestionnaireBatiments.Instance.AvancerConstruction(effortTot);
        Assert.IsTrue(!retour);

        // On est toujours au maximum de construction
        qte = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MAISON);
        var qteMax = GestionnaireBatiments.Instance.AccesQteMaxBatiment(BatimentEnum.MAISON);
        Assert.AreEqual(qteMax, qte);
    }
}

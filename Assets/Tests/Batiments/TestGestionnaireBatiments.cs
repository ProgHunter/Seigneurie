using NUnit.Framework;
using Batiment;

public class TestGestionnaireBatiments
{
    [Test]
    public void TestAttribuerEtAccesQteMaison()
    {
        var qteAttribuee = 2;
        GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MAISON, qteAttribuee);
        
        var qteRetour = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MAISON);
        
        Assert.AreEqual(qteAttribuee, qteRetour);
    }

    [Test]
    public void TestAttribuerEtAccesQteFerme()
    {
        var qteAttribuee = 2;
        GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.FERME, qteAttribuee);
        
        var qteRetour = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.FERME);
        
        Assert.AreEqual(qteAttribuee, qteRetour);
    }

    [Test]
    public void TestAttribuerEtAccesQteScierie()
    {
        var qteAttribuee = 2;
        GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.SCIERIE, qteAttribuee);

        var qteRetour = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.SCIERIE);

        Assert.AreEqual(qteAttribuee, qteRetour);
    }

    [Test]
    public void TestAttribuerEtAccesQteMine()
    {
        var qteAttribuee = 2;
        GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MINE, qteAttribuee);

        var qteRetour = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MINE);

        Assert.AreEqual(qteAttribuee, qteRetour);
    }

    [Test]
    public void TestAttribuerEtAccesQteHotelDeVille()
    {
        var qteAttribuee = 2;
        GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.HOTELDEVILLE, qteAttribuee);

        var qteRetour = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.HOTELDEVILLE);

        Assert.AreEqual(qteAttribuee, qteRetour);
    }

    [Test]
    public void TestAttribuerEtAccesQteMaxMaison()
    {
        var qteAttribuee = 3;
        GestionnaireBatiments.Instance.AttribuerQteMaxBatiment(BatimentEnum.MAISON, qteAttribuee);

        var qteRetour = GestionnaireBatiments.Instance.AccesQteMaxBatiment(BatimentEnum.MAISON);

        Assert.AreEqual(qteAttribuee, qteRetour);
    }

    [Test]
    public void TestConstruction3Maisons2Max()
    {
        var qteDebut = 0;
        var qteMax = 2;
        // Initialisation des donn�es du test
        GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MAISON, qteDebut);
        GestionnaireBatiments.Instance.AttribuerQteMaxBatiment(BatimentEnum.MAISON, qteMax);

        // D�marrer la premi�re construction
        var retour = GestionnaireBatiments.Instance.DemarrerConstruction(BatimentEnum.MAISON);
        Assert.IsTrue(retour);

        // La construction n'est pas fini
        int effortTot = GestionnaireBatiments.Instance.batimentConfigDict[BatimentEnum.MAISON].effortConstruction;
        retour = GestionnaireBatiments.Instance.AvancerConstruction(effortTot / 2);
        Assert.IsTrue(!retour);

        // Une seul construction � la fois
        retour = GestionnaireBatiments.Instance.DemarrerConstruction(BatimentEnum.MAISON);
        Assert.IsTrue(!retour);

        // Construction compl�t�e
        retour = GestionnaireBatiments.Instance.AvancerConstruction((effortTot / 2) + 1);
        Assert.IsTrue(retour);

        // On devrait avoir une maison de plus
        var qte = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MAISON);
        Assert.AreEqual(qteDebut + 1, qte);

        // La premi�re construction devrait �tre fini, une nouvelle peut �tre lanc�
        retour = GestionnaireBatiments.Instance.DemarrerConstruction(BatimentEnum.MAISON);
        Assert.IsTrue(retour);

        // La 2e construction est compl�t�e
        retour = GestionnaireBatiments.Instance.AvancerConstruction(effortTot);
        Assert.IsTrue(retour);

        // On est au Maximum de maison, le d�marrage de la 3e construction est refus�
        retour = GestionnaireBatiments.Instance.DemarrerConstruction(BatimentEnum.MAISON);
        Assert.IsTrue(!retour);

        // On ne peut avancer si aucune construction n'est en cours
        retour = GestionnaireBatiments.Instance.AvancerConstruction(effortTot);
        Assert.IsTrue(!retour);

        // On est toujours au maximum de construction
        qte = GestionnaireBatiments.Instance.AccesQteBatiment(BatimentEnum.MAISON);
        Assert.AreEqual(qteMax, qte);
    }
}

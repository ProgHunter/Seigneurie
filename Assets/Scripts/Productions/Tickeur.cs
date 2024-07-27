using Batiment;
using Profession;
using Ressource;
using UnityEngine;

namespace Production
{
    // TODO: Classe temporaire, restructuration à venir.
    public class Tickeur : MonoBehaviour
    {
        // Tick à chaque 2 secondes
        public float nbSecEntreTicks = 2f;

        // Appelé avant la première mise à jour de l'image
        // Innitialise des valeurs pour les ressources, batiments et professions
        void Start()
        {
            // Mettre toutes les productions de ressources à 25%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, 0);

            // Création de bâtiments
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MAISON, 100);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.FERME, 10);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.SCIERIE, 10);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MINE, 10);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.HOTELDEVILLE, 1);

            // Attribuer des ressources de base
            int qtePopBase = 100;
            int qteNourritureBase = 100;
            int qteBoisBase = 100;
            int qteMinerauxBase = 100;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            InventaireRessources.Instance.AttribuerQteRessource(ressourcesBase);

            // Commencer les ticks
            InvokeRepeating("Tick", nbSecEntreTicks, nbSecEntreTicks);
        }

        // Production à chaque tick
        public void Tick()
        {
            GestionnaireProductions.Instance.Production();
        }
    }
}

using Batiment;
using Profession;
using Ressource;
using UI;
using UnityEngine;

namespace Production
{
    // TODO: Classe temporaire, restructuration � venir.
    public class Tickeur : MonoBehaviour
    {
        [SerializeField] private MainUI _ui;
        // Tick � chaque 2 secondes
        public float NbSecEntreTicks = 2f;

        // Appel� avant la premi�re mise � jour de l'image
        // Innitialise des valeurs pour les ressources, batiments et professions
        void Start()
        {
            // Mettre toutes les productions de ressources � 25%
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 25);
            GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, 0);

            // Cr�ation de b�timents
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
            _ui.Init();
            // Commencer les ticks
            InvokeRepeating("Tick", NbSecEntreTicks, NbSecEntreTicks);
        }

        // Production � chaque tick
        public void Tick()
        {
            GestionnaireProductions.Instance.Production();
            _ui.UpdateAll();
        }
    }
}

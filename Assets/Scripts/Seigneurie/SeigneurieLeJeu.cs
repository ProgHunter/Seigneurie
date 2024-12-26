using System;
using Batiment;
using Production;
using Profession;
using Ressource;
using UI;
using UnityEngine;

namespace Seigneurie
{
    public class SeigneurieLeJeu : MonoBehaviour
    {
        private class Tickeur
        {
            public Tickeur(float delta)
            {
                _deltaTempsAvantLeProchainTick = delta;
                InitalizeDeltaTemps();
            }

            public Action doitTicker;
            private float _tempsDuProchainTick;
            private float _deltaTempsAvantLeProchainTick;
            public void Tick()
            {
                if (doitTicker == null)
                    return;

                if (Time.time < _tempsDuProchainTick)
                    return;
                    
                doitTicker();
                _tempsDuProchainTick += _deltaTempsAvantLeProchainTick;
            }

            private void InitalizeDeltaTemps()
            {
                _tempsDuProchainTick = Time.time + _deltaTempsAvantLeProchainTick;
            }
        }
        
        [SerializeField] private MainUI _ui;

        private Tickeur _tickeur;
        // Tick � chaque 2 secondes
        public float NbSecEntreTicks= 2f;

        // Appel� avant la premi�re mise � jour de l'image
        // Innitialise des valeurs pour les ressources, batiments et professions
        private void Start()
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
            _tickeur = new Tickeur(NbSecEntreTicks);
            _tickeur.doitTicker += AuTick;
        }

        public void Update()
        {
            _tickeur?.Tick();
        }

        private void AuTick()
        {
            GestionnaireProductions.Instance.Production();
            _ui.UpdateAll();
        }
    }
}

using System;
using Batiment;
using Production;
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

            public Action SignalTick;
            private float _tempsDuProchainTick;
            private readonly float _deltaTempsAvantLeProchainTick;
            public void Tick()
            {
                if (SignalTick == null)
                    return;

                if (Time.time < _tempsDuProchainTick)
                    return;
                    
                SignalTick();
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
        public float NbSecEntreTicks = 2f;

        // Appel� avant la premi�re mise � jour de l'image
        // Innitialise des valeurs pour les ressources, batiments et professions
        private void Start()
        {
            AttribuerDesValeursDeDepart();

            _ui.Init();
            _tickeur = new Tickeur(NbSecEntreTicks);
            _tickeur.SignalTick += SignalerTick;
        }

        public void Update()
        {
            _tickeur?.Tick();
        }

        private void SignalerTick()
        {
            GestionnaireProductions.Instance.Production();
            _ui.UpdateAll();
        }

        /// <summary>
        /// Méthode pour attribuer des valeurs autre que le config au début du jeux.
        /// Pour tester seulement !
        /// </summary>
        private void AttribuerDesValeursDeDepart()
        {
            // Mettre toutes les productions de ressources � 25%
            //GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 25);
            //GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 25);
            //GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 25);
            //GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 25);
            //GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, 0);

            // Cr�ation de b�timents
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MAISON, 10);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.FERME, 1);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.SCIERIE, 0);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MINE, 0);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.HOTELDEVILLE, 0);

            // Attribuer des ressources de base
            int qtePopBase = 1_000;
            int qteNourritureBase = 100;
            int qteBoisBase = 1_000;
            int qteMinerauxBase = 1_000;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            InventaireRessources.Instance.AttribuerQteRessource(ressourcesBase);
        }
    }
}

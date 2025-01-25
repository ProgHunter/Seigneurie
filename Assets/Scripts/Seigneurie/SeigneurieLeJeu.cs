using System;
using Batiment;
using Production;
using Ressource;
using UI;
using UnityEngine;

namespace Seigneurie
{
    /// <summary>
    /// Classe principale qui gère le tick de tous les managers et du UI
    /// </summary>
    public class SeigneurieLeJeu : MonoBehaviour
    {
        /// <summary>
        /// Classe interne qui gère les ticks de temps et envoie un signal au moment du tick
        /// </summary>
        private class Tickeur
        {
            public Tickeur(float delta)
            {
                _deltaTempsAvantLeProchainTick = delta;
                InitalizeDeltaTemps();
            }

            public Action signalTick;
            private float _tempsDuProchainTick;
            private readonly float _deltaTempsAvantLeProchainTick;
            public void Tick()
            {
                if (signalTick == null)
                    return;

                if (Time.time < _tempsDuProchainTick)
                    return;
                    
                signalTick();
                _tempsDuProchainTick += _deltaTempsAvantLeProchainTick;
            }

            private void InitalizeDeltaTemps()
            {
                _tempsDuProchainTick = Time.time + _deltaTempsAvantLeProchainTick;
            }
        }
        
        [SerializeField] private MainUI _ui;

        private Tickeur _tickeur;
        // Tick à chaque 2 secondes
        public float NbSecEntreTicks = 2f;

        // Appelé avant la première mise à jour de l'image
        // Innitialise des valeurs pour les ressources, batiments et professions
        private void Start()
        {
            //AttribuerDesValeursDeDepart();

            _ui.Init();
            _tickeur = new Tickeur(NbSecEntreTicks);
            _tickeur.signalTick += EffectuerLeTick;
        }

        public void Update()
        {
            _tickeur?.Tick();
        }
        /// <summary>
        /// Effectue toutes les actions qui doivent se produirent au tick
        /// </summary>
        private void EffectuerLeTick()
        {
            GestionnaireProductions.Instance.Production();
            _ui.MetAJourLesVues();
        }

        /// <summary>
        /// Méthode pour attribuer des valeurs autre que le config au début du jeux.
        /// Pour tester seulement !
        /// </summary>
        private void AttribuerDesValeursDeDepart()
        {
            // Mettre toutes les productions de ressources à 25%
            //GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.NATALITE, 25);
            //GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.FERMIER, 25);
            //GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.BUCHERON, 25);
            //GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MINEUR, 25);
            //GestionnaireProfessions.Instance.AttribuerPourcentValide(ProfessionEnum.MACON, 0);

            // Création de bâtiments
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MAISON, 100);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.FERME, 10);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.SCIERIE, 0);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.MINE, 0);
            GestionnaireBatiments.Instance.AttribuerQteBatiment(BatimentEnum.HOTELDEVILLE, 0);

            // Attribuer des ressources de base
            int qtePopBase = 100;
            int qteNourritureBase = 1_000;
            int qteBoisBase = 1_000;
            int qteMinerauxBase = 1_100;
            LotRessources ressourcesBase = new(qtePopBase, qteNourritureBase, qteBoisBase, qteMinerauxBase);
            GestionnaireRessources.Instance.AttribuerQteRessource(ressourcesBase);
        }
    }
}

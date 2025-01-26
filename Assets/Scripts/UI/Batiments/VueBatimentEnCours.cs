using System;
using System.Globalization;
using Batiment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Batiments
{
    /// <summary>
    /// Ligne d'un bâtiment qui est en cours de construction qui affiche combien de ticks restes avant la fin de la construction
    /// </summary>
    public sealed class VueBatimentEnCours : MonoBehaviour, IDisposable
    {
        [SerializeField] private Image _icone;
        [SerializeField] private TextMeshProUGUI _nom;
        [SerializeField] private TextMeshProUGUI _completion;
        [SerializeField] private Button _boutonAnnuler;

        private Action _mettreAJourListBatimentsDisponibles;
        private Action _mettreAJourListeBatimentsEnCours;

        private const string _aucunMaçon = "Aucun maçon";
        private const string _ticksTexte = " tick(s)";

        private void Awake()
        {
            _boutonAnnuler.onClick.AddListener(AnnulerConstruction);
        }

        public void Init(Sprite icone, string nom, long nbTicksRestants, Action mettreAJourListBatimentsDisponibles, Action mettreAJourListeBatimentsEnCours)
        {
            //TODO icone
            _nom.text = nom;
            MetAJourTicksRestants(nbTicksRestants);
            _mettreAJourListBatimentsDisponibles += mettreAJourListBatimentsDisponibles;
            _mettreAJourListeBatimentsEnCours += mettreAJourListeBatimentsEnCours;
        }

        public void MetAJourTicksRestants(long nbTicksRestants)
        {
            string nbTicksRestantsTexte = nbTicksRestants == -1 ? _aucunMaçon : nbTicksRestants.ToString("#,0", CultureInfo.CurrentCulture) + _ticksTexte;
            _completion.text = nbTicksRestantsTexte;
        }
        
        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void AnnulerConstruction()
        {
            GestionnaireBatiments.Instance.AnnulerConstruction();
            // On doit mettre à jour la liste des bâtiments disponibles pour dégriser le bouton pour
            // construire un autre bâtiment.
            _mettreAJourListBatimentsDisponibles?.Invoke();
            // Se retirer de la liste
            _mettreAJourListeBatimentsEnCours?.Invoke();
        }
    }
}

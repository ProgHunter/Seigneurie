using System;
using Batiment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Batiments
{
    /// <summary>
    /// Ligne d'un bâtiment qui est en cours de construction qui affiche combien de ticks restes avant la fin de la construction
    /// </summary>
    public class VueBatimentEnCours : MonoBehaviour, IDisposable
    {
        [SerializeField] private Image _icone;
        [SerializeField] private TextMeshProUGUI _nom;
        [SerializeField] private TextMeshProUGUI _completion;
        [SerializeField] private Button _boutonAnnuler;
        
        private Action MettreAJourBatiments;

        private const string _aucunMaçon = "Aucun maçon";
        private const string _ticksTexte = " ticks";

        private void Awake()
        {
            _boutonAnnuler.onClick.AddListener(AnnulerConstruction);
        }

        public void Init(Sprite icone, string nom, long nbTicksRestants, Action mettreAJourBatiments)
        {
            //TODO icone
            _nom.text = nom;
            MetAJourTicksRestants(nbTicksRestants);
            MettreAJourBatiments = mettreAJourBatiments;
        }

        public void MetAJourTicksRestants(long nbTicksRestants)
        {
            string nbTicksRestantsTexte = nbTicksRestants == -1 ? _aucunMaçon : nbTicksRestants + _ticksTexte;
            _completion.text = nbTicksRestantsTexte;
        }
        
        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void AnnulerConstruction()
        {
            GestionnaireBatiments.Instance.AnnulerConstruction();
            MettreAJourBatiments?.Invoke();
        }
    }
}

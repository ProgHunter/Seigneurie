using UI.Batiments;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Un manager de toutes les Vues
    /// </summary>
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private VueRessources _vueRessources;
        [SerializeField] private VueGestionnaireProfessions _vueGestionnaireProfessions;
        [SerializeField] private VueGestionnaireProductions _vueGestionnaireProductions;
        [SerializeField] private VueGestionnaireBatiments _vueGestionnaireBatiments;
        [SerializeField] private GestionOnglets _onglets;

        private void Awake()
        {
            _onglets.QuandBoutonAppuyé += MettreAJourLesVues;
        }

        public void Init()
        {
            _vueRessources.Init();
            _vueGestionnaireProfessions.Init(_vueGestionnaireProductions.MettreAJourListeProductions);
            _vueGestionnaireProductions.Init(_vueGestionnaireProfessions);
            _vueGestionnaireBatiments.Init(MettreAJourLesVues);
        }
        
        public void MettreAJourLesVues()
        {
            _vueRessources.MettreAJourListeDeRessources();
            _vueGestionnaireBatiments.MettreAJourListeVuesBatiments();
            _vueGestionnaireProductions.MettreAJourListeProductions();
        }
    }
}
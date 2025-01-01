using UI.Batiments;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Kinda a manager for all UI
    /// </summary>
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private RessourcesUI _ressources;
        [SerializeField] private VueGestionnaireProfessions _vueGestionnaireProfessions;
        [SerializeField] private VueGestionnaireProductions _vueGestionnaireProductions;
        [SerializeField] private VueGestionnaireBatiments _vueGestionnaireBatiments;

        public void Init()
        {
            _ressources.Init();
            _vueGestionnaireProfessions.Init(_vueGestionnaireProductions.MiseAJourListeProductions);
            _vueGestionnaireProductions.Init(_vueGestionnaireProfessions);
            _vueGestionnaireBatiments.Init();
        }
        
        public void UpdateAll()
        {
            _ressources.UpdateBarre();
            _vueGestionnaireBatiments.UpdateVues();
            _vueGestionnaireProductions.MiseAJourListeProductions(true, true);
        }
    }
}
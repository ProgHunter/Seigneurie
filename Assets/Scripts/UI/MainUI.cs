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

        public void Init()
        {
            _ressources.Init();
            _vueGestionnaireProfessions.Init();
            _vueGestionnaireProductions.Init();
        }
        
        public void UpdateAll()
        {
            _ressources.UpdateBarre();
        }
    }
}
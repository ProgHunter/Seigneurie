using UnityEngine;

namespace UI.Batiments
{
    /// <summary>
    /// Gestionnaire des vues portants sur les bâtiments
    /// </summary>
    public class VueGestionnaireBatiments : MonoBehaviour
    {
        [SerializeField] private VueListeBatimentsDisponibles _batimentsDisponibles;
        [SerializeField] private VueListeBatimentsTerminés _batimentsTerminés;
        [SerializeField] private VueListeBatimentsEnCours _batimentsEnCours;
    
        public void Init()
        {
            _batimentsTerminés.InitListe();
            _batimentsDisponibles.InitListe();
            _batimentsEnCours.InitListe();
        }

        public void UpdateVues()
        {
            if (!gameObject.activeInHierarchy)
                return;

            _batimentsEnCours.UpdateListe();
            _batimentsTerminés.UpdateListe();
            _batimentsDisponibles.UpdateListe();
        }
    }
}

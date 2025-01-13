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

        public void MetAJourVues()
        {
            if (!gameObject.activeInHierarchy)
                return;

            _batimentsEnCours.MetAJourListe();
            _batimentsTerminés.MetAJourListe();
            _batimentsDisponibles.MetAJourListe();
        }
    }
}

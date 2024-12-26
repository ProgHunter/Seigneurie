using UnityEngine;

namespace UI.Batiments
{
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
            _batimentsEnCours.UpdateListe();
            _batimentsTerminés.UpdateListe();
            _batimentsDisponibles.UpdateListe();
        }
    }
}

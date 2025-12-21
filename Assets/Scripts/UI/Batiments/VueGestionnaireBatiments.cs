using System;
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

        public void Init(Action mettreAJourToutesLesVues)
        {
            _batimentsTerminés.InitListe();
            _batimentsDisponibles.InitListe(mettreAJourToutesLesVues);
            _batimentsEnCours.InitListe(_batimentsDisponibles.MettreAJourListe);
        }

        public void MettreAJourListeVuesBatiments()
        {
            if (!gameObject.activeInHierarchy)
                return;

            _batimentsEnCours.MettreAJourListe();
            _batimentsTerminés.MettreAJourListe();
            _batimentsDisponibles.MettreAJourListe();
        }
    }
}

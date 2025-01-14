using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Gestion des boutons onglets et de quelle onglet doit afficher sa page
    /// </summary>
    public class GestionOnglets : MonoBehaviour
    {
        [SerializeField] List<GameObject> _ongletsVues;
        private int _indexActif;
        public Action QuandBoutonAppuyé;
        public void Awake()
        {
            _ongletsVues[_indexActif].SetActive(true);
            MetAJourLesBoutonsOnglets();
        }

        //Utilisé dans Ui
        public void OngletAppuyé(int indexOnglet)
        {
            _indexActif = indexOnglet;
            MetAJourLesBoutonsOnglets();
            QuandBoutonAppuyé?.Invoke();
        }

        private void MetAJourLesBoutonsOnglets()
        {
            for (int index = 0; index < _ongletsVues.Count; index++)
            {
                GameObject fenetre = _ongletsVues[index];
                fenetre.SetActive(_indexActif == index);
            }
        }
    }
}
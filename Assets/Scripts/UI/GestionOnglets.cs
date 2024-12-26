using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GestionOnglets : MonoBehaviour
{
    [SerializeField] List<GameObject> _ongletsVues;
    private int _indexActif;
    public void Awake()
    {
        _ongletsVues[_indexActif].SetActive(true);
        UpdateLesVues();
    }

    //Utilisé dans Ui
    public void OngletAppuyé(int indexOnglet)
    {
        _indexActif = indexOnglet;
        UpdateLesVues();
    }

    private void UpdateLesVues()
    {
        for (int index = 0; index < _ongletsVues.Count; index++)
        {
            GameObject fenetre = _ongletsVues[index];
            fenetre.SetActive(_indexActif == index);
        }
    }
}
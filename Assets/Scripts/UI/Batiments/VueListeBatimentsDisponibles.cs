using System;
using System.Collections.Generic;
using System.Linq;
using Batiment;
using UnityEngine;

namespace UI.Batiments
{
    /// <summary>
    /// Liste des bâtiments disponibles à la construction
    /// </summary>
    public class VueListeBatimentsDisponibles : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private VueBatimentDisponible _vueInstancier;
        private readonly Dictionary<BatimentEnum, VueBatimentDisponible> _dictBatimentsDisponibles = new();

        public Action MettreAJourBatimentsEnCours;
        public void InitListe()
        {
            MetAJourListe();
        }
        
        public void MetAJourListe()
        {
            if (!gameObject.activeInHierarchy)
                return;

            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            List<BatimentEnum> batiments = gestionnaireBatiments.BatimentConfigDict.Keys.ToList();
            foreach (var batiment in batiments)
            {
                // Le bâtiment ne doit pas être affiché dans la liste
                if (!gestionnaireBatiments.EstDeverrouille(batiment) ||
                    gestionnaireBatiments.AccesQteBatiment(batiment) >= gestionnaireBatiments.BatimentConfigDict[batiment].Qte.qteMax)
                {
                    // S'il est dans la liste, il faut le supprimer
                    if (_dictBatimentsDisponibles.ContainsKey(batiment))
                    {
                        _dictBatimentsDisponibles[batiment].Dispose();
                        _dictBatimentsDisponibles.Remove(batiment);
                    }
                    
                    continue;
                }
                // Le batiment doit être affiché dans la liste
                // Ajouter le bâtiment dans la liste
                if (!_dictBatimentsDisponibles.ContainsKey(batiment))
                    AjouterVueBatiment(batiment);

                _dictBatimentsDisponibles[batiment].MetAjourValeurs();
            }
        }

        private void AjouterVueBatiment(BatimentEnum batiment)
        {
            VueBatimentDisponible vue = Instantiate(_vueInstancier, _parent);
            AbstraitBatimentConfig config = GestionnaireBatiments.Instance.BatimentConfigDict[batiment];

            vue.Init(batiment, config.Icone, config.Nom, config.CoutConstruction.ToString(), config.Description,MettreAJourBatimentsEnCours);
            _dictBatimentsDisponibles.Add(batiment, vue);
        }
    }
}

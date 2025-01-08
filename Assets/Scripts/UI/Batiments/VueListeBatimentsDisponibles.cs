using System.Collections.Generic;
using Batiment;
using UnityEngine;
using Utils;

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
    
        public void InitListe()
        {
            var batiments = EnumUtils.GetEnumValues<BatimentEnum>();
            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            foreach (var batiment in batiments)
            {
                if (!gestionnaireBatiments.EstDeverrouille(batiment) ||
                    gestionnaireBatiments.AccesQteBatiment(batiment) >= gestionnaireBatiments.BatimentConfigDict[batiment].Qte.qteMax)
                    continue;

                AjouterVueBatiment(batiment);
            }
        }
        
        public void UpdateListe()
        {
            if (!gameObject.activeInHierarchy)
                return;

            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var batiments = EnumUtils.GetEnumValues<BatimentEnum>();
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

                _dictBatimentsDisponibles[batiment].UpdateVue();
            }
        }

        private void AjouterVueBatiment(BatimentEnum batiment)
        {
            VueBatimentDisponible vue = Instantiate(_vueInstancier, _parent);
            AbstraitBatimentConfig config = GestionnaireBatiments.Instance.BatimentConfigDict[batiment];

            vue.Init(batiment, config.Icone, config.Nom, config.CoutConstruction.ToString(), config.Description);
            _dictBatimentsDisponibles.Add(batiment, vue);
        }
    }
}

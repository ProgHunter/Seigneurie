using System.Collections.Generic;
using Batiment;
using UnityEngine;
using Utils;

namespace UI.Batiments
{
    public class VueListeBatimentsDisponibles : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        private readonly Dictionary<BatimentEnum, VueBatimentDisponible> _dictBatimentsDisponibles = new();
        [SerializeField] private VueBatimentDisponible _vueInstancier;
    
        public void InitListe()
        {
            foreach (var batiment in EnumUtils.GetEnumValues<BatimentEnum>())
            {
                if(!GestionnaireBatiments.Instance.EstDeverrouille(batiment))
                    continue;
                VueBatimentDisponible vue = Instantiate(_vueInstancier, _content);
                var config = GestionnaireBatiments.Instance.BatimentConfigDict[batiment];
                //TODO afficher le nom des ressources pas en majuscules
                vue.Init(batiment,config.Icone, config.Nom, config.CoutConstruction.ToString(), config.Description, config.EffortConstruction.ToString());
                _dictBatimentsDisponibles.Add(batiment,vue);
            }
        }
        
        public void UpdateListe()
        {
            foreach (var batiment in EnumUtils.GetEnumValues<BatimentEnum>())
            {
                if (_dictBatimentsDisponibles.ContainsKey(batiment) || !GestionnaireBatiments.Instance.EstDeverrouille(batiment))
                {
                    continue;
                }
                    
                VueBatimentDisponible vue = Instantiate(_vueInstancier, _content);
                AbstraitBatimentConfig config = GestionnaireBatiments.Instance.BatimentConfigDict[batiment];
                vue.Init(batiment,config.Icone, config.Nom, config.CoutConstruction.ToString(), config.Description, config.EffortConstruction.ToString());
                _dictBatimentsDisponibles.Add(batiment,vue);
            }
        }
    }
}

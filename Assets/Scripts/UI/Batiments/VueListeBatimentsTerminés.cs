using System.Collections.Generic;
using Batiment;
using UnityEngine;
using Utils;

namespace UI.Batiments
{
    public class VueListeBatimentsTerminés : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        //private readonly List<VueBatimentConstruits> _listeBatimentsConstruits = new();
        private readonly Dictionary<BatimentEnum, VueBatimentTerminé> _dictBatimentsConstruits = new();
        [SerializeField] private VueBatimentTerminé _vueInstancier;
        public void InitListe()
        {
            foreach (var batiment in EnumUtils.GetEnumValues<BatimentEnum>())
            {
                long qte = GestionnaireBatiments.Instance.AccesQteBatiment(batiment);
                if (qte == 0)
                {
                    continue;
                }

                VueBatimentTerminé vue = Instantiate(_vueInstancier, _content);
                var config = GestionnaireBatiments.Instance.BatimentConfigDict[batiment];
                vue.Init(config.Icone, config.Nom, qte);
                _dictBatimentsConstruits.Add(batiment,vue);
            }
        }
        public void UpdateListe()
        {
            foreach (var batiment in EnumUtils.GetEnumValues<BatimentEnum>())
            {
                long qte = GestionnaireBatiments.Instance.AccesQteBatiment(batiment);
                if (_dictBatimentsConstruits.ContainsKey(batiment))
                {
                    _dictBatimentsConstruits[batiment].UpdateQte(qte);
                    continue;
                }
                
                VueBatimentTerminé vue = Instantiate(_vueInstancier, _content);
                var config = GestionnaireBatiments.Instance.BatimentConfigDict[batiment];
                vue.Init(config.Icone, config.Nom, qte);
                _dictBatimentsConstruits.Add(batiment,vue);
            }
        }
    }
}

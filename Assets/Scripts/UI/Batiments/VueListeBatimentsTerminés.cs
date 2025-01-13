using System.Collections.Generic;
using Batiment;
using UnityEngine;
using Utils;

namespace UI.Batiments
{
    /// <summary>
    /// Liste des bâtiments déjà construits
    /// </summary>
    public class VueListeBatimentsTerminés : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private VueBatimentTerminé _vueInstancier;
        private readonly Dictionary<BatimentEnum, VueBatimentTerminé> _dictBatimentsConstruits = new();

        public void InitListe()
        {
            MetAJourListe();
        }

        public void MetAJourListe()
        {
            if (!gameObject.activeInHierarchy)
                return;

            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            var batiments = EnumUtils.GetEnumValues<BatimentEnum>();
            foreach (var batiment in batiments)
            {
                long qte = gestionnaireBatiments.AccesQteBatiment(batiment);
                // Le bâtiment ne doit pas être affiché dans la liste
                if (qte <= 0)
                {
                    // S'il est dans la liste, il faut le supprimer
                    if (_dictBatimentsConstruits.ContainsKey(batiment))
                    {
                        _dictBatimentsConstruits[batiment].Dispose();
                        _dictBatimentsConstruits.Remove(batiment);
                    }

                    continue;
                }

                // Le batiment doit être affiché dans la liste
                // Ajouter le bâtiment dans la liste
                if (!_dictBatimentsConstruits.ContainsKey(batiment))
                    AjouterVueBatiment(batiment, qte);
                
                _dictBatimentsConstruits[batiment].UpdateValeurs(qte);
            }
        }

        private void AjouterVueBatiment(BatimentEnum batiment, long qte)
        {
            VueBatimentTerminé vue = Instantiate(_vueInstancier, _parent);
            var config = GestionnaireBatiments.Instance.BatimentConfigDict[batiment];

            vue.Init(config.Icone, config.Nom, qte);
            _dictBatimentsConstruits.Add(batiment, vue);
        }
    }
}

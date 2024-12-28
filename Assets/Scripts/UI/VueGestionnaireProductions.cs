using System.Collections.Generic;
using Production;
using Profession;
using Ressource;
using UnityEngine;
using Utils;

namespace UI
{
    public class VueGestionnaireProductions : MonoBehaviour
    {
        private VueGestionnaireProfessions _vueGestionnaireProfessions;
        [SerializeField] private List<VueProductionRessource> _listeProductions;

        public void Init(VueGestionnaireProfessions vueGestionnaireProfessions)
        {
            if (vueGestionnaireProfessions == null)
                Debug.LogError("vueGestionnaireProfessions est null à la création de VueGestionnaireProductions.");

            _vueGestionnaireProfessions = vueGestionnaireProfessions;
            
            int i = 0;
            foreach (var ressource in EnumUtils.GetEnumValues<RessourceEnum>())
            {
                if (_listeProductions != null)
                {
                    _listeProductions[i]?.InitRessourceRepresentee(ressource);
                }

                i++;
            }
        }

        /// <summary>
        /// Permet de mettre à jour les valeurs de production actuelle et/ou anticipée.
        /// </summary>
        /// <param name="productionActuelle">Mettre à jour la vue de la production actuelle</param>
        /// <param name="productionAnticipée">Mettre à jour la vue de la production anticipée</param>
        public void MiseAJourAffichageValeursProduction(bool productionActuelle, bool productionAnticipée)
        {
            LotProfessions professions = null;
            if (productionAnticipée)
                professions = _vueGestionnaireProfessions?.AccesLotProfessionUtilisateur();

            foreach (var productionRessource in _listeProductions)
            {
                if(productionAnticipée)
                    productionRessource.ModifierValeurProductionAnticipee(professions);

                if(productionActuelle)
                    productionRessource.ModifierValeurProductionActuelle();
            }
        }
    }
}
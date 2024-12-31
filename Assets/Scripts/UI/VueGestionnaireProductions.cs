using System.Collections.Generic;
using Profession;
using Ressource;
using UnityEngine;
using Utils;

namespace UI
{
    public class VueGestionnaireProductions : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private VueProductionRessource _vueInstancier;
        private VueGestionnaireProfessions _vueGestionnaireProfessions;
        private readonly Dictionary<RessourceEnum, VueProductionRessource> _dictRessources = new();

        public void Init(VueGestionnaireProfessions vueGestionnaireProfessions)
        {
            if (vueGestionnaireProfessions == null)
                Debug.LogError("vueGestionnaireProfessions est null à la création de VueGestionnaireProductions.");

            _vueGestionnaireProfessions = vueGestionnaireProfessions;

            var ressources = EnumUtils.GetEnumValues<RessourceEnum>();
            foreach (var ressource in ressources)
            {
                if (!InventaireRessources.Instance.EstDeverrouille(ressource))
                    continue;

                AjouterVueProduction(ressource);
            }
        }

        /// <summary>
        /// Permet de mettre à jour les valeurs de production actuelle et/ou anticipée.
        /// </summary>
        /// <param name="productionActuelle">Mettre à jour la vue de la production actuelle</param>
        /// <param name="productionAnticipée">Mettre à jour la vue de la production anticipée</param>
        public void MiseAJourAffichageValeursProduction(bool productionActuelle, bool productionAnticipée)
        {
            if (!gameObject.activeInHierarchy)
                return;

            LotProfessions professions = null;
            if (productionAnticipée)
                professions = _vueGestionnaireProfessions?.AccesLotProfessionUtilisateur();

            var ressources = EnumUtils.GetEnumValues<RessourceEnum>();
            foreach (var ressource in ressources)
            {
                if (!InventaireRessources.Instance.EstDeverrouille(ressource))
                    continue;

                if (!_dictRessources.ContainsKey(ressource))
                    AjouterVueProduction(ressource);

                if (productionAnticipée)
                    _dictRessources[ressource].ModifierValeurProductionAnticipee(professions);

                if (productionActuelle)
                    _dictRessources[ressource].ModifierValeurProductionActuelle();
            }
        }

        private void AjouterVueProduction(RessourceEnum ressource)
        {
            VueProductionRessource vue = Instantiate(_vueInstancier, _parent);
            var config = InventaireRessources.Instance.RessourceConfigDict[ressource];

            vue.InitRessourceRepresentee(ressource);
            _dictRessources.Add(ressource, vue);
        }
    }
}
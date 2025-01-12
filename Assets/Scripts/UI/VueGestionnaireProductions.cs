using System.Collections.Generic;
using Profession;
using Ressource;
using UnityEngine;
using Utils;

namespace UI
{
    /// <summary>
    /// Affiche la liste des ressources produites ainsi que leurs quantités produites actuelles et anticipée selon l'état de l'assignation des professions
    /// </summary>
    public class VueGestionnaireProductions : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private VueProductionRessource _vueInstancier;
        private VueGestionnaireProfessions _vueGestionnaireProfessions;
        private readonly Dictionary<RessourceEnum, VueProductionRessource> _dictRessources = new();

        public void Init(VueGestionnaireProfessions vueGestionnaireProfessions)
        {
            if (vueGestionnaireProfessions == null)
                Debug.LogError("vueGestionnaireProfessions est null � la cr�ation de VueGestionnaireProductions.");

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
        /// Permet de mettre � jour la liste des productions affich�es.
        /// Met aussi � jour les valeurs de production actuelle et/ou anticip�e.
        /// </summary>
        /// <param name="productionActuelle">Mettre � jour la vue de la production actuelle</param>
        /// <param name="productionAnticipée">Mettre � jour la vue de la production anticip�e</param>
        public void MiseAJourListeProductions(bool productionActuelle, bool productionAnticipée)
        {
            if (!gameObject.activeInHierarchy)
                return;

            LotProfessions professions = null;
            if (productionAnticipée)
                professions = _vueGestionnaireProfessions.AccesLotProfessionUtilisateur();

            var inventaireRessources = InventaireRessources.Instance;
            var ressourceEnum = EnumUtils.GetEnumValues<RessourceEnum>();
            
            foreach (var ressource in ressourceEnum)
            {
                //On continue si c'est une ressource verrouillée
                if (UpdateListeRessourcesVisibles(inventaireRessources, ressource)) continue;

                UpdateValeursProductions(productionActuelle, productionAnticipée, ressource, professions);
            }
        }
        /// <summary>
        /// Retourne vrai si cette ressource est verrouillée
        /// </summary>
        /// <param name="inventaireRessources"></param>
        /// <param name="ressource"></param>
        /// <returns></returns>
        private bool UpdateListeRessourcesVisibles(InventaireRessources inventaireRessources, RessourceEnum ressource)
        {
            if (!inventaireRessources.EstDeverrouille(ressource))
            {
                if (_dictRessources.ContainsKey(ressource))
                {
                    _dictRessources[ressource].Dispose();
                    _dictRessources.Remove(ressource);
                }

                return true;
            }

            if (!_dictRessources.ContainsKey(ressource))
                AjouterVueProduction(ressource);
            return false;
        }

        private void UpdateValeursProductions(bool productionActuelle, bool productionAnticipée, RessourceEnum ressource,
            LotProfessions professions)
        {
            if (productionAnticipée)
                _dictRessources[ressource].ModifierValeurProductionAnticipee(professions);

            if (productionActuelle)
                _dictRessources[ressource].ModifierValeurProductionActuelle();
        }

        private void AjouterVueProduction(RessourceEnum ressource)
        {
            VueProductionRessource vue = Instantiate(_vueInstancier, _parent);

            vue.InitRessourceRepresentee(ressource);
            _dictRessources.Add(ressource, vue);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Profession;
using Ressource;
using UnityEngine;

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
                Debug.LogError("vueGestionnaireProfessions est null à la création de VueGestionnaireProductions.");

            _vueGestionnaireProfessions = vueGestionnaireProfessions;

            MettreAJourListeProductions();
        }

        /// <summary>
        /// Permet de mettre à jour la liste des productions affichées.
        /// Met aussi à jour les valeurs de production actuelle et/ou anticipée.
        /// </summary>
        /// <param name="productionActuelle">Mettre à jour la vue de la production actuelle</param>
        /// <param name="productionAnticipée">Mettre à jour la vue de la production anticipée</param>
        public void MettreAJourListeProductions(bool productionActuelle = true, bool productionAnticipée = true)
        {
            if (!gameObject.activeInHierarchy)
                return;

            LotProfessions professions = null;
            if (productionAnticipée)
                professions = _vueGestionnaireProfessions.AccesLotProfessionUtilisateur();

            var inventaireRessources = GestionnaireRessources.Instance;
            List<RessourceEnum> ressourceEnum = inventaireRessources.RessourceConfigDict.Keys.ToList();
            
            foreach (var ressource in ressourceEnum)
            {
                //On continue si c'est une ressource verrouillée
                if (MetAJourListeRessourcesVisibles(inventaireRessources, ressource)) continue;

                MetAJourValeursProductions(productionActuelle, productionAnticipée, ressource, professions);
            }
        }

        /// <summary>
        /// Retourne vrai si cette ressource est verrouillée
        /// </summary>
        /// <param name="inventaireRessources"></param>
        /// <param name="ressource"></param>
        /// <returns></returns>
        private bool MetAJourListeRessourcesVisibles(GestionnaireRessources inventaireRessources, RessourceEnum ressource)
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

        private void MetAJourValeursProductions(bool productionActuelle, bool productionAnticipée, RessourceEnum ressource,
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
using Production;
using Profession;
using Ressource;
using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Ligne de la production actuelle et anticipée d'une ressource
    /// </summary>
    public sealed class VueProductionRessource : MonoBehaviour, IDisposable
    {
        [SerializeField] private TextMeshProUGUI _nom;
        [SerializeField] private TextMeshProUGUI _productionActuelle;
        [SerializeField] private TextMeshProUGUI _productionAnticipee;

        private RessourceEnum _ressourceRepresentee;

        public void InitRessourceRepresentee(RessourceEnum value)
        {
            _ressourceRepresentee = value;
            _nom.text = GestionnaireRessources.Instance.RessourceConfigDict[_ressourceRepresentee].Nom;
        }

        public void ModifierValeurProductionActuelle()
        {
            var production = GestionnaireProductions.Instance.EvaluerProduction(_ressourceRepresentee);
            _productionActuelle.text = production.ToString("#,0", CultureInfo.CurrentCulture) + "/tick";
        }

        public void ModifierValeurProductionAnticipee(LotProfessions professions = null)
        {
            var production = GestionnaireProductions.Instance.EvaluerProduction(_ressourceRepresentee, professions);
            _productionAnticipee.text = production.ToString("#,0", CultureInfo.CurrentCulture) + "/tick";
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
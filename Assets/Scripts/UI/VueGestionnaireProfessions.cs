using System;
using System.Collections.Generic;
using System.Linq;
using Profession;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    /// <summary>
    /// Gestion des assignations à chaque profession
    /// </summary>
    public class VueGestionnaireProfessions : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private VueAssignationProfessions _vueInstancier;
        [SerializeField] private TextMeshProUGUI _pourcentageRestant;
        [SerializeField] private Button _boutonSoumettre;
        private readonly Dictionary<ProfessionEnum, VueAssignationProfessions> _dictProfession = new();

        private Action<bool, bool> _professionsSontModifiées;

        public void Awake()
        {
            _boutonSoumettre.onClick.AddListener(SoumettreChangement);
        }

        public void Init(Action<bool,bool> professionsSontModifiées)
        {
            MiseAJourListeProfessions();

            _professionsSontModifiées = professionsSontModifiées;
        }

        public LotProfessions AccesLotProfessionUtilisateur()
        {
            LotProfessions lotProfessions = new LotProfessions();
            List<ProfessionEnum> professions = GestionnaireProfessions.Instance.ProfessionDictConfig.Keys.ToList();
            foreach (var profession in professions)
            {
                var pourcentActuelle = 0;
                if (_dictProfession.ContainsKey(profession))
                    pourcentActuelle = _dictProfession[profession].AccesPourcentageActuel();

                lotProfessions.AttribuerPourcentProfession(profession, pourcentActuelle);
            }
            return lotProfessions;
        }

        /// <summary>
        /// (Action) Appelé lorsqu'un pourcentage de la liste de profession a changé de valeur.
        /// Le pourcentage de population libre est mis à jour.
        /// La production anticipé est mise à jour.
        /// </summary>
        private void UnPourcentageEstModifie()
        {
            int total = 0;
            foreach ((var profession, var vueProfession) in _dictProfession)
                total += vueProfession.AccesPourcentageActuel();

            AttribuerTextePourcentageRestant(total);
            _boutonSoumettre.interactable = total <= 100;

            _professionsSontModifiées?.Invoke(false, true);
        }

        public void MiseAJourListeProfessions()
        {
            if (!gameObject.activeInHierarchy)
                return;

            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            List<ProfessionEnum> professions = GestionnaireProfessions.Instance.ProfessionDictConfig.Keys.ToList();
            foreach (var profession in professions)
            {
                if (!gestionnaireProfessions.EstDeverrouille(profession))
                {
                    if (_dictProfession.ContainsKey(profession))
                    {
                        _dictProfession[profession].Dispose();
                        _dictProfession.Remove(profession);
                    }

                    continue;
                }

                if (!_dictProfession.ContainsKey(profession))
                    AjouterVueProfession(profession);
            }
        }

        private void AttribuerTextePourcentageRestant(int total)
        {
            _pourcentageRestant.text = 100 - total + "%";
        }

        private void SoumettreChangement()
        {
            int total = 0;
            foreach ((var profession, var vueProfession) in _dictProfession)
                total += vueProfession.AccesPourcentageActuel();

            if (total > 100)
            {
                Debug.LogError("Total des lotProfessions assignées incorrectes");
                return;
            }

            LotProfessions lotProfessions = AccesLotProfessionUtilisateur();
            GestionnaireProfessions.Instance.AttribuerPourcentValide(lotProfessions);
            // Valider si le gestionnaire est en phase avec le UI
            if (!lotProfessions.EstEgale(GestionnaireProfessions.Instance.Professions))
            {
                Debug.LogError("Le pourcentage des professions du UI sont désynchronisé du gestionnaire.");
                //TODO: Idéalement corriger les sliders et valeurs du UI pour celle du gestionnaire.
            }

            _professionsSontModifiées?.Invoke(true, false);
        }

        private void AjouterVueProfession(ProfessionEnum profession)
        {
            VueAssignationProfessions vue = Instantiate(_vueInstancier, _parent);
            _dictProfession.Add(profession, vue);
            vue.InitProfession(profession, UnPourcentageEstModifie);
        }
    }
}

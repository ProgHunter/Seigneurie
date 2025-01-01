using System;
using System.Collections.Generic;
using Profession;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class VueGestionnaireProfessions : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private AssignationProfessionsUI _vueInstancier;
        [SerializeField] private TextMeshProUGUI _pourcentageRestant;
        [SerializeField] private Button _boutonSoumettre;
        private readonly Dictionary<ProfessionEnum, AssignationProfessionsUI> _dictProfession = new();

        private Action<bool, bool> _professionsSontModifiées;

        public void Awake()
        {
            _boutonSoumettre.onClick.AddListener(SoumettreChangement);
        }

        public void Init(Action<bool,bool> professionsSontModifiées)
        {
            var professions = EnumUtils.GetEnumValues<ProfessionEnum>();
            foreach (var profession in professions)
            {
                if (!GestionnaireProfessions.Instance.EstDeverrouille(profession))
                    continue;

                AjouterVueProfession(profession);
            }

            _professionsSontModifiées = professionsSontModifiées;
        }

        public LotProfessions AccesLotProfessionUtilisateur()
        {
            LotProfessions lotProfessions = new LotProfessions();
            var professions = EnumUtils.GetEnumValues<ProfessionEnum>();
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
        public void UnPourcentageEstModifie()
        {
            int total = 0;
            foreach ((var profession, var vueProfession) in _dictProfession)
                total += vueProfession.AccesPourcentageActuel();

            AttribuerTextePourcentageRestant(total);

            _professionsSontModifiées?.Invoke(false, true);
        }

        public void MiseAJourListeProfessions()
        {
            if (!gameObject.activeInHierarchy)
                return;

            var gestionnaireProfessions = GestionnaireProfessions.Instance;
            var professions = EnumUtils.GetEnumValues<ProfessionEnum>();
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
                // TODO: Ajouter un retour utilisateur
                return;
            }

            LotProfessions lotProfessions = AccesLotProfessionUtilisateur();
            GestionnaireProfessions.Instance.AttribuerPourcentValide(lotProfessions);

            _professionsSontModifiées?.Invoke(true, false);
        }

        private void AjouterVueProfession(ProfessionEnum profession)
        {
            AssignationProfessionsUI vue = Instantiate(_vueInstancier, _parent);
            var config = GestionnaireProfessions.Instance.ProfessionDictConfig[profession];

            vue.InitProfession(profession, UnPourcentageEstModifie); ;
            _dictProfession.Add(profession, vue);
        }
    }
}

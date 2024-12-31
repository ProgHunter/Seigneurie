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
        [SerializeField] private TextMeshProUGUI _pourcentageRestant;
        [SerializeField] private List<AssignationProfessionsUI> _assignationsProfessions;
        [SerializeField] private Button _boutonSoumettre;
        //private readonly Dictionary<ProfessionEnum, AssignationProfessionsUI> _dictProfession = new();

        private Action<bool, bool> _professionsSontModifiées;

        public void Awake()
        {
            _boutonSoumettre.onClick.AddListener(SoumettreChangement);
        }

        public void Init( Action<bool,bool> professionsSontModifiées)
        {
            int i = 0;
            foreach (var profession in EnumUtils.GetEnumValues<ProfessionEnum>())
            {
                if (_assignationsProfessions != null)
                    _assignationsProfessions[i]?.InitProfession(profession, UnPourcentageEstModifie);

                i++;
            }

            _professionsSontModifiées = professionsSontModifiées;
        }

        public LotProfessions AccesLotProfessionUtilisateur()
        {
            LotProfessions professions = new LotProfessions();
            int i = 0;
            foreach(var profession in EnumUtils.GetEnumValues<ProfessionEnum>())
            {
                professions.AttribuerPourcentProfession(profession, (long)_assignationsProfessions[i]?.AccesPourcentageActuel());
                i++;
            }
            return professions;
        }

        /// <summary>
        /// (Action) Appelé lorsqu'un pourcentage de la liste de profession a changé de valeur.
        /// Le pourcentage de population libre est mis à jour.
        /// La production anticipé est mise à jour.
        /// </summary>
        public void UnPourcentageEstModifie()
        {
            int total = 0;
            
            foreach (var profession in _assignationsProfessions)
            {
                total += profession.AccesPourcentageActuel();
            }
            _pourcentageRestant.text = 100-total + "%";

            _professionsSontModifiées?.Invoke(false, true);
        }

        private void SoumettreChangement()
        {
            int total = 0;
            
            foreach (var profession in _assignationsProfessions)
            {
                
                total += profession.AccesPourcentageActuel();
            }

            if (total > 100)
            {
                Debug.LogError("Total des professions assignées incorrectes");
                // TODO: Ajouter un retour utilisateur
                return;
            }

            LotProfessions lotProfessions = AccesLotProfessionUtilisateur();
            GestionnaireProfessions.Instance.AttribuerPourcentValide(lotProfessions);

            _professionsSontModifiées?.Invoke(true, false);
        }
    }
}

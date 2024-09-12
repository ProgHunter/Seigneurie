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

        public void Awake()
        {
            _boutonSoumettre.onClick.AddListener(SoumettreChangement);
        }

        public void Init()
        {
            int i = 0;
            foreach (var profession in EnumUtils.GetEnumValues<ProfessionEnum>())
            {
                if (_assignationsProfessions != null)
                {
                    _assignationsProfessions[i]?.InitProfession(profession, UpdatePourcentageRestant);
                }

                i++;
            }
        }

        public void UpdatePourcentageRestant()
        {
            int total = 0;
            
            foreach (var profession in _assignationsProfessions)
            {
                total += profession.GetPourcentageActuel();
            }
            _pourcentageRestant.text = 100-total + "%";
        }

        private void SoumettreChangement()
        {
            int total = 0;
            
            foreach (var profession in _assignationsProfessions)
            {
                total += profession.GetPourcentageActuel();
            }

            if (total > 100)
            {
                Debug.LogError("Total des professions assignées incorrectes");
            }
            foreach (var profession in _assignationsProfessions)
            {
                GestionnaireProfessions.Instance.AttribuerPourcent(profession.GetProfession(), profession.GetPourcentageActuel());
            }
            
            UpdatePourcentageRestant();
        }
    }
}

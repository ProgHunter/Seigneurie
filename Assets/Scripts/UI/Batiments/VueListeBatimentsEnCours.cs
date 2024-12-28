using Batiment;
using Production;
using Profession;
using UnityEngine;

namespace UI.Batiments
{
    public class VueListeBatimentsEnCours : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private VueBatimentEnCours _vueInstancier;
        [SerializeField] private GameObject _msgAucunEnCours;
        private VueBatimentEnCours _vueBatiment;
        public void InitListe()
        {
            UpdateListe();
        }

        public void UpdateListe()
        {
            if (!gameObject.activeInHierarchy)
                return;
            
            if (GestionnaireBatiments.Instance.ConstructionEstEnCours())
            {
                var batiment = GestionnaireBatiments.Instance.EnConstruction;
                long pourcentMacon = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
                long nbTicks = GestionnaireProductions.Instance.NbTicksRestantsConstruction(pourcentMacon);
                if (_vueBatiment == null)
                {
                    _vueBatiment = Instantiate(_vueInstancier, _content);
                    string nom = GestionnaireBatiments.Instance.BatimentConfigDict[batiment.Item1].Nom;
                    _vueBatiment.Init(null, nom, nbTicks.ToString());
                }
                else
                {
                    _vueBatiment.UpdateValeurs(nbTicks.ToString()); 
                }

            }
            else
            {
                if (_vueBatiment != null)
                {
                    Destroy(_vueBatiment.gameObject);
                    _vueBatiment = null;
                    Debug.Log("Bâtiment terminé de construire");
                }
            }
            
            UpdateMessageListeVide();
        }

        private void UpdateMessageListeVide()
        {
            _msgAucunEnCours.SetActive(!GestionnaireBatiments.Instance.ConstructionEstEnCours());
        }
    }
}

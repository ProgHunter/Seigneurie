using Batiment;
using Production;
using Profession;
using UnityEngine;

namespace UI.Batiments
{
    /// <summary>
    /// Vue d'un seul bâtiment en cours sous forme de liste
    /// </summary>
    public class VueListeBatimentsEnCours : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private VueBatimentEnCours _vueInstancier;
        [SerializeField] private GameObject _msgAucunEnCours;
        private VueBatimentEnCours _vueBatiment;
        
        public void InitListe()
        {
            MetAJourListe();
        }

        public void MetAJourListe()
        {
            if (!gameObject.activeInHierarchy)
                return;

            var gestionnaireBatiments = GestionnaireBatiments.Instance;
            if (gestionnaireBatiments.ConstructionEstEnCours())
            {
                var batiment = gestionnaireBatiments.EnConstruction;
                long pourcentMacon = GestionnaireProfessions.Instance.AccederPourcent(ProfessionEnum.MACON);
                long nbTicksRestants = GestionnaireProductions.Instance.NbTicksRestantsConstruction(pourcentMacon);
                
                if (_vueBatiment == null)
                {
                    _vueBatiment = Instantiate(_vueInstancier, _content);
                    string nom = gestionnaireBatiments.BatimentConfigDict[batiment.Item1].Nom;

                    _vueBatiment.Init(null, nom, nbTicksRestants, MetAJourListe);
                }
                else
                {
                    _vueBatiment.MetAJourTicksRestants(nbTicksRestants);
                }
            }
            else
            {
                if (_vueBatiment != null)
                {
                    _vueBatiment.Dispose();
                    _vueBatiment = null;
                    //Debug.Log("Bâtiment terminé de construire");
                }
            }
            
            MetAJourMessageListeVide();
        }

        private void MetAJourMessageListeVide()
        {
            _msgAucunEnCours.SetActive(!GestionnaireBatiments.Instance.ConstructionEstEnCours());
        }
    }
}

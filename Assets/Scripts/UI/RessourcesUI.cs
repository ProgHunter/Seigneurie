using UnityEngine;

namespace UI
{
    public class RessourcesUI : MonoBehaviour
    {
        [SerializeField] private BarreDeResource _barreDeResource;

        public void Init()
        {
            _barreDeResource.Init();
        }
        public void UpdateBarre()
        {
            _barreDeResource.UpdateRessources();
        }
    }
}

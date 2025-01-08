using UnityEngine;

namespace UI
{
    /// <summary>
    /// Vue de toutes les ressources
    /// </summary>
    public class VueRessources : MonoBehaviour
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

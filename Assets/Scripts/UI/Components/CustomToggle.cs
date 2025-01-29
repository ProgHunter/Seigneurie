using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Components
{
    public class CustomToggle : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        private bool _estSelectionné;
        [SerializeField] private bool _commenceSelectionné;
        [SerializeField] private Graphic _graphicCible;
        [SerializeField] private ColorBlock _couleursActives;
        public Action<CustomToggle> QuandAppuyé;

        private void Start()
        {
            _graphicCible.color = _commenceSelectionné ? _couleursActives.selectedColor : _couleursActives.normalColor;
        }

        public void Déselectionner()
        {
            _graphicCible.color = _couleursActives.normalColor;
            _estSelectionné = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _graphicCible.color = _couleursActives.highlightedColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _estSelectionné = true;
            _graphicCible.color = _couleursActives.selectedColor;
            QuandAppuyé?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(!_estSelectionné)
                _graphicCible.color = _couleursActives.normalColor;
        }
    }
}
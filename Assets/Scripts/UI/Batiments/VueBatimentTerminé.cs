using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Batiments
{
    public class VueBatimentTerminé : MonoBehaviour, IDisposable
    {
        [SerializeField] private Image _icone;
        [SerializeField] private TextMeshProUGUI _nomBatiment;
        [SerializeField] private TextMeshProUGUI _quantiteConstruite;

        public void Init(Sprite icone, string nom, long quantiteConstruite)
        {
            //_icone.sprite = icone;
            _nomBatiment.text = nom;
            _quantiteConstruite.text = quantiteConstruite.ToString();
        }

        public void UpdateQte(long quantiteConstruite)
        {
            _quantiteConstruite.text = quantiteConstruite.ToString();
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}

using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Batiments
{
    /// <summary>
    /// Vue qui affiche le nombre de bâtiments de ce type qui sont déjà construits
    /// </summary>
    public sealed class VueBatimentTerminé : MonoBehaviour, IDisposable
    {
        [SerializeField] private Image _icone;
        [SerializeField] private TextMeshProUGUI _nomBatiment;
        [SerializeField] private TextMeshProUGUI _quantiteConstruite;

        public void Init(Sprite icone, string nom, long quantiteConstruite)
        {
            //TODO icone
            //_icone.sprite = icone;
            _nomBatiment.text = nom;
            _quantiteConstruite.text = quantiteConstruite.ToString();
        }

        public void UpdateValeurs(long quantiteConstruite)
        {
            _quantiteConstruite.text = quantiteConstruite.ToString("#,0", CultureInfo.CurrentCulture);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}

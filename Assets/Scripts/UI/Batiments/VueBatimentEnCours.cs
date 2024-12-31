using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VueBatimentEnCours : MonoBehaviour, IDisposable
{
    [SerializeField] private Image _icone;
    [SerializeField] private TextMeshProUGUI _nom;
    [SerializeField] private TextMeshProUGUI _completion;

    public void Init(Sprite icone, string nom, string completion)
    {
        _nom.text = nom;
        ModifierValeurCompletion(completion);
    }

    public void UpdateValeurs(string completion)
    {
        ModifierValeurCompletion(completion);
    }

    private void ModifierValeurCompletion(string completion)
    {
        _completion.text = completion + " ticks";
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }
}

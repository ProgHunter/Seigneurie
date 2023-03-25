using System.Collections.Generic;
using Ressource;
using UI;
using UnityEngine;

public class HotelDeVille : MonoBehaviour
{
    [SerializeField] private BarreDeResource _barreDeResource;

    private List<AbstractRessource> _ressources;
    public float TempsAvantProduction = 5f;
    public float TempsEcoule;
    
    private void Awake()
    {
        _ressources = new List<AbstractRessource>
        {
            
        };
        _barreDeResource.Setup(_ressources);
    }

    // Update is called once per frame
    void Update()
    {
        TempsEcoule += Time.deltaTime;
        if(TempsEcoule >= TempsAvantProduction)
        {
            TempsEcoule = 0f;
            foreach (AbstractRessource ressource in _ressources)
            {
                ressource.Quantite += 5;
                //ressource.FaireProduction();
            }

            _barreDeResource.UpdateRessources();
        }
    }
}

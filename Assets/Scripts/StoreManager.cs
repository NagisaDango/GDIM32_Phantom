using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreManager : MonoBehaviour, IDecoratorManager
{
    [SerializeField] private TMP_Text money;
    [SerializeField] private AnimalDecorator AnimalDecoratorPrefab;
    [SerializeField] private Transform animalbuttonParrent;

    private Player currentPlayers;


    void Start()
    {
        SOAnimalDefinition[] animalDefs = Resources.LoadAll<SOAnimalDefinition>("AnimalDefinitions");
        foreach (SOAnimalDefinition animal in animalDefs)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDecoratorClicked(Decorator selected)
    {
        if(selected is AnimalDecorator)
        {
            AnimalDecorator ad = selected as AnimalDecorator;
            
        }
    }

    public Decorator DecoratorFactory(IGroupable grouop, Transform parent)
    {
        return null;
    }


}

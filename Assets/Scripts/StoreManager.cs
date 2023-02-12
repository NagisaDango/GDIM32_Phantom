using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StoreManager : MonoBehaviour, IDecoratorManager
{
    [SerializeField] private TMP_Text money;
    [SerializeField] private AnimalDecorator AnimalDecoratorPrefab;
    [SerializeField] private Transform animalButtonParrent;

    [SerializeField] private Player currentPlayer; //need delete serializefield later


    void Start()
    {
        

        SOAnimalDefinition[] animalDefs = Resources.LoadAll<SOAnimalDefinition>("AnimalDefinitions");
        foreach (SOAnimalDefinition animal in animalDefs)
        {
            AnimalDecorator ad = Instantiate(AnimalDecoratorPrefab, animalButtonParrent);
            ad.Initialize(animal, this);
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
            currentPlayer.BuyAnimal(ad.AnimalDef);

        }
    }

    public Decorator DecoratorFactory(IGroupable grouop, Transform parent)
    {
        return null;
    }


}

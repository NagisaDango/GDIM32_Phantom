using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour, IDecoratorManager
{
    [SerializeField] private List<AnimalInstance> animals = new List<AnimalInstance>();
    [SerializeField] private ItemDecorator AnimalStatsDecoratorPrefab;
    [SerializeField] private Transform animalInfoParrent;

    [SerializeField] private Player Owner;




    void OnEnable()
    {
        RefreshFarmList();
    }

    void OnDisable()
    {
    }

    public Decorator DecoratorFactory(IGroupable group, Transform parent)
    {
        return null;
    }

    public void OnDecoratorClicked(Decorator selected)
    {
    }

    public void RefreshFarmList()
    {
        for (int i = animalInfoParrent.childCount - 1; i >= 0; i--)
        {
            Destroy(animalInfoParrent.GetChild(i).gameObject);
        }

        foreach (AnimalInstance animal in Owner.GetAnimals())
        {
            ItemDecorator ad = Instantiate(AnimalStatsDecoratorPrefab, animalInfoParrent);
            ad.Initialize(animal, this);
        }
    }
    


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackpackManager : MonoBehaviour, IDecoratorManager
{
    [SerializeField] private int maxItem;
    public int currentItem;
    [SerializeField] private TMP_Text weightText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private GameObject grid;
    [SerializeField] private SOAnimalDefinition animal;
    public bool addable;
    [SerializeField] private AnimalInstance animalInstance;
    [SerializeField] private List<AnimalInstance> animals = new List<AnimalInstance>();
    [SerializeField] BackpackDecorator itemDecoratorPrefab;

    private static int i;
    void Start()
    {
        
    }

    void Update()
    {
        if(currentItem<=maxItem)
        {
            addable = true;
        }
        else
        {
            addable = false;
        }
    }

    public void AddItem(AnimalInstance animal)
    {
        if(currentItem+animal.GetWeight() <= maxItem)
        {
            addable = true;
            animals.Add(animal);
            currentItem += animal.GetWeight();
        }
        else
        {
            addable= false;
        }
    }

    public void RemoveItem(AnimalInstance animal)
    {
        currentItem -= animal.GetWeight();
        animals.Remove(animal);
    }

    private void OnEnable()
    {
        descriptionText.text = "";
        createNewItem();
        UIUpdate();
    }

    public void createNewItem()
    {
        foreach (Transform child in grid.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(AnimalInstance a in animals)
        {
            BackpackDecorator bd = Instantiate(itemDecoratorPrefab, this.transform);
            bd.Initialize(a, this);
            bd.gameObject.transform.SetParent(this.grid.transform);
        }
    }

    public Decorator DecoratorFactory(IGroupable group, Transform parent)
    {
        return null;
    }

    public void OnDecoratorClicked(Decorator selected)
    {
    }


    public void OnStoreButtonClick()
    {
        if (animals.Count>0)
        {
            if (animals[0].Owner.inFarm)
            {
                for (int i = animals.Count - 1; i >= 0; i--)
                {
                    animals[i].Owner.StoreToFarm(animals[i]);
                }
                descriptionText.text = "All animals have been stored to farm!";
            }
            else
            {
                descriptionText.text = "Please go near the farm to store the animals back!";
            }
        }
        else
        {
            descriptionText.text = "You can catch some wild animal out there!";
        }
    }

    public void UIUpdate()
    {
        weightText.text = "Inventory weight: " + currentItem + "/10";
    }
}

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


    void Update()
    {
        //a bool to see if player carry weight is enough to pick up
        /*
        if(currentItem<=maxItem)
        {
            addable = true;
        }
        else
        {
            addable = false;
        }*/
    }

    public void AddItem(AnimalInstance animal)
    {
        //add the animal into bag if the weight is enough
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
    //remove the animal from bag list and its weight
    public void RemoveItem(AnimalInstance animal)
    {
        currentItem -= animal.GetWeight();
        animals.Remove(animal);
    }

    //when the bag is open, refresh the animal lists and update the UI
    private void OnEnable()
    {
        descriptionText.text = "";
        createNewItem();
        UIUpdate();
    }
    //update the UI of the bag panel
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
        //when store button clicked if there is animal in bag and player is in farm, store them all back to farm
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
            else//show message to player if they have animals but not in farm
            {
                descriptionText.text = "Please go near the farm to store the animals back!";
            }
        }
        else//show message to player if no animal in bag
        {
            descriptionText.text = "You can catch some wild animal out there!";
        }
    }
    //update the text to the player's current carry weight
    public void UIUpdate()
    {
        weightText.text = "Inventory weight: " + currentItem + "/10";
    }
}

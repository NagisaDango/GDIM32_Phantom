using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.UI.GridLayoutGroup;
//Wei Lun Tsai

public class BackpackManager : MonoBehaviour, IDecoratorManager
{
    [SerializeField] private int maxItem;
    public int currentItem;
    [SerializeField] private TMP_Text weightText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private GameObject grid;
    [SerializeField] private List<AnimalInstance> animals = new List<AnimalInstance>();
    [SerializeField] ItemDecorator itemDecoratorPrefab;

    [SerializeField] private Player currentPlayer; //todo: delete serializefield later

    void Update() { }

    public void AddItem(AnimalInstance animal)
    {
        //add the animal into bag if the weight is enough
        if(currentItem + animal.Weight <= maxItem){
            animals.Add(animal);
            currentItem += animal.Weight;
            animal.gameObject.SetActive(false);
            SpawnManager.TakeWildAnimal();
        }

    }
    //remove the animal from bag list and its weight
    public void RemoveItem(AnimalInstance animal)
    {
        currentItem -= animal.Weight;
        animals.Remove(animal);
    }

    //when the bag is open, refresh the animal lists and update the UI
    private void OnEnable()
    {
        descriptionText.text = "";
        UIUpdate();
        currentPlayer.InPanel = true;
    }
    private void OnDisable()
    {
        currentPlayer.InPanel = false;
    }


    //update the UI of the bag panel
    public void UIUpdate()
    {
        foreach (Transform child in grid.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(AnimalInstance a in animals)
        {
            ItemDecorator id = Instantiate(itemDecoratorPrefab, this.transform);
            id.Initialize(a, this);
            id.gameObject.transform.SetParent(this.grid.transform);
        }

        //update the text to the player's current carry weight
        weightText.text = "Inventory weight: " + currentItem + "/10";

    }

    public Decorator DecoratorFactory(IGroupable group, Transform parent) { return null; }

    public void OnDecoratorClicked(Decorator selected) { }


    public void OnStoreButtonClick()
    {
        //when store button clicked if there is animal in bag and player is in farm, store them all back to farm
        if (animals.Count>0)
        {
            if (animals[0].Owner.InFarm)
            {
                for (int i = animals.Count - 1; i >= 0; i--)
                {
                    animals[i].Owner.StoreToFarm(animals[i]);
                    RemoveItem(animals[i]);
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

        UIUpdate();
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//by Shengjie Zhang

public abstract class AnimalInstance : MonoBehaviour, IGroupable
{
    public Player Owner { get; set; }
    public IGroupable ParentGroup { get; set; } 
    public string DisplayName { get; private set; }
    public Sprite Icon { get; private set; }
    public SOAnimalDefinition.AnimalType Type { get; private set; }
    public int AdultGrowthValue { get; set; }
    [SerializeField] protected GameObject player; //the player object to follow
    [SerializeField] protected Player playerReference; //the reference to the Player script
    public bool canFollow; //can follow player
    public bool isFollowing; //is following a player

    private int weight; //the weight it count for the package
    public int Weight { get { return weight; } }


    private float currentGrowth = 0;

    private int currentValue; //selling price
    public int CurrentValue { get { return currentValue; } }

    private List<FoodType> preferedFood;
    public List<FoodType> PreferedFood { get { return preferedFood; } }

    //Intialize values for animal instance
    public void Initialize(string name, int value, SOAnimalDefinition.AnimalType type, Player owner, Sprite icon, int adultGrwothValue, List<FoodType> preferedFood, int weight)
    {
        gameObject.name = name;
        DisplayName = name;
        this.currentValue = value;
        Type = type;
        Owner = owner;
        Icon = icon;
        AdultGrowthValue = adultGrwothValue;
        this.preferedFood = preferedFood;
        this.weight = weight;
    }

    public void AddGrowth(int amount) { currentGrowth += amount; }

    public float GetGrowthRate()
    {
        return currentGrowth <= AdultGrowthValue ? (currentGrowth / AdultGrowthValue) : 1;
    }

    public void CloseTriggerCollider() // turn offs the trigger collider used for checking if playre is nereby.
    {
        Collider[] colliders = gameObject.GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            if (collider.isTrigger)
                collider.enabled = false;
        }
    }

    //When collide with wolf, destroy this object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wolf"))
        {
            Destroy(this.gameObject);
        }
    }
    //if player is not folllowed by any animal, make this animal the following animal
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerReference = player.GetComponent<Player>();
            if (playerReference.followingAnimals == null && isFollowing)
                playerReference.followingAnimals = this; 
        }
    }
    //stop follow the player when player not in range
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerReference.followingAnimals = null;
        }
    }





    //public bool IsComposite() // For Later drafts
    //{
    //    return false;
    //}

    //public void AddToGroup(List<IGroupable> toAdd)
    //{
    //    Debug.LogError("Should never be called");
    //}
    //public void RemoveFromGroup(List<IGroupable> toRemove)
    //{
    //    Debug.LogError("Should never be called");
    //}

    //public void RemoveAndDestroy(List<IGroupable> toRemove)
    //{
    //    Destroy(gameObject);
    //}

    //public List<IGroupable> GetSubGroups()
    //{
    //    Debug.LogError("Should never be called");
    //    return new List<IGroupable>();
    //}


}
    
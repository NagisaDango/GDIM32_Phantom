using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Wei Lun Tsai
public class SpawnManager : MonoBehaviour
{
    //[SerializeField] private List<GameObject> spawnObject; 
    private int randomSpawnIndex;//randomize spawn object

    [SerializeField] private float spawnTime; //spawn the object once per spwanTime
    private float nextSpawn; //track when will we spawn the next one
    [SerializeField] private Player player;

    [SerializeField] private List<SOAnimalDefinition> animals;//the list of things will be spawning

    private Vector3 spawnPosition;
    [SerializeField]private Vector3 center;
    [SerializeField]private Vector3 size;

    [SerializeField] private int maxTotal = 10;
    public static int currentTotal = 0;
    private void Start()
    {
        for (int i = 0; i < maxTotal; i++)
        {
            RandomSpawn();
        }
    }

    public static void TakeWildAnimal()
    {
        currentTotal--;
    }


    void Update()
    {
        //if the time already pass the spawn time, spawn one object
        if(currentTotal >= maxTotal)
        {
            nextSpawn = Time.time + spawnTime; //reset timer
            return;
        }

        if (Time.time >= nextSpawn)
        {
            RandomSpawn();
            nextSpawn = Time.time + spawnTime; //the next spawn time
        }
    }

    private void RandomSpawn()
    {
        spawnPosition = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), size.y, Random.Range(-size.z / 2, size.z / 2));//random spawn position
        randomSpawnIndex = Random.Range(0, animals.Count);//random spawn animal
        SOAnimalDefinition animalDef = animals[randomSpawnIndex];//animal definition of the spawning animal
        AnimalInstance animalInst = animalDef.Spawn(player, spawnPosition, Quaternion.identity);//spawn
        animalInst.Initialize(animalDef.GetName(), animalDef.GetSellValue(), animalDef.GetAnimalType(), player, animalDef.GetIcon(), animalDef.GetAdultGrowthValue(), animalDef.GetPreferedFood(), animalDef.GetWeight());//initialized the spawned animal
        currentTotal++; 
    }
    
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;
        //Gizmos.DrawCube(center, size);
    }
}

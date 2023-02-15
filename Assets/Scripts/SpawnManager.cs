using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField]private Transform spawnTrans;

    void Update()
    {
        //if the time already pass the spawn time, spawn one object
        if (Time.time >= nextSpawn)
        {
            spawnPosition=center+new Vector3(Random.Range(-size.x/2,size.x/2), size.y,Random.Range(-size.z/2,size.z/2));//random spawn position
            randomSpawnIndex = Random.Range(0, animals.Count);//random spawn animal
            SOAnimalDefinition animalDef = animals[randomSpawnIndex];//animal definition of the spawning animal
            nextSpawn = Time.time + spawnTime; //the next spawn time
            spawnTrans.position = spawnPosition;//spawn position
            AnimalInstance animalInst = animalDef.Spawn(player, spawnTrans);//spawn
            animalInst.Initialize(animalDef.GetName(), animalDef.GetSellValue(), animalDef.GetAnimalType(), player, animalDef.GetIcon(), animalDef.GetAdultGrowthValue(),animalDef.GetPreferedFood(), animalDef.GetWeight());//initialized the spawned animal
        }
    }
    // spawn area
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(center, size);
    }*/
}

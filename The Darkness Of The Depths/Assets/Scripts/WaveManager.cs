using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveManager : MonoBehaviour
{

    public int waveNumber;
    bool nextWave = true;
    public int numberOfRanged, numberOfMelee, numberOfSniper, numberOfTanks;
    public GameObject[] rangedPrefab = new GameObject[2];
    public GameObject tank;
    public GameObject melee;
    public int killsNeeded;
    public int kill;
    
    public List<Transform> SpawnLocations = new List<Transform>();


    private void Start()
    {
        killsNeeded = numberOfMelee + numberOfRanged + numberOfTanks + numberOfSniper;
    }

    public void LateUpdate()
    {
        if(nextWave)
        {
            SpawnWave();
            nextWave = false;
            waveNumber++;
            kill = 0;
        }

        if(kill >= killsNeeded)
        {
            nextWave = true;
        }
    }

    public void SpawnWave()
    {
        for (int i = 0; i < numberOfMelee; i++)
        {
            GameObject meleeEnemy = Instantiate(melee, SpawnLocations[Random.Range(0,SpawnLocations.Count)]);
        }
        for (int i = 0; i < numberOfRanged; i++)
        {
            GameObject rangedEnemy = Instantiate(rangedPrefab[Random.Range(0, rangedPrefab.Length)], SpawnLocations[Random.Range(0,SpawnLocations.Count)]);
        }
        for (int i = 0; i < numberOfTanks; i++)
        {
            //Spawn Tanks
        }
    }







}

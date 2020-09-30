using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WaveManager : MonoBehaviour
{

    public int waveNumber;
    bool nextWave = true;

    public GameObject[] rangedPrefab = new GameObject[1];
    public GameObject tank;
    public GameObject melee;
    public GameObject sniper;
    public int killsNeeded, kill;
    public GameObject doorlight;


    public bool nextSpawn;
    public int numberOfRanged, numberOfMelee, numberOfSniper, numberOfTanks;
    public int spawnedRanged, spawnedMelee, spawnedSniper, spawnedTanks;


    public List<Transform> SpawnLocations = new List<Transform>();


    private void Start()
    {
        killsNeeded = numberOfMelee + numberOfRanged + numberOfTanks + numberOfSniper;
        doorlight.SetActive(false);
    }

    public void LateUpdate()
    {
        if(nextWave)
        {
            spawnedMelee = 0;
            spawnedRanged = 0;
            spawnedSniper = 0;
            spawnedTanks = 0;

            
            numberOfMelee = 2 + waveNumber;
            numberOfRanged = 2 + waveNumber;
            //numberOfTanks = 1 + waveNumber;
            //numberOfSniper = 1 + waveNumber;
            kill = 0;
            killsNeeded = numberOfMelee + numberOfRanged + numberOfTanks + numberOfSniper;

            SpawnMelee();
            SpawnRanged();
            SpawnSniper();
            SpawnTank();
            
            nextWave = false;
            

        }

        if(kill >= killsNeeded)
        {
            if(waveNumber < 2)
            {
                nextWave = true;
                waveNumber++;
                
            } else
            {
                doorlight.SetActive(true);
            }
  
        }


    }

    public void SpawnMelee()
    {
        GameObject meleeEnemy = Instantiate(melee, SpawnLocations[Random.Range(0, SpawnLocations.Count)].position, Quaternion.identity);
        spawnedMelee++;
        StartCoroutine(meleeCDR());
    }

    public void SpawnRanged()
    {
        int i = Random.Range(0, rangedPrefab.Length);
        GameObject rangedEnemy = Instantiate(rangedPrefab[i], SpawnLocations[Random.Range(0, SpawnLocations.Count)].position , Quaternion.identity);
        spawnedRanged++;
        StartCoroutine(RangedCDR());
    }
    public void SpawnSniper()
    {
        //GameObject sniperEnemy = Instantiate(sniper, SpawnLocations[Random.Range(0, SpawnLocations.Count)].position , Quaternion.identity);
        spawnedSniper++;
        StartCoroutine(SniperCDR());
    }
    public void SpawnTank()
    {
        //GameObject tankEnemy = Instantiate(tank, SpawnLocations[Random.Range(0, SpawnLocations.Count)].position, Quaternion.identity);
        spawnedTanks++;
        StartCoroutine(TankCDR());
    }

    IEnumerator meleeCDR()
    {
        yield return new WaitForSeconds(2.5f);
        if(spawnedMelee < numberOfMelee)
        {
            Debug.Log("Spawned");
            SpawnMelee();
        }
       
    }
    IEnumerator RangedCDR()
    {
        yield return new WaitForSeconds(2.5f);
        if (spawnedRanged < numberOfRanged)
        {
            Debug.Log("Spawned");
            SpawnRanged();
        }
    }
    IEnumerator SniperCDR()
    {
        yield return new WaitForSeconds(1f);
        if (spawnedSniper < numberOfSniper)
        {
            SpawnSniper();
        }
    }
    IEnumerator TankCDR()
    {
        yield return new WaitForSeconds(1f);
        if (spawnedTanks < numberOfTanks)
        {
            SpawnTank();
        }
    }







}

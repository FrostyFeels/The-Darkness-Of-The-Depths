using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public string areaName;
    public WaveManager spawn;
    private void Start()
    {
        StaticManager.SetObject(areaName, gameObject);
    
    }
    void Update()
    {
        if (GameObject.ReferenceEquals(gameObject, StaticManager.activeArea) || GameObject.ReferenceEquals(gameObject, StaticManager.secondArea))
        {
            foreach (Transform aSpawnSpot in transform)
            {             
                if(spawn.SpawnLocations.Contains(aSpawnSpot) != aSpawnSpot.transform)
                {
                    spawn.SpawnLocations.Add(aSpawnSpot);
                }
                
            }
        }
        else
        {
            foreach (Transform aSpawnSpot in transform)
            {
                spawn.SpawnLocations.Remove(aSpawnSpot);
            }
        }

    }
}

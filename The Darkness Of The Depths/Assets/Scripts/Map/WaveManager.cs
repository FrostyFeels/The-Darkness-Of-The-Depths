using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WaveManager : MonoBehaviour
{
    public List<GameObject> spawnLocations = new List<GameObject>();
    public List<GameObject> enemyType = new List<GameObject>();
    

    private void Start()
    {
        for (int i = 0; i < spawnLocations.Count; i++)
        {
           EnemyType enemytype = spawnLocations[i].GetComponent<EnemyType>();
           enemyType.Add(enemytype.Enemy);
            
           
        }
        Spawner();
    }


    public void Spawner()
    {
        for (int i = 0; i < enemyType.Count; i++)
        {
            GameObject enemy = Instantiate(enemyType[i], spawnLocations[i].transform.position, Quaternion.identity);
            EnemyType enemytype = spawnLocations[i].GetComponent<EnemyType>();
        }
        
    }












}

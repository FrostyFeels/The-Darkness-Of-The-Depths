using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WaveManager : MonoBehaviour
{
    /*    public GameObject pistol;
        public GameObject shotgun;
        public GameObject auto;
        public GameObject semi;
        public GameObject tank;
        public GameObject melee;
        public GameObject sniper;*/
    /*    public List<GameObject> rangeLocation = new List<GameObject>();
        public List<GameObject> meleeLocations = new List<GameObject>();
        public List<GameObject> sniperLocations = new List<GameObject>();
        public List<GameObject> tankLocations = new List<GameObject>();*/
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

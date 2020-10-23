using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject enemyPrefab;
    public TextTutorial text;

    public bool reload;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(reload)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {

            }
        }
    }

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        movement.averagespeed = 0;

        reload = true;
    }
}

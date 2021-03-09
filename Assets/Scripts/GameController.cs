using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> enemySpawners;
    public List<GameObject> weapons;
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies()
    {
        for(int i = 0; i<5; i++)
        {
            if(GameObject.Find("EnemySpawner" + i.ToString()) != null)
            {
                enemySpawners.Add(GameObject.Find("EnemySpawner" + i.ToString()));
            }
        }

        for (int i = 0; i < enemySpawners.Count; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, enemySpawners[i].transform);
            Instantiate(weapons[Random.Range(0, weapons.Count - 1)], newEnemy.GetComponent<EnemyController>().rightHand.transform);
            Instantiate(weapons[Random.Range(0, weapons.Count - 1)], newEnemy.GetComponent<EnemyController>().leftHand.transform);
        }
    }
}

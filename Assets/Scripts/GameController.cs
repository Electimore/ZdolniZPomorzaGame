using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> enemySpawners;
    public List<GameObject> weapons;
    public List<GameObject> enemies;
    public GameObject enemyPrefab;
    public bool game;

    public List<GameObject> doors;
    public List<GameObject> detectors;

    int enemyCount;
    int enemiesAlive;

    private HUD hud;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hud = player.GetComponent<HUD>();
        game = false;
        ShowDoors(true);
    }

    void Update()
    {
        CheckForEnemiesAlive();
        hud.SetEnemyCount(enemiesAlive, enemyCount);

        if(enemiesAlive < 1 && game)
        {
            ShowDoors(false);
        }
    }

    public void SpawnEnemies()
    {
        enemySpawners.Clear();

        for(int i = 0; i<5; i++)
        {
            if(GameObject.Find("EnemySpawner" + i.ToString()) != null)
            {
                enemySpawners.Add(GameObject.Find("EnemySpawner" + i.ToString()));
            }
        }

        enemies.Clear();

        for (int i = 0; i < enemySpawners.Count; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, enemySpawners[i].transform);
            Instantiate(weapons[Random.Range(0, weapons.Count - 1)], newEnemy.GetComponent<EnemyController>().rightHand.transform);
            Instantiate(weapons[Random.Range(0, weapons.Count - 1)], newEnemy.GetComponent<EnemyController>().leftHand.transform);
            newEnemy.tag = "enemy";
            enemies.Add(newEnemy);
        }

        enemyCount = enemies.Count;
    }

    public void CheckForEnemiesAlive()
    {
        enemiesAlive = GameObject.FindGameObjectsWithTag("enemy").Length;
    }

    public void ShowDoors(bool visible)
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].SetActive(visible);
        }
        for (int i = 0; i < detectors.Count; i++)
        {
            detectors[i].SetActive(!visible);
        }
    }

    public void EndGame()
    {
        Debug.Log("Restart...");
    }
}

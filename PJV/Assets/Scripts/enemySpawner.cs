using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemySpawner : MonoBehaviour
{
   [ SerializeField ]
    private GameObject enemyPrefab;
    public GameObject Spawnpoint01;
    public GameObject Spawnpoint02;
    public GameObject Spawnpoint03;
    private GameObject CurrentSpawnpoint;

    public int RandomAmount;

    private List<GameObject> InstancedEnemies = new List<GameObject>();

    public float enemyInterval = 3.5f;

    public static int AmountofKills;
    public TextMeshProUGUI KillScore;

    void Start()
    {
        AmountofKills = 0;
        StartCoroutine(spawnEnemy(enemyInterval, enemyPrefab));
    }

    private void Update()
    {
        KillScore.text = AmountofKills.ToString();
    }

    private void SpawnpointRandomizer()
    {
        int i = Random.Range(1, 4);
        switch (i) 
        {
            case 1:
                CurrentSpawnpoint = Spawnpoint01;
                break;

            case 2:
                CurrentSpawnpoint = Spawnpoint02;
                break;

            case 3:
                CurrentSpawnpoint = Spawnpoint03;
                break;
        }
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        SpawnpointRandomizer();
        getRandomAmount();
        for (int i = 0; i < RandomAmount; i++)
        {
            GameObject newEnemy = Instantiate(enemy, CurrentSpawnpoint.transform.position, Quaternion.identity);
            InstancedEnemies.Add(newEnemy);
        }
        StartCoroutine(spawnEnemy( interval, enemy));
    }

    private void getRandomAmount()
    {
        RandomAmount = Random.Range(1, 3) + 1;
    }
}

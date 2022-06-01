using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
   [ SerializeField ]
    private GameObject enemyPrefab;
    public GameObject Spawnpoint01;
    public GameObject Spawnpoint02;
    public GameObject Spawnpoint03;
    public GameObject CurrentSpawnpoint;



    public float enemyInterval = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(enemyInterval, enemyPrefab));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnpointRandomizer()
    {
         int i = Random.Range(1, 3);
        Debug.Log(i);
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
        GameObject newEnemy = Instantiate(enemy, CurrentSpawnpoint.transform.position, Quaternion.identity);
        StartCoroutine(spawnEnemy( interval, enemy));
    }
}

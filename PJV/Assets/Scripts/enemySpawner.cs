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

    public GameObject Gate01;
    public GameObject Gate02;
    public GameObject Gate03;
    private GameObject CurrentGate;

    public int RandomAmount;

    private List<GameObject> InstancedEnemies = new List<GameObject>();

    public float enemyInterval = 3.5f;

    public float maximumOpening = 11.17f;
    public float movementSpeed = 3f;
    public float CooldownLength = 1f;
    private float CooldownTime;
    private bool inCooldown;
    private bool StartOpen;

    public static int AmountofKills;
    public TextMeshProUGUI KillScore;

    void Start()
    {
        CurrentGate = Gate01;
        inCooldown = false;
        StartOpen = false;
        AmountofKills = 0;
        StartCoroutine(spawnEnemy(enemyInterval, enemyPrefab));
    }

    private void Update()
    {
        if (Time.time - CooldownTime >= CooldownLength)
        {
            inCooldown = false;
        }

        if (inCooldown == false)
        {
            CloseGate();
        }

        if (StartOpen == true)
        {
            OpenGate();
        }

        KillScore.text = AmountofKills.ToString();
    }

    private void SpawnpointRandomizer()
    {
        int i = Random.Range(1, 4);
        switch (i) 
        {
            case 1:
                CurrentSpawnpoint = Spawnpoint01;
                CurrentGate = Gate01;
                break;

            case 2:
                CurrentSpawnpoint = Spawnpoint02;
                CurrentGate = Gate02;
                break;

            case 3:
                CurrentSpawnpoint = Spawnpoint03;
                CurrentGate = Gate03;
                break;
        }
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) //not sure if the list works properly
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

    private void OpenGate()
    {
        if (CurrentGate.transform.position.y < maximumOpening)
        {
            CurrentGate.transform.Translate(0f, movementSpeed * Time.deltaTime, 0f);
            Debug.Log("Opening Gate" + CurrentGate);
        }
        else
        {
            StartOpen = false;
        }

        inCooldown = true;
        CooldownTime = Time.time;
    }

    private void CloseGate()
    {
        if (CurrentGate.transform.position.y > 0)
        {
            CurrentGate.transform.Translate(0f, -(movementSpeed * Time.deltaTime), 0f);
        }
    }

}

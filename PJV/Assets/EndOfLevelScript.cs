using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfLevelScript : MonoBehaviour
{
    public GameObject EndOfLevelMenu;
    public TextMeshProUGUI Timer;
    public TextMeshProUGUI EnemiesBeaten;
    private void OnTriggerEnter(Collider collision)
    {
        float EndTime = Time.time;
        Debug.Log(EndTime);
        Time.timeScale = 0;

        EndOfLevelMenu.SetActive(true);
        EnemiesBeaten.text = enemySpawner.AmountofKills.ToString();
        Timer.text = EndTime.ToString();
    }
}

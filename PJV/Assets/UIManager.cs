using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class UIManager : MonoBehaviour
{
    public GameObject player;
    public Image BarMask;
    public Image BarFill;
    public Image Bar;

    public float currentFill;
    public float maxFill;

    private void Start()
    {
        maxFill = 1.5f;
        Bar.enabled = false;
        BarMask.enabled = false;
        BarFill.enabled = false;
    }
    void Update()
    {
        currentFill = Time.time - player.GetComponent<playerController>().PressTime;
        getFillAmount();
        if (player.GetComponent<playerController>().showBar == true)
        {
            Bar.enabled = true;
            BarMask.enabled = true;
            BarFill.enabled = true;
        }
        else
        {
            Bar.enabled = false;
            BarMask.enabled = false;
            BarFill.enabled = false;
        }
    }

    void getFillAmount()
    {
        float fillAmount = currentFill / maxFill;
        BarMask.fillAmount = fillAmount;
    }
}

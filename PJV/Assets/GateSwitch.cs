using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSwitch : MonoBehaviour
{
    public GameObject Gate;
    public AudioSource GateSource;
    public AudioClip Sound;
    public bool opening;
    public float maximumOpening = 11.17f;
    public float movementSpeed = 3f;

    private void Start()
    {
        GateSource.clip = Sound;
        opening = false;  
    }


    void Update()
    {
        if (opening == true)
        {
            if (Gate.transform.position.y < maximumOpening)
            {
                Gate.transform.Translate(0f, movementSpeed * Time.deltaTime, 0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit!");
        GateSource.Play();
        opening = true;
    }

}

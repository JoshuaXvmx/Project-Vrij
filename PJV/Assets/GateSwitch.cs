using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSwitch : MonoBehaviour
{
    public GameObject Gate;
    public bool opening;
    public float maximumOpening = 11.17f;
    public float movementSpeed = 3f;

    private void Start()
    {
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
    private void OnCollisionEnter(Collision collision)
    {
        opening = true;
    }

}

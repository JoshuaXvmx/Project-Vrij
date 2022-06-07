using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    public GameObject TeleportToLocation;
    public int ExitOrientation;

   void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        other.gameObject.transform.position = TeleportToLocation.transform.position;
        other.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, ExitOrientation, 0));

    }
}

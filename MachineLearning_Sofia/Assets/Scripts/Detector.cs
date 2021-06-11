using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}

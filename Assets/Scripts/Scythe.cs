using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    bool isMowReady;

    public void MowIsReady()
    {
        isMowReady = true;
    }

    public void MowIsntReady()
    {
        isMowReady = false;
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Crop"))
        {
            if (isMowReady)
            {
                col.GetComponent<Crop>().Mowing();
            }
        }
    }
}

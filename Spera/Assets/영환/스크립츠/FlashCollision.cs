using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashCollision : MonoBehaviour
{
    public bool isSturn { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FlashLight")
        {
            isSturn = true;
        }
    }

    IEnumerator SturnAniWait()
    {
        yield return new WaitForSeconds(2f);
        isSturn = false;
    }
}

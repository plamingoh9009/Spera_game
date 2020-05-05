using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFlashLight : MonoBehaviour
{
    public GameObject player;
    public bool isUseFlash { get; private set; }

    private void Start()
    {
        isUseFlash = false;

    }
    public void OnClickFlash()
    {
        isUseFlash = !isUseFlash;
        player.transform.Find("FlashLight");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class CameraSetup : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            //씬에 있는 시네머신 가상 카메라를 찾고
            CinemachineFreeLook followCam = FindObjectOfType<CinemachineFreeLook>();
            followCam.Follow = transform;
            followCam.LookAt = transform;
        }
    }
    
}
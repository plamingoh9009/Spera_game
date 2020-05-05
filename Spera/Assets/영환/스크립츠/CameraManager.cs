using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    FixedTouchField _touchField;
    CinemachineFreeLook _mainCam;
    CinemachineCollider _cameraCollider;

    public float CameraAngleSpeed = 0.1f;

    public GameObject player;
    public CinemachineVirtualCamera frontCam;
    public CinemachineVirtualCamera letfCam;

    void Start()
    {
        _touchField = FindObjectOfType<FixedTouchField>();
        _mainCam = GetComponent<CinemachineFreeLook>();
        _cameraCollider = GetComponent<CinemachineCollider>();

        _mainCam.Follow = player.transform.Find("LookPoint");
        _mainCam.LookAt = player.transform.Find("LookPoint");
        //frontCam.LookAt = player.transform.Find("LookPoint");
        //letfCam.LookAt = player.transform.Find("LookPoint");

        _mainCam.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;

        _cameraCollider.m_IgnoreTag = "Object";
        _cameraCollider.m_MinimumDistanceFromTarget = 0.1f;

        //1,2초 뒤에 각각의 함수를 실행해라
        Invoke("setCam1", 1);
        Invoke("setCam2", 2);
    }

    void Update()
    {
        CamMove();
    }

    void CamMove()
    {
        //만약 터치영역을 눌렀으면
        if (_touchField.Pressed)
        {
            _mainCam.m_YAxis.m_InputAxisName = "Mouse Y";
            _mainCam.m_XAxis.m_InputAxisValue = _touchField.TouchDist.x * CameraAngleSpeed;
        }
        else
        {
            _mainCam.m_XAxis.m_InputAxisName = "";
            _mainCam.m_YAxis.m_InputAxisName = "";
            _mainCam.m_XAxis.m_InputAxisValue = 0;
            _mainCam.m_YAxis.m_InputAxisValue = 0;
        }
    }

    //void setCam1()
    //{
    //    frontCam.Priority = 0;
    //}
    //void setCam2()
    //{
    //    letfCam.Priority = 0;
    //}
}

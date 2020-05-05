using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class charactorMovingCameraPoTon : MonoBehaviourPun
{
    public Camera Camera;
    private Joy JoyStick;
    private FixedTouchField touchField;

    //CameraManager cameraManager;
    Rigidbody CharactRigidbody;
    Quaternion rotation = Quaternion.identity;
    private Vector3 dir;
    public float speed = 0.5f;
    private float turn;
    private bool isOnceTurn;
    float tochAngle;
    private float vertical;
    private float CameraAngleY;
    private float CameraAngleSpeed = 0.1f;
    private float CameraPosY = 3f;
    private float CameraPosSpeed = 0.02f;

    void Start()
    {
        CharactRigidbody = GetComponent<Rigidbody>();
        JoyStick = GameObject.Find("JoyStickBackground").GetComponent<Joy>();
        touchField = GameObject.Find("cameraBack").GetComponent<FixedTouchField>();
    }

    private void FixedUpdate()
    {
        if(!photonView.IsMine&&PhotonNetwork.IsConnected)
        {
            return;
        }
        if(touchField.Pressed == true)
        {
            turn = Camera.transform.rotation.eulerAngles.y;
            isOnceTurn = true;
        }
        CharacterRotate();
    }

    void CharacterRotate()
    {
        if (JoyStick.MoveFlag)
        {
            if(isOnceTurn)
            {
                transform.rotation = Quaternion.Euler(new Vector3(JoyStick.JoyVec.x, turn, 0));
                isOnceTurn = false;
            }

            Debug.Log(Camera.transform.rotation.eulerAngles.x);

            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            tochAngle = Mathf.Atan2(JoyStick.JoyVec.x, JoyStick.JoyVec.y) * Mathf.Rad2Deg;
            tochAngle += turn;
            transform.rotation = Quaternion.Euler(new Vector3(JoyStick.JoyVec.x, tochAngle, 0));
            //Debug.Log(tochAngle);
        }
    }
}

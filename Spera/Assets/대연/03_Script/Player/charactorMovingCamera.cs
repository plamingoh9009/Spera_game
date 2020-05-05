using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class charactorMovingCamera : MonoBehaviourPun
{
    public Camera Camera;
    private Joy JoyStick;
    private FixedTouchField touchField;

    CameraManager cameraManager;
    Rigidbody CharactRigidbody;
    Quaternion rotation = Quaternion.identity;
    private Vector3 dir;
    public float speed;
    private float turn;
    private bool isOnceTurn;
    float tochAngle;
    private float vertical;
    private float CameraAngleY;

    PlayerInfo _playerInfo;                 // 플레이어의 상태를 가져온다.

    // private float CameraAngleSpeed = 0.1f;
    // private float CameraPosY = 3f;
    //private float CameraPosSpeed = 0.02f;

    void Start()
    {
        CharactRigidbody = GetComponent<Rigidbody>();
        JoyStick = GameObject.Find("JoyStickBackground").GetComponent<Joy>();
        touchField = GameObject.Find("cameraBack").GetComponent<FixedTouchField>();
        //cameraManager = GameObject.Find("CMcheaserFreeLook").GetComponent<CameraManager>();

        _playerInfo = GetComponent<PlayerInfo>();
        speed = 5.0f;
    }

    private void FixedUpdate()
    {
        //if (!photonView.IsMine && PhotonNetwork.IsConnected)
        //{
        //    return;
        //}

        if (touchField.Pressed == true)
        {
            turn = Camera.transform.rotation.eulerAngles.y;
            isOnceTurn = true;
        }
        // 플레이어는 죽었을 때, 특정 행동을 할 때는 움직일 수 없다.
        if (_playerInfo.isDeath) { }
        else
        {
            if (_playerInfo.isStopMoving == true) { }
            else
            {
                //Debug.Log("Player Is StopMove" + _playerInfo.isStopMoving);
                CharacterRotate();
            }
        }
    }

    void CharacterRotate()
    {
        if (JoyStick.MoveFlag)
        {
            if (isOnceTurn)
            {
                transform.rotation = Quaternion.Euler(new Vector3(JoyStick.JoyVec.x, turn, 0));
                isOnceTurn = false;
            }

            //  Debug.Log(Camera.transform.rotation.eulerAngles.x);
            // 살금살금 걷기 중이라면 속도를 반으로 깎는다.
            if (_playerInfo.isCrouched)
            {
                transform.Translate(Vector3.forward * speed * 0.5f * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }

            tochAngle = Mathf.Atan2(JoyStick.JoyVec.x, JoyStick.JoyVec.y) * Mathf.Rad2Deg;
            tochAngle += turn;
            transform.rotation = Quaternion.Euler(new Vector3(JoyStick.JoyVec.x, tochAngle, 0));
            //Debug.Log(tochAngle);
        }
    }
}

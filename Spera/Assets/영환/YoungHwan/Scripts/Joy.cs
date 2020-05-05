using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

using myTools = CommonFunction;
public class Joy : MonoBehaviourPun
{
    Transform _player;        // 플레이어 좌표
    Transform _stick;         // 조이스틱

    public float speed;
    public Vector3 stickFirstPos;  // 조이스틱의 처음 위치
    public Vector3 JoyVec;         // 조이스틱의 벡터(방향)
    public Vector3 JoyTransformForward;
    private float Radius;           // 조이스틱 배경의 반 지금.
    public bool MoveFlag;           // 플레이오 움직임 스위치
   // CameraManager cameraManager;    //카메라 매니저 가져오기

    private void Start()
    {
        // 플레이어 오브젝트 찾는 코드
        _player = myTools.LoadObject(myTools.LoadType.PLAYER).transform;
        _stick = transform.GetChild(0).transform;

        // 캔버스 크기 잡는 변수 
        Radius = GetComponent<RectTransform>().sizeDelta.y * 2f;
        stickFirstPos = _stick.transform.position;

        // 캔버스 크기에 대한 반지름 조절
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;

        MoveFlag = false;
    }

    private void FixedUpdate()
    {
        
        //if (MoveFlag)
        //{
        //    //Player.transform.Translate(Vector3.forward* Time.deltaTime *speed);
        //}
    }

    // 드래그
    // BaseEventData :  EnentSystem에 연결된 baseEventData의 생성자 입니다.
    public void Drag(BaseEventData _Data)
    {
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        // 조이스틱을 이동시킬 방향을 구함.(오른쪽 ,왼쪽 ,위, 아래)
        JoyVec = (Pos - stickFirstPos).normalized;

        // 조이스틱의 처음 위치와 현재 내가 터치하고 있는 위치의 거리를 구한다.
        float Dis = Vector3.Distance(Pos, stickFirstPos);

        // 거리가 반지름보다 작으면 조이스틱을 현재 터치하고 있는곳으로 이동.
        if (Dis < Radius)
        {
            _stick.position = stickFirstPos + JoyVec * Dis;
            MoveFlag = true;
        }

        // 거리가 반지름보다 커지면 조이스틱을 반지름의 크기만큼만 이동.
        else
        {
            _stick.position = stickFirstPos + JoyVec * Radius;
            MoveFlag = true;
        }

        // 플레이어 이동관련 
       
     //  Player.eulerAngles = new Vector3(0,Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);

       // Debug.Log("조이스틱 움직이는 중");
    }

    // 드래그 끝
    public void DragEnd()
    {
        _stick.position = stickFirstPos; // 스틱을 원래의 위치로
        JoyVec = Vector3.zero;          // 방향을 0으로
      //  Debug.Log("돌아왔다.");
        MoveFlag = false;    
    }

}

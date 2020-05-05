using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterAnimation : MonoBehaviour
{
    enum AniType
    {

    }

    Animator animator;
    Joy JoyStick;

    CommonFunction _cFunc;                  // 필요한 연산을 미리 정의해 둔 클래스다.
    float _moveSpeed;                       // 플레이어가 받아오는 스피드 값이다.
    public bool isInteracting { get; set; }  // 플레이어가 현재 상호작용중인지 정의하는 변수다.
    //public 
    // CheaserHitBButton hitButton;

    void Start()
    {
        animator = GetComponent<Animator>();
        JoyStick = GameObject.Find("JoyStickBackground").GetComponent<Joy>(); // 다른클래스를 호출한다.

        _cFunc = new CommonFunction();
        _moveSpeed = 0;
        isInteracting = false;
        // hitButton = GameObject.Find("AttckButton").GetComponent<CheaserHitBButton>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!photonView.IsMine)
        //{
        //    return;
        //}

        if (JoyStick.MoveFlag == false)
        {
            animator.SetBool("isRun", JoyStick.MoveFlag);
            //  Debug.Log("아이들상태입니다.");
        }
        if (JoyStick.MoveFlag == true)
        {
            animator.SetBool("isRun", JoyStick.MoveFlag);
            // Debug.Log("달리는 상태입니다.");
        }
        // 캐릭터의 움직임은 idle, run이 블렌딩 되어 있다.
        // MoveSpeed 값으로 어느 모션을 렌더할지 결정한다.
      //  _moveSpeed = _cFunc.ComputeBigValue(Mathf.Abs(JoyStick.JoyVec.x),
          //  Mathf.Abs(JoyStick.JoyVec.y));
        animator.SetFloat("MoveSpeed", _moveSpeed);

        if (isInteracting == true)
        {
            animator.SetBool("isAttack", isInteracting);
           // animator.SetBool("IsInteracting", isInteracting);
            StartCoroutine("PlayInteractMotion");
        }
    }

    IEnumerator PlayInteractMotion()
    {
        yield return new WaitForSeconds(2f);
        isInteracting = false;
        animator.SetBool("isAttack", isInteracting);
    }
}

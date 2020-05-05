using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCrouchedWalk : MonoBehaviour
{
    public GameObject playetObject;
    Animator _animator;
    //PlayerMove _playerMove;

    public bool isCrouchedWalk { get; private set; }
   
    public void OnClickCrouchedWalk()
    {
        _animator = playetObject.GetComponent<Animator>();
        //_playerMove = playetObject.GetComponent<PlayerMove>();

        isCrouchedWalk = _animator.GetBool("IsCrouchedWalk");
        _animator.SetBool("IsCrouchedWalk", !isCrouchedWalk);

        // 플레이어 움직이는 속도 리미트 걸 때 사용하는 bool 변수
        //if (!isCrouchedWalk)
        //{
        //    _playerMove.isCrouchedWalk = true;
        //}
        //else
        //{
        //    _playerMove.isCrouchedWalk = false;
        //}
    }
}

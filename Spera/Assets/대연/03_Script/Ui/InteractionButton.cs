using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyTools = CommonFunction;
public class InteractionButton : MonoBehaviour
{
    GameObject _myPlayer;                   // 플레이어 오브젝트
    PlayerInfo _playerInfo;                 // 플레이어 정보
    Animator _playerAni;                    // 플레이어 애니메이터
    PlayerRangeHandler _playerCollision;    // 플레이어 콜리전

    float _hammerDuration;                  // 망치 애니메이션 돌리는 시간

    private void Awake()
    {
        _hammerDuration = 3.8f;
    }
    private void Start()
    {
        _myPlayer = MyTools.LoadObject(MyTools.LoadType.PLAYER);
        if (_myPlayer == null)
        {
            MyTools.LoadObjectMessage(false, "InteractionButton.cs -> myPlayer");
        }
        _playerInfo = _myPlayer.GetComponent<PlayerInfo>();
        _playerAni = _myPlayer.GetComponent<Animator>();

        _playerCollision = _myPlayer.transform.Find("InteractCollider").GetComponent<PlayerRangeHandler>();
    }

    /// <summary>
    /// 상호작용 버튼과 연결된 함수
    /// </summary>
    public void OnInteraction()
    {
        if (_playerInfo.isShop)
        {
            _playerInfo.OpenShopUi();
        }// if: 현재 상점 근처라면 상점을 연다
        else
        {
            if (_playerCollision.statue != null)
            {
                Debug.Log("Player Collision statue: " + _playerCollision.statue);
                OnHammerWait();
            }
            else
            {
                OnHammerButton();
            }
        }
    }

    /// <summary>
    /// 망치 휘두를 때 실행하는 함수
    /// </summary>
    public void OnHammerButton()
    {
        //Debug.Log("Attack: " + _playerInfo.isAttacking + "\nDoing: " + _playerInfo.isDoingAction);
        // 아무 액션도 재생하고 있지 않을 때 해머를 휘두른다.
        if (_playerInfo.isAttacking == false &&
            _playerInfo.isDoingAction == false)
        {
            _playerInfo.sledgeHammer.SetActive(true);
            _playerInfo.isStopMoving = true;
            _playerInfo.isAttacking = true;
            _playerInfo.isDoingAction = true;
            _playerAni.SetBool("IsHammer", true);

            StartCoroutine(HammerDuration());

            // 해머를 휘두를 때 target이 석상이라면 석상 상태를 풀어준다.
            if (_playerCollision != null && _playerCollision.target != null)
            {
                ReviveStatuePlayer();
            }
        }
    }
    IEnumerator HammerDuration()
    {
        yield return new WaitForSeconds(_hammerDuration);
        _playerInfo.sledgeHammer.SetActive(false);
        _playerInfo.isStopMoving = false;
        _playerInfo.isAttacking = false;
        _playerInfo.isDoingAction = false;
        _playerAni.SetBool("IsHammer", false);

        _playerInfo.isHammerWaiting = false;
    }
    /// <summary>
    ///  석상이 된 다른 플레이어를 살리는 함수
    /// </summary>
    void ReviveStatuePlayer()
    {
        if (_playerCollision.targetInfo.isStatue)
        {
            _playerCollision.targetInfo.HitStatuePlayer();
        }
        // 다른 플레이어를 살렸다면 점수
        if (_playerCollision.targetInfo.statuePlayerHitCnt == 2)
        {
            GameManager.instance.AddScore(PLAYER_ACTION.ASSIST);
        }
    }

    public void OnHammerWait()
    {
        // 아무 액션도 재생하고 있지 않을 때 대기상태로 들어간다.
        if (_playerInfo.isHammerWaiting == false &&
            _playerInfo.isDoingAction == false)
        {
            _playerInfo.sledgeHammer.SetActive(true);
            _playerInfo.isStopMoving = true;
            _playerInfo.isHammerWaiting = true;
            _playerInfo.isDoingAction = true;
            _playerAni.SetBool("IsHammerWaiting", true);
        }
    }
    public void OffHammerWait()
    {
        if (_playerInfo.isHammerWaiting == true &&
            _playerInfo.isDoingAction == true)
        {
            _playerInfo.sledgeHammer.SetActive(false);
            _playerInfo.isStopMoving = false;
            _playerInfo.isHammerWaiting = false;
            _playerInfo.isDoingAction = false;
            _playerAni.SetBool("IsHammerWaiting", false);
        }
    }
}

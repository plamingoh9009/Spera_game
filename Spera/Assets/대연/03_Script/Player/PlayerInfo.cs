using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyTools = CommonFunction;
/// <summary>
/// 플레이어의 정보를 가지고 있는 클래스다.
/// 플레이어의 콜리전 처리도 담당한다.
/// </summary>
public class PlayerInfo : MonoBehaviour
{
    #region Varaiable
    public enum ObjType
    {
        EMPTY,
        SHOP
    }
    ObjType _objType;

    public float shieldDuration;                  // 실드 파티클 유지시간
    public float healDuration;                    // 힐 파티클 유지시간
    public float searchRadius;                    // 추적자를 감지하는 범위 반지름이다. 단위는 1.0f == 1 Unit

    public int playerHp { get; set; }              // 플레이어의 Hp
    public int statuePlayerHitCnt { get; set; }    // 석상이 된 플레이어가 맞을 때 쓰는 카운트

    public bool isIam;                             // 현재 오리지널 캐릭터인지 식별하는 변수
    public bool isStatue { get; set; }             // 플레이어가 석상이 되었는지 식별하는 변수
    public bool isShop { get; private set; }       // 상점 Ui를 열기위한 변수
    public bool isDeath { get; set; }              // 죽는지 알기 위한 변수
    public bool isCheaserAroundMe { get; private set; } // 추적자가 근처에 있는지 식별하는 변수

    public bool isCrouched { get; set; }           // 플레이어가 살금살금 움직일지 결정하는 변수
    public bool isStopMoving { get; set; }         // 플레이어가 움직일지, 말지 결정하는 변수

    public bool isDoingAction { get; set; }         // 플레이어가 애니메이션을 재생중인지 식별하는 변수
    public bool isAttacking { get; set; }           // 플레이어가 해머를 쓰는지 알기 위한 변수
    public bool isHammerWaiting { get; set; }       // 플레이어가 석상을 때리기 위해 기다리는지 식별하는 변수
    public bool isFlashlight { get; set; }          // 손전등 사용중인지 알기 위한 변수

    public bool isHealing { get; private set; }     // 메디킷 사용중인지 알기 위한 변수
    public bool isShielding { get; private set; }   // 실드 사용중인지 알기 위한 변수

    Animator _playerAni;

    GameObject _playerParticle;
    ParticleSystem _playerBleeding;
    ParticleSystem _playerShield;
    ParticleSystem _playerHeal;
    ParticleSystem _playerStatueHit;

    public GameObject playerHand { get; private set; }                 // 플레이어의 손
    public GameObject flashLight { get; private set; }                // 플레이어의 손전등
    public GameObject sledgeHammer { get; private set; }               // 플레이어의 망치
    #endregion
    #region AtStart()
    private void Awake()
    {
        _objType = ObjType.EMPTY;

        isIam = true;
        isStatue = false;
        isShop = false;
        isDeath = false;

        isCrouched = false;
        isStopMoving = false;
        isDoingAction = false;
        isAttacking = false;
        isHealing = false;
        isShielding = false;

        playerHp = 3;
        statuePlayerHitCnt = 0;

        shieldDuration = 5.0f;
        healDuration = 5.0f;
        searchRadius = 17.5f;

        // 플레이어가 들고 있는 오브젝트를 찾아온다.
        playerHand = MyTools.SearchObjectWithTag(transform.gameObject, "PlayerHand");
        if (playerHand == null)
        {
            MyTools.LoadObjectMessage(false, "CharactorAnimationV2.cs -> Player's Hand Object");
        }
        else
        {
            sledgeHammer = playerHand.transform.Find("sledgeHammer").gameObject;
            flashLight = playerHand.transform.Find("FlashLight").gameObject;
        }
        if (sledgeHammer == null)
        {
            MyTools.LoadObjectMessage(false, "CharactorAnimationV2.cs -> SledgeHammer Object");
        }
        if (flashLight == null)
        {
            MyTools.LoadObjectMessage(false, "CharactorAnimationV2.cs -> FlashLight Object");
        }
    }
    private void Start()
    {
        // 플레이어가 사용할 파티클을 찾아온다.
        _playerParticle = transform.Find("PlayerParticle").gameObject;
        _playerBleeding = _playerParticle.transform.Find("Bleeding").GetComponent<ParticleSystem>();
        _playerShield = _playerParticle.transform.Find("Shield").GetComponent<ParticleSystem>();
        _playerHeal = _playerParticle.transform.Find("HealStream").GetComponent<ParticleSystem>();
        _playerStatueHit = _playerParticle.transform.Find("RockHit").GetComponent<ParticleSystem>();

        // 플레이어 애니메이션을 찾아온다.
        _playerAni = GetComponent<Animator>();

        // 근처에 추적자가 있는지 확인해서 bool 변수 업데이트
        StartCoroutine(SearchCheaserAroundMe());
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        SetupObjTypeFromCollider(other.tag);
    }

    /// <summary>
    /// Collider에 부딪힌 상대가 누구인지 판단해서 처리하는 함수
    /// </summary>
    /// <param name="colliderTag">충돌한 상대방의 태그</param>
    void SetupObjTypeFromCollider(string colliderTag)
    {
        if (string.Compare(colliderTag, "Shop") == 0)
        {
            //Debug.Log("Collider Shop");
            _objType = ObjType.SHOP;
            isShop = true;
        }
    }

    /// <summary>
    /// 상점 UI를 여는 함수
    /// </summary>
    public void OpenShopUi()
    {
        //Debug.Log("PlayerInfo.cs -> OpenShop");
        GameObject objShop = MyTools.LoadObject(MyTools.LoadType.SHOP_PANEL);
        objShop.GetComponent<Shop>().OpenShop();
    }


    /// <summary>
    /// 플레이어가 치료받는 함수
    /// </summary>
    public void HealPlayer()
    {
        playerHp++;
    }

    #region Using_item
    /// <summary>
    /// 실드 아이템을 썼을 때 사용하는 함수
    /// </summary>
    public void OnShield()
    {
        if (_playerShield == null)
        {
            MyTools.LoadObjectMessage(false, "PlayerInfo.cs -> _playerShield");
        }
        else
        {
            isShielding = true;     // 쉴드 작동중
            _playerShield.Play();   // 보호막 파티클 재생
            StartCoroutine(ShieldDuration());
        }
    }
    IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(shieldDuration);
        isShielding = false;
        _playerShield.Stop();
    }

    /// <summary>
    /// 힐 키트를 썼을 때 사용하는 함수
    /// </summary>
    public void OnHealkit(bool isPlayHealAnimation = false)
    {
        if (_playerHeal == null)
        {
            MyTools.LoadObjectMessage(false, "PlayerInfo.cs -> _playerHeal particle");
        }
        else
        {
            if (isPlayHealAnimation)
            {
                isHealing = true;     // 힐 하는 중
                // 다른 사람 치료해 줄 때 애니메이션 재생
                _playerAni.SetBool("IsHealing", true);
                // 다른 사람 힐 해줄 때 점수
                GameManager.instance.AddScore(PLAYER_ACTION.HEAL);
            }
            else
            {
                HealPlayer();
                isHealing = true;     // 힐 하는 중
                _playerHeal.Play();   // 힐 파티클 재생
            }
            StartCoroutine(HealDuration(isPlayHealAnimation));
        }
    }
    IEnumerator HealDuration(bool isPlayHealAnimation)
    {
        yield return new WaitForSeconds(healDuration);

        if (isPlayHealAnimation)
        {
            isHealing = false;
            _playerAni.SetBool("IsHealing", false);
        }
        else
        {
            isHealing = false;
            _playerHeal.Stop();
        }
    }
    #endregion
    #region Player_hit
    /// <summary>
    /// 상대방이 플레이어를 때릴 때 사용하는 함수
    /// </summary>
    public void HitPlayer()
    {
        // 플레이어가 쉴드를 사용중이라면 피가 닳지 않는다.
        if (isShielding) { }
        else
        {
            playerHp--;             // 플레이어 체력 깎기
            _playerBleeding.Play(); // 피흘리는거 실행
        }
    }
    /// <summary>
    /// 상대방이 석상이 된 플레이어를 때릴 때 쓰는 함수
    /// </summary>
    public void HitStatuePlayer()
    {
        if (statuePlayerHitCnt < 2)
        {
            statuePlayerHitCnt++;
            _playerStatueHit.Play();
        }
        else
        {
            statuePlayerHitCnt = 0;
            HealPlayer();
            OnShield();
        }
    }
    #endregion

    IEnumerator SearchCheaserAroundMe()
    {
        //Debug.Log("This is SearchCheaser");
        Collider[] colliders = null;
        Collider target = null;

        yield return new WaitForSeconds(2.0f);
        // 추적자를 찾는 코드
        colliders = Physics.OverlapSphere(transform.position, searchRadius, 1 << LayerMask.NameToLayer("Cheaser"));
        if (colliders.Length > 0)
        {
            // 추적자는 언제나 1명이니 처음 찾은 콜라이더를 target 한다.
            foreach(var myCollider in colliders)
            {
                target = myCollider;
                break;
            }
            isCheaserAroundMe = true;
            //Debug.Log("Cheaser around me dddd: " + colliders.Length);
        }
        else
        {
            colliders = null;
            target = null;
            isCheaserAroundMe = false;
            //Debug.Log("No Cheaser around me..: ");
        }
        StartCoroutine(SearchCheaserAroundMe());
    }
}

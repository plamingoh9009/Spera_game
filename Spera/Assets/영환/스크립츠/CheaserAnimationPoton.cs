using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CheaserAnimationPoton : MonoBehaviourPun
{
    public bool isAttackInfo { get; private set; }

    // 파티클 작업
    public Transform paticleTrans;
    public ParticleSystem paticleSkill1;
    public ParticleSystem paticleSkill2;
    public ParticleSystem paticleSkill3;
    
    Animator animator;
    Joy  JoyStick;
    CheaserHitBButton hitButton;
    FlashCollision flashCollision;
    CapsuleCollider cheaserCollider;
    ChSkillButton1 chSkillButton1;      // 스킬버튼1
    ChSkillButton2 chSkillButton2;      // 스킬버튼2
    Skill3Button   chSkillButton3;      // 스킬버튼3

    // 애니메이션 블랜딩
    private float skill1Blend = 0.0f;
    private float skill2Blend = 0.0f;

    // 애니메이션이 끝나고 플레이어 아웃라인보이게함
    public bool isRealShowLine { get; set; }

    // 쉐이더를 바꿔줄 녀석
    public Material skill1Shader;
    public Material originShader;
    private SkinnedMeshRenderer cheaserMesh;

    void Start()
    {
        animator = GetComponent<Animator>();
        JoyStick = GameObject.Find("JoyStickBackground").GetComponent<Joy>(); // 다른클래스를 호출한다.
        hitButton = GameObject.Find("AttckButton").GetComponent<CheaserHitBButton>();
        flashCollision = GameObject.Find("Cheaser").GetComponent<FlashCollision>();
        cheaserCollider = this.GetComponent<CapsuleCollider>();
        
        // 스킬 버튼
        chSkillButton1 = GameObject.FindWithTag("CheaserSkillUiButton").GetComponent<ChSkillButton1>();
        chSkillButton2 = GameObject.FindWithTag("CheaserSkillUiButton2").GetComponent<ChSkillButton2>();
        chSkillButton3 = GameObject.FindWithTag("CheaserSkillUiButton3").GetComponent<Skill3Button>();

        // 파티클 제어
        paticleSkill1.Stop();
        paticleSkill2.Stop();

        // 쉐이더 변경
        cheaserMesh = this.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        if ((!photonView.IsMine & PhotonNetwork.IsConnected))
        {
            return;
        }
        #region Animation

        if (JoyStick.MoveFlag == false)
        {
            animator.SetBool("isRun", JoyStick.MoveFlag);
        }
        if(JoyStick.MoveFlag == true)
        {
            animator.SetBool("isRun", JoyStick.MoveFlag);
        }

        if(hitButton.isAttack == false) // 공격애니메이션 중간 지점 전에 콜라이더 꺼주기
        {
            isAttackInfo = false;
        }

        if (hitButton.isAttack == true)
        {
          isAttackInfo = true;
          animator.SetBool("isAttack", isAttackInfo);
          StartCoroutine("AttackAnimation");
        }

        if(flashCollision.isSturn == true)
        {
            Debug.Log("스턴");
        }
        SkillAnimation();
        #endregion

    }


    #region AnimationFunction

    void SkillAnimation()
    {
        // 스킬1
        if(chSkillButton1.isFlashing == true)
        {
            // 쉐이더 바꿔주는 부분
            cheaserMesh.material = skill1Shader;
            
            if (paticleSkill1.isPlaying == true)
            {
                skill1Blend += 0.1f;
            }
            animator.SetFloat("skill1Blen", skill1Blend);
            animator.SetBool("isSKill1", chSkillButton1.isFlashing);

            StartCoroutine("Skill1Button");
        }

        // 스킬2
        if(chSkillButton2.isHidePlayerShow == true)
        {
            skill2Blend = 1.0f;
            animator.SetBool("isSkill2", chSkillButton2.isHidePlayerShow);
            animator.SetFloat("Skill2Blen", skill2Blend);
            StartCoroutine("Skill2Button");
        }

        // 스킬3
        if(chSkillButton3.isSummonCrow == true)
        {
            animator.SetBool("isSkill3", chSkillButton3.isSummonCrow);
            StartCoroutine("Skill3Button");
        }
    }

    #endregion

    #region IEnumeratorFunc
    IEnumerator Skill1Button()
    {
        paticleSkill1.Play(true);
        yield return new WaitForSeconds(1f);
        skill1Blend -= 0.1f;
        chSkillButton1.isFlashing = false;
        yield return new WaitForSeconds(1f);
        animator.SetFloat("Skill1Blen", skill1Blend);
        animator.SetBool("isSKill1", chSkillButton1.isFlashing);
        cheaserMesh.material = originShader;
        paticleSkill1.Stop(true);
        skill1Blend = 0.0f;
    }


     IEnumerator Skill2Button()
     {
        paticleSkill2.Play(true);
        yield return new WaitForSeconds(2f);
        skill2Blend -= 0.01f;
        if(skill2Blend <= 0.8f)
        {
            isRealShowLine = true;
        }
        chSkillButton2.isHidePlayerShow = false;
        animator.SetFloat("Skill2Blen", skill2Blend);
        animator.SetBool("isSkill2", chSkillButton2.isHidePlayerShow);
     }
    
    IEnumerator Skill3Button()
    {
        yield return new WaitForSeconds(2f);
         chSkillButton3.isSummonCrow = false;
        animator.SetBool("isSkill3", chSkillButton3.isSummonCrow);

    }

     IEnumerator AttackAnimation()
     {
         yield return new WaitForSeconds(2f);
         hitButton.isAttack = false;
         animator.SetBool("isAttack", hitButton.isAttack);
     }

    #endregion


}

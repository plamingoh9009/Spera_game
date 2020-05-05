using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheaserAttackMode : MonoBehaviour
{
    private ChSkillButton1 chSkillButton1;
    private ChSkillButton2 chSkillButton2;
    //private Skill3Button chSkillButton3;

    private CheaserMoveCamera characterSpeed;
    private CheaserAnimationPoton cheaserAni;
    private Outline playerOutLine;

    public AudioClip flasingAudioClip;
    public float flashingAddSpeed =0.1f;
    public Transform cheaserTrans;

    [SerializeField]
    private float skillTime = 1f;
    private float skill2WaitTime = 6f;
    private float movingDistance = 0.01f;

    RaycastHit rayHit;
    Ray cheaserRay;     // Ray캐스트를 사용하여 앞에 장애물을 파악한다.
    private float distance = 1.8f;
    private bool isDontGo = false;

    // 스킬3 까마귀의 스폰 타임 지정
    public bool isSpawnCrow { get; set; }
    private float CrowSpawnTime = 100.0f;


    void Start()
    {
        chSkillButton1 = GameObject.FindWithTag("CheaserSkillUiButton").GetComponent<ChSkillButton1>();
        chSkillButton2 = GameObject.FindWithTag("CheaserSkillUiButton2").GetComponent<ChSkillButton2>();
       // chSkillButton3 = GameObject.FindWithTag("CheaserSkillUiButton3").GetComponent<Skill3Button>();
        characterSpeed = GameObject.Find("Cheaser").GetComponent<CheaserMoveCamera>();
        playerOutLine = GameObject.FindGameObjectWithTag("Player").GetComponent<Outline>();
        cheaserAni = this.GetComponent<CheaserAnimationPoton>();
        cheaserRay = new Ray();
    }

    void FixedUpdate()
    {
        cheaserRay.origin = cheaserTrans.position;
        cheaserRay.direction = cheaserTrans.transform.forward;
        Debug.DrawRay(cheaserRay.origin, cheaserRay.direction * distance, Color.green);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.right);

        for (int i = 0; i > hits.Length; i++)
        {
            rayHit = hits[i];
            Debug.Log("부딪혔다.");

            isDontGo = true;
        }

        Skill1();
        SKill2();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           // Debug.Log("플레이어 때리고 있음");
        }
    }

    private void Skill1()
    {
        // skill1구현부분
        if (chSkillButton1.isFlashing == true )
        {
            if (isDontGo == false)
            {
                movingDistance += flashingAddSpeed;
                //characterSpeed.speed = flashingAddSpeed;
                transform.Translate(0, 0, movingDistance * Time.deltaTime);
            }
            StartCoroutine("SkillSpeedUp");
        }

        if (Physics.Raycast(cheaserRay.origin, cheaserRay.direction, out rayHit, distance))
        {
            if (rayHit.collider.gameObject.name != "Skele")
            {
                Debug.Log(rayHit.collider.name);
                isDontGo = true;
                cheaserAni.paticleSkill1.Stop(true);
            }
        }
        // skill1구현부분

    }

    private void SKill2()
    {
        // skill2구현부분
        if (cheaserAni .isRealShowLine == true)
        {
            playerOutLine.enabled = true;
        }
        if (playerOutLine.enabled == true)
        {
            StartCoroutine("Skill2Invisibility");
        }
    }


    IEnumerator SkillSpeedUp() // Flashing 스킬 
    {
        yield return new WaitForSeconds(skillTime);
        chSkillButton1.isFlashing = false;
        characterSpeed.speed = 5.0f;
        isDontGo = false;
    }

    IEnumerator Skill2Invisibility() // 플레이어 투명화
    {
        yield return new WaitForSeconds(skill2WaitTime);
        playerOutLine.enabled = false;
        chSkillButton2.isHidePlayerShow = false;
        cheaserAni.isRealShowLine = false;
    }
}

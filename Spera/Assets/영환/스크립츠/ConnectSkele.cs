using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectSkele : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private Animator playerAni; 
    public bool isDamage;
    public bool isCollisionConnect;

    private void Awake()
    {
        playerInfo = this.GetComponent<PlayerInfo>();
        playerAni = GetComponent<Animator>();
        isDamage = false;
        isCollisionConnect = false;
    }

    private void FixedUpdate()
    {
        if(isCollisionConnect == true)
        {
            StartCoroutine("WaitDamage");
        }

        if (isDamage == true)
        {
            StartCoroutine("DamageAniWati");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheaserWeapone" && isCollisionConnect == false)
        {
            playerInfo.playerHp -= 1;
            Debug.Log(playerInfo.playerHp);
            isCollisionConnect = true;
            isDamage = true;
            playerAni.SetBool("isDamage", isDamage);
        }
    }
    
    IEnumerator WaitDamage()
    {
        yield return new WaitForSeconds(3f);
        isCollisionConnect = false;
    }

    IEnumerator DamageAniWati()
    {
        yield return new WaitForSeconds(0.75f);
        isDamage = false;
        playerAni.SetBool("isDamage", isDamage);
    }
}

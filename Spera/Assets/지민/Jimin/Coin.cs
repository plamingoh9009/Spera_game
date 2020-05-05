using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coin = 1;
    public float rotateSpeed = 5.0f;
    // 플레이어들을 가져오는 변수
    GameObject[] players; 

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Player") != null)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //플레이어는 코인을 얻는다
            AddCoinToPlayer(other.transform.position);
            //플레이어와 코인이 충돌됐을때 오브젝트풀로 다시 반환해준다
            float random = Random.Range(10.0f, 30.0f);
            ObjectPoolManager.instance.ReturnCoin(this , random);
        }
        if (other.tag == "Fence")
        {
            //모서리에 닿으면 다시 반환 //이부분 야매임
            ObjectPoolManager.instance.currentCoinReturn--;
            ObjectPoolManager.instance.ReturnCoin(this , 1);
        }
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed);
    }

    /// <summary>
    /// 코인을 충돌한 플레이어에게 추가하는 함수
    /// </summary>
    /// <param name="colliderPosition">코인과 충돌한 collider의 
    /// other(플레이어) position 값</param>
    void AddCoinToPlayer(Vector3 colliderPosition)
    {
        foreach(var player in players)
        {
            if(colliderPosition == player.transform.position)
            {
                player.GetComponent<PickupCoin>().coinCnt++;
                GameManager.instance.AddScore(PLAYER_ACTION.GET_COIN);
            }
        }
    }
}



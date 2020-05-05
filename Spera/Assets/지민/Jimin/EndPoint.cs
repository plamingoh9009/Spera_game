using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //끝나는 지점과 플레이어가 닿으면 게임끝.
            GameManager.instance.InGameEnd(true);
        }
    }
}

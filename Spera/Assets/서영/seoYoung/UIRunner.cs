using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
러너들이 담긴 UI 정보 
 */

public class UIRunner : MonoBehaviour
{
    PlayerInfo Player_Info;                             //플레이어 인포         
    public GameObject[] Blood;                    //피 모임

    void Start()
    {
        Player_Info = FindObjectOfType<PlayerInfo>();
    }

    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    Player_Info.playerHp -= 1;
        //    Debug.Log(Player_Info.playerHp);
        //}
        if(Player_Info.playerHp == 3)
        {
            Blood[0].SetActive(true);
            Blood[1].SetActive(true);
            Blood[2].SetActive(true);
        }
        else if(Player_Info.playerHp == 2)
        {
            Blood[0].SetActive(false);
            Blood[1].SetActive(true);
            Blood[2].SetActive(true);
        }
        else if (Player_Info.playerHp == 1)
        {
            Blood[0].SetActive(false);
            Blood[1].SetActive(false);
            Blood[2].SetActive(true);
        }
        if (Player_Info.playerHp == 0)
        {
            Blood[0].SetActive(false);
            Blood[1].SetActive(false);
            Blood[2].SetActive(false);
        }
    }
    
    public void OneButton()
    {

    }
    public void TwoButton()
    {

    }
    public void ThreeButton()
    {

    }

}

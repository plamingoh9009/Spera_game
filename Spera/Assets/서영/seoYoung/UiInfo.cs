using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyTools = CommonFunction;
public class UiInfo : MonoBehaviour
{
    UILabel _coinScore;
    //public GameObject bloodone;
    //public GameObject bloodtwo;
    //public GameObject bloodthree;

    public int coinCnt { get; set; }

    //PlayerInfo Hp;
    //void Update()
    //{
    //    if(Hp.playerHp == 2)
    //    {
    //        bloodone.SetActive(false);
    //    }
    //    if (Hp.playerHp == 1)
    //    {
    //        bloodtwo.SetActive(false);
    //    }
    //    if (Hp.playerHp == 0)
    //    {
    //        bloodthree.SetActive(false);
    //    }
    //} 

    private void Start()
    {
        _coinScore = transform.Find("Label").GetComponent<UILabel>();
        if(_coinScore == null)
        {
           /* 임시로 꺼둠 */
          //  MyTools.LoadObjectMessage(false, "UiInfo.cs -> UILabel component");
        }

        coinCnt = 0;
    }
    private void FixedUpdate()
    {
        StartCoroutine("ScoreCoin", 0.5f);
    }

    public void ScoreCoin()
    {
        _coinScore.text = " : " + coinCnt;
    }



}



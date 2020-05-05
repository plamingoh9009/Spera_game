using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameScore : MonoBehaviour
{
    public UILabel Score;                   //점수
    public UILabel highScore;
    public static UiInfo coinsocre;

    private void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
     void Update()
    {
        // int num =;
        //Score.text = num.ToString();
        //PlayerPrefs.SetInt("HighScore", num);
        // GameScore.= coinsocre.ToString();
    }


}

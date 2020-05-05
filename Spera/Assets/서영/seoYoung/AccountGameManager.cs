using System.Collections;
using System.Collections.Generic;
//uisng Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

//게임 계정을 보관하는 곳 
public class AccountGameManager : MonoBehaviour     //MonoBehaviourPun
{
    
    public Text Idtext;

    //싱글톤
    private static AccountGameManager Account_Instance = null;

    private void Awake()
    {
        if (Account_Instance == null)
        {
            Account_Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
         
            Destroy(this.gameObject);
        }
    }




}

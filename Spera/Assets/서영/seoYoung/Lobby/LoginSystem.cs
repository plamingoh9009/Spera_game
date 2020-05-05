using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoginSystem : MonoBehaviour
{
    public UILabel ID_InputLable;
    public GameObject WaringLoginWindow;
    public GameObject MatchingBtn;
    public GameObject LoginPanel;
    public GameObject CreaterWinow;
    public GameObject GameRecordWIndow;
   public GameObject ReInputLable;


    public LookPointMove MoveCamera;                                //지민오빠 코드 불러오기(카메라가 이동하는 효과)

    public float m_fDuration = 3.0f;
    public float m_sDruration = 3.0f;

    //로그인 완료 버튼
    public void LoginBtn()
    {
        if (ID_InputLable.text.Length <= 0)
        {   // 계정 안쓰면 경고창 뜨게 만드는거
            WaringLoginWindow.SetActive(true);
        }
        else
        {   /*  */
            LoginManager.instance.userID = ID_InputLable.text;
            //LoginCheck loginCheck = GameObject.Find("LoginCheck").GetComponent<LoginCheck>();
            //loginCheck.isLogin = true;
            StartCoroutine(GoneSign());
        }
    }
    //계정이 없어서 경고창이 덧을 경우
    public void CancleButton()
    {
        WaringLoginWindow.SetActive(false);
    }
    public void onCreaterButton()
    {
        CreaterWinow.SetActive(true);
    }
    public void DownCreaterButton()
    {
        CreaterWinow.SetActive(false);
    }

    //상 아이콘을 클릭할 경우
     public void ClickonGameRecord()
    {
        GameRecordWIndow.SetActive(true);
    }
    public void ClickCloseGameRecord()
    {
        GameRecordWIndow.SetActive(false);
    }


    public IEnumerator GoneSign()
    {
        TweenAlpha.Begin(LoginPanel, m_fDuration, 0);
        yield return new WaitForSeconds(m_fDuration);
        StartCoroutine(GoCamera());
    }

    public IEnumerator GoCamera()
    {
        MoveCamera.IsStart = true;
        TweenScale.Begin(MatchingBtn, m_sDruration, Vector3.one * 1);
        yield return new WaitForSeconds(m_fDuration);
        MatchingBtn.SetActive(true);
    }
}

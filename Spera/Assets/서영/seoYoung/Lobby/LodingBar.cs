using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
/*
 변수 앞에 s는 scene의 약자임
 */


public class LodingBar : MonoBehaviourPunCallbacks
{
    public enum USERGUEST
    {
        FIRST_USER,
        SECOND_USER,
        THIRD_USER,
        FOURTH_USER,
        FIFTH_USER
    }

    public UISlider s_SliderLoding = null;
    public UILabel s_LabelLoding;
    public GameObject[] usertab;                                                                      //유저 탭  
    public UILabel[] username;                                                                           //유저 닉네임
    LoginSystem Loginsystem;
    USERGUEST guest;
    void Start()
    {
        //switch (guest)
        //{
        //    case USERGUEST.FIRST_USER:
        //        username[0].text = Loginsystem.ID_InputLable.text;
        //        break;
        //    case USERGUEST.SECOND_USER:
        //        username[1].text = Loginsystem.ID_InputLable.text;
        //        break;
        //    case USERGUEST.THIRD_USER:
        //        username[2].text = Loginsystem.ID_InputLable.text;
        //        break;
        //    case USERGUEST.FOURTH_USER:
        //        username[3].text = Loginsystem.ID_InputLable.text;
        //        break;
        //    case USERGUEST.FIFTH_USER:
        //        username[4].text = Loginsystem.ID_InputLable.text;
        //        break;
        //}
        StartCoroutine(LoadScene());
        Connect();
    }
    public void Connect()
    {
        //중복 접속 시도를 막기위해 접속 버튼 잠시 비활성화
        //joinButton.interactable = false;
        //마스터 서버에 접속중이라면
        if (PhotonNetwork.IsConnected)
        {
            //connectionInfoText.text = "룸에 접속...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            //마스터 서버에 접속중이 아니라면, 마스터 서버에 접속 시도
            //connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";
            //마스터 서버로의 재접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //접속 상태 표시
        //connectionInfoText.text = "빈 방이 없음, 새로운 방 생성";
        //최대 5명 수용가능한 빈방 생성
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 5 });
    }

    //룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    {
        //접속 상태 표시
        //connectionInfoText.text = "방 참가 성공";

        //if (count == 2)
        //{
        //PhotonNetwork.LoadLevel("cityJack");
        //}
        //else
        //{
        //    connectionInfoText.text = "대기중";
        //}
    }
    IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("cityJack");
        operation.allowSceneActivation = false;
        usertab[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        usertab[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        usertab[2].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        usertab[3].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        usertab[4].SetActive(true);

        // slider의 valiue를 매 프레임 증가
        while (!operation.isDone)                                                                                    //isDone은 다운로드가 완료되었는지 확인으로 사용
        {
            s_LabelLoding.text = " " + Mathf.RoundToInt(s_SliderLoding.value * 100);
            //yield return  new WaitForSeconds(15);                                                                                                //update()가 실행될 때까지 기다림. (null)을 양보 반환했던 코루틴이 이어서 진행됨.
            yield return  null;                                                                                                //update()가 실행될 때까지 기다림. (null)을 양보 반환했던 코루틴이 이어서 진행됨.

            if (s_SliderLoding.value < 0.9f)
            {
                s_SliderLoding.value = Mathf.MoveTowards(s_SliderLoding.value, 0.9f, Time.deltaTime);
            }
            if (s_SliderLoding.value >= 0.9f)
            {
                s_SliderLoding.value = Mathf.MoveTowards(s_SliderLoding.value, 1f, Time.deltaTime);
            }
            if (s_SliderLoding.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }

    }
}
/*
 비동기 로드는 Scene을 불러올 때 멈추지 않고 다른 작업을 할 수 있습니다.
    LoadScene()로 Scene을 불러오면 완료될 때까지 다른 작업을 수행하지 않습니다.
 */


/*
 유니티에서 화면의 변화를 일으키기 위해서는 update()함수 내에서 작업을 하게 된다. 
Update 함수는 매 프레임을 그릴때마다 호출되며 60fps의 경우라면 초당 60번의 update()함수에서 발생한다.
하지만 다수의 프레임을 오고가며 어떤 작업을 한다면 프레임 드랍이 발생할 수 있다. 그래서 코루틴을 사용함
  */

using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";
    public Text connectionInfoText;
    public Button joinButton;

    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;

        PhotonNetwork.ConnectUsingSettings();
        //룸 접속 버튼 비활성화
        //joinButton.interactable = false;
        connectionInfoText.text = "마스터 서버에 접속중...";
    }

    public override void OnConnectedToMaster()
    {
        //룸 접속 버튼 활성화
        //joinButton.interactable = true;
        //접속 정보 표시
        connectionInfoText.text = "온라인 : 마스터 서버와 연결됨";
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        //룸 접속 버튼 비활성화
        //joinButton.interactable = false;
        //접속 정보 표시
        connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";

        //마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();

    }

    

  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public enum CHEASER_ACTION
{
    ATTACK = 150, //공격했을때
    KILL = 300, //죽였을때
    GET_COIN = 10
    // 사용법 : GameManager.instance.AddScore(CHEASER_ACTION.ATTACK);
}
public enum PLAYER_ACTION
{
    ITEM = 100, //아이템사용
    GET_COIN = 10, //코인 먹었을때
    DESTROY_STATUE = 300, //석상 부쉈을때
    ASSIST = 250, //팀을 도왔을때
    HEAL = 250, //남치료했을떄
    SUCCESS = 100 //석상풀때 퍼즐성공시
}

public class GameManager : MonoBehaviourPunCallbacks//, IPunObservable
{
    #region 싱글톤
        // 싱글톤 접근용 프로퍼티
        public static GameManager instance
        {
            get
            {
                if (m_instance == null) // 싱글톤 변수에 오브젝트가 할당되지 않았다면
                {
                    // 씬에서 게임매니저 오브젝트를 찾아서 할당
                    m_instance = FindObjectOfType<GameManager>();
                }

                return m_instance;
            }
        }

        private static GameManager m_instance;
    #endregion

    private int count;
    //#region Photon Callbacks
    ////룸에 있지 않은경우
    ////public override void OnPlayerLeftRoom(Player other)
    ////{
    ////    if(PhotonNetwork.IsMasterClient)
    ////    {
    ////        Debug.LogFormat("OnPlayerLeftRoom IsMasterClient{0}", PhotonNetwork.IsMasterClient);
    ////        LoadArena();
    ////    }

    ////}
    ////public void OnplayerEnteredRoom(Player other)
    ////{
    ////    if(PhotonNetwork.IsMasterClient)
    ////    {
    ////        Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient{0}", PhotonNetwork.IsMasterClient);

    ////        LoadArena();
    ////    }
    ////}

    //#endregion

    //#region Public Methods
    ////룸을 떠날때
    //public void LeaveRoom()
    //{
    //    PhotonNetwork.LeaveRoom();
    //}
    //#endregion

    //#region Private Methods

    ////void LoadArena()
    ////{
    ////    if(!PhotonNetwork.IsMasterClient)
    ////    {
    ////        Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
    ////    }
    ////    PhotonNetwork.LoadLevel("cityJack");
    ////}
    //#endregion



    public GameObject playerPrefab;
    public GameObject chaserPrefab;
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{

    //}


    #region 인게임

    public InGameInfo inGameInfo;
    private int gameCnt; //게임을 몇판했는지. 게임시작할때마다 증가
    public bool isGameover { get; private set; }
    public string userID;

    public void AddScore(object newScore)
    {
        //점수 증가
        inGameInfo.score += (int)newScore;
    }

    //게임 시작하면
    public void InGameStart(GameObject playerPrefab)
    {
        gameCnt++;

        //게임정보 초기화
        inGameInfo.charName = playerPrefab.name;
        inGameInfo.score = 0;
        inGameInfo.killCnt = 0;
        inGameInfo.isWin = false;
    }

    //게임이 끝나면 (탈출하면)
    public void InGameEnd(bool isWin = false , int killCnt = 0)
    {
        inGameInfo.isWin = isWin;
        inGameInfo.killCnt = killCnt;

        // 인게임데이터폴더 안에 인게임인포를 게임카운트.json파일로 저장
        JsonManager.SaveJsonData(inGameInfo, "InGameData", gameCnt.ToString());

        //씬체인지

    }

    #endregion
    #region UI

    /*Game  UI를 관리하는 곳 */

    public GameObject OptionMenu;                                            //옵션 메뉴
    public GameObject AreyouGoingOutWindow;                                 //나갈거니 창?
    public GameObject AreReadyGameWindow;                                  //게임시작 창?


    //옵션버튼 클릭시 
    public void OpenOptionBtn()
    {
        OptionMenu.SetActive(true);
    }
    //OPtin메뉴에서 Done버튼 클릭할 경우
    public void CloseOptionBtn()
    {
        OptionMenu.SetActive(false);
    }
    //OPtin메뉴 >> EXIT 누른 상태 
    public void ClickEXITBtn()
    {
        AreyouGoingOutWindow.SetActive(true);
    }
    //OPtin메뉴 >>   EXIT >>NO
    public void GoingOutNoBtn()
    {
        AreyouGoingOutWindow.SetActive(false);
    }
    public void YesGoingOutNoBtn()
    {
        AreyouGoingOutWindow.SetActive(false);
        OptionMenu.SetActive(false);
    }

    public void AreYouReadyButton()
    {
        AreReadyGameWindow.SetActive(true);
    }
    public void NOAreYouReadyButton()
    {
        AreReadyGameWindow.SetActive(false);
    }
    //추가할 코드 
    public void GoBack()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            Application.Quit();
        }
    }
    //추가할 코드 

    #endregion

        
    private void Awake()
    {
        // 씬에 싱글톤 오브젝트 된 다른 게임매니저 오브젝트가 있다면
        if (instance != this)
        {
            Destroy(gameObject);
        }
           // AreReadyGameWindow = GameObject.Find("ReadyGameWindow").gameObject;

        //게임 한판도 안했으니 기록 초기화
        gameCnt = 0;
        //DontDestroyOnLoad(gameObject);
        //Json데이터들 삭제해주기 추가해야됨
    }

    void Start()
    {
        count = PhotonNetwork.CountOfPlayers;
        Debug.Log(count);
        Vector3 randomSpawnPos = playerPrefab.transform.position;
        randomSpawnPos.x += 50.0f;
        randomSpawnPos.z += 50.0f;
        randomSpawnPos.y += 10.0f;
        switch (count) {
            case 1:
                PhotonNetwork.Instantiate(chaserPrefab.name, randomSpawnPos, Quaternion.identity);
                break;
            case 2:
                PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
                break;
            case 3:
                PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
                break;
            case 4:
                PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
                break;
            case 5:
                PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
                break;
        }
    }
}

[System.Serializable]
public struct InGameInfo
{
    public string charName; //플레이했던 캐릭터이름
    public int score;       //점수

    //플레이어용
    public bool isWin;      //승패
    //체이서용
    public int killCnt;     //킬횟수
}


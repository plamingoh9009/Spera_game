using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyTools = CommonFunction;
public class PickupCoin : MonoBehaviour
{
    GameObject _coinObjPool;        // 지민이가 만든 코인 오브젝트 풀을 가져온다.
    GameObject _nguiCoinObj;        // 오브젝트에서 서영이가 만든 플레이어인포 스크립트를 가져온다.
    UiInfo _uiInfo;                 // 오브젝트에서 서영이가 만든 플레이어인포 스크립트를 가져온다.

    public int coinCnt { get; set; }    // 먹은 코인의 숫자다.

    void Start()
    {
        _coinObjPool = MyTools.LoadObject(MyTools.LoadType.COIN_OBJECT_POOL);
        _nguiCoinObj = MyTools.LoadObject(MyTools.LoadType.NGUI_COIN_OBJ);
        if (_nguiCoinObj != null)
        {
            _uiInfo = _nguiCoinObj.GetComponent<UiInfo>();
        }
        if (_uiInfo == null)
        {
            MyTools.LoadObjectMessage(false, "PickupCoin.cs -> UiInfo component");
        }

        coinCnt = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_uiInfo != null)
        {
            _uiInfo.coinCnt = coinCnt;
            //Debug.Log(GameManager.instance.inGameInfo.score);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyTools = CommonFunction;
public class HammerFromCube : MonoBehaviour
{
    GameObject _player;
    PlayerInfo _playerInfo;
    private void Start()
    {
        _player = MyTools.LoadObject(MyTools.LoadType.PLAYER);
        _playerInfo = _player.GetComponent<PlayerInfo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("This is HammerCube");
        if (other.CompareTag("PlayerCollision"))
        {
            Debug.Log("[Statue hit cnt]: " + _playerInfo.statuePlayerHitCnt);
            _playerInfo.HitStatuePlayer();
        }
        else
        {
            Debug.Log("other tag is " + other.tag);
        }
    }
}

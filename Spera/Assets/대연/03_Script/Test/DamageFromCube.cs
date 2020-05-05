using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyTools = CommonFunction;
public class DamageFromCube : MonoBehaviour
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
        Debug.Log("This is DamageCube");
        if (other.CompareTag("PlayerCollision"))
        {
            _playerInfo.HitPlayer();
            Debug.Log("[Player Hit]: " + _playerInfo.playerHp);
        }
        else
        {
            Debug.Log("other tag is " + other.tag);
        }
    }
}

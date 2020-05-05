using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeHandler : MonoBehaviour
{
    GameObject _player;
    PlayerInfo _playerInfo;

    GameObject[] _otherPlayers;
    public GameObject target { get; private set; }
    public GameObject statue { get; private set; }
    public PlayerInfo targetInfo { get; private set; }

    public bool isSelf { get; private set; }

    private void Awake()
    {
        isSelf = true;
        target = null;
        statue = null;
        targetInfo = null;
    }

    void Start()
    {
        _player = transform.parent.gameObject;
        _otherPlayers = GameObject.FindGameObjectsWithTag("Player");
        _playerInfo = _player.GetComponent<PlayerInfo>();
    }

    private void OnTriggerStay(Collider other)
    {
        // other가 플레이어인지 식별한다.
        if (other.CompareTag("Player"))
        {
            // other가 플레이어라면 어느 플레이어인지 특정한다.
            target = FindOtherOne(other.transform.position);
            targetInfo = target.GetComponent<PlayerInfo>();
        }
        else
        {
            target = null;
        }

        // 나와 충돌한 플레이어가 존재한다면 isSelf는 false다
        if (target != null)
        {
            isSelf = false;
        }
        else
        {
            isSelf = true;
        }

        // other가 석상인지 식별한다.
        if(other.CompareTag("Statue"))
        {
            statue = other.gameObject;
        }
        else
        {
            statue = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MissingCollider();
    }

    /// <summary>
    /// 콜리더를 벗어났을 때 other를 식별하기 위해 사용한 변수를 초기화하는 함수다.
    /// </summary>
    void MissingCollider()
    {
    }

    /// <summary>
    /// 나와 충돌한 플레이어가 어느 플레이어인지 찾는 함수
    /// </summary>
    /// <param name="otherPos">충돌한 다른 플레이어의 포지션 값(Vector3)</param>
    /// <returns>찾은 플레이어의 GameObject</returns>
    GameObject FindOtherOne(Vector3 otherPos)
    {
        foreach (var player in _otherPlayers)
        {
            if (otherPos == player.transform.position)
            {
                return player;
            }
        }
        return null;
    }
}

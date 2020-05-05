using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using myTools = CommonFunction;
public class CrouchedWalkHandler : MonoBehaviour
{
    GameObject _myPlayer;
    Animator _playerAni;
    PlayerInfo _playerInfo;

    private void Start()
    {
        _myPlayer = myTools.LoadObject(myTools.LoadType.PLAYER);
        _playerAni = _myPlayer.GetComponent<Animator>();
        _playerInfo = _myPlayer.GetComponent<PlayerInfo>();
    }

    public void OnClickButton()
    {
        if(_playerInfo.isCrouched)
        {
            _playerInfo.isCrouched = false;
            _playerAni.SetBool("IsCrouched", false);
        }
        else
        {
            _playerInfo.isCrouched = true;
            _playerAni.SetBool("IsCrouched", true);
        }
    }
}

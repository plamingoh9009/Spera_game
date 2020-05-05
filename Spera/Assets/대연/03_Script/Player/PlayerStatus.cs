using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private PlayerInfo _playerInfo;
    charactorMovingCamera _playerMove;

    public enum MATERIAL { ELEMENT1, ELEMENT2 }
    public Texture playerTexure;
    public Texture statueTexture;

    private SkinnedMeshRenderer[] childMeshRenderList;

    private Animator playerAnimatorChange;
    private float aniStopTime = 1.233333f;
    private float aniEndLen;

    MATERIAL playerMaterial;

    private void Awake()
    {
        _playerInfo = GetComponent<PlayerInfo>();
        childMeshRenderList = GetComponentsInChildren<SkinnedMeshRenderer>();
        playerAnimatorChange = GetComponent<Animator>();
        playerMaterial = MATERIAL.ELEMENT1;

        _playerMove = GetComponent<charactorMovingCamera>();
    }

    private void FixedUpdate()
    {
        FindSkinnedRender();

        // 현재 실행중인 애니메이션을 핼프 모션에서 멈추기
        if (0 < _playerInfo.playerHp && _playerInfo.playerHp <= 1)
        {
            _playerInfo.isStatue = true;
            _playerInfo.isStopMoving = true;

            // 석상이 되었다면 텍스쳐를 돌처럼 바꾼다.
            StartCoroutine(StatueAniDuration());
        }
        else
        {
            _playerInfo.isStatue = false;
            // 다시 움직이려면 다른 조건이 필요함.
            //_playerInfo.isStopMoving = false;

            // 석상이 아니라면 텍스쳐를 원래대로 되돌린다.
            playerMaterial = MATERIAL.ELEMENT1;
            playerAnimatorChange.speed = 1.0f;
        }

        playerAnimatorChange.SetBool("IsStatue", _playerInfo.isStatue);
        ChangeMaterial(playerMaterial);
    }

    void FindSkinnedRender() // 자식의 SkinnedMeshRenderer의 컴포넌트를 다 가져오는 함수
    {
        foreach (SkinnedMeshRenderer materialMesh in childMeshRenderList)
        {
            materialMesh.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        }
    }

    void ChangeMaterial(MATERIAL _playerMaterial)
    {
        switch (_playerMaterial)
        {
            case MATERIAL.ELEMENT1:
                foreach (SkinnedMeshRenderer materialMesh in childMeshRenderList)
                {
                    materialMesh.material.SetTexture("_MainTex", playerTexure);
                }
                break;

            case MATERIAL.ELEMENT2:
                foreach (SkinnedMeshRenderer materialMesh in childMeshRenderList)
                {
                    materialMesh.material.SetTexture("_MainTex", statueTexture);
                }
                break;
        }
    }

    private bool isEqual(float _aniEndLen, float _aniStopTime)
    {
        if (_aniEndLen >= _aniStopTime - Mathf.Epsilon && _aniEndLen <= _aniStopTime + Mathf.Epsilon)
            return true;
        else
            return false;
    }

    IEnumerator StatueAniDuration()
    {
        yield return new WaitForSeconds(aniStopTime);
        playerMaterial = MATERIAL.ELEMENT2;
        playerAnimatorChange.speed = 0.0f;
    }
}

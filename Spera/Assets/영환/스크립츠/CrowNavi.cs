using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // AI, 내비게이션 시스템 관련 코드 가져오는거


// 까마귀 AI를 구현
public class CrowNavi : MonoBehaviour
{
    public GameObject[] players;        // 플레이어를 담을 오브젝트
    private GameObject TargetPlayer;    // 추적대상
    public LayerMask playerTarget;      // 추적 대상 레이어
    private NavMeshAgent pathFinder;    // 경로계산 AI 에이전트
  //  private CapsuleCollider


    private CheaserAttackMode cheaserAttackMode;

    // 추적할 대상이 존재하는지 알려주는 함수
    private bool hasTarget
    {
        get
        {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if(players != null && cheaserAttackMode.isSpawnCrow == true)
            {
                return true;
            }
            return false;
        }
    }
    
    private void Awake()
    {
        cheaserAttackMode = GetComponentInParent<CheaserAttackMode>();
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // 주기적으로 추적할 대상의 위치를 찾아 경로를 갱신
    private IEnumerator UpdatePath()
    {
        // 스폰타임때까지 움직인다.
        while(cheaserAttackMode.isSpawnCrow == true)
        {
            if (hasTarget)
            {
                // 추적 대상 존재 : 경로를 갱신하고 AI 이동을 계속 진행
                pathFinder.isStopped = false;
                foreach (GameObject gameObjects in players)
                {
                    pathFinder.SetDestination(gameObjects.transform.position);
                }
            }
            else
            {
                //추적 대상 없음 : AI 이동 중지
                pathFinder.isStopped = true;

                // 20 유닛의 반지름을 가진 가상의 구를 그렸을때 , 구 와 겹치는
                // 단, targetLayers에 해당하는 레이어를 가진 콜라이더만 가져오도록 필터링
                Collider[] colliders =
                    Physics.OverlapSphere(transform.position, 20f, playerTarget);
                
                // 모든 콜라이더들을 순회하면서, 살아있는 플레이어를 찾기
                for(int i = 0; i < colliders.Length; i++)
                {
                    // 콜라이더로 부터 GameObject 컴포넌트 가져오기
                    GameObject gameObject = colliders[i].GetComponent<GameObject>();

                    // GameObject 가 존재하며, 까마귀의 스폰타임이 남아있다면
                    if (gameObject != null && cheaserAttackMode.isSpawnCrow == true)
                    {
                        // 추적 대상을 해당 gameObject로 설정
                        TargetPlayer = gameObject;
                        // for문 루프 즉시 정지
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

}

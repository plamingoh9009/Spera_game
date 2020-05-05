using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectPoolManager : MonoBehaviour
{
    //싱글톤 인스턴스
    public static ObjectPoolManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<ObjectPoolManager>();
            }
            return m_instance;
        }
    }
    private static ObjectPoolManager m_instance;

    public Queue<Coin> coinPoolQueue = new Queue<Coin>(); //코인을 담아놓을 풀
    public GameObject coinPrefab;
    public int addCoinCnt = 20;      //초기에 생성할 코인 갯수

    public int currentCoinReturn = 0;
    public int maxCoinReturn = 100; //100개를 넘기면 코인생산 중단
    public float coinMaxDistance = 45.0f; //코인뿌려줄 범위

    void Awake()
    {
        //오브젝트풀 초기화 (addCoinCnt 만큼)
        PoolInit(addCoinCnt);
        //코인 생성
        CoinInit();
    }

    void PoolInit(int initCnt)
    {
        for (int i = 0; i < initCnt; i++)
        {
            //갯수만큼 코인을 오브젝트풀에 넣어둔다
            coinPoolQueue.Enqueue(CreateNewCoin());
        }
    }

    Coin CreateNewCoin() //새로운 코인을 반환
    {
        Coin newCoin = Instantiate(coinPrefab.GetComponent<Coin>());
        newCoin.gameObject.SetActive(false);
        newCoin.transform.SetParent(GameObject.Find("CoinObjectPool").gameObject.transform);
        return newCoin;
    }

    private void CoinInit()
    {
        //스타팅 지점의 위치에 맞게 코인을 생성
        //Transform[] startPoint = GameObject.Find("CoinSpawnPoints").GetComponentsInChildren<Transform>();

        //for (int i = 0; i < startPoint.Length; i++)
        //{
        //    Coin coin = GetCoin();
        //    coin.transform.position = startPoint[i].position;
        //}

        for (int i = 0; i < coinPoolQueue.Count; i++)
        {
            StartCoroutine(CoinRespawn(1));
        }
    }

    public Coin GetCoin()
    {
        //오브젝트풀에 있는 코인을 다른 곳으로 보낼 때 쓴다

        //내뱉은 갯수 증가
        currentCoinReturn++;

        if (coinPoolQueue.Count>0)
        {
            //오브젝트풀에 코인이 존재하면 그 코인을 보냄
            Coin coin = coinPoolQueue.Dequeue();

            //인게임코인즈를 부모로 만듬
            coin.transform.SetParent(GameObject.Find("InGameCoins").gameObject.transform);
            coin.gameObject.SetActive(true);
            return coin;
        }
        else
        {
            //없으면 만들어서 보냄
            Coin coin = CreateNewCoin();
            coin.transform.SetParent(GameObject.Find("InGameCoins").gameObject.transform);
            coin.gameObject.SetActive(true);
            return coin;
        }
    }

    public void ReturnCoin(Coin coin , float respawnTime)
    {
        //반환받은 코인을 다시 오브젝트풀에 넣어준다..
        coin.gameObject.SetActive(false);
        //돌아왔으니 부모를 다시 코인오브젝트풀로 설정
        coin.transform.SetParent(GameObject.Find("CoinObjectPool").gameObject.transform);
        instance.coinPoolQueue.Enqueue(coin);

        //일정 시간이 지나면 코인은 리스폰된다
        //단 인게임에 뿌려진 코인이 100개를 넘으면 리스폰안됨
        if (maxCoinReturn <= currentCoinReturn) return;

        StartCoroutine(CoinRespawn(respawnTime));
    }

    IEnumerator CoinRespawn(float respawnTime)
    {
        //일정시간 뒤에 코인을 내뱉음
        yield return new WaitForSeconds(respawnTime);

        Vector3 spawnPosition = GetRandomPointOnNavmesh(transform.position, coinMaxDistance);

        Coin coin = GetCoin();

        //어째선지 위치가 계속 무한이 떠서 에러가뜸
        //그래서 0으로 맞춰줌. 이부분 야매
        if (float.IsInfinity(spawnPosition.x))
        {
            spawnPosition.x = 0;

            spawnPosition.y = 0;

            spawnPosition.z = 0;
        }
        coin.transform.position = spawnPosition + new Vector3(0f,1f,0f);
    }

    Vector3 GetRandomPointOnNavmesh(Vector3 center, float distance)
    {
        //네비매쉬를 통해 지나갈수있는 길중 하나를 랜덤하게 내뱉음
        Vector3 randomPos = Random.insideUnitSphere * distance + center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        return hit.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public bool isEnd;

    void Start()
    {
        StartCoroutine(EndCheck());
    }

    IEnumerator EndCheck()
    {
        StatueCheck();

        yield return new WaitForSeconds(1);

        //석상 4개가 모두 부숴질때까지 무한히 1초마다 석상체크
        if (!isEnd) StartCoroutine(EndCheck());
    }

    public void StatueCheck()
    {
        //필드의 모든 석상 가져옴
        GameObject[] statues = GameObject.FindGameObjectsWithTag("Statue");

        //필드상에 석상 갯수가 0이면 
        if (statues.Length == 0 )
        {
            //탈출구 생성
            isEnd = true;
            CreateEndPoint();
        }
    }
    public void CreateEndPoint()
    {
        int random = Random.Range(1, 4);

        //안개 지움
        GameObject fog = GameObject.Find("BackGroundObject/Fog/Fog" + random.ToString()).transform.GetChild(2).gameObject;
        fog.SetActive(false);

        //닿으면 종료되는 지점 생성
        GameObject endPoint = transform.GetChild(random-1).gameObject;
        endPoint.SetActive(true);
    }
}

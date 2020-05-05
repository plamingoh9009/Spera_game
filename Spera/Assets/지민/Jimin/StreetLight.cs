using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLight : MonoBehaviour
{
    GameObject _light;
    int _loopTime;

    void Start()
    {
        //라이트 가져와
        _light = transform.Find("Spot Light").gameObject;
        //코루틴 시작
        StartCoroutine(SetState());
    }

    IEnumerator SetState()
    {
        _loopTime = Random.Range(20, 60); //20~60초 간격으로 랜덤하게 상태를 변경시킴
        int random = Random.Range(0, 4);  //0~4까지 상태를 랜덤으로 정함

        switch (random)
        {
            case 0: //불 꺼짐
                break;
            case 1: // 짧은시간(3초) 켜짐
                StartCoroutine(LightActive(3));
                break;
            case 2: // 긴 시간(10초) 켜짐
                StartCoroutine(LightActive(10));
                break;
            case 3: // 깜빡임
                //약 2초동안 0.3초간격으로 깜빡임
                InvokeRepeating("BlinkLightActive", _loopTime - 2, 0.3f);
                break;
        }

        yield return new WaitForSeconds(_loopTime);
        // loopTime이 지나면 자기 자신을 불러서 무한 반복

        _light.SetActive(false);
        _loopTime = Random.Range(20, 60);
        CancelInvoke(); //<-     InvokeRepeating 함수를 종료시키는 함수
        StartCoroutine(SetState());
    }

    IEnumerator LightActive(int time)
    {
        //빛을 켜고 일정 시간이 지나면 빛을 꺼줌
        _light.SetActive(true);
        yield return new WaitForSeconds(time);
        _light.SetActive(false);
    }

    void BlinkLightActive()
    {
        //켜져있으면 꺼주고 꺼져있으면 켜줌
        switch (_light.activeSelf)
        {
            case true:
                _light.SetActive(false);
                break;
            case false:
                _light.SetActive(true);
                break;
        }
    }
}

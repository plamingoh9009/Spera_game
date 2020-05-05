using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                                                                                                                                                         //UI를 사용하기 전에 네임스페이스에 작성하기
using UnityEngine.EventSystems;                                                                                                                                                    //UI를 사용하기 위한 EventSystem


public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image BgImg;                                                                                                                                                                     // 조이스틱에 사용할 뒷배경
    private Image JoystickImg;                                                                                                                                                           //조이스틱 이미지
    private Vector3 inputVector;                                                                                                                                                        //JoystickImg이미지를 이동하기 위한 벡터 위치값

    private void Start()
    {
        BgImg = GetComponent<Image>();
        JoystickImg = transform.GetChild(0).GetComponent<Image>();                                                                                    //JoystickImg이미지 위치 초기화
    }
    //IDragHandler
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;                                                                                                                                                                                 //JoystickImg이미지 위치 
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(BgImg.rectTransform                                                     
             ,ped.position
             , ped.pressEventCamera
             , out pos))
        {
            pos.x = (pos.x / BgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / BgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x*2-1 , 0, pos.y * 2 - 1);                                                                                                                      //JoystickImg이미지 터치했을때 이동
            //벡터 제곱근(magnitude)이 1보다 크면?            벡터는 정규화
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //JoystickImg이미지 움직이기   BgImg에서벗어나지 않도록 6으로 나눔
            JoystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (BgImg.rectTransform.sizeDelta.x / 6),
                inputVector.z *(BgImg.rectTransform.sizeDelta.y/6));
        }
    }
    //IPointerDownHandler
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    //IPointerUpHandler
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        JoystickImg.rectTransform.anchoredPosition = Vector3.zero;                                              //JoystickImg 위치지정
    }
    //플레이어 수평.x
    public float Horizontal()
    {
        return inputVector.x;
    }
    //플레이어 수직.z
    public float Vertical()
    {
        return inputVector.z;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public Item item;                           //현재 아이템 
    public UISprite itmeimg;              //UI 이미지
    public int itemCount;                          //아이템 수량

   void Update()
    {
        SetSlot();
    }
    public void  SetSlot()
    {
        if(itemCount ==0)
        {
            itmeimg.color = Color.red;
        }
    }
}

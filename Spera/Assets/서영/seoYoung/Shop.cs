using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopPanel;                                   //shop창
    public UILabel descript_item;                                      //아이템 대사 
    public UILabel item_name;                                           //아이템 이름 
    public GameObject soldoutLabel;
    GameObject[] players;


    //아이템 슬롯
    [SerializeField]
    private GameObject SlotParente;
  private ItemSlot[] slots;

    void Start()
    {
        slots = SlotParente.GetComponentsInChildren<ItemSlot>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ShopPlayer(other.transform.position);
        }
    }
    //상점 열기
    public void OpenShop()
    {
        ShopPanel.SetActive(true);
    }
    //상점 닫기
    public void CloseShop()
    {
        ShopPanel.SetActive(false);
    }

    //아이템 정보
    public void InfoItemButton(ItemSlot _item)
    {
        item_name.text = _item.item.itemInfo.name;
        descript_item.text = _item.item.itemInfo.description;
    }

   public  ItemButtonHandler inven;                        // 플레이어 인벤토리
    public UiInfo playermoney;                            //플레이어  정보 (돈)
    //구매 
    public void SellItemButton(ItemSlot _itemslot)
    {
        //무엇을 구매하였으며 
        //수량 확인
        if (_itemslot.itemCount==0)
        {
            soldoutLabel.SetActive(true);
        }
        else
        {
            //가격
            if(playermoney.coinCnt>= _itemslot.item.itemInfo.Price)
            {
                //아이템 타입이 빈값일 경우
                if(inven.item)
                {
                    _itemslot.itemCount -= 1;
                    soldoutLabel.SetActive(false);
                    playermoney.coinCnt -= _itemslot.item.itemInfo.Price;
                    inven.itemType = (ItemButtonHandler.ItemType)_itemslot.item.itemInfo.ID;
                    switch (_itemslot.item.itemInfo.ID)
                    {
                        case 0:
                            Debug.Log("haha");
                            inven.speeditem.SetActive(true);
                            inven.item.SetActive(false);
                            break;
                        case 1:
                            inven.HealKititem.SetActive(true);
                            inven.item.SetActive(false);
                            break;
                        case 2:
                            inven.FlashLightitem.SetActive(true);
                            inven.item.SetActive(false);
                            break;
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
  

    //플레이어들 중 한플레이어가 아이템을 구매했을 때 사용할 함수
    void ShopPlayer(Vector3 colliderposition)
    {
        foreach(var player in players)
        {
            if(colliderposition == player.transform.position)
            {
                player.GetComponent<ItemSlot>().itemCount++;
            }
        }
    }


}

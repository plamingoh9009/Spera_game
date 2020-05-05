using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemInfo itemInfo;

    public virtual void Start()
    {
        //시작되면 json데이터로 초기화
        
        LoadJsonData();
    }

    public virtual void Use(GameObject target)
    {

    }

    public void AddItem(int price, int id, string name, string desc)
    {
        ItemInfo temp;
        temp.Price = price;
        temp.ID = id;
        temp.name = name;
        temp.description = desc;
        JsonManager.SaveJsonData(temp, "Item", this.GetType().Name);
    }

    //세이브 로드. 컴퍼넌트에서 톱니바퀴모양 누르면 저장불러오기가능
    [ContextMenu("Save Json Data")]
    public void SaveJsonData()
    {
        JsonManager.SaveJsonData(itemInfo, "Item", this.GetType().Name);
    }

    [ContextMenu("Load Json Data" )]
    public void LoadJsonData()
    {
        itemInfo = JsonManager.LoadJsonData<ItemInfo>("Item", this.GetType().Name);
    }


}

//Json데이터로 만들기 위해 직렬화
[System.Serializable]
public struct ItemInfo
{
    //public GameObject prefab;
    public int Price;                                                                     //아이템 가격
    public int ID;
    public string name;
    public string description;
}
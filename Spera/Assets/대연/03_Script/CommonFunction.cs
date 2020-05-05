using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CommonFunction은 필요한 연산을 미리 정의해 둔 클래스다.
/// </summary>
public class CommonFunction : MonoBehaviour
{
    #region Variable
    /// <summary>
    /// 원하는 오브젝트를 불러오기 위한 고유의 값이다.
    /// </summary>
    public enum LoadType
    {
        COIN_OBJECT_POOL,
        NGUI_CAMERA,
        NGUI_COIN_OBJ,
        PLAYER,
        CHEASER,
        PLAYER_HAND,
        GLOBAL_ITEMS,
        SHOP_PANEL,
    }
    #endregion

    #region LoadObject()
    /// <summary>
    /// 포톤에서 쓰기 위해서 필요한 오브젝트를 찾아주는 함수다.
    /// </summary>
    /// <param name="type">LoadType에 정의된 오브젝트 고유 번호</param>
    /// <returns>찾고 싶은 오브젝트 또는 null 값</returns>
    public static GameObject LoadObject(LoadType type)
    {
        GameObject myObject = null;
        GameObject[] myObjects = null;
        string path = "";
        switch (type)
        {
            #region Coin_object_pool
            case LoadType.COIN_OBJECT_POOL:
                path = "ObjectPoolManager/CoinObjectPool";
                myObject = GameObject.Find(path);
                if (myObject == null)
                {
                    LoadObjectMessage(false, path);
                }
                else
                {
                    //LoadObjectMessage(true, path);
                }
                break;
            #endregion
            #region Ngui_camera
            case LoadType.NGUI_CAMERA:
                path = "NGUI/Camera";
                myObject = GameObject.Find(path);
                if (myObject == null)
                {
                    LoadObjectMessage(false, path);
                }
                else
                {
                    //LoadObjectMessage(true, path);
                }
                break;
            #endregion
            #region Ngui_coin_object
            case LoadType.NGUI_COIN_OBJ:
                path = "NGUI/Camera/PlayerPanel/Coin";
                myObject = GameObject.Find(path);
                if (myObject == null)
                {
                    LoadObjectMessage(false, path);
                }
                else
                {
                    //LoadObjectMessage(true, path);
                }
                break;
            #endregion
            #region Shop_panel
            case LoadType.SHOP_PANEL:
                path = "NGUI/Camera/ShopPanel";
                myObject = GameObject.Find(path);
                if (myObject == null)
                {
                    LoadObjectMessage(false, path);
                }
                else
                {
                    //LoadObjectMessage(true, path);
                }
                break;
            #endregion
            #region Player
            case LoadType.PLAYER:
                myObjects = GameObject.FindGameObjectsWithTag("Player");
                PlayerInfo playerInfo = null;
                if (myObjects == null)
                {
                    LoadObjectMessage(false, "My Player");
                }
                else
                {
                    foreach (var player in myObjects)
                    {
                        playerInfo = player.GetComponent<PlayerInfo>();
                        if (playerInfo != null && playerInfo.isIam)
                        {
                            myObject = player;
                            break;
                        }
                    }
                    if (myObject == null)
                    {
                        LoadObjectMessage(false, "Player.isIam");
                    }
                    else
                    {
                        //LoadObjectMessage(true, "My Player");
                    }
                }
                break;
            #endregion
            #region Global_items
            case LoadType.GLOBAL_ITEMS:
                path = "BackGroundObject/Object/GlobalItems";
                myObject = GameObject.Find(path);
                if (myObject == null)
                {
                    LoadObjectMessage(false, path);
                }
                else
                {
                    //LoadObjectMessage(true, path);
                }
                break;
            #endregion
            default:
                myObject = null;
                break;
        }
        return myObject;
    }
    public static void LoadObjectMessage(bool isLoadOk, string path)
    {
        if (isLoadOk)
        {
            //Debug.Log("[Complete]: " + path + " loads complete.");
        }
        else
        {
            Debug.Log("[Error]: " + path + " can't found..");
        }
    }
    #endregion
    #region ComputeBigValue()
    /// <summary>
    /// 두 값중에 큰 값을 리턴한다. 값이 같을 경우 둘 중에 하나 리턴한다.
    /// </summary>
    /// <param name="value1">비교할 첫번째 값이다.</param>
    /// <param name="value2">비교할 두번째 값이다.</param>
    /// <returns>크거나 같은 값이다.</returns>
    public static float ComputeBigValue(float value1, float value2)
    {
        float bigValue = 0;
        if (value1 < value2)
        {
            bigValue = value2;
        }
        else
        {
            bigValue = value1;
        }
        return bigValue;
    }
    #endregion
    #region IsValueAbsOverTarget()
    public static bool IsValueAbsOverTarget(float value, float target)
    {
        if (Mathf.Abs(value) > target)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region SearchObjectWighTag()
    /// <summary>
    /// Target obj의 child obj를 완전탐색하는 함수다. tag를 달고 있는 자식 obj를 찾아온다.
    /// </summary>
    /// <param name="obj">Target object</param>
    /// <param name="tag">찾을 태그</param>
    /// <returns>tag를 달고 있는 자식 object</returns>
    public static GameObject SearchObjectWithTag(GameObject obj, string tag)
    {
        int cnt = obj.transform.childCount;
        GameObject search = null;
        GameObject result = null;
        if (cnt > 0)
        {
            for (int i = 0; i < cnt; i++)
            {
                search = obj.transform.GetChild(i).gameObject;
                if (search.CompareTag(tag))
                {
                    //Debug.Log("Find SledgeHammer: " + search + result);
                    return search;
                }
                else
                {
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        result = SearchObjectWithTag(search, tag);
                    }
                }
            }
        }
        else
        {
            if (obj.CompareTag(tag))
            {
                return obj;
            }
        }
        return result;
    }
    #endregion
}

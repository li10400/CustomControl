using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CreateItems
{
    public Vector2 ItemSize;//item 大小

    public float Interval;//两个item 之间的间隔

    public int ItemCount;//将创建的item 数量

    public GameObject ItemPre, Parent;

    public List<Item> ListItem;//将创建好的item 存储到该list 中

    public float LeftBounds, RightBounds;//左边界，右边界
    CustomSlider _customSlider;

    public void Creat(CustomSlider _customSlider) {
        this._customSlider = _customSlider;
        ItemCount = ItemCount % 2 == 0 ? ItemCount + 1 : ItemCount;//将item数量设置成奇数
        CreatItems();
    }

    //主要是创建items
    private void CreatItems()
    {
        float offset = ((ItemSize.x + Interval) * (ItemCount-1)) / 2;
        for (int i = 0; i < ItemCount; i++)
        {
            GameObject go = InstancePre();
            Vector3 v=go.transform.localPosition;
            v.x = (ItemSize.x + Interval)*i-offset;
            go.transform.localPosition = v;
            Item item = go.GetComponent<Item>();
            item.SetIndex(i, _customSlider);
            ListItem.Add(item);
        }
        LeftBounds = ListItem[0].transform.localPosition.x- ItemSize.x - Interval;
        RightBounds = -LeftBounds;
    }

    private GameObject InstancePre() {
        GameObject go = GameObject.Instantiate(ItemPre);
        go.transform.parent = Parent.transform;
        go.transform.localPosition = Vector3.zero;
        return go;
    } 







}

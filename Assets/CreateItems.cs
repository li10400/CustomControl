using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CreateItems
{
    public Vector2 ItemSize;

    public float Interval;

    public int ItemCount;

    public GameObject ItemPre, Parent;

    public List<Item> ListItem;

    public float LeftBounds, RightBounds;

    public void Creat() {
        ItemCount = ItemCount % 2 == 0 ? ItemCount + 1 : ItemCount;
        CreatItems();
    }

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
            item.SetIndex(i);
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

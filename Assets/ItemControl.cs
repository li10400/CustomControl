using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemControl 
{
    CustomSlider _customSlider;
    CreateItems _createItems;
    public void Init(CustomSlider _customSlider, CreateItems _createItems) {
         this._customSlider=_customSlider;
        this._createItems = _createItems;
    }

    public void OnDrag(Vector2 v) {
        Slide(v.x);
    }

    //左右滑动
    private void Slide(float x) {
        List<Item> list = _createItems.ListItem;
        foreach (Item it in list) {
            Transform t = it.transform;
            Vector2 v = t.localPosition;
            v.x += x;
            it.transform.localPosition = v;
        }
        OutOfBoundsDealwith(list);
    }

    //越界处理
    private void OutOfBoundsDealwith(List<Item> list) {
        List<Item> l =new List<Item>();
        List<Item> r = new List<Item>();

        foreach (Item lt in list) {
            if (ItemPostionX(lt) < _createItems.LeftBounds) {
                l.Add(lt);
            } else if (ItemPostionX(lt) > _createItems.LeftBounds) {
                r.Add(lt);
            }
        }
        if (l.Count >= 0 || r.Count >= 0) {
            QuickSort(list,0, list.Count - 1);
            QuickSort(l, 0, l.Count - 1);
            QuickSort(r, 0, r.Count - 1);

            Item mixItem = list[0];
            Item maxItem = list[list.Count-1];

            float distance = _createItems.Interval + _createItems.ItemSize.x;
            for (int i = 0; i < l.Count; i++)
            {
                Vector2 v = maxItem.transform.localPosition;
                v.x += distance * (i + 1);
                l[i].transform.localPosition = v;
            }


            for (int i = 0; i < r.Count; i++)
            {


            }
        }


       
    }

    private void QuickSort(List<Item> list,int left, int right)
    {
        int l = left;
        int r = right;

        float pivot = ItemPostionX(list,(left + right) / 2);
        Item temp = null;
        while (l < r)
        {
            while (ItemPostionX(list,l) < pivot)
            {
                l+=1;
            }
            while (ItemPostionX(list,r) > pivot)
            {
                r-=1;
            }

            if (l >= r)
            {
                break;
            }
            temp = GetItem(l);

            Item lt = GetItem(l);
            Item rt = GetItem(r);

            lt = rt;
            rt = temp;

            if (ItemPostionX(list,l) == pivot)
            {
                r-=1;
            }
            if (ItemPostionX(list,r) == pivot)
            {
                l+=1;
            }    
        }
        if (l >= r)
        {
            l -= 1;
            r += 1;
        }

        if (left < r)
        {
            QuickSort(list, left, r);
        }

        if (right > l)
        {
            QuickSort(list, l, right);
        }
    }

    private float ItemPostionX(List<Item> list,int index)
    {
        return list[index].transform.localPosition.x;
    }

    private float ItemPostionX(Item item)
    {   
        return item.transform.localPosition.x;
    }

    private Item GetItem(int index) {
        List<Item> l = _createItems.ListItem;
        return l[index];
    }
}

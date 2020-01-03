using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemControl
{
    CustomSlider _customSlider;
    CreateItems _createItems;

    public float Speed;//居中速度
    Coroutine _coroutine;
    public void Init(CustomSlider _customSlider, CreateItems _createItems)
    {
        this._customSlider = _customSlider;
        this._createItems = _createItems;
    }

    public void OnDrag(Vector2 v)
    {
        Slide(v.x);
    }

    //左右滑动
    private void Slide(float x)
    {
        List<Item> list = _createItems.ListItem;
        foreach (Item it in list)
        {
            Transform t = it.transform;
            Vector2 v = t.localPosition;
            v.x += x;
            it.transform.localPosition = v;
        }
        OutOfBoundsDealwith(list);
    }
 
    //越界处理
    private void OutOfBoundsDealwith(List<Item> list)
    {
        List<Item> l = new List<Item>();//x坐标低于左边界的item，临时记录到这里
        List<Item> r = new List<Item>();//x坐标低于右边界的item，临时记录到这里

        foreach (Item lt in list)
        {
            if (ItemPostionX(lt) < _createItems.LeftBounds)
            {
                l.Add(lt);
            }
            else if (ItemPostionX(lt) > _createItems.RightBounds)
            {
                r.Add(lt);
            }
        }


        ///一下代码对左右越界item 进行处理
        if (l.Count >= 1)
        {
            QuickSort(list, 0, list.Count - 1);
            QuickSort(l, 0, l.Count - 1);

            Item maxItem = list[list.Count - 1];
            float distance = _createItems.Interval + _createItems.ItemSize.x;
            for (int i = 0; i < l.Count; i++)
            {
                Vector2 v = maxItem.transform.localPosition;
                v.x += distance * (i + 1);
                l[i].transform.localPosition = v;
            }
        }
        else if (r.Count >= 1)
        {
            QuickSort(list, 0, list.Count - 1);
            QuickSort(r, 0, r.Count - 1);

            Item mixItem = list[0];
            float distance = _createItems.Interval + _createItems.ItemSize.x;
            for (int i = r.Count-1,j=0; i >=0 ; i--,j++)
            {
                Vector2 v = mixItem.transform.localPosition;
                v.x -= distance * (j+1);
                r[i].transform.localPosition = v;
            }
        }
    }

    //利用快排将list中item 进行从小到大排序
    private void QuickSort(List<Item> list, int left, int right)
    {
        int l = left;
        int r = right;
        float pivot = ItemPostionX(list, (left + right) / 2);
        Item temp = null;
        while (l < r)
        {
            while (ItemPostionX(list, l) < pivot)
            {
                l += 1;
            }
            while (ItemPostionX(list, r) > pivot)
            {
                r -= 1;
            }

            if (l >= r)
            {
                break;
            }
            temp =list[l];

            list[l] = list[r];
            list[r] = temp;

            if (ItemPostionX(list, l) == pivot)
            {
                r -= 1;
            }
            if (ItemPostionX(list, r) == pivot)
            {
                l += 1;
            }
        }
        if (l == r)
        {
            l += 1;
            r -= 1;
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

    private float ItemPostionX(List<Item> list, int index)
    {
        return list[index].transform.localPosition.x;
    }

    private float ItemPostionX(Item item)
    {
        return item.transform.localPosition.x;
    }

    ///滑动完毕自动居中
    public void AutomaticCenter() {
        List<Item> list = _createItems.ListItem;
        QuickSort(list, 0, list.Count - 1);
        Item CentreItem = list[list.Count / 2];
        if (_coroutine != null)
        {
            _customSlider.StopCoroutine(_coroutine);
        }
        _coroutine = _customSlider.StartCoroutine(CenterAime(CentreItem));
    }

 
    //点击某个item,让其滚动到居中位置
    public void ItemCenter(Item item) {
        if (_coroutine != null) {
            _customSlider.StopCoroutine(_coroutine);
        }

        _coroutine = _customSlider.StartCoroutine(CenterAime(item));
    }


    IEnumerator CenterAime(Item item) {
      float x=  item.transform.localPosition.x;
       x= -x / Speed;
        for (int i = 0; i < Speed; i++) {
            Slide(x);
            yield return null;
        }
    }
 
}

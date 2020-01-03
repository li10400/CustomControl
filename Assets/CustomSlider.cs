using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CustomSlider : MonoBehaviour,IDragHandler, IEndDragHandler
{
    public CreateItems CreateItems;//主要作用创建item
    public ItemControl ItemControl;//进行无限制的左右滑动
    void Start()
    {
        CreateItems.Creat(this);
        ItemControl.Init(this,CreateItems);
    }


    public void OnDrag(PointerEventData eventData)
    {
        ItemControl.OnDrag(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ItemControl.AutomaticCenter();
    }

    public void ItemCenter(Item item)
    {
        ItemControl.ItemCenter( item);
    }
}

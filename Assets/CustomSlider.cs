using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CustomSlider : MonoBehaviour,IDragHandler, IEndDragHandler
{
    public CreateItems CreateItems;
    public ItemControl ItemControl;
    void Start()
    {
        CreateItems.Creat();
        ItemControl.Init(this,CreateItems);
    }


    public void OnDrag(PointerEventData eventData)
    {
        ItemControl.OnDrag(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}

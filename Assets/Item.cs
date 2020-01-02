using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour,IPointerClickHandler
{
    private int _index;
    public Text ShowTex;

    public void SetIndex(int _index) {
        this._index = _index;
        ShowTex.text = _index.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      
    }

}

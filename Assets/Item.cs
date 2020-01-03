using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour,IPointerClickHandler
{
    private int _index;
    public Text ShowTex;
    CustomSlider _customSlider;
    public void SetIndex(int _index, CustomSlider _customSlider) {
        this._index = _index;
        this._customSlider = _customSlider;
        ShowTex.text = _index.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_customSlider != null) {
            _customSlider.ItemCenter(this);
        }
    }

}

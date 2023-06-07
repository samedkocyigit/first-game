using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ClickyButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default,_pressed;
    [SerializeField] private AudioClip _compressClip, _uncompressedClip;
    [SerializeField] private AudioSource _source;
   public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _pressed;
        _source.PlayOneShot(_compressClip);
        //throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _default;
        _source.PlayOneShot(_uncompressedClip);
        //throw new System.NotImplementedException();
    }

    public void IWasClicked()
    {
        Debug.Log("Clicked");
    }

}

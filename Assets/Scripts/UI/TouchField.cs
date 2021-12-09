using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Vector2 touchDist {get; private set;}

    private Vector2 _pointerOld;
    private int _pointerId;
    private bool _pressed;
    
    public void OnPointerDown(PointerEventData eventData)
    {        
        _pressed = true;
        _pointerId = eventData.pointerId;
        _pointerOld = eventData.position;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;        
    }

    private void Update()
    {
        if (_pressed)
        {
            if (_pointerId >= 0 && _pointerId < Input.touches.Length)
            {              
                touchDist = Input.touches[_pointerId].position - _pointerOld;
                _pointerOld = Input.touches[_pointerId].position;
            }
            else
            {                
                touchDist =  new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _pointerOld;
                _pointerOld = Input.mousePosition;
            }
        }
        else
        {
            touchDist = new Vector2();
        }
    }    
}


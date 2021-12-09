using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    private bool _open;    

    public void Operate()
    {
        if (_open)
        {            
            transform.position -= offset;
        }
        else
        {
            transform.position += offset;
        }
        
        _open = !_open;
    }

    public void Activate()
    {
        if (!_open)
        {
            transform.position += offset;
            _open = !_open;
        }
    }

    public void Deactivate()
    {
        if (_open)
        {
            transform.position -= offset;
            _open = !_open;
        }
    }

    private void Start()
    {
        _open = false;
    }
    
    private void Update()
    {
        
    }
}

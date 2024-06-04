using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeActiveObject : MonoBehaviour
{
    private bool _active = false;

    public void ChangeActive()
    {
        
        if(!_active)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

        _active = !_active;
    }
}

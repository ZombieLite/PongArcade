using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2BossAnimMove : MonoBehaviour
{
    [SerializeField] GameObject _animTarget;
    // Start is called before the first frame update
    void Start()
    {
        _animTarget.GetComponent<Animator>().Play("PanelMovieng");
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = _animTarget.transform.rotation;
        transform.position = _animTarget.transform.position;
    }
}

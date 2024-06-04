using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject _txtLevelStart;
    [SerializeField] GameObject EntityLevelCreate;
    void Start()
    {
        _txtLevelStart.SetActive(true);
        gameObject.SetActive(false);
    }
}

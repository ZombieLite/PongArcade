using UnityEngine;
using UnityEngine.UI;

public class SM_Support : MonoBehaviour
{
    [SerializeField] GameObject version;
    void Start()
    {
        version.GetComponent<Text>().text = "Version: " + Application.version;
    }

    public void OpenVKontakte()
    {
        Application.OpenURL("http://vk.com/zombielite");
    }

}

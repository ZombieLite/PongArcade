using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MissionManager : MonoBehaviour
{
    [SerializeField] string nextLevel;
    private string endName;
    
    Text txt;
    Animator animator;

    private void Start()
    {
        txt = GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    public void LevelCompleted()
    {
        txt.color = new Color32(210, 255, 215, 255);
        endName = "Mission Completed";
        StartCoroutine(MissionCompletedTextShow());
    }

    public void LevelFailed()
    {
        txt.color = new Color32(255, 0, 0, 255);
        txt.text = "";
        animator.Play("MissionManager");
        txt.text = "Mission Failed";
        Invoke("MissionFailedPanel", 2.0f);
    }

    public void MissionFailedPanel()
    {
        txt.text = "";
        transform.GetChild(1).gameObject.SetActive(true);
    }

    IEnumerator MissionCompletedTextShow()
    {
        yield return new WaitForSeconds(1.0f);
        int _maxchars;
        _maxchars = endName.Length;

        string _bufString = "";

        for(int i = 0; i < _maxchars; i++)
        {
            _bufString = "" + _bufString + endName[i];
            txt.text = _bufString;
            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(2.0f);
        txt.text = "";

        if (nextLevel.Length <= 1)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        } 
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}

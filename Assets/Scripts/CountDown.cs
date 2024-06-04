using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class CountDown : MonoBehaviour
{
    public static UnityEvent StartRound = new UnityEvent();

    private void Start()
    {
        TimerStart();
    }

    public void TimerStart()
    {
        gameObject.SetActive(true);

        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        int txt = 3;
        while (true)
        {
            if(txt == 0)
            {
                txt = 3;
                StartRound?.Invoke();
                gameObject.SetActive(false);
                yield break;
            }

            gameObject.GetComponent<Text>().text = "" + txt;
            yield return new WaitForSeconds(1.0f);

            txt--;
        }
    }
}

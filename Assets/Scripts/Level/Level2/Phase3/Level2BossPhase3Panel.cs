using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level2BossPhase3Panel : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject mission;

    private int _health;
    private bool _protected;

    Image _borderColour;
    Image _shieldColour;

    private void Start()
    {
        _borderColour = GetComponent<Image>();
        _shieldColour = transform.GetChild(0).GetComponent<Image>();
        Level2BossPhase3StartEvent();
    }

    public void Level2BossPhase3SetHealth(int hp)
    {
        _health = hp;
    }

    public void Level2BossPhase3StartEvent()
    {
        
        switch(_health)
        {
            case 0:
                _colorPanel(new Color32(255, 0, 0, 0));
                break;
            case 1:
                _colorPanel(new Color32(255, 255, 55, 0));
                break;
            case 2:
                GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                break;
        }
        
        StartCoroutine(Level2BossPhase3StartEvent_Coroutine());
    }

    private void _colorPanel(Color32 color)
    {
        transform.GetChild(0).gameObject.GetComponent<Image>().color = color;
        GetComponent<Image>().color = color;
    }

    IEnumerator Level2BossPhase3StartEvent_Coroutine()
    {
        while (true)
        {
            Color alphaBorder = GetComponent<Image>().color;
            Color alpha = transform.GetChild(0).gameObject.GetComponent<Image>().color;

            if (alphaBorder.a < 1)
            {
                alphaBorder.a += 0.07f;

                if (alphaBorder.a > 1)
                {
                    alphaBorder.a = 1;
                    transform.parent.GetComponent<Level2BossPhase3>().Level2BossPhase3ThinkStart();
                    StopCoroutine(Level2BossPhase3StartEvent_Coroutine());
                    break;
                }

                GetComponent<Image>().color = alphaBorder;
            }

            switch (_health)
            {
                case 0:
                    if (alpha.a >= 0.39f)
                    {
                        continue;
                    }
                    break;
                case 1:
                    if (alpha.a >= 0.23f)
                    {
                        continue;
                    }
                    break;
                case 2:
                    if (alpha.a >= 0f)
                    {
                        continue;
                    }
                    break;
            }
            alpha.a += 0.03f;
            transform.GetChild(0).gameObject.GetComponent<Image>().color = alpha;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Level2BossPhase3PanelDamage()
    {
        if (!_protected)
        {
            if (_health > 0)
                StartCoroutine(_Level2BossPhase3PanelDamage());
            else
            {
                transform.parent.GetComponent<Level2BossPhase3>().Level2BossPhase4Start();
                Destroy(gameObject);
            }
        }
    }

    IEnumerator _Level2BossPhase3PanelDamage()
    {
        _protected = true;
        int i = 30;
        bool change = false;
        while (i > 0)
        {
            yield return new WaitForSeconds(0.08f);

            switch (_health)
            {
                case 1:
                    if (!change)
                    {
                        _borderColour.color = new Color32(255, 255, 0, 255);
                        _shieldColour.color = new Color32(255, 255, 55, 100);
                    }
                    else
                    {
                        _borderColour.color = new Color32(255, 0, 0, 255);
                        _shieldColour.color = new Color32(255, 0, 0, 100);
                    }
                    break;
                case 2:
                    if (!change)
                    {
                        _borderColour.color = new Color32(255, 255, 255, 255);
                        _shieldColour.color = new Color32(255, 255, 55, 60);
                    }
                    else
                    {
                        _borderColour.color = new Color32(255, 255, 0, 255);
                        _shieldColour.color = new Color32(255, 255, 55, 100);
                    }
                    break;
            }
            change = !change;
            i--;
        }
        _health--;
        _protected = false;
        StopCoroutine(_Level2BossPhase3PanelDamage());
    }

    public void Level2BossPhaseFinalMakeDeathEnemy()
    {
        GameObject ent = Instantiate(explosion);
        ent.transform.position = transform.position;
        mission.GetComponent<MissionManager>().LevelCompleted();

        RoundEnd.InvokeRoundEnd();
    }
}

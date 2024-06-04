using UnityEngine;
using System.Collections;

public class Level2BossPhase3 : MonoBehaviour
{
    private bool prePhase = true;
    private bool isRoundEnd = false;
    int _ability = 0;

    GameObject bossFinal;
    GameObject weapon1, weapon2;

    ArrayList BossList = new ArrayList();
    [SerializeField] GameObject bulletRed;
    [SerializeField] GameObject bulletBlue;
    [SerializeField] GameObject BulletYellow;
    [SerializeField] GameObject posLeftCentr;

    private void Start()
    {
        RoundEnd.EventRoundEnd.AddListener(StartRoundEnd);
    }

    public void Level2BossStartPhase3(GameObject boss)
    {
        GameObject _panel;
        int _num = (boss.transform.childCount - 1);
        while(_num > 0)
        {
            _num--;
            _panel = boss.transform.GetChild(_num).gameObject;

            if (_panel != null && _panel.activeSelf && BossList.Count < 2)
            {
                BossList.Add(_panel);
            }
        }

        foreach(GameObject _panelLast in BossList)
        {
            transform.GetChild(BossList.IndexOf(_panelLast)).gameObject.GetComponent<Level2BossPhase3Panel>().Level2BossPhase3SetHealth(_panelLast.GetComponent<Level2BossPanel>().Level2BossGetPanelHealth());
        }

        Destroy(boss);

        switch(BossList.Count)
        {
            case 1:
                transform.GetChild(0).gameObject.transform.localPosition = posLeftCentr.transform.localPosition;
                transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 2:
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
                break;
        }
    }

    public void Level2BossPhase3ThinkStart()
    {
        if(prePhase)
            StartCoroutine(Level2BossPhase3ThinkCoroutine());

        prePhase = false;
    }

    IEnumerator Level2BossPhase3ThinkCoroutine()
    {
        int _abilityBuffer = 0, _state = 0, _numeration = 0;
        GameObject b_first = transform.GetChild(0).gameObject;
        GameObject b_second = transform.GetChild(1).gameObject;
        while (true)
        {
            if(isRoundEnd)
            {
                yield return new WaitForSeconds(1.0f);
                continue;
            }

            if(_ability != _abilityBuffer)
            {
                _numeration = 0;
                _state = 0;
                _abilityBuffer = _ability;
            }

            switch (_ability)
            {
                case 0:
                    if (b_second.activeSelf == true)
                        _ability = 1;
                    else
                        _ability = 3;
                    break;
                case 1:
                    int rnd = Random.Range(0, 3);
                    GameObject _bullet;
                    if (rnd == 0)
                    {
                        _bullet = Instantiate(bulletBlue);
                    } else
                    {
                        _bullet = Instantiate(bulletRed);
                    }
                    Vector2 pos = Vector2.zero;
                    switch (_state)
                    {
                        case 0:
                            _state++;

                            pos = b_first.transform.position + Vector3.right;
                            _bullet.transform.position = pos;

                            break;
                        case 1:
                            _state--;
                            
                            pos = b_second.transform.position + Vector3.right;
                            _bullet.transform.position = pos;

                            break;
                    }
                    Vector2 vecAttack;
                    vecAttack = (pos + Vector2.right) - pos;
                    _bullet.GetComponent<Rigidbody2D>().AddForce(vecAttack.normalized * 15.0f, ForceMode2D.Impulse);
                    
                    _numeration++;
                    if(_numeration >= 10)
                    {
                        Level2BossPhase3Bomb bomb = FindObjectOfType<Level2BossPhase3Bomb>();
                        if(bomb == null)
                        {
                            _numeration--;
                            yield return new WaitForSeconds(0.3f);
                            break;
                        }
                        _state = 0;
                        _numeration = 0;
                        _ability = 2;   
                    }               
                    yield return new WaitForSeconds(0.3f);
                    break;
                case 2:
                    switch (_state)
                    {
                        case 0:
                            _bullet = Instantiate(BulletYellow);

                            pos = b_first.transform.position + Vector3.right;
                            
                            _bullet.transform.position = pos;
                            vecAttack = (pos + Vector2.right) - pos;
                            
                            _bullet.GetComponent<Rigidbody2D>().AddForce(vecAttack.normalized * 5.0f, ForceMode2D.Impulse);

                            _state++;
                            yield return new WaitForSeconds(0.5f);
                            break;
                        case 1:
                            _bullet = Instantiate(BulletYellow);

                            pos = b_second.transform.position + Vector3.right;
                            
                            _bullet.transform.position = pos;
                            vecAttack = (pos + Vector2.right) - pos;
                            
                            _bullet.GetComponent<Rigidbody2D>().AddForce(vecAttack.normalized * 5.0f, ForceMode2D.Impulse);

                            _state++;
                            yield return new WaitForSeconds(2.0f);
                            break;
                        case 2:
                            _Level2BossPhase3StopYellow();

                            _state++;
                            yield return new WaitForSeconds(1.0f);
                            break;
                        case 3:
                            _Level2BossPhase3MagnetActivate(true);
                            yield return new WaitForSeconds(2.0f);
                            _state++;
                            break;
                        case 4:
                            _Level2BossPhase3MagnetActivate(false);
                            _state = 0;
                            _ability = 1;
                            yield return new WaitForSeconds(1.0f);
                            break;
                    }
                    break;
                case 3: // WAITING
                    Level2BossPhase3Bomb[] b = FindObjectsOfType<Level2BossPhase3Bomb>();
                    foreach (Level2BossPhase3Bomb _bomb in b)
                    {
                        Destroy(_bomb.gameObject);
                    }
                    _Level2BossPhase3StopYellow();
                    _ability++;
                 
                    yield return new WaitForSeconds(2.0f);
                    break;
                case 4:
                    /*
                    for(int i = 0; i<2; i++)
                    {
                        if (transform.GetChild(i).gameObject == null || !transform.GetChild(i).gameObject.activeSelf)
                            continue;

                        bossFinal = transform.GetChild(i).gameObject;
                    }
                    */
                    bossFinal = transform.GetChild(0).gameObject;

                    GameObject lopast = GameObject.Find("LopastFinalPhase");
                    weapon1 = lopast.transform.GetChild(1).gameObject;
                    weapon2 = lopast.transform.GetChild(0).gameObject;
                    weapon1.SetActive(true);
                    weapon2.SetActive(true);
                    
                    StartCoroutine(Level2BossPhaseFinalMoveEnemy());
                    _ability++;
                    yield return new WaitForSeconds(1.0f);
                    break;
                case 5:
                    bossFinal.GetComponent<Level2BossPhaseFinalAttack>().Level2BossPhaseFinalStart();
                    bossFinal.GetComponent<Level2BossPhaseFinalHealthBar>().Level2BossPhaseFinalHealthBarActive();
                    yield break;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void Level2BossPhase4Start()
    {
        _ability = 3;
    }

    private void _Level2BossPhase3MagnetActivate(bool active)
    {
        Level2BossPhase3Bomb[] bomb = FindObjectsOfType<Level2BossPhase3Bomb>();
        foreach(Level2BossPhase3Bomb _bomb in bomb)
        {
            _bomb.transform.GetChild(0).gameObject.SetActive(active);
        }
    }

    private void _Level2BossPhase3StopYellow()
    {
        Level2BossPhase3YellowBullet[] yellowBullet = FindObjectsOfType<Level2BossPhase3YellowBullet>();
        foreach (Level2BossPhase3YellowBullet _yellowBullet in yellowBullet)
        {
            _yellowBullet.Level2BossPhase3StopEventYellow();
        }
    }

    private void StartRoundEnd()
    {
        isRoundEnd = true;
    }

    IEnumerator Level2BossPhaseFinalMoveEnemy()
    {
        int _change = 0;
        Vector2 _velocity = Vector2.zero;
        Rigidbody2D rb = bossFinal.GetComponent<Rigidbody2D>();

        while(true)
        {
            if(bossFinal == null)
            {
                yield break;
            }

            switch (_change)
            {
                case 0:
                    _velocity = (Vector2)bossFinal.transform.position + Vector2.down - (Vector2)bossFinal.transform.position;
                    rb.AddForce(_velocity * 240.0f, ForceMode2D.Force);
                    _change++;
                    break;
                case 1:
                    _velocity = (Vector2)bossFinal.transform.position + Vector2.up - (Vector2)bossFinal.transform.position;
                    rb.AddForce(_velocity * 240.0f, ForceMode2D.Force);
                    _change--;
                    break;

            }
            yield return new WaitForSeconds(2.0f);
        }
    }
}

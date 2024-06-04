using UnityEngine;
using System.Collections;

public class Level2BossPhase2 : MonoBehaviour
{
    [SerializeField] Sprite NewModels;
    [SerializeField] Sprite NewBorders;
    [SerializeField] GameObject Boss2Warp;

    private GameObject phase2;
    private GameObject[] obj = new GameObject[4];
    private GameObject[] pos = new GameObject[3];
    private Vector2[] _vec = new Vector2[3];

    Rigidbody2D rb;

    public void Awake()
    {
        obj[0] = transform.GetChild(0).gameObject;
        obj[1] = transform.GetChild(1).gameObject;
        obj[2] = transform.GetChild(2).gameObject;
        obj[3] = transform.GetChild(3).gameObject;
    }

    private void Start()
    {
        Level2BossPanel.PanelDestroy.AddListener(Level2BossPahse2Change);

        phase2 = GameObject.Find("Phase2");
        rb = GetComponent<Rigidbody2D>();

        pos[0] = phase2.transform.GetChild(0).gameObject;
        pos[1] = phase2.transform.GetChild(1).gameObject;
        pos[2] = phase2.transform.GetChild(2).gameObject;

        _vec[0] = phase2.transform.GetChild(0).gameObject.transform.position;
        _vec[1] = phase2.transform.GetChild(1).gameObject.transform.position;
        _vec[2] = phase2.transform.GetChild(2).gameObject.transform.position;

    }

    public Vector2 Level2BossPhase2PositionCenter()
    {
        return phase2.transform.position;
    }

    public void Level2BossPhase2StartAbility()
    {
        StartCoroutine(Level2BossFade());
    }

    
    public void Level2BossPahse2Change()
    {
        //if(GetComponent<Level2Boss>().Level2BossGetAbility() == 2)
        //    StopCoroutine(Level2BossFade());
    }

    IEnumerator Level2BossFade()
    {
        ArrayList ListObj = new ArrayList();
        int _state = 0;
        int _numeration = 0;
        yield return new WaitForSeconds(0.5f); 
        while(true)
        {
            switch(_state)
            {
                #region FADE
                case 0:
                    foreach (GameObject o in obj)
                    {
                        if (o == null)
                            continue;

                        Color alpha = o.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
                        Color alphaBorder = o.transform.GetComponent<SpriteRenderer>().color;


                        if (alphaBorder.a >= 0)
                        {
                            alphaBorder.a -= 0.09f;
                            o.transform.GetComponent<SpriteRenderer>().color = alphaBorder;
                            o.transform.root.GetComponent<SpriteRenderer>().color = alphaBorder;
                        }
                        
                        if(alpha.a >= 0)
                        {
                            alpha.a -= 0.09f;
                            o.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = alpha;
                        }
                    }

                    if (_numeration == 20)
                    {
                        transform.root.GetComponent<SpriteRenderer>().enabled = false;
                        _numeration = 0;
                        _state++;
                        yield return new WaitForSeconds(0.1f);
                    }
                    _numeration++;
                    yield return new WaitForSeconds(0.1f);
                    break;
                case 1:                  
                    rb.rotation = 0;
                    rb.isKinematic = true;
                    rb.GetComponent<CircleCollider2D>().isTrigger = true;
                    transform.Rotate(0, 0, 0);

                    for (int i = 0; i < 3; i++)
                    {
                        pos[i].transform.position = _vec[i];
                        pos[i].transform.rotation = new Quaternion(0, 0, 0, 0);
                    }

                    if (GetComponent<Level2Boss>().Level2BossGetAbility() >= 3)
                    {
                        yield return null;
                    }

                    System.Random rnd = new System.Random();
                    for (int i = 2; i >= 0; i--)
                    {
                        int j = rnd.Next(i + 1);
                        GameObject temp = pos[j];
                        pos[j] = pos[i];
                        pos[i] = temp;
                    }

                    transform.rotation = new Quaternion(0, 0, 0, 0);
                    foreach (GameObject o in obj)
                    {
                        if (o == null || !o.activeSelf)
                            continue;

                        o.GetComponent<SpriteRenderer>().sprite = NewBorders;
                        o.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = NewModels;
                        o.transform.rotation = new Quaternion(0, 0, 0, 0);
                        //o.transform.rotation = new Quaternion(0, 0, -90f, 0);
                        o.transform.GetChild(0).transform.localRotation = Quaternion.identity;
                        o.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                        o.GetComponent<EdgeCollider2D>().isTrigger = true;

                        ListObj.Add(o);
                    }

                    foreach (GameObject o in ListObj)
                    {
                        o.transform.localPosition = (pos[ListObj.IndexOf(o)].transform.position - transform.position);
                    }
                    _state++;

                    GameObject sound = Instantiate(Boss2Warp);
                    AudioSource _sound = sound.GetComponent<AudioSource>();
                    _sound.Play();
                    Destroy(sound, 2.0f);

                    yield return new WaitForSeconds(0.1f);
                    break;
                case 2:
                    if (_numeration == 20)
                    {
                        _numeration = 0;
                        _state++;
                        yield return new WaitForSeconds(0.1f);
                    }
                    _numeration++;

                    foreach (GameObject o in ListObj)
                    {
                        Color alpha = o.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
                        Color alphaBorder = o.transform.GetComponent<SpriteRenderer>().color;

                        if(alphaBorder.a < 1) {
                            alphaBorder.a += 0.07f;

                            if (alphaBorder.a > 1)
                                alphaBorder.a = 1;

                                o.transform.GetComponent<SpriteRenderer>().color = alphaBorder;
                        }

                        switch (o.GetComponent<Level2BossPanel>().Level2BossGetPanelHealth())
                        {
                            case 0:
                                if (alpha.a >= 0.39f)
                                {
                                    //yield return new WaitForSeconds(0.1f);
                                    continue;
                                }
                                break;
                            case 1:
                                if (alpha.a >= 0.23f)
                                {
                                    //yield return new WaitForSeconds(0.1f);
                                    continue;
                                }
                                break;
                            case 2:
                                if (alpha.a >= 0.39f)
                                {
                                    //yield return new WaitForSeconds(0.1f);
                                    continue;
                                }
                                break;
                        }
                        alpha.a += 0.03f;
                        o.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = alpha;

                    }
                    yield return new WaitForSeconds(0.04f);
                    break;
                #endregion
                case 3:
                    pos[0].GetComponent<Animator>().Play("PanelMovieng");
                    pos[1].GetComponent<Animator>().Play("PanelMovieng2");
                    pos[2].GetComponent<Animator>().Play("PanelMovieng3");
                    _state++;
                    yield return new WaitForSeconds(0.1f);
                    break;
                case 4:
                    float len = 0.0f, lenBuff = 90;
                    GameObject target = null;

                    for (int i = 0; i < 3; i++)
                    {
                        foreach (GameObject o in ListObj)
                        {
                            len = Vector2.Distance(o.transform.position, phase2.transform.GetChild(i).transform.position);
                            if (len < lenBuff)
                            {
                                lenBuff = len;
                                target = o;
                            }
                        }
                        target.GetComponent<Level2BossPanel>().Level2BossMovePanel(phase2.transform.GetChild(i).gameObject);
                        lenBuff = 90;
                    }
                    _state++;
                    yield return new WaitForSeconds(6.0f);
                    break;
                case 5:
                    foreach (GameObject o in ListObj)
                    {
                        if (o == null)
                            continue;

                        o.AddComponent<Rigidbody2D>();
                        o.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                        o.GetComponent<EdgeCollider2D>().isTrigger = true;
                    }
                    _state++;
                    yield return new WaitForSeconds(0.1f);
                    break;
                case 6:
                    Vector2 vector = Vector2.zero;
                    GameObject wallLeft = GameObject.Find("RIGHT");
                    GameObject wallRight = GameObject.Find("LEFT");
                    foreach (GameObject o in ListObj)
                    {
                        if (o == null)
                            continue;

                        vector = wallRight.transform.position - wallLeft.transform.position;
                        o.GetComponent<Rigidbody2D>().AddForce(vector.normalized * 2.0f, ForceMode2D.Impulse);

                    }
                    _state++;
                    yield return new WaitForSeconds(3.0f);
                    break;
                case 7:
                    foreach (GameObject o in ListObj)
                    {
                        if (o == null)
                            continue;

                        o.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        o.GetComponent<EdgeCollider2D>().isTrigger = true;
                    }
                    ListObj.Clear();
                    _state = 0;
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
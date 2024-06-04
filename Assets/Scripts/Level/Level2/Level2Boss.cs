using UnityEngine;
using System.Collections;

public class Level2Boss : MonoBehaviour
{
    [SerializeField] GameObject redBall;
    [SerializeField] GameObject blueBall;
    [SerializeField] GameObject yellowBall;
    private GameObject _center, _position, _player;
    private RandomCore rnd;
    private Rigidbody2D rb;
    private Level2BossPhase2 phase2;
    private int speedRedTouch, ability, state, numeration = 0;
    private void Start()
    {
        Level2BossPanel.PanelDestroy.AddListener(Level2BossPanelDestroy);
        rnd = GetComponent<RandomCore>();
        rb = GetComponent<Rigidbody2D>();
        phase2 = GetComponent<Level2BossPhase2>();

        _position = GameObject.Find("Position");
        _center = GameObject.Find("NULL");
        _player = GameObject.Find("Player");

        ability = 1;
        Level2BossChangePosition();
        StartCoroutine(Level2BossThink());
    }

    IEnumerator Level2BossThink()
    {
        while (true) {
            switch (ability)
            {
                #region PHASE1
                case 1: // RED BALL
                    float _len;
                    switch (state)
                    {
                        #region FIRST_RED
                        case 0:
                            rb.isKinematic = false;
                            transform.position = Vector2.MoveTowards(transform.position, _center.transform.position, 0.05f);
                            _len = Vector2.Distance(transform.position, _center.transform.position);
                            if (_len <= 0)
                            {
                                rb.velocity = Vector2.zero;
                                GetComponent<Level2BossRotate>().Level2BossChangeRotation(1.3f);
                                state = 1;
                                speedRedTouch = 0;
                                rb.isKinematic = true;
                                yield return new WaitForSeconds(3.0f);
                            }
                            break;
                        case 1:
                            switch(speedRedTouch)
                            {
                                case 0:
                                    GameObject _b;
                                    Vector2 _attack = Vector2.zero;
                                    GameObject _position = Level2BossBallAttackPosition();
                                    //_position = 

                                    for(int i = 0; i < 10; i++) {
                                        if (numeration == 3 || numeration == 6 || numeration == 9)
                                        {
                                            _b = Instantiate(blueBall);
                                        } else
                                            _b = Instantiate(redBall);



                                        _b.transform.position = _position.transform.position;
                                        _attack = _b.transform.position - transform.position;

                                        _b.GetComponent<Rigidbody2D>().AddForce(_attack * 11.0f, ForceMode2D.Impulse);
                                        yield return new WaitForSeconds(0.06f);
                                    }
                                    numeration++;
                                    yield return new WaitForSeconds(0.2f);

                                    if (numeration >= 10)
                                    {
                                        state = 1;
                                        numeration = 0;
                                        speedRedTouch = 1;
                                        rb.velocity = Vector2.zero;
                                        GetComponent<Level2BossRotate>().Level2BossChangeRotation(50.0f);
                                        yield return new WaitForSeconds(4.0f);
                                    }
                                    break;
                                case 1:
                                    rb.isKinematic = false;
                                    Vector2 _vec;
                                    _vec = _player.transform.position - transform.position;
                                    GetComponent<Rigidbody2D>().AddForce(_vec.normalized * 10.0f, ForceMode2D.Impulse);
                                    GetComponent<AudioSource>().Play();

                                    speedRedTouch = 0;
                                    state = 2;
                                    GetComponent<Level2BossRotate>().Level2BossChangeRotation(1.8f);
                                    yield return new WaitForSeconds(2.0f);
                                    break;
                            }
                            break;
                        #endregion
                        case 2:
                            rb.isKinematic = true;
                            transform.position = Vector2.MoveTowards(transform.position, _center.transform.position, 0.05f);
                            _len = Vector2.Distance(transform.position, _center.transform.position);
                            if (_len <= 0)
                            {
                                rb.velocity = Vector2.zero;
                                state = 3;
                                speedRedTouch = 0;
                                yield return new WaitForSeconds(3.0f);
                            }
                            break;
                        case 3:
                            GameObject ball = null;
                            Vector2 _vector = Vector2.zero;
                                
                            for(int s = 0; s < 98; s++)
                            {
                                BossBallManager(ball, _vector, 0, s);
                                BossBallManager(ball, _vector, 1, s);
                                BossBallManager(ball, _vector, 2, s);
                                BossBallManager(ball, _vector, 3, s);

                                yield return new WaitForSeconds(0.04f);
                                
                            }
                            state = 0;
                            GetComponent<Level2BossRotate>().Level2BossChangeRotation(1.3f);
                            yield return new WaitForSeconds(1.0f);
                            break;
                    }
                    break;
                #endregion
                case 2: // Phase 2
                    switch(state)
                    {
                        case 0:
                            GetComponent<Level2BossRotate>().Level2BossChangeRotation(0.0f);
                            state++;
                            yield return new WaitForSeconds(3.0f);
                            break;
                        case 1:
                            rb.isKinematic = false;
                            transform.position = Vector2.MoveTowards(transform.position, phase2.Level2BossPhase2PositionCenter(), 0.05f);
                            _len = Vector2.Distance(transform.position, phase2.Level2BossPhase2PositionCenter());
                            if (_len <= 0)
                            {
                                GetComponent<Level2BossRotate>().Level2BossStopRotation(0f);
                                phase2.Level2BossPhase2StartAbility();
                                rb.velocity = Vector2.zero;
                                rb.isKinematic = true;
                                state = 2;
                                yield return new WaitForSeconds(1.0f);
                            }
                            break;
                        case 2:
                            yield return new WaitForSeconds(0.1f);
                            break;
                    }
                    break;
                case 3:
                    ability++;
                    yield return new WaitForSeconds(6.0f);
                    break;
                case 4:
                    GameObject phase3 = GameObject.Find("Enemy");
                    phase3.GetComponent<Level2BossPhase3>().Level2BossStartPhase3(gameObject);
                    yield return null;
                    break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void Level2BossPanelDestroy()
    {
        if (ability > 3)
            return;
        
        ability++;
        state = 0;

        GameObject[] ball;
        ball = GameObject.FindGameObjectsWithTag("Ball");
        foreach(GameObject b in ball)
        {
            if (b == null)
                continue;

            Destroy(b);
        }
    }

    private void Level2BossChangePosition()  
    {
        //Vector2 _rndPosition = rnd.RandomPosition();
        Vector2 _rndPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-4f, 4f));
        rb.AddForce(_rndPosition, ForceMode2D.Impulse);
    }

    private GameObject Level2BossBallAttackPosition()
    {
        return _position.transform.GetChild(Random.Range(0, 4)).gameObject;
    }

    public int Level2BossGetAbility()
    {
        return ability;
    }

    bool _yellow = true; 
    int _color;
    private void BossBallManager(GameObject ball, Vector2 _vector, int chield, int i)
    {
        if (i > 8 && i < 18 || i > 26 && i < 36 || i > 44 && i < 54 || i > 62 && i < 72 || i > 80 && i < 90)
        {
            if(_yellow)
            {
                _color = Random.Range(0, 2);
                _yellow = false;
            }

            if (_color == 0)
                ball = Instantiate(blueBall);
            else
                ball = Instantiate(yellowBall);
        }
        else
        {
            _yellow = true;
            ball = Instantiate(redBall);
        }

        ball.transform.position = _position.transform.GetChild(chield).gameObject.transform.position;
        _vector = ball.transform.position - transform.position;
        ball.GetComponent<Rigidbody2D>().AddForce(_vector * 11.0f, ForceMode2D.Impulse);
    }
}

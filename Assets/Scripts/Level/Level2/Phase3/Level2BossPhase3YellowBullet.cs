using UnityEngine;
using System.Collections;

public class Level2BossPhase3YellowBullet : MonoBehaviour
{
    [SerializeField] GameObject yellowCluster;
    public bool blueCluster = false;

    private bool isStopped = false;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        AudioSource _audio = GetComponent<AudioSource>();
        _audio.pitch = Random.Range(0.9f, 1.1f);
        _audio.Play();

        if(blueCluster)
        {
            GetComponent<Animator>().Play("Attack_Blue");
        } else
        {
            GetComponent<Animator>().Play("Attack_Yellow");
        }
        StartCoroutine(_Level2BossPhase3YellowCluster());
    }

    public void Level2BossPhase3StopEventYellow()
    {
        isStopped = true;
        StopCoroutine(_Level2BossPhase3YellowCluster());
    }

    IEnumerator _Level2BossPhase3YellowCluster()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        if (isStopped)
        {
            yield break;
        }
        float _rotation;
        _rotation = rb.rotation;

        switch(Mathf.Round(_rotation))
        {
            case -90:
                _Level2BossPhase3ClusterCreate(0f, 90f, 180f);
                break;
            case 0:
                _Level2BossPhase3ClusterCreate(-90f, 90f, 180f);
                break;
            case 90:
                _Level2BossPhase3ClusterCreate(180f, -90f, 0f);
                break;
            case 180:
                _Level2BossPhase3ClusterCreate(-90f, 0f, 90f);
                break;
        }
    }

    private void _Level2BossPhase3ClusterCreate(float first, float two, float three)
    {
        GameObject _cluster;
        Vector3 _anglePos = Vector3.zero;
        for (int i = 0; i < 3; i++)
        {
            _cluster = Instantiate(yellowCluster);

            if(Random.Range(2, 4) == 3)
                _cluster.GetComponent<Level2BossPhase3YellowBullet>().blueCluster = true;
            else
                _cluster.GetComponent<Level2BossPhase3YellowBullet>().blueCluster = false;

            switch (i)
            {
                case 0:
                    _cluster.GetComponent<Rigidbody2D>().rotation = first;
                    _anglePos = _AngleToVector(first);
                    break;
                case 1:
                    _cluster.GetComponent<Rigidbody2D>().rotation = two;
                    _anglePos = _AngleToVector(two);
                    break;
                case 2:
                    _cluster.GetComponent<Rigidbody2D>().rotation = three;
                    _anglePos = _AngleToVector(three);
                    break;
            }

            Vector2 _velocity;
            _cluster.transform.position = transform.position + _anglePos;
            _velocity = _cluster.transform.position - transform.position;
            _cluster.GetComponent<Rigidbody2D>().AddForce(_velocity.normalized * 5.0f, ForceMode2D.Impulse);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if(collision.tag == "Player")
        {
            if (blueCluster)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.01f, 0.001f);
                Level2BossPhase3StopEventYellow();
            }
            else
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }

        if(collision.tag == "Bomb")
        {
            if (!collision.transform.GetChild(0).gameObject.activeSelf)
                return;

            collision.gameObject.GetComponent<Level2BossPhase3Bomb>().Level2BossPhase3SetEnergy(collision.gameObject.GetComponent<Level2BossPhase3Bomb>().Level2BossPhase3GetEnergy() + 5.0f);
            Destroy(gameObject);
        }
    }

    private Vector2 _AngleToVector(float delta)
    {
        switch (Mathf.Round(delta))
        {
            case -90: return Vector2.up;
            case 0: return Vector2.left;
            case 90: return Vector2.down;
            case 180: return Vector2.right;
        }
        return Vector2.zero;
    }
}

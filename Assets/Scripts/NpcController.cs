using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField]GameObject explosion;
    GameObject focusBall;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        focusBall = FindBall();

        if (focusBall == null)
        {
            return;
        }

        Vector2 velocity = Vector2.zero;
        Vector2 ballPosition = focusBall.transform.position;
        Vector2 NpcPosition = gameObject.transform.position;
        float Len = ballPosition[1] - NpcPosition[1];


        if (-0.2f < Len && Len < 0.2f)
        {
            velocity = Vector2.zero;
        }
        else
            velocity.y = Len;

        velocity = velocity.normalized;
        velocity.x = 0.0f;

        rb.velocity = velocity * 600.0f * Time.fixedDeltaTime;
    }

    private GameObject FindBall()
    {
        GameObject _focus = null;
        Vector2 npcVector;
        Vector2 ballVector;
        float Len;
        float BuffLen = 0;

        npcVector = transform.position;

        GameObject[] _ball = GameObject.FindGameObjectsWithTag("Ball");

        foreach(GameObject ent in _ball)
        {
            ballVector = ent.transform.position;

            Len = Vector2.Distance(npcVector, ballVector);

            if(Len > BuffLen)
            {
                BuffLen = Len;
                _focus = ent;
            }
        }

        return _focus;
    }
    public void Level1BossStartExplosionNpc()
    {
        GameObject expl = Instantiate(explosion);
        expl.transform.position = transform.position;
    }
}

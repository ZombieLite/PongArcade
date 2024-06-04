using UnityEngine;
using System.Collections;

public class Level2BossRotate : MonoBehaviour
{
    int active;
    Rigidbody2D rb;
    [SerializeField] const float speed = 0.5f;
    private float _speed;


    private void Start()
    {
        _speed = speed;
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (active == 0)
            return;

        if(_speed != 0.0f)
            rb.rotation += _speed + Time.fixedDeltaTime;
    }

    public void Level2BossStopRotation(float rotateSpeed)
    {
        active = 0;
        StopCoroutine(SpeedRising(rotateSpeed));
        StopCoroutine(SpeedDecrease(rotateSpeed));
        _speed = 0.0f;
        rb.velocity = Vector2.zero;
    }

    public void Level2BossChangeRotation(float rotateSpeed = speed)
    {
        if (rotateSpeed < _speed)
        {
            active = 1;
            StartCoroutine(SpeedDecrease(rotateSpeed));
        }
        else if (rotateSpeed > speed)
        {
            active = 2;
            StartCoroutine(SpeedRising(rotateSpeed));
        }
    }

    IEnumerator SpeedDecrease(float rotateSpeed)
    {
        StopCoroutine(SpeedRising(rotateSpeed));
        while (true)
        {
            if (active != 1)
                yield break;

            yield return new WaitForSeconds(0.3f);
            if (rotateSpeed < _speed)
            {
                _speed--;
            }
            else
                yield break;
        }
    }

    IEnumerator SpeedRising(float rotateSpeed)
    {
        StopCoroutine(SpeedDecrease(rotateSpeed));
        while (true)
        {
            if (active != 2)
                yield break;

            yield return new WaitForSeconds(0.3f);
            if (_speed < rotateSpeed)
            {
                _speed += 1.0f;
            }
            else
                yield break;
        }
    }
}

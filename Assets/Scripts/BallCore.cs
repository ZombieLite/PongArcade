using UnityEngine;
using UnityEngine.Events;

public class BallCore : MonoBehaviour
{
    [SerializeField] GameObject BallSplash;

    public static UnityEvent HitLeft = new UnityEvent();
    public static UnityEvent HitRight = new UnityEvent();

    Vector2 ballSpeed;
    float addForce;
    private void Start()
    {   
        BallStart();
    }

    private void FixedUpdate()
    {
        ballSpeed = gameObject.GetComponent<Rigidbody2D>().velocity;
    }

    public void BallStart()
    {
        Vector2 _ballVector = Vector2.zero;
        _ballVector[0] = Random.Range(-1.0f, 1.0f);
        _ballVector[1] = Random.Range(-1.0f, 1.0f);
        _ballVector.Normalize();
        gameObject.GetComponent<Rigidbody2D>().velocity = _ballVector * 10;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("LeftWall"))
        {
            HitLeft?.Invoke();
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("RightWall"))
        {
            HitRight?.Invoke();
            Destroy(gameObject);
            return;
        }

        Rigidbody2D _rb = gameObject.GetComponent<Rigidbody2D>();

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if(ballSpeed.magnitude <= 39)
            {
                ballSpeed.x *= 1.02f;
                ballSpeed.y *= 1.02f;
            }
            GetComponent<AudioSource>().Play();
        }
        
        Vector2 _newVelocity = Vector2.zero;
        _newVelocity = Vector2.Reflect(ballSpeed, collision.contacts[0].normal);
        _rb.velocity = _newVelocity;

        if (collision.collider.tag == "Player" || collision.collider.tag == "Boss")
        {
            GameObject sound = Instantiate(BallSplash);
            Destroy(sound, 1.0f);
        }
    }

    public float GetBallSpeed()
    {
        return ballSpeed.magnitude;
    }
}

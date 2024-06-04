using UnityEngine;

public class Level2BossBlueBall : MonoBehaviour
{
    [SerializeField] GameObject destroySound;
    [SerializeField] GameObject touchSound;
    private void Start()
    {
        AudioSource _sound = GetComponent<AudioSource>();
        _sound.pitch = Random.Range(1f, 1.1f);
        _sound.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {         
        if (collision.collider.tag == "Wall")
            Destroy(gameObject);

        if (collision.collider.tag == "Boss")
        {
            GameObject _sound = Instantiate(destroySound);
            Destroy(_sound, 1.0f);
        }

        if (collision.collider.tag == "Player")
        {
            GameObject _sound = Instantiate(touchSound);
            Destroy(_sound, 1.0f);

            Vector2 _vecBall, _vecPlayer, _vecReflect;
            _vecBall = gameObject.transform.position;
            _vecPlayer = collision.collider.transform.position;
            _vecReflect = Vector2.Reflect(_vecBall, _vecPlayer).normalized;
            gameObject.GetComponent<Rigidbody2D>().velocity = _vecReflect * 4.0f;
        }
    }
}

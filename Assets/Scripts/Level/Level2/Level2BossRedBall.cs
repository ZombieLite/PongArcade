using UnityEngine;

public class Level2BossRedBall : MonoBehaviour
{
    private void Start()
    {
        AudioSource _sound = GetComponent<AudioSource>();
        _sound.pitch = Random.Range(0.9f, 1.1f);
        _sound.Play();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Boss")
            Destroy(gameObject);

        if (collision.collider.tag == "Ball")
            Destroy(gameObject);

        if (collision.collider.tag == "Wall")
            Destroy(gameObject);

        if (collision.collider.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }
}

using UnityEngine;

public class Level2BossYellowBall : MonoBehaviour
{
    [SerializeField] GameObject sound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject _sound = Instantiate(sound);
        Destroy(_sound, 1.0f);

        if (collision.collider.tag == "Boss")
            Destroy(gameObject);

        if (collision.collider.tag == "Ball")
            Destroy(gameObject);

        if (collision.collider.tag == "Wall")
            Destroy(gameObject);

        if (collision.collider.tag == "Player")
        {
            Destroy(gameObject);
        }

    }
}
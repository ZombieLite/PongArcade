using UnityEngine;

public class Level2BossPhaseFinalBulletEnemy : MonoBehaviour
{
    private bool isRoundEnd = false;
    void Start()
    {
        Destroy(gameObject, 2.0f);
        RoundEnd.EventRoundEnd.AddListener(StartRoundEnd);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRoundEnd)
            return;

        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void StartRoundEnd()
    {
        isRoundEnd = true;
    }
}

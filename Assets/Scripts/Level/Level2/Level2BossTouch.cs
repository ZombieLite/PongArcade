using UnityEngine;

public class Level2BossTouch : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Vector2 _reflect;
            _reflect = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.AddForce(_reflect * 3.0f, ForceMode2D.Impulse);
        }
    }
}

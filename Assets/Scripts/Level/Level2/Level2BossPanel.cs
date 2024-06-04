using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Level2BossPanel : MonoBehaviour
{
    public static UnityEvent PanelDestroy = new UnityEvent();

    [SerializeField] GameObject sound;

    private int health = 2;
    private SpriteRenderer spr;
    private SpriteRenderer sprChield;
    private Rigidbody2D rb;
    private bool protect = false;
    private bool phase2ff = false;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        sprChield = transform.GetChild(0).GetComponent<SpriteRenderer>();
        rb = transform.root.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (phase2ff && collision.gameObject.layer == LayerMask.NameToLayer("RightWall"))
        {
            phase2ff = false;
            transform.GetComponent<EdgeCollider2D>().isTrigger = false;
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (transform.root.gameObject.GetComponent<Level2Boss>().Level2BossGetAbility() >= 2)
            {
                return;
            }

            Vector2 _reflect;
            _reflect = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.AddForce(_reflect * 5.0f, ForceMode2D.Impulse);
        }

        if (collision.gameObject.tag == "Player")
        {
            if (!phase2ff && transform.root.gameObject.GetComponent<Level2Boss>().Level2BossGetAbility() >= 2)
            {
                Level2BossPanelDamage();
                return;
            }
            Destroy(collision.gameObject);
        }

        if (collision.collider.tag == "Ball")
        {
            Destroy(collision.gameObject);
            Level2BossPanelDamage();
        }
    }

    private void Level2BossPanelDamage()
    {
        if (!protect)
        {
            if (health > 0)
                StartCoroutine(Level2BossDamage());
            else
            {
                PanelDestroy?.Invoke();

                GameObject _sound = Instantiate(sound);
                Destroy(_sound, 1.0f);

                Destroy(gameObject);
            }
        }
    }

    IEnumerator Level2BossDamage()
    {
        protect = true;
        int i = 30;
        bool change = false;
        while(i > 0)
        {
            yield return new WaitForSeconds(0.08f);

            switch(health)
            {
                case 1:
                    if (!change)
                    {
                        spr.color = new Color32(255, 255, 0, 255);
                        sprChield.color = new Color32(255, 255, 55, 100);
                    }
                    else
                    {
                        spr.color = new Color32(255, 0, 0, 255);
                        sprChield.color = new Color32(255, 0, 0, 100);
                    }
                    break;
                case 2:
                    if (!change)
                    {
                        spr.color = new Color32(255, 255, 255, 255);
                        sprChield.color = new Color32(255, 255, 55, 60);
                    }
                    else
                    {
                        spr.color = new Color32(255, 255, 0, 255);
                        sprChield.color = new Color32(255, 255, 55, 100);
                    }
                    break;
            }
            change = !change;
            i--;
        }
        health--;
        protect = false;
        StopCoroutine(Level2BossDamage());
    }

    public int Level2BossGetPanelHealth()
    {
        return health;
    }

    public void Level2BossMovePanel(GameObject obj)
    {
        StartCoroutine(Level2BossMovePanelCoroutine(obj));
    }

    IEnumerator Level2BossMovePanelCoroutine(GameObject obj)
    {
        rb.isKinematic = false;
        transform.GetComponent<EdgeCollider2D>().isTrigger = true;
        phase2ff = true;

        Animator _animator = obj.GetComponent<Animator>();
        AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        //_animator.IsInTransition

        yield return new WaitForSeconds(0.1f);
        while(phase2ff)
        {
            transform.SetPositionAndRotation(obj.transform.position, obj.transform.rotation);

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void Level2BossMovePanelActiveDamage()
    {
        phase2ff = false;
    }
}

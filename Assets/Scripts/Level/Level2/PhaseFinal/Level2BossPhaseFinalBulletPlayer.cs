using UnityEngine;

public class Level2BossPhaseFinalBulletPlayer : MonoBehaviour
{
    [SerializeField] GameObject laserTouch;
    private bool isRoundEnd = false;

    private void Start()
    {
        Destroy(gameObject, 3.0f);
        RoundEnd.EventRoundEnd.AddListener(StartRoundEnd);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isRoundEnd)
            return;

        if (collider.tag == "Boss")
        {
            GameObject _laser = Instantiate(laserTouch);
            _laser.transform.position = transform.position;
            _laser.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
            _laser.GetComponent<AudioSource>().Play();

            Destroy(_laser, 0.1f);

            collider.GetComponent<Level2BossPhaseFinalHealthBar>().Level2BossPhaseFinalHealthBarDamage(2.0f);
            Destroy(gameObject);
        }
    }

    private void StartRoundEnd()
    {
        isRoundEnd = true;
    }
}

using UnityEngine;
using System.Collections;

public class Level2BossPhaseFinalAttackPlayer : MonoBehaviour
{
    private bool roundEnd = false;
    [SerializeField] GameObject bullet;
    private void Start()
    {
        RoundEnd.EventRoundEnd.AddListener(MissinInCompleted);
        StartCoroutine(Level2BossPhaseFinalBulletPlayerStart());
    }

    IEnumerator Level2BossPhaseFinalBulletPlayerStart()
    {
        GameObject _bullet;
        Vector2 _pos;
        AudioSource audioSource = transform.parent.GetComponent<AudioSource>();

        yield return new WaitForSeconds(2.0f);
        while(true)
        {
            if (roundEnd)
            {
                yield return new WaitForSeconds(1.0f);
                continue;
            }

            _bullet = Instantiate(bullet);
            _pos = (Vector2)transform.position + Vector2.left * 1.5f;
            _bullet.transform.position = _pos;

            _pos -= (Vector2)transform.position;
            _bullet.GetComponent<Rigidbody2D>().AddForce(_pos * 5.0f, ForceMode2D.Impulse);

            audioSource.Stop();
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.Play();

            yield return new WaitForSeconds(0.4f);
        }
    }

    private void MissinInCompleted()
    {
        roundEnd = true;
    }
}

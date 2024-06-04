using System.Collections;
using UnityEngine;

public class Level2BossPhaseFinalAttack : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    private bool isRoundEnd = false;

    public void Level2BossPhaseFinalStart()
    {
        StartCoroutine(_Level2BossPhaseFinalAttack());
        RoundEnd.EventRoundEnd.AddListener(StartRoundEnd);
    }

    IEnumerator _Level2BossPhaseFinalAttack()
    {
        GameObject _bullet;
        Vector2 _pos;
        AudioSource audioSource = transform.parent.GetComponent<AudioSource>();

        while (true)
        {
            if (isRoundEnd)
            {
                yield return new WaitForSeconds(1.0f);
                continue;
            }

            _bullet = Instantiate(bullet);
            _pos = (Vector2)transform.position + Vector2.right * 2.0f;
            _bullet.transform.position = _pos;

            _pos -= (Vector2)transform.position;

            _bullet.GetComponent<Rigidbody2D>().AddForce(_pos * 5.0f, ForceMode2D.Impulse);

            audioSource.Stop();
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.Play();

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void StartRoundEnd()
    {
        isRoundEnd = true;
    }
}

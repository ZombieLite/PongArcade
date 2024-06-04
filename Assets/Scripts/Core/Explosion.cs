using UnityEngine;

public class Explosion : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        Destroy(gameObject, 1.5f);
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.8f, 1.1f);
        audioSource.Play();
    }

}

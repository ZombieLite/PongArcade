using UnityEngine;
using System.Collections;

public class Level2BossPhase3FireBullet : MonoBehaviour
{ 
    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
            Destroy(other.gameObject);
    }
}

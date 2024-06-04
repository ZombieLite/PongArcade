using UnityEngine;

public class Level2BossPhase3BlueBullet : MonoBehaviour
{
    [SerializeField] GameObject bombEntity;
    void OnParticleCollision(GameObject bomb)
    {
        if(bomb.tag == "Bomb")
        {
            bomb.GetComponent<Level2BossPhase3Bomb>().Level2BossPhase3SetEnergy(bomb.GetComponent<Level2BossPhase3Bomb>().Level2BossPhase3GetEnergy() + 10.0f);
            Destroy(transform.gameObject);
            return;
        }

        if (bomb.tag == "Player")
        {
            Vector2 posPlayer, posBomb, pos;
            GameObject _bomb;
            posBomb = transform.position;
            posPlayer = bomb.transform.position;
            pos.x = posPlayer.x - 2;
            pos.y = posBomb.y;
            
            _bomb = Instantiate(bombEntity);
            _bomb.transform.position = pos;
            Destroy(transform.gameObject);
        }
    }
}

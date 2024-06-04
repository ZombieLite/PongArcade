using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;

    private void Start()
    {
        CountDown.StartRound.AddListener(BallSpawn);
        ScoreManager.RespawnBall.AddListener(BallSpawn);
    }

    private void BallSpawn()
    {
        Instantiate(ballPrefab, gameObject.transform);    
    }
}

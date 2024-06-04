using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static UnityEvent RespawnBall = new UnityEvent();

    [SerializeField] int LimitBall;
    [SerializeField] GameObject mission;

    int _scoreLeft;
    int _scoreRight;

    private void Start()
    {
        CountDown.StartRound.AddListener(StartScore);
        BallCore.HitLeft.AddListener(ScoreRight);
        BallCore.HitRight.AddListener(ScoreLeft);
    }

    private void StartScore()
    {
        GameObject LeftScore = transform.GetChild(0).gameObject;
        GameObject RightScore = transform.GetChild(1).gameObject;

        LeftScore.GetComponent<Text>().text = "0";
        RightScore.GetComponent<Text>().text = "0";

        if(LimitBall > 1)
        {
            LeftScore.SetActive(true);
            RightScore.SetActive(true);
        }
    }

    private void ScoreLeft()
    {
        GameObject LeftScore = transform.GetChild(0).gameObject;

        _scoreLeft++;
        if (_scoreLeft >= LimitBall)
        {
            GameObject player = GameObject.Find("Player");
            Destroy(player);

            mission.GetComponent<MissionManager>().LevelFailed();
            return;
        }

        LeftScore.GetComponent<Text>().text = ""+_scoreLeft;
        RespawnBall?.Invoke();
    }

    private void ScoreRight()
    {
        GameObject RightScore = transform.GetChild(1).gameObject;

        _scoreRight++;
        if (_scoreRight >= LimitBall)
        {
            GameObject npc = GameObject.Find("NPC");
            npc.GetComponent<NpcController>().Level1BossStartExplosionNpc();
            Destroy(npc);

            mission.GetComponent<MissionManager>().LevelCompleted();
            return;
        }

        RightScore.GetComponent<Text>().text = "" + _scoreRight;
        RespawnBall?.Invoke();
    }
}

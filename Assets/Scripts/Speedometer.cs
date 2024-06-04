using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    GameObject _ball;
    Text speedometer;
    float speed;

    private void Start()
    {
        speedometer = GetComponent<Text>();
    }

    private void Update()
    {
        if( _ball == null )
        {
            _ball = GameObject.FindGameObjectWithTag("Ball");
            return;
        }

        speed = _ball.GetComponent<BallCore>().GetBallSpeed();

        speed = speed * 3;
        speedometer.text = "Скорость: " + Mathf.Round(speed);
    }
}

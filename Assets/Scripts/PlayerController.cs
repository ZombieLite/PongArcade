using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float PlayerSpeed;
    [SerializeField] GameObject mobController;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject mission;
    
    private float vertical;
    private MobileController _dataMController;

    private void Start()
    {
        _dataMController = mobController.GetComponent<MobileController>();
    }

    private void FixedUpdate()
    {
        if (!Input.anyKey)
            return;

        Vector2 dirY = Vector2.zero;

        if (_dataMController.GetDrag())
            vertical = _dataMController.GetVertical();
        else
        {
            vertical = Input.GetAxis("Vertical");
            vertical *= 20.0f;
        }

        dirY = new Vector2(0, vertical);
        dirY = transform.TransformDirection(dirY) * PlayerSpeed * Time.fixedDeltaTime;
        gameObject.GetComponent<Rigidbody2D>().velocity = dirY;

    }

    private void OnDestroy()
    {
        mission.GetComponent<MissionManager>().LevelFailed();
        GameObject ent = Instantiate(explosion);
        ent.transform.position = transform.position;
        RoundEnd.InvokeRoundEnd();
    }
}

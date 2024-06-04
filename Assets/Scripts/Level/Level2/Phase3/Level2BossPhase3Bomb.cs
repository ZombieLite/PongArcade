using UnityEngine;
using UnityEngine.UI;

public class Level2BossPhase3Bomb : MonoBehaviour
{
    [SerializeField] GameObject _progressPrefab;
    [SerializeField] GameObject soundImpact;
    private float _energy = 0;
    private GameObject _panel;
    private GameObject _progressEnt;
    private Image _progressBar;
    Rigidbody2D rb;

    private void Start()
    {
        GetComponent<ParticleSystem>().Stop();
        GetComponent<ParticleSystem>().Clear();
        rb = GetComponent<Rigidbody2D>();
        _panel = GameObject.Find("PanelForBomb");
        _progressEnt = Instantiate(_progressPrefab);
        _progressEnt.transform.SetParent(_panel.transform);
        _progressEnt.transform.localScale = new Vector3(1, 1, 1);
        _progressEnt.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
        _progressBar = _progressEnt.transform.GetChild(0).GetComponent<Image>();

    }

    private void FixedUpdate()
    {
        if (_progressEnt != null)
            _progressBar.fillAmount = _energy / 100.0f;
        else
            rb.rotation += 80 * Time.fixedDeltaTime;
    }

    public void Level2BossPhase3SetEnergy(float energy)
    {
        if (_progressEnt == null)
            return;

        if(energy >= 100)
        {
            _energy = 0;
            Destroy(_progressEnt);
            _panel.SetActive(false);
            GetComponent<ParticleSystem>().Play();

            Vector2 _pos, _endPos, _vector;
            _pos = transform.position;
            _endPos = _pos - Vector2.right;
            _vector = _endPos - _pos;
            rb.AddForce(_vector * 5.0f, ForceMode2D.Impulse);
            return;
        }
        _energy = energy;
    }

    public float Level2BossPhase3GetEnergy()
    {
        return _energy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Boss")
        {
            GameObject _sound;
            _sound = Instantiate(soundImpact);
            Destroy(_sound, 1.0f);

            collision.collider.GetComponent<Level2BossPhase3Panel>().Level2BossPhase3PanelDamage();
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Level2BossPhaseFinalHealthBar : MonoBehaviour
{
    [SerializeField] GameObject healthbar;
    private float _health = 50f;
    private bool _active = false;
    private float _progress = 1;
    
    public void Level2BossPhaseFinalHealthBarActive()
    {
        healthbar.SetActive(true);
        _active = true;
    }

    private void FixedUpdate()
    {
        if(_active == false)
            return;

        _progress = _health / 50;

        Mathf.Clamp01(_progress);

        healthbar.transform.GetChild(1).GetComponent<Image>().fillAmount = _progress;
    }

    public void Level2BossPhaseFinalHealthBarDamage(float damage)
    {
        if(damage >= _health)
        {
            transform.GetComponent<Level2BossPhase3Panel>().Level2BossPhaseFinalMakeDeathEnemy();
            Destroy(healthbar);
            Destroy(gameObject);
        }

        _health -= damage;
    }
}
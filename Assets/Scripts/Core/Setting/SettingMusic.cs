using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingMusic : MonoBehaviour
{
    [SerializeField] AudioMixerGroup _sound;
    [SerializeField] GameObject slider;
    private Slider _slider;
    private float _volume;
    private bool soundActive;

    private void Awake()
    {
        _slider = slider.GetComponent<Slider>();

        _sound.audioMixer.GetFloat("VolumeMaster", out _volume);
        _slider.value = _volume;
    }
    public void SettingAudioToggle(bool active)
    {
        if (_slider == null)
            return;

        if(_volume == -80)
        {
            _slider.interactable = false;
            return;
        }

        if (active)
        {
            _slider.interactable = true;
            soundActive = true;
            AudioListener.volume = 1;
        }
        else
        {
            _slider.interactable = false;
            soundActive = false;
            AudioListener.volume = 0;
        }
    }

    private void OnEnable()
    {
        soundActive = GetBoolFlag("soundActive");
        transform.GetChild(0).gameObject.GetComponent<Toggle>().isOn = soundActive;
        _slider.interactable = soundActive;
    }

    private void OnDisable()
    {
        SetBoolFlag("soundActive", soundActive);
        PlayerPrefs.Save();
    }

    public void SettingAudioVolume(float volume)
    {
        _sound.audioMixer.SetFloat("VolumeMaster", volume);
    }

    public void SettingClosed()
    {
        gameObject.SetActive(false);
    }

    public void SettingRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SettingInMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public static void SetBoolFlag(string nameFlag, bool boobs)
    {
        PlayerPrefs.SetInt(nameFlag, boobs ? 1 : 0);
    }

    public static bool GetBoolFlag(string nameFlag)
    {
        return PlayerPrefs.HasKey(nameFlag) ? PlayerPrefs.GetInt(nameFlag) == 1 : false;
    }
}

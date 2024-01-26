using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSliderWidget : Option
{
    public float defaultVolume = 0.25f;
    public float minDB = -80f, maxDB = 0f;
    private float tmpVolume;
    private float currentVolume;

    [SerializeField] private Sprite unmutedSprite;
    [SerializeField] private Sprite mutedSprite;

    [SerializeField] private Toggle toggle;
    [SerializeField] private Animator animator;
    [SerializeField] private Slider slider;
    [SerializeField] private AudioMixer masterMixer;

    public override void Init()
    {
        currentVolume = PlayerPrefs.GetFloat(prefKey + "Volume", defaultVolume);
        tmpVolume = currentVolume;
        if (toggle.isOn == (PlayerPrefs.GetInt(prefKey + "Muted", 0) == 0))
            slider.value = currentVolume;
        toggle.isOn = PlayerPrefs.GetInt(prefKey + "Muted", 0) == 0;
    }

    public override void Apply()
    {
        currentVolume = tmpVolume;
        PlayerPrefs.SetInt(prefKey + "Muted", toggle.isOn ? 0 : 1);
        PlayerPrefs.SetFloat(prefKey + "Volume", currentVolume);
    }

    public override void Revert()
    {
        slider.value = currentVolume;
        toggle.isOn = PlayerPrefs.GetInt(prefKey + "Muted", 0) == 0;
    }

    public void OnToggle(bool val)
    {
        animator.SetBool("Muted", !val);
        slider.SetValueWithoutNotify(val ? tmpVolume : 0);
        masterMixer.SetFloat(prefKey + "Volume", val ? Mathf.Log10(tmpVolume) * 20 : -80f);
    }

    public void OnVolumeChanged(float val)
    {
        tmpVolume = val;
        toggle.isOn = val > 0.0001f;
        masterMixer.SetFloat(prefKey + "Volume", Mathf.Log10(val) * 20);
    }
}

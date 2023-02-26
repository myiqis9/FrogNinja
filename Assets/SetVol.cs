using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetVol : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM;
    [SerializeField] private string nameParam;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        float vol = PlayerPrefs.GetFloat(nameParam, 0.5f);
        slider.value = vol;
        audioM.SetFloat(nameParam, Mathf.Log10(vol) * 30);
    }

    public void SetVolume(float vol)
    {
        audioM.SetFloat(nameParam, Mathf.Log10(vol) * 30);
        PlayerPrefs.SetFloat(nameParam, vol);
    }
}

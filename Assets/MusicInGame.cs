using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicInGame : MonoBehaviour
{
    public string nameParam;
    public AudioMixer audioM;

    // Start is called before the first frame update
    void Start()
    {
        float vol = PlayerPrefs.GetFloat(nameParam, 0.5f);
        audioM.SetFloat(nameParam, Mathf.Log10(vol) * 30);
    }
}

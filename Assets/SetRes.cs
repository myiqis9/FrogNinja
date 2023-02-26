using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRes : MonoBehaviour
{
    private Dropdown dropdown;
    private Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        Resolution currentRes = Screen.currentResolution;

        int i = 0;
        int pos = 0;

        foreach(Resolution r in resolutions)
        {
            string val = r.ToString();
            options.Add(val);
            if (r.width == currentRes.width && r.height == currentRes.height 
                                    && r.refreshRate == currentRes.refreshRate)
            {
                pos = i;
            }
            i++;
        }

        dropdown.AddOptions(options);
        dropdown.value = pos;
    }

    public void SetResolution()
    {
        Resolution r = resolutions[dropdown.value];
        Screen.SetResolution(r.width, r.height, Screen.fullScreenMode, r.refreshRate);
    }
}

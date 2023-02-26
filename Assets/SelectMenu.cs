using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Selectable[] defaultItems;

    IEnumerator Start()
    {
        yield return new WaitForFixedUpdate();
        PanelToggle(0);
    }

    public void PanelToggle(int pos)
    {
        Input.ResetInputAxes();
        for(int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(pos == i);
            if(pos == i)
            {
                defaultItems[i].Select();
            }
        }
    }

    public void SavePrefs()
    {
        PlayerPrefs.Save();
    }
}

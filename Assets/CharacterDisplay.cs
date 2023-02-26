using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    [SerializeField] private Image maskedPlayer;
    [SerializeField] private Image frogNinja;

    private void Start()
    {
        int random = Random.Range(0, 2);
        maskedPlayer.GetComponent<Image>();
        frogNinja.GetComponent<Image>();

        switch (random)
        {
            case 0:
                frogNinja.enabled = false;
                break;
            case 1:
                maskedPlayer.enabled = false;
                break;
        }
    }
}

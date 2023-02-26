using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogNinja : MonoBehaviour
{
    private GameManager manager;
    public GameObject confetti;
    private CharControl player;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        player = GameObject.Find("MaskedPlayer").GetComponent<CharControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Celebration());
        }
    }

    private IEnumerator Celebration()
    {
        confetti.transform.position = new Vector3(player.transform.position.x, 
            player.transform.position.y + 8, player.transform.position.z);
        confetti.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1.5f);
        manager.gameOver = true;
    }
}

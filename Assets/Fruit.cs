using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private GameManager manager;
    public static Fruit instance = null;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            manager.AddPoints();
            StartCoroutine(Collected());
        }
    }

    private IEnumerator Collected()
    {
        anim.Play("Collectedanim");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}

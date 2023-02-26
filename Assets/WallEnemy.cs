using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEnemy : MonoBehaviour
{
    [SerializeField] public LayerMask enemyMask;
    [SerializeField] public float speed;

    private Rigidbody2D body;
    private Transform trans;
    private Animator anim;
    public bool hit = false;
    private float width;
    private Vector2 vel;
    private float tempSpeed = 0f;
    private CharControl controller;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        trans = this.transform;
        width = this.GetComponent<SpriteRenderer>().bounds.extents.y;
        controller = GameObject.Find("MaskedPlayer").GetComponent<CharControl>();
    }

    private void Update()
    {
        anim.SetBool("hit", hit);
    }

    void FixedUpdate()
    {
        // find the front of the sprite and check for ground before moving forward
        Vector2 linecastPos = trans.position - trans.right * width;
        bool isGrounded = Physics2D.Linecast(linecastPos, linecastPos + Vector2.left, enemyMask);
        Debug.DrawLine(linecastPos, linecastPos + Vector2.left);

        //if there's no ground, turn around
        if (!isGrounded)
        {
            Vector3 rotation = trans.eulerAngles;
            rotation.x += 180;
            trans.eulerAngles = rotation;
        }

        //always move forward
        vel = body.velocity;
        vel.y = -trans.right.y * speed;
        body.velocity = vel;
        hit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(EnemyHit());
            controller.TakeDamage();

            //knockback
            controller.knockbackCount = controller.knockbackLength;
            if (collision.transform.position.x < transform.position.x)
                controller.knockFromRight = true;
            else
                controller.knockFromRight = false;
        }
    }

    private IEnumerator EnemyHit()
    {
        float prevSpeed = speed;
        speed = tempSpeed;

        hit = true;
        yield return new WaitForSeconds(0.5f);
        speed = prevSpeed;
    }
}

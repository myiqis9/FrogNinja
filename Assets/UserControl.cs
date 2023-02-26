using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserControl : MonoBehaviour
{
    private Animator anim;
    private float h = 0;
    public bool jump;
    public bool wall;
    public bool damage;
    private CharControl player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = this.GetComponent<CharControl>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        h = move.x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.performed;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("h", Mathf.Abs(h));
        anim.SetBool("wall", wall);
        anim.SetBool("hit", damage);
    }

    private void FixedUpdate()
    {
        player.Move(h, jump);
        jump = false;
        damage = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        anim.SetFloat("MoveX", movement.x);
        anim.SetFloat("MoveY", movement.y);
        if (movement != Vector2.zero)
        {
            anim.SetFloat("LastMoveX", movement.x);
            anim.SetFloat("LastMoveY", movement.y);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement.normalized * speed;
    }
}

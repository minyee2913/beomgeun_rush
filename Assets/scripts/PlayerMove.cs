using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameManager gameManager;

    public float maxSpeed;

    private Rigidbody2D rigid;
    private SpriteRenderer render;

    private void Awake()
    {
        rigid= GetComponent<Rigidbody2D>();
        render= GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if(h < 0)
        {
            render.flipX = true;
        }

        else if(h > 0) 
        {
            render.flipX = false;
        }

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if(rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if(rigid.velocity.x < -maxSpeed)
        {
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        }
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
        }

        else if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.2f, rigid.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name != "Square")
        {
            StartCoroutine(Hit());
        }
    }

    IEnumerator Hit()
    {
        gameManager.alive = false;
        render.color = Color.red;

        yield return new WaitForSeconds(1f);
        StartCoroutine(gameManager.LoadAnim());

        render.color = Color.white;
    }

}

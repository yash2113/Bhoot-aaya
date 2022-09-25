using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{   [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;

    private Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;
    private string WALK_ANIMATION = "walk";
    private string JUMP_ANIMATION = "jump";
    private string ENEMY_TAG = "Enemy";

    private bool isGrounded=true;
    private string GROUND = "ground";
    public float Health = 1f;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        sr=GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayermoveKeyword();
        AnimatePlayer();
        PlayerJump();
    }
    private void FixedUpdate()
    {
        
    }

    void PlayermoveKeyword()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position +=new Vector3(movementX, 0f,0f)*moveForce*Time.deltaTime;
        
    }

    void AnimatePlayer()
    {   //we are going to right side
        if (movementX>0)
        {
            sr.flipX =false;
            anim.SetBool(WALK_ANIMATION, true);
        }
        //we are going to left side
        else if (movementX < 0)
        {   sr.flipX = true;
            anim.SetBool(WALK_ANIMATION, true);
        }

        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
        
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            anim.SetBool(JUMP_ANIMATION, true);
            myBody.AddForce(new Vector2 (0f,jumpForce),ForceMode2D.Impulse);
                 
        }
        
 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND))
        {
            anim.SetBool(JUMP_ANIMATION, false);
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Health -= 0.2f;
            source.Play();

        }
        if (Health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("end");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Health -= 0.2f;

            source.Play();


        }
        if(Health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("end");
        }
    }
}

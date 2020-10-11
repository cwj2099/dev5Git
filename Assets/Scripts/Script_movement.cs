using DragonBones;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Script_movement : MonoBehaviour
{
    public Animator ani;
    public SpriteRenderer spr;
    public GameObject sword;
    public Sprite spr_sword1;
    public Sprite spr_sword2;
    public Rigidbody2D thisRigidbody2d;
    public float force = 10f;
    public float grav_ground = 7;
    public float grav_air = 7;
    public float attack_length;
    public Script_manager manager;
    public Script_GroundCheck groundScript;
    public Boolean fly = true;
    public GameObject tele;
    public LayerMask theMask;

    Vector2 direction;
    int counter1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (groundScript.grounded){
            ani.SetBool("onGround", true);
            fly = true;
            if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                ani.SetBool("Moving",false);

            }
            else {
                ani.SetBool("Moving", true);

            }
            
            thisRigidbody2d.gravityScale = grav_ground;
            if (Input.GetKeyDown(KeyCode.Space)) {
                thisRigidbody2d.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            }
        }
        else {
            thisRigidbody2d.gravityScale = grav_air;
            ani.SetBool("onGround", false);

            if (Input.GetKeyDown(KeyCode.Space)&&manager.flyable&&fly)
            {
                ani.SetBool("onGround", true);
                fly = false;
                thisRigidbody2d.AddForce(Vector2.up * force *1.5f, ForceMode2D.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            transform.position = tele.transform.position-new Vector3(0,0,1f);
        }

        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mPos-transform.position;
        direction=direction.normalized;
        Ray ray1 = new Ray(transform.position, direction);
        RaycastHit2D hit = Physics2D.Raycast(ray1.origin, ray1.direction, attack_length, theMask);
        Debug.DrawRay(ray1.origin, ray1.direction*attack_length , Color.white);


        sword.transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(direction.x,direction.y)*Mathf.Rad2Deg); 
        if (manager.hookable)
        {
            if (Input.GetMouseButtonDown (0))
            {
                counter1 = 60;
                //print("attempted flying");
                thisRigidbody2d.velocity = Vector2.zero;//clear force
                if (hit.collider != null && hit.collider.gameObject.CompareTag("enemy"))
                {
                    //print("hitted");
                    thisRigidbody2d.AddForce(direction * force * 4f, ForceMode2D.Impulse);
                }
                else {
                    thisRigidbody2d.AddForce(direction * force * 2f, ForceMode2D.Impulse);
                }
                //thisRigidbody2d.AddForce(Vector2.up * thisRigidbody2d.gravityScale, ForceMode2D.Impulse);

            }
        }
        if (counter1 > 0) {
            counter1--;
            if (counter1 > 1) { thisRigidbody2d.gravityScale = 0; }
            else if (counter1 == 1) { thisRigidbody2d.velocity = Vector2.zero; }
            sword.GetComponent<SpriteRenderer>().sprite = spr_sword2;
        }
        else { sword.GetComponent<SpriteRenderer>().sprite = spr_sword1; }
        
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A)) {
            thisRigidbody2d.AddForce(Vector2.left * force * Time.fixedDeltaTime, ForceMode2D.Impulse);

            spr.flipX = true;
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            thisRigidbody2d.AddForce(Vector2.right * force * Time.fixedDeltaTime, ForceMode2D.Impulse);

            spr.flipX = false;
        }

        

        

    }
}

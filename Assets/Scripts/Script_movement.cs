using DragonBones;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Script_movement : MonoBehaviour
{
    //recourses to load
    public Sprite spr_sword1;
    public Sprite spr_sword2;
    public Sprite spr_sword3;
    //Objects to link
    public LayerMask theMask;
    public Animator ani;
    public SpriteRenderer spr;
    public GameObject sword;
    public Rigidbody2D thisRigidbody2d;
    public Script_manager manager;
    public Script_GroundCheck groundScript;
    public GameObject tele;
    //Variables: physics
    public float force = 10f;
    public float grav_ground = 7;
    public float grav_air = 7;
    public float attack_length;
    public int slashTime_normal = 60;
    public int slashTime_hit = 90;
    public int floatTime = 60;

    //Varaiables: logic
    public Boolean fly = true;
    public Vector2 direction;
    public int maxSlash=3;
    public int slashes;
    public int counter1;
    public int counter2;
    public bool hitted = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        //Adjust gravity sacle and animation according to movement and grounded or not
        transform.position = new Vector3(transform.position.x, transform.position.y, -9);
        if (groundScript.grounded){
            ani.SetBool("onGround", true);
            fly = true;
            if (counter1 < 1) { slashes = maxSlash; }//can't restore during the move
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

        //Teleport back to home
        if (Input.GetKeyDown(KeyCode.Q)) {
            transform.position = tele.transform.position-new Vector3(0,0,1f);
        }

        //Calculate the aiming
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mPos-transform.position;
        direction=direction.normalized;
        Ray ray1 = new Ray(transform.position, direction);
        RaycastHit2D hit = Physics2D.Raycast(ray1.origin, ray1.direction, attack_length, theMask);
        Debug.DrawRay(ray1.origin, ray1.direction*attack_length , Color.white);

        //Rotate the sword
        if (counter1 < 1) { sword.transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg); }

        //initallize the slash
        if (manager.hookable)
        {
            if (Input.GetMouseButtonDown(0) && slashes>0 &&counter1<1)
            {
                slashes--;
                //print("attempted flying");
                thisRigidbody2d.velocity = Vector2.zero;//clear force
                if (hit.collider != null && hit.collider.gameObject.CompareTag("enemy"))
                {
                    //print("hitted");
                    counter1 = slashTime_hit;
                    slashes++;
                    hitted = true;
                    thisRigidbody2d.AddForce(direction * force * 4f, ForceMode2D.Impulse);
                }
                else {
                    counter1 = slashTime_normal;
                    thisRigidbody2d.AddForce(direction * force * 2f, ForceMode2D.Impulse);
                }
                //thisRigidbody2d.AddForce(Vector2.up * thisRigidbody2d.gravityScale, ForceMode2D.Impulse);

            }
        }
        //Slash Traveling
        if (counter1 > 0) {
            counter1--;
            //if going on
            if (counter1 > 1) { thisRigidbody2d.gravityScale = 0; }
            //if ends
            else if (counter1 == 1) {
                //clear force
                thisRigidbody2d.velocity = Vector2.zero;
                //if successfully hitted the enemy, enter phase 2 
                if (hitted)
                {
                    counter2 = floatTime;
                }
            }
            if (hitted) { sword.GetComponent<SpriteRenderer>().sprite = spr_sword3; }
            else{ sword.GetComponent<SpriteRenderer>().sprite = spr_sword2; }
        }
        else { 
            sword.GetComponent<SpriteRenderer>().sprite = spr_sword1;
            hitted = false; 
        }

        //after float
        if (counter2 > 0) {
            counter2--;
            thisRigidbody2d.velocity = Vector2.zero;
        }

        //apperance accoridng to the states
        if (counter1 > 1 || counter2 > 1)
        {
            ani.speed = 0;
        }
        else { ani.speed = 1; }
        
    }

    void FixedUpdate()
    {
        //General Movement
        if (Input.GetKey(KeyCode.A)) {
            thisRigidbody2d.AddForce(Vector2.left * force * Time.fixedDeltaTime, ForceMode2D.Impulse);

            //spr.flipX = true;
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            thisRigidbody2d.AddForce(Vector2.right * force * Time.fixedDeltaTime, ForceMode2D.Impulse);

            //spr.flipX = false;
        }

        //Facing according to the sword
        if (sword.transform.rotation.z > 0) { spr.flipX = true; }
        else { spr.flipX = false; }

        

    }
}

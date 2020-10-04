using DragonBones;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Script_movement : MonoBehaviour
{
    public DragonBones.UnityArmatureComponent thisSprite;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (groundScript.grounded){
            fly = true;
            if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                if (thisSprite.animationName != "idle") { thisSprite.animationName = "idle"; thisSprite.animation.FadeIn("idle", 0.2f, 0); }
            }
            else {
                if (thisSprite.animationName != "ground") { thisSprite.animationName = "ground"; thisSprite.animation.FadeIn("ground", 0.2f, 0); }
            }
            
            thisRigidbody2d.gravityScale = grav_ground;
            if (Input.GetKeyDown(KeyCode.Space)) {
                thisRigidbody2d.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            }
        }
        else {
            thisRigidbody2d.gravityScale = grav_air;
            if (thisSprite.animationName != "float") { thisSprite.animationName = "float"; thisSprite.animation.FadeIn("float", 0.2f, 0); }
            if (Input.GetKeyDown(KeyCode.Space)&&manager.flyable&&fly)
            {
                thisSprite.animation.Play("float");
                fly = false;
                thisRigidbody2d.AddForce(Vector2.up * force *1.5f, ForceMode2D.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            transform.position = tele.transform.position;
        }

        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mPos-transform.position;
        direction=direction.normalized;
        Ray ray1 = new Ray(transform.position, direction);
        RaycastHit2D hit = Physics2D.Raycast(ray1.origin, ray1.direction, attack_length, theMask);
        Debug.DrawRay(ray1.origin, ray1.direction*attack_length , Color.white);

        //DragonBones.AnimationState animationState = thisSprite.animation.FadeIn("default", 0.1f, 1, 0, "ATTACK_ANIMATION_GROUP");
        //animationState.AddBoneMask("weapon");
        //print(Vector2.Angle(transform.position, mPos));
        if (thisSprite.armature.flipX == true)
        {
            thisSprite.armature.GetBone("weapon").offset.rotation = 355 - (Mathf.Atan2(direction.x, direction.y));
        }
        else {
            thisSprite.armature.GetBone("weapon").offset.rotation = (Mathf.Atan2(direction.x, direction.y) - 60);
        }
        if (manager.hookable)
        {
            if (Input.GetMouseButtonDown (0))
            {
                //print("attempted flying");
                //thisRigidbody2d.velocity = Vector2.zero;
                if (hit.collider!=null&&hit.collider.gameObject.CompareTag("ground"))
                {
                    thisRigidbody2d.AddForce(direction * force * 1.5f, ForceMode2D.Impulse);
                }
                //thisRigidbody2d.AddForce(Vector2.up * thisRigidbody2d.gravityScale, ForceMode2D.Impulse);

            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A)) {
            thisRigidbody2d.AddForce(Vector2.left * force * Time.fixedDeltaTime, ForceMode2D.Impulse);
            thisSprite.armature.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            thisRigidbody2d.AddForce(Vector2.right * force * Time.fixedDeltaTime, ForceMode2D.Impulse);
            thisSprite.armature.flipX = false;
        }

        

        

    }
}

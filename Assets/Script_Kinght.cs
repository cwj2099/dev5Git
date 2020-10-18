using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Kinght : MonoBehaviour
{
    public SpriteRenderer mySprite;
    public Rigidbody2D myBody;
    public LayerMask theMask;
    public float speed = 1f;
    public Vector2 facing;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        facing = Vector2.right;
        if (!mySprite.flipX) { facing = Vector2.right; } else { facing = Vector2.left; }
        Ray ray1 = new Ray(transform.position+new Vector3(facing.x * 6f, facing.y-3, 0f), facing);
        RaycastHit2D hit = Physics2D.Raycast(ray1.origin, facing, 2, theMask);
        Debug.DrawRay(ray1.origin, facing * 2, Color.white);
        if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            // print("walled");
            mySprite.flipX = !mySprite.flipX;
        }
        transform.Translate(facing * speed * Time.deltaTime, Space.World);
        //myBody.AddForce(facing * speed * Time.deltaTime, ForceMode2D.Impulse);



    }
}

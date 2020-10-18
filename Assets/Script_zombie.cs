using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_zombie : MonoBehaviour
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
        if (!mySprite.flipX) {  facing = Vector2.right; } else {  facing = Vector2.left; }
        Ray ray1 = new Ray(transform.position+new Vector3(facing.x*2f,facing.y,0f), facing);
        RaycastHit2D hit = Physics2D.Raycast(ray1.origin, facing, 1, theMask);
        Debug.DrawRay(ray1.origin, facing*1, Color.white);
        if (hit.collider!=null&&hit.collider.gameObject.GetComponent<Rigidbody2D>()!=null)
        {
            // print("walled");
            mySprite.flipX = !mySprite.flipX;
        }
        myBody.AddForce(facing * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);

        

    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            //print("attacking player");
            GameObject player = collision.gameObject;
            if (player.GetComponent<Script_movement>().counter2 == 0 && player.GetComponent<Script_movement>().counter1 == 0)
            {
                Vector2 direction = player.transform.position - transform.position;
                direction = direction.normalized;
                player.GetComponent<Rigidbody2D>().AddForce(direction * 50f, ForceMode2D.Impulse);
                player.GetComponent<Script_movement>().Hp -= damage;
                player.GetComponent<Script_movement>().counter2 = 0;
                player.GetComponent<Script_movement>().counter1 = 0;
            }
        }
    }*/
}

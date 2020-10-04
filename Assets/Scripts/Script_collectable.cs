using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_collectable : MonoBehaviour
{
    public Script_manager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            manager.collection += 1;
            Destroy(gameObject);
        }
    }
}

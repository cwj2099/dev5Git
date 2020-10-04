using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_end : MonoBehaviour
{
    public string content;
    public GameObject target;
    public Script_test text;
    public Script_manager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.collection2++;
        collision.transform.position = target.transform.position;
        text.content = content;
        Destroy(gameObject);

    }
}

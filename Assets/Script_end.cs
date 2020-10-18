using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_end : MonoBehaviour
{
    public string content;
    public Text text;
    public GameObject target;
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
        if (collision.gameObject.CompareTag("Player")) {
            manager.collection2++;
            text.text = content;
            collision.gameObject.transform.position = target.transform.position;
            Destroy(gameObject);
        }

    }
}

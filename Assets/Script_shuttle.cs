using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_shuttle : MonoBehaviour
{
    public Text text;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            player.GetComponent<Script_movement>().tele = gameObject;
            player.GetComponent<Script_movement>().Hp = player.GetComponent<Script_movement>().MaxHp;
            text.text = "Respawn Point Updated";
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_manager : MonoBehaviour
{
    public Script_movement Player;

    public Boolean flyable = false;
    public Boolean hookable = false;
    public int hooks=1;
    public int collection = 0;
    public int collection2 = 0;
    public Image health;
    public Image Sword1;
    public Image Sword2;
    public Image Sword3;
    public Text textBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health.rectTransform.sizeDelta = new Vector2(Player.Hp * 40f, 40f);
        if (collection >= 1) { flyable = true; }
        if (collection >= 2) { hookable = true; }
        if(collection >= 3) { hooks = 2; }
        if (collection >= 4) { hooks = 3; }

        if (!hookable) { Sword1.gameObject.SetActive(false); Sword2.gameObject.SetActive(false); Sword3.gameObject.SetActive(false); }
        else
        {
            if (Player.slashes == 0) { Sword1.gameObject.SetActive(false); Sword2.gameObject.SetActive(false); Sword3.gameObject.SetActive(false); }
            if (Player.slashes == 1) { Sword1.gameObject.SetActive(true); Sword2.gameObject.SetActive(false); Sword3.gameObject.SetActive(false); }
            if (Player.slashes == 2) { Sword1.gameObject.SetActive(true); Sword2.gameObject.SetActive(true); Sword3.gameObject.SetActive(false); }
            if (Player.slashes == 3) { Sword1.gameObject.SetActive(true); Sword2.gameObject.SetActive(true); Sword3.gameObject.SetActive(true); }

        }

    }
}

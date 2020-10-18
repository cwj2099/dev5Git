using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_spawner : MonoBehaviour
{
    public GameObject toSpawn;
    GameObject child;
    public int spawnDelay = 120;
    public int counter1;
    // Start is called before the first frame update
    void Start()
    {
        child=Instantiate(toSpawn, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (child == null&&counter1==0)
        {
            counter1 = spawnDelay;
        }

        if (counter1 > 0)
        {
            counter1--;
            if (counter1 == 1) {
                child = Instantiate(toSpawn, transform.position, Quaternion.identity);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    public GameObject DefaultObject;
    public GameObject ObjectForSpawn;

    void Start()
    {

        var obj = ObjectForSpawn.GetComponent<SpriteRenderer>();
        for (float i = -obj.bounds.size.x; i <= obj.bounds.size.x; i += obj.bounds.size.x)
        {
            for (float j = -obj.bounds.size.y; j <= obj.bounds.size.y; j += obj.bounds.size.y)
            {
                //if (i == obj.bounds.size.x && j == obj.bounds.size.y)
                //{
                    Instantiate(ObjectForSpawn, new Vector3(i, j, 0), new Quaternion());
                //}
                //else
                //{
                   // Instantiate(ObjectForSpawn, new Vector3(i, j, 0), new Quaternion());
               // }
            }
        }
    }

    void Update()
    {

    }
}

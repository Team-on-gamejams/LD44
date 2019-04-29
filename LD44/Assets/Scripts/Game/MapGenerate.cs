using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    public GameObject MainObject;
    public GameObject ObjectForSpawn;

    void Start()
    {
        var obj = ObjectForSpawn.GetComponent<SpriteRenderer>();
        for (float i = -obj.bounds.size.x; i <= obj.bounds.size.x; i += obj.bounds.size.x)
        {
            for (float j = -obj.bounds.size.y; j <= obj.bounds.size.y; j += obj.bounds.size.y)
            {
                if (i == 0 && j == 0)
                {
                    Instantiate(MainObject, new Vector3(i, j, 0), new Quaternion());
                }
                else
                {
                    Instantiate(ObjectForSpawn, new Vector3(i, j, 0), new Quaternion());
                }
            }
        }
    }
}

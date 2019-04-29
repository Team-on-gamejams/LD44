using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    public GameObject[] Objects;

    void Start()
    {
        var obj = Objects[0].GetComponent<SpriteRenderer>();
        for (float i = -obj.bounds.size.x; i <= obj.bounds.size.x; i += obj.bounds.size.x)
        {
            for (float j = -obj.bounds.size.y; j <= obj.bounds.size.y; j += obj.bounds.size.y)
            {
                if (i == 0 && j == 0)
                {
                    Instantiate(Objects[0], new Vector3(i, j, 0), new Quaternion());
                }
                else
                {
                    Instantiate(Objects[Random.Range(1,5)], new Vector3(i, j, 0), new Quaternion());
                }
            }
        }





        //var obj = ObjectForSpawn.GetComponent<SpriteRenderer>();
        //for (float i = -obj.bounds.size.x; i <= obj.bounds.size.x; i += obj.bounds.size.x)
        //{
        //    for (float j = -obj.bounds.size.y; j <= obj.bounds.size.y; j += obj.bounds.size.y)
        //    {
        //        if (i == 0 && j == 0)
        //        {
        //            Instantiate(MainObject, new Vector3(i, j, 0), new Quaternion());
        //        }
        //        else
        //        {
        //            Instantiate(ObjectForSpawn, new Vector3(i, j, 0), new Quaternion());
        //        }
        //    }
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    public GameObject DefaultObject;
    public GameObject ObjectForSpawn;
    const int MatrixCount = 2;

    void Start()
    {

        var obj = ObjectForSpawn.GetComponent<SpriteRenderer>();
        for (float i = 0; i <= (MatrixCount * obj.bounds.size.x); i += obj.bounds.size.x)
        {
            for (float j = 0; j <= (MatrixCount * obj.bounds.size.y); j += obj.bounds.size.y)
            {
                if (i == obj.bounds.size.x && j == obj.bounds.size.y)
                {
                    Instantiate(DefaultObject, new Vector3(i, j, 0), new Quaternion());
                }
                else
                {
                    Instantiate(ObjectForSpawn, new Vector3(i, j, 0), new Quaternion());
                }
            }
        }
    }

    void Update()
    {

    }
}

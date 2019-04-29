using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    public GameObject ObjectForSpawn;
    const int MatrixCount = 3;

    void Start()
    {
        
        var obj = ObjectForSpawn.GetComponent<SpriteRenderer>();
        for (float i = 0; i < (MatrixCount * obj.size.x); i+= obj.size.x)
        {
            for (float j = 0; j < (MatrixCount * obj.size.y); j+= obj.size.x)
            {
                Instantiate(gameObject, new Vector3(i, j, 0), new Quaternion());
            }
        }
    }

    void Update()
    {

    }
}

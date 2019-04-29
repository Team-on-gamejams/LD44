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
        for (float i = 0; i < (MatrixCount * obj.size.x * 2); i+= obj.size.x * 2)
        {   
            for (float j = 0; j < (MatrixCount * obj.size.y * 2); j+= obj.size.y * 2)
            {
                Instantiate(ObjectForSpawn, new Vector3(i + (obj.size.x / 2), j + (obj.size.y), 0), new Quaternion());
            }
        }
    }

    void Update()
    {
   
    }
}

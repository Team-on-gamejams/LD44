using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    public GameObject[] Objects;

    void Start()
    {
        var obj = Objects[0].GetComponent<SpriteRenderer>();
        for (float i = 0; i < 2; ++i)
        {
            for (float j = 0; j < 2; ++j)
            {
                //if(i== -obj.bounds.size.x && j == -obj.bounds.size.y)
                //{
                //    Instantiate(Objects[0], new Vector3(i, j, 0), new Quaternion());
                //}
                //else if (i == 0 && j == 0)
                //{
                //    Instantiate(Objects[3], new Vector3(i, j, 0), new Quaternion());
                //}
                //else
                //{
                //    Instantiate(Objects[Random.Range(1,5)], new Vector3(i, j, 0), new Quaternion());
                //}
				Instantiate(Objects[Random.Range(0, Objects.Length)], new Vector3(obj.bounds.size.x * i - obj.bounds.size.x / 2, obj.bounds.size.y * j - obj.bounds.size.y / 2, 0), new Quaternion());

			}
		}    
    }
}

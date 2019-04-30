using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyPopupScript : MonoBehaviour
{
    public Object SenderObject;
    public Object ViewObject;
    public void PopUp()
    {
        Debug.Log("+ View");
        var @object = Instantiate(ViewObject, Input.mousePosition, new Quaternion());
        LeanTween.delayedCall(1, () =>
        {
            DestroyView(@object);
        });
    }

    private void DestroyView(Object @object)
    {
        Destroy(@object);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyPopupScript : MonoBehaviour
{
    private bool MouseOver = false;
    public Object ObjectToView; 

    public void IsMouseOverUI()
    {
        MouseOver = true;
        Instantiate(ObjectToView);  
    }
        
    public void OnMoseExit()
    {

    }

}
    
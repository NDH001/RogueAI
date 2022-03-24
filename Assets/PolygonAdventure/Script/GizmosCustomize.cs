using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GizmosCustomize: MonoBehaviour
{



    public GameObject go;


    public void Start()
    {
        //go = new GameObject();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
      
        Gizmos.DrawCube(go.transform.position, Vector3.one);
        
        
    }

}

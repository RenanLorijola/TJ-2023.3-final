using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 pos;

    void Start()
    {
        pos = transform.position;
    }

    void Update() 
    {
        bool TeclaA = Input.GetKey(KeyCode.A);
        bool TeclaD = Input.GetKey(KeyCode.D);
        bool TeclaS = Input.GetKey(KeyCode.S);
        bool TeclaW = Input.GetKey(KeyCode.W);
        if(TeclaA){
            pos.x -=0.1f;
        }
        if(TeclaD){
            pos.x +=0.1f;
        }
        if(TeclaS){
            pos.z -=0.1f;
        }
        if(TeclaW){
            pos.z +=0.1f;
        }
        gameObject.transform.position = pos;
    }
}

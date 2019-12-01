using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 dPos;
    private Vector3 pos;
    private bool open;

    public void Operate()
    {
        //pos = open ? transform.position - dPos : pos = transform.position + dPos;

        if (open) 
        {
            pos = transform.position - dPos;
        }
        else
        {
            pos = transform.position + dPos;
        }

        transform.position = pos;
        open = !open;
    }
}

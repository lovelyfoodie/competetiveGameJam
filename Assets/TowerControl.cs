using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    public Transform currentCenter;
    private Transform _originalCenter;

    public float Velocity
    {
        get
        {
            return -1f; //TODO
        }
    }
    public float Deflection
    {
        get
        {
            return (_originalCenter.position - currentCenter.position).magnitude;
        }
    }

    private void Awake()
    {
         _originalCenter = currentCenter; 
    }
    
}

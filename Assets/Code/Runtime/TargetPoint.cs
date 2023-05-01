using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    public Transform my_body;
    
    public Vector3 get_position()
    {
        return my_body.position;
    }


}

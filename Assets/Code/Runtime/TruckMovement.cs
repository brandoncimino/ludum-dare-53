using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{

    public Transform my_target;
    
    public Transform my_body;
    public float my_move_speed = 0.02f;
    public float my_rotation_speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dt = Time.deltaTime;
        my_body.position += my_move_speed * my_body.forward;

        var direction = (my_target.position - my_body.position).normalized;
        var look_rotation = Quaternion.LookRotation(direction);
        var new_rotation = Quaternion.Slerp(my_body.rotation, look_rotation, dt * my_rotation_speed);
        my_body.rotation = new_rotation;


    }
    
    
}

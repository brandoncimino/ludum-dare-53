using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTrailor : Movement<TargetPoint>
{
    public MovementTruck my_truck;
    public float hitch_distance = 0.05f;
    public Transform my_front_hitch;
    
    protected override void Search_new_target()
    {
        my_target = my_truck.attach_trailor(this);
    }
    
    protected override void Move_to_target()
    {
        if (my_target is null)
        {
            Search_new_target();
        }
        
        // rotate towards target
        var direction = (my_target.get_position() - my_body.position);
        var dt = Time.deltaTime; 
        var look_rotation = Quaternion.LookRotation(direction.normalized);
        // var new_rotation = Quaternion.Slerp(my_body.rotation, look_rotation, dt * my_rotation_speed);
        my_body.rotation = look_rotation;  // rotate
        
        // stay at equal distance from target
        var offset = my_body.position - my_front_hitch.position;
        my_body.position = my_target.get_position() + offset - hitch_distance * my_body.forward;

    }
}

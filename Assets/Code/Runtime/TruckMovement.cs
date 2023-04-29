using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{

    [CanBeNull] public TargetPoint my_target;
    [CanBeNull] public TargetPoint my_last_target;
    public bool bool_following_track_direction = true;
    
    public Transform my_body;
    public float my_move_speed = 0.02f;
    public float my_rotation_speed = 1;
    
    private float target_accuracy = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move_towards_target();
    }

    private void Move_towards_target()
    {
        if (my_target is null)
        {
            Search_new_target();
        }
        
        var direction = (my_target.get_position() - my_body.position);
        if(direction.magnitude < target_accuracy)
        {
            Approach_target();
            return;
        }
            
        var dt = Time.deltaTime; 
        var look_rotation = Quaternion.LookRotation(direction.normalized);
        var new_rotation = Quaternion.Slerp(my_body.rotation, look_rotation, dt * my_rotation_speed);
        my_body.rotation = new_rotation;  // rotate
        my_body.position += my_move_speed * my_body.forward;  // move forward
    }

    private void Approach_target()
    {
        // tell current target it has been reached
        var bool_may_continue = my_target.Approach(this);

        if (!bool_may_continue)
        {
            // todo: call functions that decide what todo based on reason for having to stop
            return;
        }
        
        // identify next target
        my_last_target = my_target;
        my_target = my_target.Provide_next_target(bool_following_track_direction);
        
        // finish movement for this round
        Move_towards_target();
    }

    private void Search_new_target()
    {
        // see if there's a new target in your vision range
        
        // if not, turn around to your last track and drive in the other direction
        my_target = my_last_target.Suggest_next_target(bool_following_track_direction);  // target for turning around
        bool_following_track_direction = !bool_following_track_direction;  // reverse direction for tracks
    }
    
}

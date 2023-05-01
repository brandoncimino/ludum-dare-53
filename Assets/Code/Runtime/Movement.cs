using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement<T> : MonoBehaviour where T : TargetPoint
{
    
    public Transform my_body;
    public float my_move_speed = 0.02f;
    public float my_rotation_speed = 1;
    [field: NonSerialized]
    public virtual T? my_target { get; set; }
    private float target_accuracy = 0.7f;

    public TargetPoint my_hitch;
    public MovementTrailor? my_trailor;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move_to_target();
    }
    
    protected virtual void Move_to_target()
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
        var move_speed = Mathf.Min(dt * my_move_speed, direction.magnitude);
        my_body.position += move_speed * my_body.forward;  // move forward
    }
    protected virtual void Search_new_target()
    {
        Debug.Log("No target variable set, using Movement script only");
        throw new NotImplementedException("You shouldn't call this method unless you override it in a child class!");
    }

    protected virtual void Approach_target()
    {
        return;
    }
    
    public TargetPoint attach_trailor(MovementTrailor trailor)
    {
        if (my_trailor != null)
        {
            return my_trailor.attach_trailor(trailor);
        }

        my_trailor = trailor;
        return my_hitch;
    }

}

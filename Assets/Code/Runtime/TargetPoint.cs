using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class TargetPoint : MonoBehaviour
{

    public Transform my_body;
    private Random my_decision_maker = new Random();
    
    [CanBeNull] public List <TargetPoint> my_next = null;
    [CanBeNull] public List <TargetPoint> my_previous = null;
    public TrackPiece my_track;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 get_position()
    {
        return my_body.position;
    }

    public bool Approach(TruckMovement truck)
    {
        on_arrival(truck);
        return decide_if_truck_may_pass(truck);
    }

    [CanBeNull]
    public TargetPoint Provide_next_target(bool bool_following_track_direction)
    {
        var my_options = bool_following_track_direction ? my_next : my_previous;
        return my_options.Count == 0 ? null : my_options[my_decision_maker.Next(my_options.Count)];
    }

    public virtual TargetPoint Suggest_next_target(bool bool_following_track_direction)
    {
        return bool_following_track_direction ? my_track.my_start : my_track.my_stop;
    }
    
    public virtual bool? Say_hello()
    {
        return null;
    }

    private void on_arrival(TruckMovement truck)
    {
        // todo: in subclasses, this function can be overwritten for something to happen
        return;
    }

    private bool decide_if_truck_may_pass(TruckMovement truck)
    {
        // todo: in subclasses, this function ca nbe overwritten for something more to happen
        return true;
    }
    
    
    
}

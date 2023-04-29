using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using Random = System.Random;

public class TruckMovement : MonoBehaviour
{
    private Random my_decision_maker = new Random();

    [CanBeNull] public TargetPoint my_target;
    [CanBeNull] public TargetPoint my_last_target;
    public bool bool_following_track_direction = true;
    public TrackPiece my_track;
    
    public Transform my_body;
    public float my_move_speed = 0.02f;
    public float my_rotation_speed = 1;

    private float target_accuracy = 0.7f;

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
        
        Debug.Log("just outside if statement");
        
        if (my_target.my_track != my_track)
        {
            Debug.Log("inside if statement");
            var bool_new_orientation = my_target.Say_hello();
            Debug.Log(bool_new_orientation);
            
            bool_following_track_direction = bool_new_orientation ?? bool_following_track_direction;
        }
        my_track = my_target.my_track;

        // finish movement for this round
        Move_towards_target();
    }

    private void Search_new_target()
    {
        // see if there's a new target in your vision range
        var tracks_I_see = Look_for_track();
        var n_tracks = tracks_I_see.Count;
        
        if (n_tracks > 0)
        {
            // choose a track to appraoch
            var i_chosen = my_decision_maker.Next(n_tracks);
            var track = tracks_I_see[i_chosen];
            
            // find out which target I should approach
            var next_target = track.Say_hello(my_body.position);
            
            // set as new target
            bool_following_track_direction = next_target.Say_hello() ?? bool_following_track_direction;
            my_target = next_target;
            my_track = my_target.my_track;
            
            return;
        }

        // if you cannot see a track in front of you, turn back
        if (my_last_target != null)
        {
            // turn around to your last track and drive in the other direction
            my_target = my_last_target.Suggest_next_target(bool_following_track_direction);  // target for turning around
            bool_following_track_direction = !bool_following_track_direction;  // reverse direction for tracks
            my_track = my_target.my_track;  // keep track of my tracks
        }
        
        // if you don't know where you came from, drive in circles
        // todo: implement default direction
        
    }

    private List<TrackPiece> Look_for_track()
    {
        Collider[] what_I_see = Physics.OverlapSphere(my_body.position + 2.5f * my_body.forward, 3f);
        
        var my_choices = new List<TrackPiece> { };
        
        foreach (var collider in what_I_see)
        {
            var track = collider.gameObject.GetComponent<TrackPiece>();
            if (track != null)
            {
                my_choices.Add(track);
            }
        }

        return my_choices;
    }

    
}

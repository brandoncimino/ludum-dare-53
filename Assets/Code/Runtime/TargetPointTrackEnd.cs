using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointTrackEnd : TargetPointTrack
{
    public TargetPointTrackEnd my_significant_other;
    public bool bool_is_start;
    
    // Start is called before the first frame update
    void Start()
    {
        Find_friends();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override TargetPointTrack Suggest_next_target(bool bool_following_track_direction)
    {
        return my_significant_other;
    }

    public override bool? Say_hello()
    {
        return bool_is_start;
    }

    public void Find_friends()
    {
        var center = bool_is_start ? -2f * my_body.forward : 2f * my_body.forward;
        center += my_body.position;

        Collider[] what_I_see = Physics.OverlapSphere(center, 2f);
        
        foreach (var collider in what_I_see)
        {
            var friend = collider.gameObject.GetComponent<TargetPointTrackEnd>();
            if (friend != null)
            {
                Make_friend(friend);
                friend.Make_friend(this);
            }
        }
    }
    
    public void Make_friend(TargetPointTrackEnd friend)
    {
        if (friend == this) return;
        
        if (bool_is_start)
        {
            if (!my_previous.Contains(friend)) my_previous.Add(friend);
            return;
        }
        
        if (!my_next.Contains(friend)) my_next.Add(friend);
        
    }
}

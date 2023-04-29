using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointEnd : TargetPoint
{
    public TargetPointEnd my_significant_other;
    public bool bool_is_start;
    public List<TargetPointEnd> my_friends;
    
    // Start is called before the first frame update
    void Start()
    {
        Find_friends();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override TargetPoint Suggest_next_target(bool bool_following_track_direction)
    {
        return my_significant_other;
    }

    public override bool? Say_hello()
    {
        return bool_is_start;
    }

    public void Find_friends()
    {
        var center = my_body.position;
        
        if (bool_is_start)
        {
            center -= 2f * my_body.forward;
        }
        else
        {
            center += 2f * my_body.forward;
        }
        
        Collider[] what_I_see = Physics.OverlapSphere(center, 2f);
        my_friends = new List<TargetPointEnd> { };
        
        foreach (var collider in what_I_see)
        {
            var friend = collider.gameObject.GetComponent<TargetPointEnd>();
            if (friend != null)
            {
                my_friends.Add(friend);
            }
        }

        foreach (var friend in my_friends)
        {
            if (friend == this) continue;
            if (bool_is_start)
            {
                if (!my_previous.Contains(friend))
                {
                    my_previous.Add(friend);
                }
            }
            else
            {
                if (!my_next.Contains(friend))
                {
                    my_next.Add(friend);
                }   
            }


        }
        
        
    }
}

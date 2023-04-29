using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointEnd : TargetPoint
{
    public TargetPointEnd my_significant_other;
    public bool bool_is_start;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override TargetPoint Suggest_next_target(bool bool_following_track_direction)
    {
        return my_significant_other;
    }

    public bool Say_hello()
    {
        return bool_is_start;
    }
}

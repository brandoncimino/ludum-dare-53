using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPiece : MonoBehaviour
{
    public List<TargetPoint> my_lines;
    public TargetPointEnd my_start;
    public TargetPointEnd my_stop;
    
    // Start is called before the first frame update
    void Start()
    {
        // todo: ensure target lines in my_lines are in correct order
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TargetPointEnd Say_hello(Vector3 position)
    {
        var dist_to_start = (position - my_start.my_body.position).magnitude;
        var dist_to_stop = (position - my_stop.my_body.position).magnitude;

        return dist_to_start < dist_to_stop ? my_start : my_stop;
    }
    
    
}

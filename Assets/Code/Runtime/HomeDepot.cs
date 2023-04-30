using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeDepot : MonoBehaviour
{
    public float time_to_spawn;
    public float time_between_spawns = 5f;

    public Transform my_door;
    public GameObject my_prefab;
    
    // Start is called before the first frame update
    void Start()
    {
        time_to_spawn = Time.time + time_between_spawns;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > time_to_spawn)
        {
            Spawn();
            time_to_spawn += time_between_spawns;
        }
    }

    private void Spawn()
    {
        var new_truck = Instantiate(my_prefab, my_door.position, my_door.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow1 : MonoBehaviour
{
    //public GameObject[] waypoints;
    public UnityStandardAssets.Utility.WaypointCircuit circuit;
    int currenWP = 0;

    float speed = 3.0f, accuarcy = 1.0f, rotSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        //waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (circuit.Waypoints.Length == 0) return;

        Vector3 lookAtGoal = new Vector3(circuit.Waypoints[currenWP].transform.position.x,
                                        this.transform.position.y,
                                        circuit.Waypoints[currenWP].transform.position.z);
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotSpeed);

        if(direction.magnitude < accuarcy)
        {
            currenWP++;
            if(currenWP>=circuit.Waypoints.Length)
            {
                currenWP = 0;
            }
        }
        this.transform.Translate(0,0,speed*Time.deltaTime);
    }
}

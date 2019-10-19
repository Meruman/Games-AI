using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform goal;
    public float speed = 0.5f;
    float accuracy = 1.0f;
    //Para ajustar la velocidad de rotacion al principio
    public float rotSpeed = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
        //For rotation:
        Vector3 direction = lookAtGoal - this.transform.position;
        //this.transform.LookAt(lookAtGoal); no need if we want rotation to work properly
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime*rotSpeed);
        //If the animation movement is not smooth (Like a zombie), the next code wont work properly:
        //if (Vector3.Distance(this.transform.position, lookAtGoal) > accuracy)
        //   this.transform.Translate(0, 0, speed*Time.deltaTime);
    }
}

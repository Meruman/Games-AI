using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{

    public float speed = 3.0f, accuarcy = 1.0f, rotSpeed = 1.0f;
    public Transform player1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(player1.transform.position.x,
                                        player1.transform.position.y,
                                        player1.transform.position.z);
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotSpeed);

        if(direction.magnitude > accuarcy)
            this.transform.Translate(0,0,speed*Time.deltaTime);
    }
}

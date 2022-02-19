using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState : MonoBehaviour
{
    public bool dropped = false;
    private void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "drop")
        {
            dropped = true;
        }
    }
}

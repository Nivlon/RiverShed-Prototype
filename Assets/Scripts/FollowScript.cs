using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    [SerializeField]
    GameObject objectToFollow;

    [SerializeField]
    bool followXAxis, followYAxis;

    // Update is called once per frame
    void Update()
    {
        if(followXAxis) {
            transform.position = new Vector2(objectToFollow.transform.position.x,transform.position.y);
        }
        if(followYAxis) {
            transform.position = new Vector2(transform.position.x,objectToFollow.transform.position.y);
        }
    }
}

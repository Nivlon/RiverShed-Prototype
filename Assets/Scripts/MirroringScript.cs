using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirroringScript : MonoBehaviour
{
    [SerializeField]
   GameObject mirrorableObject, mirrorPointObject;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector2(mirrorableObject.transform.position.x,2*mirrorPointObject.transform.position.y - mirrorableObject.transform.position.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;

    [Range(0f,1f)]
    public float parallaxDistance;


    private Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =
            new Vector2(
                cameraTransform.position.x * parallaxDistance + startPosition.x,
                startPosition.y
                );
            ;
    }
}

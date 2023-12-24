using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraman : MonoBehaviour
{
    public Transform player;
    Vector3 offset;
    Transform camera;
    void Start()
    {
        camera = transform;
        offset = camera.position - player.position;
    }
    void Update()
    {
        camera.position = offset+player.position;
    }
}

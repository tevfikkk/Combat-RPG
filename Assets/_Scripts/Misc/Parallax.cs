using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxOffset = -.15f;

    private Camera cam;
    private Vector2 startPos; // starting position of the background
    private Vector2 travel => (Vector2)cam.transform.position - startPos; // how far the camera has moved since the start

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = startPos + travel * parallaxOffset;
    }
}

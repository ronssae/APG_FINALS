using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform background;
    [SerializeField] private GameObject[] Spikes;
    [SerializeField] private GameObject objectFollow;
    private float xOffset = 12.6f;

    void Start()
    {
        if (!objectFollow) objectFollow = GameObject.Find("SpiderMan");
        Spikes = GameObject.FindGameObjectsWithTag("Obstacle");
    }

    void Update()
    {
        var cameraPosition = mainCamera.transform.position;
        var bgPosition = background.transform.position;
        var spike1Position = Spikes[0].transform.position;
        var spike2Position = Spikes[1].transform.position;

        cameraPosition.x = objectFollow.transform.position.x + xOffset;
        bgPosition.x = objectFollow.transform.position.x + xOffset;
        spike1Position.x = objectFollow.transform.position.x + xOffset;
        spike2Position.x = objectFollow.transform.position.x + xOffset;

        mainCamera.transform.position = cameraPosition;
        background.transform.position = bgPosition;
        Spikes[0].transform.position = spike1Position;
        Spikes[1].transform.position = spike2Position;
    }
}

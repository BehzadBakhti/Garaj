using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public SmoothOrbitCam camera = null;
    public float cameraDistance = 5;
    public float cameraZoomDuration = 3;

    void Start()
    {
        DOTween.To(() => camera.distance, (z) => camera.distance = z, cameraDistance, cameraZoomDuration).SetEase(Ease.OutCubic);
    }

    public void StartGame()
    {

    }
}

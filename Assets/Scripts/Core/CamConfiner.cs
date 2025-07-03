using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Collections;

public class CinemachineConfinerSetter : MonoBehaviour
{

    private GameObject bounds;
    private CinemachineVirtualCamera virtualCamera;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayedConfinerSetup());
    }

    private IEnumerator DelayedConfinerSetup()
    {
        yield return null; // wait one frame

        bounds = GameObject.FindGameObjectWithTag("Bounds");

        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (virtualCamera != null && bounds != null)
        {
            var confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
            PolygonCollider2D poly = bounds.GetComponent<PolygonCollider2D>();
            if (confiner != null && poly != null)
            {
                confiner.m_BoundingShape2D = poly;
                confiner.InvalidateCache();
            }
        }
    }
}




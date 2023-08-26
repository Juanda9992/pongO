using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Scaling : MonoBehaviour
{
    private float defaultAspectRadio = 16f/9f;
    [SerializeField] private Camera myCamera;
    private float currentOrtSize = 5f;
    private float currentAspectRadio;
    // Start is called before the first frame update
    void Start()
    {
        UpdateAspectRadio();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateAspectRadio();
        AdaptFov();
    }

    private void UpdateAspectRadio()
    {
        currentAspectRadio = (float)Screen.width / Screen.height;
    }

    private void AdaptFov()
    {
        float targetOrthographicSize = currentOrtSize * (defaultAspectRadio / currentAspectRadio);
        myCamera.orthographicSize = targetOrthographicSize;
    }
}

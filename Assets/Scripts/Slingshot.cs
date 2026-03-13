using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Incribed")]
    public GameObject projLinePrefab;
    [SerializeField] private LineRenderer rubber;
    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;
    [SerializeField] private Configuration configuration;
    private Transform ballPrefab; 
    Vector3 ballPosition = Vector3.zero;
    Vector3 GetMousePositionInWorld()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePositionInWorld.z -= Camera.main.transform.position.z;
        if(mousePositionInWorld.magnitude > configuration.Radius)
        {
            mousePositionInWorld.Normalize();
            mousePositionInWorld *= configuration.Radius;
        }
        return mousePositionInWorld;
    }

    void Start()
    {
        rubber.SetPosition(0, firstPoint.position);
        rubber.SetPosition(2, secondPoint.position);

    }
    
    void Update()
    {
        if (FollowCam.currentView != FollowCam.eView.slingshot) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(mouseWorldPos, transform.position) <= configuration.Radius * 2)
            {
                ballPrefab = Instantiate(configuration.BallPrefab).transform;
            }
        }
        if (Input.GetMouseButton(0) && ballPrefab != null)
        {
            ballPosition = GetMousePositionInWorld();
            ballPrefab.position = ballPosition;
            rubber.SetPosition(1, ballPosition);
        }
        if (Input.GetMouseButtonUp(0) && ballPrefab != null)
        {
            Rigidbody rigidbody = ballPrefab.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.velocity = -ballPosition * configuration.Power;
            FollowCam.SWITCH_VIEW(FollowCam.eView.slingshot);
            FollowCam.POI = ballPrefab.gameObject;
            Instantiate<GameObject>(projLinePrefab, ballPrefab.gameObject.transform);
            MissionDemolition.SHOT_FIRED();

            // Sound calls
            SoundManager.PLAY_SNAP();
            SoundManager.START_WHIR();

            ballPrefab = null;
        }
    }
}
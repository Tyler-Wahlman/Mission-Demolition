using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static private FollowCam S;
    static public GameObject POI;
    public enum eView {none, slingshot, castle, both};

    [Header("Inscribed")]
    public float easing = 0.05f;
    public float zoomEasing = 0.2f;
    public GameObject viewBothGo;

    [Header("Dynamic")]
    public float camZ;
    public Vector2 minXY;

    private Camera cam;
    public eView nextView = eView.slingshot;
    
    static public eView currentView = eView.slingshot;

    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
        cam = Camera.main;
        minXY.x = 10;
        minXY.y = -4;
    }

    void FixedUpdate()
    {
        Vector3 destination = minXY;

        if (POI != null)
        {
            Rigidbody poiRigid = POI.GetComponent<Rigidbody>();
            if ((poiRigid != null) && poiRigid.IsSleeping())
            {
                POI = null;
                cam.orthographicSize = 10;
            }
        }
        if (POI != null)
        {
            destination = POI.transform.position;
            if (destination.y > 1f)
            {
                float targetSize = destination.y + 10;
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, zoomEasing);
            }
        }

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y + 10, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        transform.position = destination;
    }
    public void SwitchView(eView newView)
    {
        if(newView == eView.none)
        {
            newView = nextView;
        }
        currentView = newView;
        switch (newView)
        {
            case eView.slingshot:
                POI = null;
                nextView = eView.castle;
                cam.orthographicSize = 10;
                break;
            case eView.castle:
                POI = MissionDemolition.GET_CASTLE();
                nextView = eView.both;
                cam.orthographicSize = 10;
                break;
            case eView.both:
                POI = viewBothGo;
                nextView = eView.slingshot;
                break;
        }
    }
    public void SwitchView()
    {
        SwitchView(eView.none);
    }

    static public void SWITCH_VIEW(eView newView)
    {
        if (S == null) return;
        S.SwitchView(newView);
    }


}
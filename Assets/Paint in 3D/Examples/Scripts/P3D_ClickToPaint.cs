using System;
using UnityEngine;
using VRTK;

#if UNITY_EDITOR
[UnityEditor.CanEditMultipleObjects]
[UnityEditor.CustomEditor(typeof(P3D_ClickToPaint))]
public class P3D_ClickToPaint_Editor : P3D_Editor<P3D_ClickToPaint>
{
    protected override void OnInspector()
    {
        DrawDefault("Requires");

        DrawDefault("LayerMask");

        DrawDefault("GroupMask");

        DrawDefault("Paint");

        DrawDefault("Brush");
    }
}
#endif

// This script allows you to paint the texture under the current mouse position
// NOTE: This requires the paint targets have the P3D_Paintable component
public class P3D_ClickToPaint : MonoBehaviour
{
    public enum NearestOrAll
    {
        Nearest,
        All
    }

    [Tooltip("The key that must be held down to mouse look")]
    public KeyCode Requires = KeyCode.Mouse0;

    [Tooltip("The GameObject layers you want to be able to paint")]
    public LayerMask LayerMask = -1;

    [Tooltip("The paintable texture groups you want to be able to paint")]
    public P3D_GroupMask GroupMask = -1;

    [Tooltip("Which surfaces it should hit")]
    public NearestOrAll Paint;

    [Tooltip("The settings for the brush we will paint with")]
    public P3D_Brush Brush;
    public static P3D_ClickToPaint P3D_;
    private SteamVR_TrackedObject trackedObj;
    public static LineRenderer line;

    private void Start()
    {
        P3D_ = this;
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        line = gameObject.GetComponent<LineRenderer>();

    }
    private void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        Ray ray = new Ray(transform.position, transform.forward);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    Debug.DrawLine(ray.origin, hit.point, Color.green);

        //}
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.forward * 50);
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            //»æ»­
            P3D_Paintable.ScenePaintBetweenNearest(Brush, line.GetPosition(0), line.GetPosition(1), LayerMask, GroupMask);
            Vector3 vector = line.GetPosition(0);
        }

    }
    /// <summary>
    /// ¸ü»»ÑÕÉ«
    /// </summary>
    /// <param name="brushColor">ÑÕÉ«</param>
    public void SetColor(Color brushColor)
    {
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.startColor = lr.endColor = brushColor;
        line.material.color = brushColor;
        Brush.Color = brushColor;
        HandLeft.handLeft.img.color = brushColor;
    }
}

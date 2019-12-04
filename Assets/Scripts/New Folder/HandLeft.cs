using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HandLeft : MonoBehaviour
{
    public static HandLeft handLeft;
    private SteamVR_TrackedObject trackedObj;
    private P3D_Brush Brush;
    public List<Color> BrushColor;
    private int colorIdx;
    public Image img; 
    // Use this for initialization
    void Start()
    {
        handLeft = this;
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 cc = device.GetAxis();

            float touchangle = JimmyUtility.VectorAngle(new Vector2(1, 0), cc);
            int touchPos = JimmyUtility.AngleToPosition(touchangle);
            print("touched:" + touchPos);
            if (touchPos == 3)
            {
                colorIdx--;
                if (colorIdx < 0)
                    colorIdx = BrushColor.Count - 1;
            }
            else if (touchPos == 4)
            {
                colorIdx++;
                if (colorIdx > BrushColor.Count - 1)
                    colorIdx = 0;
            }
            if (touchPos == 3 || touchPos == 4)
            {
                P3D_ClickToPaint.P3D_.SetColor(BrushColor[colorIdx]);
            }


        } else if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)) {

            SceneManager.LoadScene("BrushDemoWithUIVRTK1");

        }

    }
    
}

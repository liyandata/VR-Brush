using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;
/// <summary>
/// 挂载到手柄控制器上(Controller (right))
/// </summary>
public class HandControllerLeft : MonoBehaviour
{
    //手柄
    SteamVR_TrackedObject trackedObj;
  

    void Awake()
    {
        //获取手柄脚本组件
        trackedObj =GetComponent<SteamVR_TrackedObject>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //获取手柄输入
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        //此处可以换其他的函数触发GetPress/GetTouch /GetPressUp GetTouchDown/GetTouchUp/GetAxis
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //  Debug.Log("按下圆盘");
            Vector2 cc = device.GetAxis();
            // 圆盘分成上下左右 
           // float jiaodu = VectorAngle(new Vector2(1, 0), cc);
            //if (jiaodu > 45 && jiaodu < 135)
            //{
            //    Debug.Log("下");
            //}
            //if (jiaodu < -45 && jiaodu > -135)
            //{
            //    Debug.Log("上");
            //}
            //if ((jiaodu < 180 && jiaodu > 135) || (jiaodu < -135 && jiaodu > -180))
            //{
            //    Debug.Log("左");
            //}
            //if ((jiaodu > 0 && jiaodu < 45) || (jiaodu > -45 && jiaodu < 0))
            //{
            //    Debug.Log("右");
            //}


            float touchangle = JimmyUtility.VectorAngle(new Vector2(1, 0), cc);
            int touchPos = JimmyUtility.AngleToPosition(touchangle);
            print("touched:" + touchPos);
            if (touchPos == 3)
            {
                JimmyGao.BrushManager.colorIdx--;
                if (JimmyGao.BrushManager.colorIdx < 0)
                    JimmyGao.BrushManager.colorIdx = JimmyGao.BrushManager.Instance.BrushColor.Count - 1;
            }
            else if (touchPos == 4)
            {
                JimmyGao.BrushManager.colorIdx++;
                if (JimmyGao.BrushManager.colorIdx > JimmyGao.BrushManager.Instance.BrushColor.Count - 1)
                    JimmyGao.BrushManager.colorIdx = 0;
            }
            if (touchPos == 3 || touchPos == 4)
            {
                JimmyGao.BrushManager.Instance.SetColor(JimmyGao.BrushManager.Instance.BrushColor[JimmyGao.BrushManager.colorIdx]);
            }

            if (touchPos == 1)
            {
                JimmyGao.BrushManager.Instance.BrushObj.transform.localScale *= 2;
                LineRenderer lr = JimmyGao.BrushManager.Instance.InitLineObj.GetComponent<LineRenderer>();
                lr.widthMultiplier *= 2;
            }
            if (touchPos == 2)
            {
                JimmyGao.BrushManager.Instance.BrushObj.transform.localScale /= 2;
                LineRenderer lr = JimmyGao.BrushManager.Instance.InitLineObj.GetComponent<LineRenderer>();
                lr.widthMultiplier /= 2;
            }
        }

        else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("按下扳机键");
            var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
            SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse(5000);


        }
        else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("按下手柄侧键");
        }
        else if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Debug.Log("按下手柄菜单键");

            SceneManager.LoadScene("BrushDemoWithUIVRTK2");
        }
        else if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Debug.Log("按下手柄菜单键");
        }
    }
     //方向圆盘最好配合这个使用 圆盘的.GetAxis()会检测返回一个二位向量，可用角度划分圆盘按键数量 
    //这个函数输入两个二维向量会返回一个夹角 180 到 -180 
    //float VectorAngle(Vector2 from, Vector2 to)
    //{ 
    //    float angle; 
    //    Vector3 cross = Vector3.Cross(from, to); 
    //    angle = Vector2.Angle(from, to); 
    //    return cross.z > 0 ? -angle : angle; 
    //} 

}

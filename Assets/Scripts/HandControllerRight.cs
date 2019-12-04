using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;
using VRTK.Highlighters;
using VRTK.SecondaryControllerGrabActions;
/// <summary>
/// 挂载到手柄控制器上(Controller (right))
/// </summary>
public class HandControllerRight : MonoBehaviour
{
    //手柄
    SteamVR_TrackedObject trackedObj;
    public GameObject GrabObj;



    void Awake()
    {
        //获取手柄脚本组件
        trackedObj = GetComponent<SteamVR_TrackedObject>();
      

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
            float jiaodu = VectorAngle(new Vector2(1, 0), cc);
            if (jiaodu > 45 && jiaodu < 135)
            {
                Debug.Log("下");
            }
            if (jiaodu < -45 && jiaodu > -135)
            {
                Debug.Log("上");
            }
            if ((jiaodu < 180 && jiaodu > 135) || (jiaodu < -135 && jiaodu > -180))
            {
                Debug.Log("左");
            }
            if ((jiaodu > 0 && jiaodu < 45) || (jiaodu > -45 && jiaodu < 0))
            {
                Debug.Log("右");
            }

        }
        else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("按下扳机键");
            var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
            SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse(5000);

            /*
            GameObject line = new GameObject ();

            currLine = line.AddComponent<LineRenderer> ();

            currLine.startWidth = 0.01f;
            currLine.endWidth = 0.01f;
            */

            GameObject line = Instantiate(JimmyGao.BrushManager.Instance.InitLineObj);
            JimmyGao.BrushManager.currLine = line.GetComponent<LineRenderer>();
            JimmyGao.BrushManager.segNum = 1;
            JimmyGao.BrushManager.currLine.positionCount = JimmyGao.BrushManager.segNum;
            JimmyGao.BrushManager.currLine.SetPosition(JimmyGao.BrushManager.segNum - 1, JimmyGao.BrushManager.Instance.BrushObj.transform.position);
            JimmyGao.BrushManager.lastPos = JimmyGao.BrushManager.Instance.BrushObj.transform.position;
            JimmyGao.BrushManager.CurrentState = 1;

            //Grab 
            line.AddComponent<VRTK_InteractableObject>().touchHighlightColor = Color.green;
            line.AddComponent<VRTK_OutlineObjectCopyHighlighter>().thickness=0.3f;
            /* 碰撞器**/



            JimmyGao.BrushManager.Instance.GoList.Add(line);

        }
        else if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
           // Debug.Log("长按扳机键");

            //震动
            //var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
            //SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse(5000);

            Vector3 currentPos = JimmyGao.BrushManager.Instance.BrushObj.transform.position;

            if (Vector3.Distance(JimmyGao.BrushManager.lastPos, currentPos) > 0.02)
            {
                JimmyGao.BrushManager.segNum++;
                JimmyGao.BrushManager.currLine.positionCount = JimmyGao.BrushManager.segNum;
                JimmyGao.BrushManager.currLine.SetPosition(JimmyGao.BrushManager.segNum - 1, JimmyGao.BrushManager.Instance.BrushObj.transform.position);
                JimmyGao.BrushManager.lastPos = currentPos;

            }
        }
        else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("扳机键抬起");

            JimmyGao.BrushManager.CurrentState = 0;

        }
        else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("按下手柄侧键");

            if (GrabObj!=null)
            {
                GrabObj.transform.parent = this.transform; 
            }
        }
        else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("抬起手柄侧键");
            if (GrabObj!=null)
            {
                GrabObj.transform.parent = null; 
            }
        }
        else if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Debug.Log("按下手柄菜单键");
            //后退一步
            if (JimmyGao.BrushManager.Instance.GoList.Count > 0)
            {
                Destroy(JimmyGao.BrushManager.Instance.GoList[JimmyGao.BrushManager.Instance.GoList.Count - 1]);
                JimmyGao.BrushManager.Instance.GoList.Remove(JimmyGao.BrushManager.Instance.GoList[JimmyGao.BrushManager.Instance.GoList.Count - 1]);
            }
        }
       
    }

    //方向圆盘最好配合这个使用 圆盘的.GetAxis()会检测返回一个二位向量，可用角度划分圆盘按键数量 
    //这个函数输入两个二维向量会返回一个夹角 180 到 -180 
    float VectorAngle(Vector2 from, Vector2 to)
    {
        float angle;
        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }


   

}

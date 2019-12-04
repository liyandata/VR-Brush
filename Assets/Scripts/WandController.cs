
using System.Collections;
using UnityEngine;

public class WandController : MonoBehaviour
{

    private Valve.VR.EVRButtonId gripButtion = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButtion = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public GameObject pickUp;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObject.index); } }

    private SteamVR_TrackedObject trackedObject;

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
    void Update()
    {
        if (controller == null)
        {
            Debug.Log("controller 不存在");
        }

        if (controller.GetPressDown(triggerButtion) && pickUp != null)
        {
            Debug.Log("pressDown Trigger");
            pickUp.transform.parent = this.transform;
        }

        if (controller.GetPressUp(triggerButtion) && pickUp != null)
        {
            Debug.Log("Press Up Trigger");
            pickUp.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.tag=="GrabObj")
        {
            Debug.Log("进入碰撞");
            pickUp = other.gameObject; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GrabObj")
        {
            Debug.Log("离开碰撞");
            pickUp = null;
        }
    }
}
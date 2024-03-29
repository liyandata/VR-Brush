﻿namespace VRTK.Examples
{
    using UnityEngine;

    public class Whirlygig : VRTK_InteractableObject
    {
        float spinSpeed = 0f;
        Transform rotator;

        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);
            spinSpeed = 360f;
        }

        public override void StopUsing(GameObject usingObject)
        {
            base.StopUsing(usingObject);
            spinSpeed = 0f;
        }

        protected void Start()
        {
            rotator = transform.Find("Capsule");
        }

        protected override void Update()
        {
            base.Update();
            rotator.transform.Rotate(new Vector3(spinSpeed * Time.deltaTime, 0f, 0f));
            print("asdfasfas55s6f464f6a5df");
        }
    }
}
using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = Input.GetAxis("Horizontal_K1");
            float v = Input.GetAxis("Vertical_K1");

			float handbrake = Input.GetAxis("Fire_K1");
            m_Car.Move(h, v, v, handbrake);
        }
    }
}

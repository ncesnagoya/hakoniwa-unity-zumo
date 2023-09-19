using Hakoniwa.PluggableAsset.Assets.Robot.Parts;
using Hakoniwa.PluggableAsset.Communication.Connector;
using Hakoniwa.PluggableAsset.Communication.Pdu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hakoniwa.PluggableAsset.Assets.Robot.Zumo
{
    public class ZumoImu : MonoBehaviour, IRobotPartsSensor
    {
        private GameObject root;
        private GameObject sensor;
        private string root_name;
        private PduIoConnector pdu_io;
        private IPduWriter pdu_writer;

        private float deltaTime;
        private Vector3 prev_velocity = Vector3.zero;
        private ArticulationBody my_body;
        private Vector3 prev_angle = Vector3.zero;
        private Vector3 delta_angle = Vector3.zero;


        public RosTopicMessageConfig[] getRosConfig()
        {
            return null;
        }

        public void Initialize(object obj)
        {
            GameObject tmp = null;
            try
            {
                tmp = obj as GameObject;
            }
            catch (InvalidCastException e)
            {
                Debug.LogError("Initialize error: " + e.Message);
                return;
            }

            if (this.root == null)
            {
                this.root = tmp;
                this.root_name = string.Copy(this.root.transform.name);
                this.pdu_io = PduIoConnector.Get(root_name);
                if (this.pdu_io == null)
                {
                    throw new ArgumentException("can not found pdu_io:" + root_name);
                }
                var pdu_writer_name = root_name + "_sensorPdu";
                this.pdu_writer = this.pdu_io.GetWriter(pdu_writer_name);
                if (this.pdu_writer == null)
                {
                    throw new ArgumentException("can not found pdu_reader:" + pdu_writer_name);
                }
                this.sensor = this.gameObject;
                this.my_body = this.GetComponentInChildren<ArticulationBody>();
                if (this.my_body == null)
                {
                    throw new ArgumentException("ZumoImu can not find RigidBody: " + this.sensor.name);
                }
                this.deltaTime = Time.fixedDeltaTime;
            }
        }

        public bool isAttachedSpecificController()
        {
            return false;
        }

        public void UpdateSensorValues()
        {
            this.UpdateSensorData(this.pdu_writer.GetWriteOps().Ref("imu"));
        }
        private void UpdateSensorData(Pdu pdu)
        {
            //orientation
            UpdateOrientation(pdu.Ref("mag"));

            //angular_velocity
            UpdateAngularVelocity(pdu.Ref("gyro"));

            //linear_acceleration
            UpdateLinearAcceleration(pdu.Ref("acc"));
        }

        /*
         * ROS (right-handed coordinate system) transformation:
         *  ROS.x =  Unity.z
         *  ROS.y = -Unity.x
         *  ROS.z =  Unity.y
         * 
         * For acceleration data, it's necessary to consider the scale of the Zumo's dimensions,
         * so the velocity of the target object (my_body.velocity) needs to be divided by the scale (ZumoModelController.scale).
         */
        private void UpdateLinearAcceleration(Pdu pdu)
        {
            // Obtain the velocity of the target object in its local coordinate system (relative coordinates), not in world coordinates.
            Vector3 current_velocity = this.sensor.transform.InverseTransformDirection(my_body.velocity);
            // scale conversion
            current_velocity = current_velocity / ZumoModelController.scale;
            Vector3 acceleration = (current_velocity - prev_velocity) / deltaTime;
            this.prev_velocity = current_velocity;
            this.delta_angle = this.GetCurrentEulerAngle() - prev_angle;
            this.prev_angle = this.GetCurrentEulerAngle();
            //gravity element
            acceleration += transform.InverseTransformDirection(Physics.gravity);

            pdu.SetData("x", (double)acceleration.z);
            pdu.SetData("y", (double)-acceleration.x);
            pdu.SetData("z", (double)acceleration.y);
        }
        private void UpdateOrientation(Pdu pdu)
        {
            pdu.SetData("x", (double)this.sensor.transform.rotation.eulerAngles.z);
            pdu.SetData("y", (double)-this.sensor.transform.rotation.eulerAngles.x);
            pdu.SetData("z", (double)this.sensor.transform.rotation.eulerAngles.y);
        }
        private void UpdateAngularVelocity(Pdu pdu)
        {
            pdu.SetData("x", (double)my_body.angularVelocity.z);
            pdu.SetData("y", (double)-my_body.angularVelocity.x);
            pdu.SetData("z", (double)my_body.angularVelocity.y);
        }
        private Vector3 GetCurrentEulerAngle()
        {
            return this.sensor.transform.rotation.eulerAngles;
        }
    }

}

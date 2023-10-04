using Hakoniwa.PluggableAsset.Assets.Robot.Parts;
using Hakoniwa.PluggableAsset.Communication.Connector;
using Hakoniwa.PluggableAsset.Communication.Pdu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hakoniwa.PluggableAsset.Assets.Robot.Zumo
{
    enum ZumoMotorType
    {
        MotorType_Left = 0,
        MotorType_Right,
        MotorType_Num
    }

    public class ZumoMotorController : MonoBehaviour, IRobotPartsController
    {
        public float scale = 0.1f;
        private GameObject root;
        private string root_name;
        private PduIoConnector pdu_io;
        private IPduReader pdu_reader;
        private IRobotPartsMotor[] front_motors = new IRobotPartsMotor[(int)ZumoMotorType.MotorType_Num];
        private IRobotPartsMotor[] back_motors = new IRobotPartsMotor[(int)ZumoMotorType.MotorType_Num];

        public int motor_force = 20;
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
                var pdu_reader_name = root_name + "_" + "actuatorPdu";
                this.pdu_reader = this.pdu_io.GetReader(pdu_reader_name);
                if (this.pdu_reader == null)
                {
                    throw new ArgumentException("can not found pdu_reader:" + pdu_reader_name);
                }
                this.front_motors[(int)ZumoMotorType.MotorType_Left] = this.transform.Find("Front").Find("Left").GetComponentInChildren<IRobotPartsMotor>();
                this.front_motors[(int)ZumoMotorType.MotorType_Right] = this.transform.Find("Front").Find("Right").GetComponentInChildren<IRobotPartsMotor>();
                this.back_motors[(int)ZumoMotorType.MotorType_Left] = this.transform.Find("Back").Find("Left").GetComponentInChildren<IRobotPartsMotor>();
                this.back_motors[(int)ZumoMotorType.MotorType_Right] = this.transform.Find("Back").Find("Right").GetComponentInChildren<IRobotPartsMotor>();
                this.front_motors[(int)ZumoMotorType.MotorType_Left].SetForce(motor_force);
                this.front_motors[(int)ZumoMotorType.MotorType_Right].SetForce(motor_force);
                this.back_motors[(int)ZumoMotorType.MotorType_Left].SetForce(motor_force);
                this.back_motors[(int)ZumoMotorType.MotorType_Right].SetForce(motor_force);
            }
            for (int i = 0; i < this.front_motors.Length; i++)
            {
                if (this.front_motors[i] != null)
                {
                    this.front_motors[i].Initialize(root);
                }
            }
            for (int i = 0; i < this.back_motors.Length; i++)
            {
                if (this.back_motors[i] != null)
                {
                    this.back_motors[i].Initialize(root);
                }
            }
        }

        public void DoControl()
        {
            var left_power = this.pdu_reader.GetReadOps().Refs("motors")[(int)ZumoMotorType.MotorType_Left].GetDataInt32("power");
            var right_power = this.pdu_reader.GetReadOps().Refs("motors")[(int)ZumoMotorType.MotorType_Right].GetDataInt32("power");


            this.front_motors[(int)ZumoMotorType.MotorType_Left].SetTargetVelicty(this.scale * (float)left_power);
            this.front_motors[(int)ZumoMotorType.MotorType_Right].SetTargetVelicty(this.scale * (float)right_power);

            this.back_motors[(int)ZumoMotorType.MotorType_Left].SetTargetVelicty(this.scale * (float)left_power);
            this.back_motors[(int)ZumoMotorType.MotorType_Right].SetTargetVelicty(this.scale * (float)right_power);
        }

        public RosTopicMessageConfig[] getRosConfig()
        {
            return null;
        }
    }

}

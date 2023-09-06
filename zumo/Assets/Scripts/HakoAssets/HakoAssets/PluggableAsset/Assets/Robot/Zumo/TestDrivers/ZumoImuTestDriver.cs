using Hakoniwa.PluggableAsset.Assets.Robot.Parts;
using Hakoniwa.PluggableAsset.Communication.Connector;
using Hakoniwa.PluggableAsset.Communication.Pdu;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Hakoniwa.PluggableAsset.Assets.Robot.Zumo
{
    public class ZumoImuTestDriver : MonoBehaviour, IRobotPartsSensor
    {
        private GameObject root;
        private PduIoConnector pdu_io;
        private IPduWriter pdu_writer;
        private string root_name;
        public string topic_name = "sensor";
        public string roboname = "ZumoRoboModel";
        private TextMeshPro testMeshPro;

        public RosTopicMessageConfig[] getRosConfig()
        {
            return new RosTopicMessageConfig[0];
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
                this.pdu_io = PduIoConnector.Get(roboname);
                if (this.pdu_io == null)
                {
                    throw new ArgumentException("can not found pdu_io:" + root_name);
                }
                var pdu_writer_name = roboname + "_" + this.topic_name + "Pdu";
                this.pdu_writer = this.pdu_io.GetWriter(pdu_writer_name);
                if (this.pdu_writer == null)
                {
                    throw new ArgumentException("can not found pdu_writer:" + pdu_writer_name);
                }
                this.testMeshPro = this.GetComponentInChildren<TextMeshPro>();
                if (this.testMeshPro == null)
                {
                    throw new ArgumentException("can not found TextMeshPro");
                }

            }
        }

        private Vector3 acc;
        private Vector3 mag;
        private Vector3 gyro;
        public bool isAttachedSpecificController()
        {
            return false;
        }

        public void UpdateSensorValues()
        {
            this.acc = new Vector3(
                (float)this.pdu_writer.GetReadOps().Ref("imu").Ref("acc").GetDataFloat64("x"),
                (float)this.pdu_writer.GetReadOps().Ref("imu").Ref("acc").GetDataFloat64("y"),
                (float)this.pdu_writer.GetReadOps().Ref("imu").Ref("acc").GetDataFloat64("z")
            );
            this.mag = new Vector3(
                (float)this.pdu_writer.GetReadOps().Ref("imu").Ref("mag").GetDataFloat64("x"),
                (float)this.pdu_writer.GetReadOps().Ref("imu").Ref("mag").GetDataFloat64("y"),
                (float)this.pdu_writer.GetReadOps().Ref("imu").Ref("mag").GetDataFloat64("z")
            );
            this.gyro = new Vector3(
                (float)this.pdu_writer.GetReadOps().Ref("imu").Ref("gyro").GetDataFloat64("x"),
                (float)this.pdu_writer.GetReadOps().Ref("imu").Ref("gyro").GetDataFloat64("y"),
                (float)this.pdu_writer.GetReadOps().Ref("imu").Ref("gyro").GetDataFloat64("z")
            );

        }

        void Update()
        {
            string formattedString = string.Format("mag ( {0:+ 000.00;- 000.00;+ 000.00}, {1:+ 000.00;- 000.00;+ 000.00}, {2:+ 000.00;- 000.00;+ 000.00} )", this.mag.x, this.mag.y, this.mag.z);
            this.testMeshPro.text = formattedString;
            this.testMeshPro.text += "\n";

            formattedString = string.Format("gyro ( {0:+ 000.00;- 000.00;+ 000.00}, {1:+ 000.00;- 000.00;+ 000.00}, {2:+ 000.00;- 000.00;+ 000.00} )", this.gyro.x, this.gyro.y, this.gyro.z);
            this.testMeshPro.text += formattedString;
            this.testMeshPro.text += "\n";

            formattedString = string.Format("acc ( {0:+ 000.00;- 000.00;+ 000.00}, {1:+ 000.00;- 000.00;+ 000.00}, {2:+ 000.00;- 000.00;+ 000.00} )", this.acc.x, this.acc.y, this.acc.z);
            this.testMeshPro.text += formattedString;

        }
    }

}

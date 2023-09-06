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
    public class ZumoReflectSensorTestDriver : MonoBehaviour, IRobotPartsSensor
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

        public bool isAttachedSpecificController()
        {
            return false;
        }
        public uint value;
        public int sensor_id = 0;
        public void UpdateSensorValues()
        {
            this.value = this.pdu_writer.GetReadOps().Refs("line_sensors")[this.sensor_id].GetDataUInt32("brightness");
        }

        void Update()
        {
            this.testMeshPro.text = "[" + this.sensor_id + "]=" + this.value;
        }
    }

}

using Hakoniwa.PluggableAsset.Assets.Robot.Parts;
using Hakoniwa.PluggableAsset.Communication.Connector;
using Hakoniwa.PluggableAsset.Communication.Pdu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hakoniwa.PluggableAsset.Assets.Robot.Zumo
{
    public class ZumoLed : MonoBehaviour, IRobotPartsController
    {
        private GameObject root;
        private string root_name;
        private PduIoConnector pdu_io;
        private IPduReader pdu_reader;
        public Material onMaterial;
        public Material offMaterial;
        public string led_name = "green";
        private Renderer my_renderer;


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
                my_renderer = GetComponent<Renderer>();
            }

        }
        public void DoControl()
        {
            var green = this.pdu_reader.GetReadOps().Ref("led").GetDataBool("green");
            if (green)
            {
                this.my_renderer.material = this.onMaterial;
            }
            else
            {
                this.my_renderer.material = this.offMaterial;
            }
        }


        public RosTopicMessageConfig[] getRosConfig()
        {
            return null;
        }
    }

}

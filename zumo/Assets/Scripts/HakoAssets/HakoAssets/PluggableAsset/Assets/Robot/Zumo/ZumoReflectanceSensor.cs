using Hakoniwa.PluggableAsset.Assets.Robot.Parts;
using Hakoniwa.PluggableAsset.Communication.Connector;
using Hakoniwa.PluggableAsset.Communication.Pdu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hakoniwa.PluggableAsset.Assets.Robot.Zumo
{
    public class ZumoReflectanceSensor : MonoBehaviour, IRobotPartsSensor
    {
        private GameObject root;
        private string root_name;
        private PduIoConnector pdu_io;
        private IPduWriter pdu_writer;
        private Camera dispCamera;
        private Texture2D targetTexture;
        public int sensor_id = 0;

        public RosTopicMessageConfig[] getRosConfig()
        {
            return null;
        }
        /*
         * camera
         */
        public static int pixel_x = 32;
        public static int pixel_y = 32;
        public static float fieldOfView = 8;
        public static int deepness = 24;
        public static float farClipPlane = 0.2f;
        public static float nearClipPlane = 0.03f;

        /*
         * light
         */
        public static float lightRange = 0.2f;
        public static float lightIntencity = 1.0f;
        public static float lightSpotAngle = 10f;

        private float sensor_rgb_max = 255;

        public void Initialize(System.Object obj)
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
                this.dispCamera = this.GetComponentInChildren<Camera>();
                if (this.dispCamera == null)
                {
                    throw new ArgumentException("can not found camera");
                }

                this.dispCamera.sensorSize = new Vector3(pixel_x, pixel_y);
                this.dispCamera.fieldOfView = fieldOfView;
                this.dispCamera.farClipPlane = farClipPlane;
                this.dispCamera.nearClipPlane = nearClipPlane;
                this.dispCamera.targetTexture = new RenderTexture(pixel_x, pixel_y, deepness, RenderTextureFormat.BGRA32);
                var tex = dispCamera.targetTexture;
                targetTexture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);


                var light = this.GetComponentInChildren<Light>();
                if (light == null)
                {
                    throw new ArgumentException("can not found spot light");
                }
                light.range = lightRange;
                light.spotAngle = lightSpotAngle;
                light.intensity = lightIntencity;

            }
        }

        public bool isAttachedSpecificController()
        {
            return false;
        }

        private float GetLightValue(Texture2D tex)
        {
            var cols = tex.GetPixels();

            Color avg = new Color(0, 0, 0);
            foreach (var col in cols)
            {
                avg += col;
            }
            avg /= cols.Length;
            var rgb = avg;
            var rgb_r = avg.r;
            var rgb_g = avg.g;
            var rgb_b = avg.b;

            var sensor_rgb_r = (int)(avg.r * this.sensor_rgb_max);
            var sensor_rgb_g = (int)(avg.g * this.sensor_rgb_max);
            var sensor_rgb_b = (int)(avg.b * this.sensor_rgb_max);

            return 0.299f * avg.r + 0.587f * avg.g + 0.114f * avg.b;
        }
        public float lightValue;
        public void UpdateSensorValues()
        {
            var tex = dispCamera.targetTexture;
            RenderTexture.active = dispCamera.targetTexture;

            targetTexture.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
            targetTexture.Apply();

            this.lightValue = GetLightValue(targetTexture);
            //Debug.Log("lightValue=" + this.lightValue);

            Pdu[] line_sensors = this.pdu_writer.GetWriteOps().Refs("line_sensors");
            line_sensors[sensor_id].GetPduWriteOps().SetData("brightness", (uint)(this.lightValue * 1000f));
        }
    }

}

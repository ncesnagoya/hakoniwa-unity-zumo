using Hakoniwa.PluggableAsset.Assets.Robot.Parts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hakoniwa.PluggableAsset.Assets.Robot.Zumo
{

    public class ZumoModelController : MonoBehaviour, IRobotPartsController, IRobotPartsConfig
    {
        /*
         * In the Unity world, 1 unit = 1 meter.
         * The size of the Zumo is approximately 100mm in total length, and this size is considered as 1 unit.
         * Therefore, we replicate the dimensions of the Zumo model in the Unity world by scaling it up by a factor of 10.
         */
        public static readonly float scale = 10f;

        public void Initialize(object root)
        {
            /* nothing to do */
        }
        public void DoControl()
        {
            /* nothing to do */
        }

        public RosTopicMessageConfig[] getRosConfig()
        {
            RosTopicMessageConfig[] cfg = new RosTopicMessageConfig[2];
            cfg[0] = new RosTopicMessageConfig();
            cfg[1] = new RosTopicMessageConfig();
            /* Sensor */
            cfg[0].topic_type_name = "zumo_msgs/ZumoPduSensor";
            cfg[0].topic_message_name = "sensor";
            cfg[0].sub = false;

            /* Actuator */
            cfg[1].topic_type_name = "zumo_msgs/ZumoPduActuator";
            cfg[1].topic_message_name = "actuator";
            cfg[1].sub = true;

            return cfg;
        }

        public IoMethod io_method = IoMethod.RPC;
        public CommMethod comm_method = CommMethod.UDP;
        public int pdu_size_sensor = 104;
        public int pdu_size_actuator = 24;
        public RoboPartsConfigData[] GetRoboPartsConfig()
        {
            var topic = this.getRosConfig();

            RoboPartsConfigData[] configs = new RoboPartsConfigData[2];
            configs[0] = new RoboPartsConfigData();
            configs[1] = new RoboPartsConfigData();

            /* Sensor */
            configs[0].io_dir = IoDir.WRITE;
            configs[0].io_method = this.io_method;
            configs[0].value.org_name = topic[0].topic_message_name;
            configs[0].value.type = topic[0].topic_type_name;
            configs[0].value.class_name = ConstantValues.pdu_reader_class;
            configs[0].value.conv_class_name = ConstantValues.conv_pdu_reader_class;
            configs[0].value.pdu_size = this.pdu_size_sensor;
            configs[0].value.write_cycle = 1;
            configs[0].value.method_type = this.comm_method.ToString();

            /* Actuator */
            configs[1].io_dir = IoDir.READ;
            configs[1].io_method = this.io_method;
            configs[1].value.org_name = topic[1].topic_message_name;
            configs[1].value.type = topic[1].topic_type_name;
            configs[1].value.class_name = ConstantValues.pdu_reader_class;
            configs[1].value.conv_class_name = ConstantValues.conv_pdu_reader_class;
            configs[1].value.pdu_size = this.pdu_size_actuator;
            configs[1].value.method_type = this.comm_method.ToString();

            return configs;
        }

    }

}


/*
 *  TOPPERS Hakoniwa ZumoSim
 *      Toyohashi Open Platform for Embedded Real-Time Systems
 *      Hakoniwa(a virtual simulation environment) for Zumo
 *
 *  Copyright (C) 2023-2024 by Center for Embedded Computing Systems
 *              Graduate School of Informatis, Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 */

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


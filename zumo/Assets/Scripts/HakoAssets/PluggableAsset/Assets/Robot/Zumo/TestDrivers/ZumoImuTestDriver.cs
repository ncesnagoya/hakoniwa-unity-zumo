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

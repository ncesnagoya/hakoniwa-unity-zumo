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

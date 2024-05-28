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

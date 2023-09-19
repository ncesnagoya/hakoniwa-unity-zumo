#ifndef _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduMotor_HPP_
#define _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduMotor_HPP_

#include "pdu_primitive_ctypes.h"
#include "ros_primitive_types.hpp"
#include "pdu_primitive_ctypes_conv.hpp"
/*
 * Dependent pdu data
 */
#include "zumo_msgs/pdu_ctype_ZumoPduMotor.h"
/*
 * Dependent ros data
 */
#include "zumo_msgs/msg/zumo_pdu_motor.hpp"

/*
 * Dependent Convertors
 */

/***************************
 *
 * PDU ==> ROS2
 *
 ***************************/
static inline int hako_convert_pdu2ros_ZumoPduMotor(Hako_ZumoPduMotor &src,  zumo_msgs::msg::ZumoPduMotor &dst)
{
    //primitive convert
    hako_convert_pdu2ros(src.power, dst.power);
    return 0;
}

template<int _src_len, int _dst_len>
int hako_convert_pdu2ros_array_ZumoPduMotor(Hako_ZumoPduMotor src[], std::array<zumo_msgs::msg::ZumoPduMotor, _dst_len> &dst)
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_pdu2ros_ZumoPduMotor(src[i], dst[i]);
    }
    return ret;
}
template<int _src_len, int _dst_len>
int hako_convert_pdu2ros_array_ZumoPduMotor(Hako_ZumoPduMotor src[], std::vector<zumo_msgs::msg::ZumoPduMotor> &dst)
{
    dst.resize(_src_len);
    for (int i = 0; i < _src_len; i++) {
        (void)hako_convert_pdu2ros_ZumoPduMotor(src[i], dst[i]);
    }
    return 0;
}

/***************************
 *
 * ROS2 ==> PDU
 *
 ***************************/
static inline int hako_convert_ros2pdu_ZumoPduMotor(zumo_msgs::msg::ZumoPduMotor &src, Hako_ZumoPduMotor &dst)
{
    //primitive convert
    hako_convert_ros2pdu(src.power, dst.power);
    return 0;
}

template<int _src_len, int _dst_len>
int hako_convert_ros2pdu_array_ZumoPduMotor(std::array<zumo_msgs::msg::ZumoPduMotor, _src_len> &src, Hako_ZumoPduMotor dst[])
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_ros2pdu_ZumoPduMotor(src[i], dst[i]);
    }
    return ret;
}
template<int _src_len, int _dst_len>
int hako_convert_ros2pdu_array_ZumoPduMotor(std::vector<zumo_msgs::msg::ZumoPduMotor> &src, Hako_ZumoPduMotor dst[])
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_ros2pdu_ZumoPduMotor(src[i], dst[i]);
    }
    return ret;
}

#endif /* _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduMotor_HPP_ */

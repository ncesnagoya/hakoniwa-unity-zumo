#ifndef _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduActuator_HPP_
#define _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduActuator_HPP_

#include "pdu_primitive_ctypes.h"
#include "ros_primitive_types.hpp"
#include "pdu_primitive_ctypes_conv.hpp"
/*
 * Dependent pdu data
 */
#include "zumo_msgs/pdu_ctype_ZumoPduActuator.h"
/*
 * Dependent ros data
 */
#include "zumo_msgs/msg/zumo_pdu_actuator.hpp"

/*
 * Dependent Convertors
 */
#include "zumo_msgs/pdu_ctype_conv_ZumoPduLed.hpp"
#include "zumo_msgs/pdu_ctype_conv_ZumoPduMotor.hpp"

/***************************
 *
 * PDU ==> ROS2
 *
 ***************************/
static inline int hako_convert_pdu2ros_ZumoPduActuator(Hako_ZumoPduActuator &src,  zumo_msgs::msg::ZumoPduActuator &dst)
{
    //struct array convertor
    (void)hako_convert_pdu2ros_array_ZumoPduMotor<M_ARRAY_SIZE(Hako_ZumoPduActuator, Hako_ZumoPduMotor, motors), 2>(
        src.motors, dst.motors);
    //struct convert
    hako_convert_pdu2ros_ZumoPduLed(src.led, dst.led);
    return 0;
}

template<int _src_len, int _dst_len>
int hako_convert_pdu2ros_array_ZumoPduActuator(Hako_ZumoPduActuator src[], std::array<zumo_msgs::msg::ZumoPduActuator, _dst_len> &dst)
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_pdu2ros_ZumoPduActuator(src[i], dst[i]);
    }
    return ret;
}
template<int _src_len, int _dst_len>
int hako_convert_pdu2ros_array_ZumoPduActuator(Hako_ZumoPduActuator src[], std::vector<zumo_msgs::msg::ZumoPduActuator> &dst)
{
    dst.resize(_src_len);
    for (int i = 0; i < _src_len; i++) {
        (void)hako_convert_pdu2ros_ZumoPduActuator(src[i], dst[i]);
    }
    return 0;
}

/***************************
 *
 * ROS2 ==> PDU
 *
 ***************************/
static inline int hako_convert_ros2pdu_ZumoPduActuator(zumo_msgs::msg::ZumoPduActuator &src, Hako_ZumoPduActuator &dst)
{
    //struct array convertor
    (void)hako_convert_ros2pdu_array_ZumoPduMotor<2, M_ARRAY_SIZE(Hako_ZumoPduActuator, Hako_ZumoPduMotor, motors)>(
        src.motors, dst.motors);
    //struct convert
    hako_convert_ros2pdu_ZumoPduLed(src.led, dst.led);
    return 0;
}

template<int _src_len, int _dst_len>
int hako_convert_ros2pdu_array_ZumoPduActuator(std::array<zumo_msgs::msg::ZumoPduActuator, _src_len> &src, Hako_ZumoPduActuator dst[])
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_ros2pdu_ZumoPduActuator(src[i], dst[i]);
    }
    return ret;
}
template<int _src_len, int _dst_len>
int hako_convert_ros2pdu_array_ZumoPduActuator(std::vector<zumo_msgs::msg::ZumoPduActuator> &src, Hako_ZumoPduActuator dst[])
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_ros2pdu_ZumoPduActuator(src[i], dst[i]);
    }
    return ret;
}

#endif /* _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduActuator_HPP_ */

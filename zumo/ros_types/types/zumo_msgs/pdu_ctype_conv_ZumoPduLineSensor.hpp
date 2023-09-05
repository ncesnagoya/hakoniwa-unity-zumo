#ifndef _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduLineSensor_HPP_
#define _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduLineSensor_HPP_

#include "pdu_primitive_ctypes.h"
#include "ros_primitive_types.hpp"
#include "pdu_primitive_ctypes_conv.hpp"
/*
 * Dependent pdu data
 */
#include "zumo_msgs/pdu_ctype_ZumoPduLineSensor.h"
/*
 * Dependent ros data
 */
#include "zumo_msgs/msg/zumo_pdu_line_sensor.hpp"

/*
 * Dependent Convertors
 */

/***************************
 *
 * PDU ==> ROS2
 *
 ***************************/
static inline int hako_convert_pdu2ros_ZumoPduLineSensor(Hako_ZumoPduLineSensor &src,  zumo_msgs::msg::ZumoPduLineSensor &dst)
{
    //primitive convert
    hako_convert_pdu2ros(src.brightness, dst.brightness);
    return 0;
}

template<int _src_len, int _dst_len>
int hako_convert_pdu2ros_array_ZumoPduLineSensor(Hako_ZumoPduLineSensor src[], std::array<zumo_msgs::msg::ZumoPduLineSensor, _dst_len> &dst)
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_pdu2ros_ZumoPduLineSensor(src[i], dst[i]);
    }
    return ret;
}
template<int _src_len, int _dst_len>
int hako_convert_pdu2ros_array_ZumoPduLineSensor(Hako_ZumoPduLineSensor src[], std::vector<zumo_msgs::msg::ZumoPduLineSensor> &dst)
{
    dst.resize(_src_len);
    for (int i = 0; i < _src_len; i++) {
        (void)hako_convert_pdu2ros_ZumoPduLineSensor(src[i], dst[i]);
    }
    return 0;
}

/***************************
 *
 * ROS2 ==> PDU
 *
 ***************************/
static inline int hako_convert_ros2pdu_ZumoPduLineSensor(zumo_msgs::msg::ZumoPduLineSensor &src, Hako_ZumoPduLineSensor &dst)
{
    //primitive convert
    hako_convert_ros2pdu(src.brightness, dst.brightness);
    return 0;
}

template<int _src_len, int _dst_len>
int hako_convert_ros2pdu_array_ZumoPduLineSensor(std::array<zumo_msgs::msg::ZumoPduLineSensor, _src_len> &src, Hako_ZumoPduLineSensor dst[])
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_ros2pdu_ZumoPduLineSensor(src[i], dst[i]);
    }
    return ret;
}
template<int _src_len, int _dst_len>
int hako_convert_ros2pdu_array_ZumoPduLineSensor(std::vector<zumo_msgs::msg::ZumoPduLineSensor> &src, Hako_ZumoPduLineSensor dst[])
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_ros2pdu_ZumoPduLineSensor(src[i], dst[i]);
    }
    return ret;
}

#endif /* _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduLineSensor_HPP_ */

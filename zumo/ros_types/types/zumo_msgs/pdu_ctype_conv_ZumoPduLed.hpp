#ifndef _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduLed_HPP_
#define _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduLed_HPP_

#include "pdu_primitive_ctypes.h"
#include "ros_primitive_types.hpp"
#include "pdu_primitive_ctypes_conv.hpp"
/*
 * Dependent pdu data
 */
#include "zumo_msgs/pdu_ctype_ZumoPduLed.h"
/*
 * Dependent ros data
 */
#include "zumo_msgs/msg/zumo_pdu_led.hpp"

/*
 * Dependent Convertors
 */

/***************************
 *
 * PDU ==> ROS2
 *
 ***************************/
static inline int hako_convert_pdu2ros_ZumoPduLed(Hako_ZumoPduLed &src,  zumo_msgs::msg::ZumoPduLed &dst)
{
    //primitive convert
    hako_convert_pdu2ros(src.red, dst.red);
    //primitive convert
    hako_convert_pdu2ros(src.blue, dst.blue);
    //primitive convert
    hako_convert_pdu2ros(src.green, dst.green);
    //primitive convert
    hako_convert_pdu2ros(src.yellow, dst.yellow);
    return 0;
}

template<int _src_len, int _dst_len>
int hako_convert_pdu2ros_array_ZumoPduLed(Hako_ZumoPduLed src[], std::array<zumo_msgs::msg::ZumoPduLed, _dst_len> &dst)
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_pdu2ros_ZumoPduLed(src[i], dst[i]);
    }
    return ret;
}
template<int _src_len, int _dst_len>
int hako_convert_pdu2ros_array_ZumoPduLed(Hako_ZumoPduLed src[], std::vector<zumo_msgs::msg::ZumoPduLed> &dst)
{
    dst.resize(_src_len);
    for (int i = 0; i < _src_len; i++) {
        (void)hako_convert_pdu2ros_ZumoPduLed(src[i], dst[i]);
    }
    return 0;
}

/***************************
 *
 * ROS2 ==> PDU
 *
 ***************************/
static inline int hako_convert_ros2pdu_ZumoPduLed(zumo_msgs::msg::ZumoPduLed &src, Hako_ZumoPduLed &dst)
{
    //primitive convert
    hako_convert_ros2pdu(src.red, dst.red);
    //primitive convert
    hako_convert_ros2pdu(src.blue, dst.blue);
    //primitive convert
    hako_convert_ros2pdu(src.green, dst.green);
    //primitive convert
    hako_convert_ros2pdu(src.yellow, dst.yellow);
    return 0;
}

template<int _src_len, int _dst_len>
int hako_convert_ros2pdu_array_ZumoPduLed(std::array<zumo_msgs::msg::ZumoPduLed, _src_len> &src, Hako_ZumoPduLed dst[])
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_ros2pdu_ZumoPduLed(src[i], dst[i]);
    }
    return ret;
}
template<int _src_len, int _dst_len>
int hako_convert_ros2pdu_array_ZumoPduLed(std::vector<zumo_msgs::msg::ZumoPduLed> &src, Hako_ZumoPduLed dst[])
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_ros2pdu_ZumoPduLed(src[i], dst[i]);
    }
    return ret;
}

#endif /* _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduLed_HPP_ */

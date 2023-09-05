#ifndef _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduIrSensor_HPP_
#define _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduIrSensor_HPP_

#include "pdu_primitive_ctypes.h"
#include "ros_primitive_types.hpp"
#include "pdu_primitive_ctypes_conv.hpp"
/*
 * Dependent pdu data
 */
#include "zumo_msgs/pdu_ctype_ZumoPduIrSensor.h"
/*
 * Dependent ros data
 */
#include "zumo_msgs/msg/zumo_pdu_ir_sensor.hpp"

/*
 * Dependent Convertors
 */

/***************************
 *
 * PDU ==> ROS2
 *
 ***************************/
static inline int hako_convert_pdu2ros_ZumoPduIrSensor(Hako_ZumoPduIrSensor &src,  zumo_msgs::msg::ZumoPduIrSensor &dst)
{
    //primitive convert
    hako_convert_pdu2ros(src.distance, dst.distance);
    return 0;
}

template<int _src_len, int _dst_len>
int hako_convert_pdu2ros_array_ZumoPduIrSensor(Hako_ZumoPduIrSensor src[], std::array<zumo_msgs::msg::ZumoPduIrSensor, _dst_len> &dst)
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_pdu2ros_ZumoPduIrSensor(src[i], dst[i]);
    }
    return ret;
}
template<int _src_len, int _dst_len>
int hako_convert_pdu2ros_array_ZumoPduIrSensor(Hako_ZumoPduIrSensor src[], std::vector<zumo_msgs::msg::ZumoPduIrSensor> &dst)
{
    dst.resize(_src_len);
    for (int i = 0; i < _src_len; i++) {
        (void)hako_convert_pdu2ros_ZumoPduIrSensor(src[i], dst[i]);
    }
    return 0;
}

/***************************
 *
 * ROS2 ==> PDU
 *
 ***************************/
static inline int hako_convert_ros2pdu_ZumoPduIrSensor(zumo_msgs::msg::ZumoPduIrSensor &src, Hako_ZumoPduIrSensor &dst)
{
    //primitive convert
    hako_convert_ros2pdu(src.distance, dst.distance);
    return 0;
}

template<int _src_len, int _dst_len>
int hako_convert_ros2pdu_array_ZumoPduIrSensor(std::array<zumo_msgs::msg::ZumoPduIrSensor, _src_len> &src, Hako_ZumoPduIrSensor dst[])
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_ros2pdu_ZumoPduIrSensor(src[i], dst[i]);
    }
    return ret;
}
template<int _src_len, int _dst_len>
int hako_convert_ros2pdu_array_ZumoPduIrSensor(std::vector<zumo_msgs::msg::ZumoPduIrSensor> &src, Hako_ZumoPduIrSensor dst[])
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_ros2pdu_ZumoPduIrSensor(src[i], dst[i]);
    }
    return ret;
}

#endif /* _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduIrSensor_HPP_ */

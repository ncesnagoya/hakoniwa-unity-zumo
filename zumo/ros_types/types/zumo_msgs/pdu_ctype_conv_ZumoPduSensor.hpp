#ifndef _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduSensor_HPP_
#define _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduSensor_HPP_

#include "pdu_primitive_ctypes.h"
#include "ros_primitive_types.hpp"
#include "pdu_primitive_ctypes_conv.hpp"
/*
 * Dependent pdu data
 */
#include "zumo_msgs/pdu_ctype_ZumoPduSensor.h"
/*
 * Dependent ros data
 */
#include "zumo_msgs/msg/zumo_pdu_sensor.hpp"

/*
 * Dependent Convertors
 */
#include "geometry_msgs/pdu_ctype_conv_Vector3.hpp"
#include "zumo_msgs/pdu_ctype_conv_ZumoPduImu.hpp"
#include "zumo_msgs/pdu_ctype_conv_ZumoPduIrSensor.hpp"
#include "zumo_msgs/pdu_ctype_conv_ZumoPduLineSensor.hpp"

/***************************
 *
 * PDU ==> ROS2
 *
 ***************************/
static inline int hako_convert_pdu2ros_ZumoPduSensor(Hako_ZumoPduSensor &src,  zumo_msgs::msg::ZumoPduSensor &dst)
{
    //struct array convertor
    (void)hako_convert_pdu2ros_array_ZumoPduIrSensor<M_ARRAY_SIZE(Hako_ZumoPduSensor, Hako_ZumoPduIrSensor, irs), 2>(
        src.irs, dst.irs);
    //struct array convertor
    (void)hako_convert_pdu2ros_array_ZumoPduLineSensor<M_ARRAY_SIZE(Hako_ZumoPduSensor, Hako_ZumoPduLineSensor, line_sensors), 6>(
        src.line_sensors, dst.line_sensors);
    //struct convert
    hako_convert_pdu2ros_ZumoPduImu(src.imu, dst.imu);
    return 0;
}

template<int _src_len, int _dst_len>
int hako_convert_pdu2ros_array_ZumoPduSensor(Hako_ZumoPduSensor src[], std::array<zumo_msgs::msg::ZumoPduSensor, _dst_len> &dst)
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_pdu2ros_ZumoPduSensor(src[i], dst[i]);
    }
    return ret;
}
template<int _src_len, int _dst_len>
int hako_convert_pdu2ros_array_ZumoPduSensor(Hako_ZumoPduSensor src[], std::vector<zumo_msgs::msg::ZumoPduSensor> &dst)
{
    dst.resize(_src_len);
    for (int i = 0; i < _src_len; i++) {
        (void)hako_convert_pdu2ros_ZumoPduSensor(src[i], dst[i]);
    }
    return 0;
}

/***************************
 *
 * ROS2 ==> PDU
 *
 ***************************/
static inline int hako_convert_ros2pdu_ZumoPduSensor(zumo_msgs::msg::ZumoPduSensor &src, Hako_ZumoPduSensor &dst)
{
    //struct array convertor
    (void)hako_convert_ros2pdu_array_ZumoPduIrSensor<2, M_ARRAY_SIZE(Hako_ZumoPduSensor, Hako_ZumoPduIrSensor, irs)>(
        src.irs, dst.irs);
    //struct array convertor
    (void)hako_convert_ros2pdu_array_ZumoPduLineSensor<6, M_ARRAY_SIZE(Hako_ZumoPduSensor, Hako_ZumoPduLineSensor, line_sensors)>(
        src.line_sensors, dst.line_sensors);
    //struct convert
    hako_convert_ros2pdu_ZumoPduImu(src.imu, dst.imu);
    return 0;
}

template<int _src_len, int _dst_len>
int hako_convert_ros2pdu_array_ZumoPduSensor(std::array<zumo_msgs::msg::ZumoPduSensor, _src_len> &src, Hako_ZumoPduSensor dst[])
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_ros2pdu_ZumoPduSensor(src[i], dst[i]);
    }
    return ret;
}
template<int _src_len, int _dst_len>
int hako_convert_ros2pdu_array_ZumoPduSensor(std::vector<zumo_msgs::msg::ZumoPduSensor> &src, Hako_ZumoPduSensor dst[])
{
    int ret = 0;
    int len = _dst_len;
    if (_dst_len > _src_len) {
        len = _src_len;
        ret = -1;
    }
    for (int i = 0; i < len; i++) {
        (void)hako_convert_ros2pdu_ZumoPduSensor(src[i], dst[i]);
    }
    return ret;
}

#endif /* _PDU_CTYPE_CONV_HAKO_zumo_msgs_ZumoPduSensor_HPP_ */

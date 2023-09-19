#ifndef _pdu_ctype_zumo_msgs_ZumoPduSensor_H_
#define _pdu_ctype_zumo_msgs_ZumoPduSensor_H_

#include "pdu_primitive_ctypes.h"
#include "geometry_msgs/pdu_ctype_Vector3.h"
#include "zumo_msgs/pdu_ctype_ZumoPduImu.h"
#include "zumo_msgs/pdu_ctype_ZumoPduIrSensor.h"
#include "zumo_msgs/pdu_ctype_ZumoPduLineSensor.h"

typedef struct {
        Hako_ZumoPduIrSensor irs[2];
        Hako_ZumoPduLineSensor line_sensors[6];
        Hako_ZumoPduImu    imu;
} Hako_ZumoPduSensor;

#endif /* _pdu_ctype_zumo_msgs_ZumoPduSensor_H_ */

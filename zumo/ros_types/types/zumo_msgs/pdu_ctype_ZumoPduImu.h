#ifndef _pdu_ctype_zumo_msgs_ZumoPduImu_H_
#define _pdu_ctype_zumo_msgs_ZumoPduImu_H_

#include "pdu_primitive_ctypes.h"
#include "geometry_msgs/pdu_ctype_Vector3.h"

typedef struct {
        Hako_Vector3    acc;
        Hako_Vector3    gyro;
        Hako_Vector3    mag;
} Hako_ZumoPduImu;

#endif /* _pdu_ctype_zumo_msgs_ZumoPduImu_H_ */

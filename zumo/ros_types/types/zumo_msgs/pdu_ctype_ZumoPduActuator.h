#ifndef _pdu_ctype_zumo_msgs_ZumoPduActuator_H_
#define _pdu_ctype_zumo_msgs_ZumoPduActuator_H_

#include "pdu_primitive_ctypes.h"
#include "zumo_msgs/pdu_ctype_ZumoPduLed.h"
#include "zumo_msgs/pdu_ctype_ZumoPduMotor.h"

typedef struct {
        Hako_ZumoPduMotor motors[2];
        Hako_ZumoPduLed    led;
} Hako_ZumoPduActuator;

#endif /* _pdu_ctype_zumo_msgs_ZumoPduActuator_H_ */

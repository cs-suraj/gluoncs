﻿/*!
 *   SerialCommunication.c
 *   Implements the gluonpilot communication protocol.
 *   
 *   
 *   
 *   @author  Tom Pycke
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using Communication.Frames.Configuration;
using Communication.Frames.Incoming;

namespace Communication
{
    public abstract class SerialCommunication
    {
        protected SerialPort _serialPort;   // COM communication port
        public string LogToFilename;

        // delegates used for the communication frames events
        public delegate void ReceiveCommunication(string line);
        public delegate void ReceiveNonParsedCommunication(string line);
        public delegate void ReceiveGyroAccRawCommunicationFrame(GyroAccRaw ga);
        public delegate void ReceiveGyroAccProcCommunicationFrame(GyroAccProcessed ga);
        public delegate void ReceivePressureTempCommunicationFrame(PressureTemp info);
        public delegate void ReceiveAllConfigCommunicationFrame(AllConfig config);
        public delegate void ReceiveRcInputCommunicationFrame(RcInput rcinput);
        public delegate void ReceiveGpsBasicCommunicationFrame(GpsBasic gpsbasic);
        public delegate void ReceiveAttitudeCommunicationFrame(Attitude attitude);
        public delegate void ReceiveDatalogTableCommunicationFrame(DatalogTable table);
        public delegate void ReceiveDatalogLineCommunicationFrame(DatalogLine line);
        public delegate void ReceiveNavigationInstructionCommunicationFrame(NavigationInstruction ni);
        public delegate void ReceiveControlInfoCommunicationFrame(ControlInfo ci);
        public delegate void ReceiveServosCommunicationFrame(Servos s);

        public delegate void LostCommunication();
        public delegate void EstablishedCommunication();


        // General: all lines received will be broadcasted by this event
        public abstract event ReceiveCommunication CommunicationReceived;
        public abstract event ReceiveNonParsedCommunication NonParsedCommunicationReceived;  
        // Gyro & Acc
        public abstract event ReceiveGyroAccRawCommunicationFrame GyroAccRawCommunicationReceived;
        public abstract event ReceiveGyroAccProcCommunicationFrame GyroAccProcCommunicationReceived;
        // Pressure & Temperature
        public abstract event ReceivePressureTempCommunicationFrame PressureTempCommunicationReceived;
        // Configuration
        public abstract event ReceiveAllConfigCommunicationFrame AllConfigCommunicationReceived;
        // RC transmitter
        public abstract event ReceiveRcInputCommunicationFrame RcInputCommunicationReceived;
        // Gps Basic
        public abstract event ReceiveGpsBasicCommunicationFrame GpsBasicCommunicationReceived;
        // Attitude
        public abstract event ReceiveAttitudeCommunicationFrame AttitudeCommunicationReceived;
        // Datalog
        public abstract event ReceiveDatalogTableCommunicationFrame DatalogTableCommunicationReceived;
        public abstract event ReceiveDatalogLineCommunicationFrame DatalogLineCommunicationReceived;
        // Navigation
        public abstract event ReceiveNavigationInstructionCommunicationFrame NavigationInstructionCommunicationReceived;
        // ControlInfo
        public abstract event ReceiveControlInfoCommunicationFrame ControlInfoCommunicationReceived;
        public abstract event ReceiveServosCommunicationFrame ServosCommunicationReceived;
        // Communication status
        public abstract event LostCommunication CommunicationLost;
        public abstract event EstablishedCommunication CommunicationEstablished;

        public abstract double SecondsConnectionLost();

        public static string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }

        public string PortName
        {
            get { return _serialPort.PortName; }
        }

        public int BaudRate
        {
            get { return _serialPort.BaudRate; }
        }

        public bool IsOpen
        {
            get
            {
                return _serialPort.IsOpen;
            }
        }

        public abstract void Close();

        public abstract void Send(AllConfig ac);

        public abstract void ReadAllConfig();

        public abstract void SendFlashConfiguration();

        public abstract void SendLoadConfigurationFromFlash();
        
        public abstract void SendLoadConfigurationDefault();

        public abstract void SendDatalogFormat();

        public abstract void SendDatalogTableRequest();

        public abstract void SendDatalogTableRead(int i);

        public abstract void SendNavigationInstruction(NavigationInstruction ni);

        public abstract void SendJumpToNavigationLine(int line);

        public abstract void SendNavigationBurn();

        public abstract void SendNavigationRead();

        public abstract void SendNavigationLoad();

        public abstract void SendReboot();

        public abstract void SendWriteTelemetry(int basicgps, int gyroaccraw, int gyroaccproc, int ppm, int pressuretemp, int attitude, int control);

        public abstract void SetSimulationOn();

        public abstract void SendSimulationUpdate(double lat_rad, double lng_rad, double roll_rad, double pitch_rad, double altitude_m, double speed_ms, double heading_rad);
    }
}
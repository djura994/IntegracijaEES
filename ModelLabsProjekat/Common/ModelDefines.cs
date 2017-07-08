using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
    public enum DMSType : short
    {
        MASK_TYPE = unchecked((short)0xFFFF),


        DAYTYPE = 0x0001,
        SEASON = 0x0002,
        REGULARTIMEPOINT = 0x0003,
        REGULATINGCONTROL = 0x0004,
        REGULATIONSCHEDULE = 0x0005,
        SWITCHSCHEDULE = 0x0006,
        BREAKER = 0x0007,

    }

    [Flags]
    public enum ModelCode : long
    {

        IDOBJ = 0x1000000000000000,
        IDOBJ_ALIASNAME = 0x1000000000000107,
        IDOBJ_MRID = 0x1000000000000207,
        IDOBJ_NAME = 0x1000000000000307,
        IDOBJ_GID = 0x1000000000000404,

        PSR = 0x1100000000000000,

        BIS = 0x1200000000000000,
        BIS_STARTTIME = 0x1200000000000104,
        BIS_VAL1UNIT = 0x120000000000030a,

        RTP = 0x1300000000030000,
        RTP_SEQNUMBER = 0x1300000000030103,
        RTP_VAL1 = 0x1300000000030205,
        RTP_VAL2 = 0x1300000000030305,
        RTP_INTERVALSCHEDULE = 0x1300000000030409,

        DAYTYPE = 0x1400000000010000,
        DAYTYPE_SDTS = 0x1400000000010119,

        SEASON = 0x1500000000020000,
        SEASON_EDATE = 0x1500000000020104,
        SEASON_SDATE = 0x1500000000020204,
        SEASON_SDTS = 0x1500000000020319,

        REGUCONTROL = 0x1110000000040000,
        REGUCONTROL_DISCRETE = 0x1110000000040101,
        REGUCONTROL_MODE = 0x111000000004020a,
        REGUCONTROL_MONPHASE = 0x111000000004030a,
        REGUCONTROL_TARRANGE = 0x1110000000040405,
        REGUCONTROL_TARVAL = 0x1110000000040505,
        REGUCONTROL_REGSCHEDULE = 0x1110000000040619,

        EQUIPMENT = 0x1120000000000000,
        EQUIPMENT_AGGREGATE = 0x1120000000000101,
        EQUIPMENT_NORINSERV = 0x1120000000000201,

        CONDEQUIPMENT = 0x1121000000000000,

        SWITCH = 0x1121100000000000,
        SWITCH_NOROPEN = 0x1121100000000101,
        SWITCH_RETAINED = 0x1121100000000201,
        SWITCH_SWONCOUNT = 0x1121100000000303,
        SWITCH_SWONDATE = 0x1121100000000404,
        SWITCH_SWSCH = 0x1121100000000519,

        PRSWITCH = 0x1121110000000000,
       // PRSWITCH_BRCAP = 0x112111000000010a,

        BREAKER = 0x1121111000070000,
        BREAKER_INTRT = 0x1121111000070105,

        REGINTERVSCHED = 0x1210000000000000,
        REGINTERVSCHED_TIMEPOINTS = 0x1210000000000119,

        SDTS = 0x1211000000000000,
        SDTS_DAYTYPE = 0x1211000000000109,
        SDTS_SEASON = 0x1211000000000209,

        REGUSCHEDULE = 0x1211100000050000,
        REGUSCHEDULE_REGCONTR = 0x1211100000050109,

        SWITCHSCH = 0x1211200000060000,
        SWITCHSCH_SWITCH = 0x1211200000060109,



    }

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}



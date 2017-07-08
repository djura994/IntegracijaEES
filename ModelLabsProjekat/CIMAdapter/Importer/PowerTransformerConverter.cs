namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

        #region Populate ResourceDescription
        public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
        {
            if ((cimIdentifiedObject != null) && (rd != null))
            {
                if (cimIdentifiedObject.MRIDHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
                }
                if (cimIdentifiedObject.NameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
                }
                if (cimIdentifiedObject.AliasNameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
                }
            }
        }


        public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
        {
            if ((cimPowerSystemResource != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
            }
        }

        public static void PopulateRegulatingControlProperties(FTN.RegulatingControl cimRegulatingControl, ResourceDescription rd)
        {
            if ((cimRegulatingControl != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimRegulatingControl, rd);

                if (cimRegulatingControl.DiscreteHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGUCONTROL_DISCRETE, cimRegulatingControl.Discrete));
                }
             /*   if (cimRegulatingControl.ModeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGUCONTROL_MODE, (short)GetDMSRegulatingControlModeKind(cimRegulatingControl.Mode)));
                }
                if (cimRegulatingControl.MonitoredPhaseHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGUCONTROL_MONPHASE, (short)GetDMSPhaseCode(cimRegulatingControl.MonitoredPhase)));
                }*/
            }
        }

        public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd)
        {
            if ((cimEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);

                if (cimEquipment.AggregateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
                }
                if (cimEquipment.NormallyInServiceHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORINSERV, cimEquipment.NormallyInService));
                }
            }
        }

        public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd)
        {
            if ((cimConductingEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd);
            }
        }


        public static void PopulateSwitchProperties(FTN.Switch cimSwitch, ResourceDescription rd)
        {
            if ((cimSwitch != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimSwitch, rd);

                if (cimSwitch.NormalOpenHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_NOROPEN, cimSwitch.NormalOpen));
                }
                if (cimSwitch.RetainedHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_RETAINED, cimSwitch.Retained));
                }
                if (cimSwitch.SwitchOnCountHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_SWONCOUNT, cimSwitch.SwitchOnCount));
                }
                if (cimSwitch.SwitchOnDateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_SWONDATE, cimSwitch.SwitchOnDate.Ticks));
                }

            }
        }

        public static void PopulateBreakerProperties(FTN.Breaker cimBreaker, ResourceDescription rd)
        {
            
                PowerTransformerConverter.PopulateSwitchProperties(cimBreaker, rd);

            if (cimBreaker.InTransitTimeHasValue)
            {
                rd.AddProperty(new Property(ModelCode.BREAKER_INTRT, cimBreaker.InTransitTime));
            }

        }

        public static void PopulateSwitchScheduleProperties(FTN.SwitchSchedule cimSwitchSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSwitchSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateSeasonDayTypeScheduleProperties(cimSwitchSchedule, rd, importHelper, report);
            }
        }

        public static void PopulateBasicIntervalScheduleProperties(BasicIntervalSchedule cimBasicIntervalSchedule, ResourceDescription rd)
        {
            if ((cimBasicIntervalSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBasicIntervalSchedule, rd);

                if (cimBasicIntervalSchedule.StartTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BIS_STARTTIME, cimBasicIntervalSchedule.StartTime.Ticks));
                }
                
               /* if (cimBasicIntervalSchedule.Value1UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BIS_VAL1UNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value1Unit)));
                } */
                
            }
        }

        public static void PopulateRegularIntervalScheduleProperties(FTN.RegularIntervalSchedule cimRegularIntervalSchedule, ResourceDescription rd)
        {
            if ((cimRegularIntervalSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateBasicIntervalScheduleProperties(cimRegularIntervalSchedule, rd);
            }
        }

        public static void PopulateSeasonDayTypeScheduleProperties(FTN.SeasonDayTypeSchedule cimSeasonDayTypeSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSeasonDayTypeSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateRegularIntervalScheduleProperties(cimSeasonDayTypeSchedule, rd);

                if (cimSeasonDayTypeSchedule.DayTypeHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSeasonDayTypeSchedule.DayType.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to DayType: rdfID \"").Append(cimSeasonDayTypeSchedule.DayType.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SDTS_DAYTYPE, gid));
                }

                if (cimSeasonDayTypeSchedule.SeasonHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSeasonDayTypeSchedule.Season.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to Season: rdfID \"").Append(cimSeasonDayTypeSchedule.Season.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SDTS_SEASON, gid));
                }
            }
        }

        public static void PopulateRegulationScheduleProperties(FTN.RegulationSchedule cimRegulationSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {

            if ((cimRegulationSchedule != null) && (rd != null))
            {

                PowerTransformerConverter.PopulateSeasonDayTypeScheduleProperties(cimRegulationSchedule, rd, importHelper, report);

                if (cimRegulationSchedule.RegulatingControlHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRegulationSchedule.RegulatingControl.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegulationSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimRegulationSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to RegulatingControl: rdfID \"").Append(cimRegulationSchedule.RegulatingControl.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGUSCHEDULE_REGCONTR, gid));
                }
            }
        }


        public static void PopulateRegularTimePoint(FTN.RegularTimePoint cimRegularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegularTimePoint != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimRegularTimePoint, rd);

                if (cimRegularTimePoint.SequenceNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.RTP_SEQNUMBER, cimRegularTimePoint.SequenceNumber));
                }
                if (cimRegularTimePoint.Value1HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.RTP_VAL1, cimRegularTimePoint.Value1));
                }
                if (cimRegularTimePoint.Value2HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.RTP_VAL2, cimRegularTimePoint.Value2));
                }
                if (cimRegularTimePoint.IntervalScheduleHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRegularTimePoint.IntervalSchedule.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegularTimePoint.GetType().ToString()).Append(" rdfID = \"").Append(cimRegularTimePoint.ID);
                        report.Report.Append("\" - Failed to set reference to IntervalSchedule: rdfID \"").Append(cimRegularTimePoint.IntervalSchedule.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.RTP_INTERVALSCHEDULE, gid));
                }

            }

        }

        public static void PopulateDayTypeProperties(FTN.DayType cimDayType, ResourceDescription rd)
        {
            if ((cimDayType != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimDayType, rd);
            }
        }

        public static void PopulateSeasonProperties(FTN.Season cimSeason, ResourceDescription rd)
        {
            if ((cimSeason != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimSeason, rd);
                if (cimSeason.EndDateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SEASON_EDATE, cimSeason.EndDate.Ticks));
                }
                if (cimSeason.StartDateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SEASON_SDATE, cimSeason.StartDate.Ticks));
                }
            }
        }

        #endregion Populate ResourceDescription

        #region Enums convert
        public static PhaseCode GetDMSPhaseCode(PhaseCode phases)
		{
			switch (phases)
			{
				case PhaseCode.A:
					return PhaseCode.A;
				case PhaseCode.AB:
					return PhaseCode.AB;
				case PhaseCode.ABC:
					return PhaseCode.ABC;
				case PhaseCode.ABCN:
					return PhaseCode.ABCN;
				case PhaseCode.ABN:
					return PhaseCode.ABN;
				case PhaseCode.AC:
					return PhaseCode.AC;
				case PhaseCode.ACN:
					return PhaseCode.ACN;
				case PhaseCode.AN:
					return PhaseCode.AN;
				case PhaseCode.B:
					return PhaseCode.B;
				case PhaseCode.BC:
					return PhaseCode.BC;
				case PhaseCode.BCN:
					return PhaseCode.BCN;
				case PhaseCode.BN:
					return PhaseCode.BN;
				case PhaseCode.C:
					return PhaseCode.C;
				case PhaseCode.CN:
					return PhaseCode.CN;
				case PhaseCode.N:
					return PhaseCode.N;
				
				default: return PhaseCode.Unknown;
			}
		}
        public static UnitSymbol GetDMSUnitSymbol(UnitSymbol unitSymbol)
        {
            switch (unitSymbol)
            {
                case UnitSymbol.A:
                    return UnitSymbol.A;
                case UnitSymbol.deg:
                    return UnitSymbol.deg;
                case UnitSymbol.degC:
                    return UnitSymbol.degC;
                case UnitSymbol.F:
                    return UnitSymbol.F;
                case UnitSymbol.g:
                    return UnitSymbol.g;
                case UnitSymbol.h:
                    return UnitSymbol.h;
                case UnitSymbol.H:
                    return UnitSymbol.H;
                case UnitSymbol.Hz:
                    return UnitSymbol.Hz;
                case UnitSymbol.J:
                    return UnitSymbol.J;
                case UnitSymbol.m:
                    return UnitSymbol.m;
                case UnitSymbol.m2:
                    return UnitSymbol.m2;
                case UnitSymbol.m3:
                    return UnitSymbol.m3;
                case UnitSymbol.min:
                    return UnitSymbol.min;
                case UnitSymbol.N:
                    return UnitSymbol.N;
                case UnitSymbol.ohm:
                    return UnitSymbol.ohm;
                case UnitSymbol.Pa:
                    return UnitSymbol.Pa;
                case UnitSymbol.rad:
                    return UnitSymbol.rad;
                case UnitSymbol.s:
                    return UnitSymbol.s;
                case UnitSymbol.S:
                    return UnitSymbol.V;
                case UnitSymbol.VA:
                    return UnitSymbol.VA;
                case UnitSymbol.VAh:
                    return UnitSymbol.VAh;
                case UnitSymbol.VAr:
                    return UnitSymbol.VAr;
                case UnitSymbol.VArh:
                    return UnitSymbol.VArh;
                case UnitSymbol.W:
                    return UnitSymbol.W;
                case UnitSymbol.Wh:
                    return UnitSymbol.Wh;
                default:
                    return UnitSymbol.none;
            }
        }
        public static RegulatingControlModeKind GetDMSRegulatingControlModeKind(RegulatingControlModeKind regulatingControlModeKind)
        {
            switch (regulatingControlModeKind)
            {
                case RegulatingControlModeKind.activePower:
                    return RegulatingControlModeKind.activePower;
                case RegulatingControlModeKind.admittance:
                    return RegulatingControlModeKind.admittance;
                case RegulatingControlModeKind.currentFlow:
                    return RegulatingControlModeKind.currentFlow;
                case RegulatingControlModeKind.fiXed:
                    return RegulatingControlModeKind.fiXed;
                case RegulatingControlModeKind.powerFactor:
                    return RegulatingControlModeKind.powerFactor;
                case RegulatingControlModeKind.reactivePower:
                    return RegulatingControlModeKind.reactivePower;
                case RegulatingControlModeKind.temperature:
                    return RegulatingControlModeKind.temperature;
                case RegulatingControlModeKind.timeScheduled:
                    return RegulatingControlModeKind.timeScheduled;
                case RegulatingControlModeKind.voltage:
                    return RegulatingControlModeKind.voltage;
                default:
                    return RegulatingControlModeKind.voltage;
            }
        }

        #endregion Enums convert
    }
}

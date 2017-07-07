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
				
			}
		}

		

		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				
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


        #endregion Enums convert
    }
}

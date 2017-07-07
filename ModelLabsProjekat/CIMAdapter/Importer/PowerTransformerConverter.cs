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


		#endregion Enums convert
	}
}

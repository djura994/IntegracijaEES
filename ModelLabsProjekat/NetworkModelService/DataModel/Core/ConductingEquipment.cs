using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
	public class ConductingEquipment : Equipment
	{		
		private PhaseCode phases;
		private float ratedVoltage;
		private long baseVoltage = 0;
			
		public ConductingEquipment(long globalId) : base(globalId) 
		{
		}
		
		public PhaseCode Phases
		{
			get
			{
				return phases;
			}

			set
			{
				phases = value;
			}
		}

		public float RatedVoltage
		{
			get { return ratedVoltage; }
			set { ratedVoltage = value; }
		}

		public long BaseVoltage
		{
			get { return baseVoltage; }
			set { baseVoltage = value; }
		}

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				ConductingEquipment x = (ConductingEquipment)obj;
				return (x.phases == this.phases && x.ratedVoltage == this.ratedVoltage && x.baseVoltage == this.baseVoltage);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#region IAccess implementation

		public override bool HasProperty(ModelCode property)
		{
			switch (property)
			{
				case ModelCode.CONDEQ_PHASES:				
				case ModelCode.CONDEQ_RATEDVOLTAGE:
				case ModelCode.CONDEQ_BASVOLTAGE:
					return true;

				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.CONDEQ_PHASES:
					prop.SetValue((short)phases);
					break;

				case ModelCode.CONDEQ_RATEDVOLTAGE:
					prop.SetValue(ratedVoltage);
					break;

				case ModelCode.CONDEQ_BASVOLTAGE:
					prop.SetValue(baseVoltage);
					break;

				default:
					base.GetProperty(prop);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.CONDEQ_PHASES:					
					phases = (PhaseCode)property.AsEnum();
					break;
			
				case ModelCode.CONDEQ_RATEDVOLTAGE:
					ratedVoltage = property.AsFloat();
					break;

				case ModelCode.CONDEQ_BASVOLTAGE:
					baseVoltage = property.AsReference();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}	

		#endregion IAccess implementation

		#region IReference implementation

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (baseVoltage != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
			{
				references[ModelCode.CONDEQ_BASVOLTAGE] = new List<long>();
				references[ModelCode.CONDEQ_BASVOLTAGE].Add(baseVoltage);
			}

			base.GetReferences(references, refType);
		}

		#endregion IReference implementation
	}
}

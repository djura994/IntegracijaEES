using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{	
	public class TransformerWinding : ConductingEquipment
	{
		private WindingConnection connectionType;

		private WindingType windingType;

		private bool grounded;

		private float ratedS;

		private float ratedU;

		private float phaseToGroundVoltage;

		private float phaseToPhaseVoltage;

		private long powerTransformer = 0;

		private List<long> windingTests = new List<long>();		

		public TransformerWinding(long globalId)
			: base(globalId)
		{
		}

		public WindingType WindingType
		{
			get
			{
				return windingType;
			}

			set
			{
				windingType = value;
			}
		}
		
		public bool Grounded
		{
			get
			{
				return grounded;
			}

			set
			{
				grounded = value;
			}
		}

		public WindingConnection ConnectionType
		{
			get
			{
				return connectionType;
			}

			set
			{
				connectionType = value;
			}
		}

		public float RatedS
		{
			get
			{
				return ratedS;
			}

			set
			{
				ratedS = value;
			}
		}

		public float RatedU
		{
			get
			{
				return ratedU;
			}

			set
			{
				ratedU = value;
			}
		}

		public float PhaseToGroundVoltage
		{
			get
			{
				return phaseToGroundVoltage;
			}

			set
			{
				phaseToGroundVoltage = value;
			}
		}

		public float PhaseToPhaseVoltage
		{
			get
			{
				return phaseToPhaseVoltage;
			}

			set
			{
				phaseToPhaseVoltage = value;
			}
		}

		public long PowerTransformer
		{
			get { return powerTransformer; }
			set { powerTransformer = value; }
		}

		public List<long> WindingTest
		{
			get { return windingTests; }
			set { windingTests = value; }
		}

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				TransformerWinding x = (TransformerWinding)obj;
				return (x.windingType == this.windingType && x.grounded == this.grounded && x.connectionType == this.connectionType &&
						x.ratedS == this.ratedS && x.ratedU == this.ratedU && x.phaseToGroundVoltage == this.phaseToGroundVoltage &&
						x.phaseToPhaseVoltage == this.phaseToPhaseVoltage && x.powerTransformer == this.powerTransformer &&
						CompareHelper.CompareLists(x.windingTests, this.windingTests));
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

		public override bool HasProperty(ModelCode t)
		{
			switch (t)
			{						
				case ModelCode.TRWINDING_CONNTYPE:			
				case ModelCode.TRWINDING_GROUNDED:
				case ModelCode.TRWINDING_RATEDS:
				case ModelCode.TRWINDING_RATEDU:
				case ModelCode.TRWINDING_WINDTYPE:
				case ModelCode.TRWINDING_PHASETOGRNDVOLTAGE:
				case ModelCode.TRWINDING_PHASETOPHASEVOLTAGE:
				case ModelCode.TRWINDING_POWERTRW:
				case ModelCode.TRWINDING_TESTS:
					return true;

				default:	   
					return base.HasProperty(t);
					
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{				
				case ModelCode.TRWINDING_CONNTYPE:
					property.SetValue((short)connectionType);
					break;

				case ModelCode.TRWINDING_GROUNDED:
					property.SetValue(grounded);
					break;

				case ModelCode.TRWINDING_RATEDS:
					property.SetValue(ratedS);
					break;

				case ModelCode.TRWINDING_RATEDU:
					property.SetValue(ratedU);
					break;

				case ModelCode.TRWINDING_WINDTYPE:
					property.SetValue((short)windingType);
					break;

				case ModelCode.TRWINDING_PHASETOGRNDVOLTAGE:
					property.SetValue(phaseToGroundVoltage);
					break;

				case ModelCode.TRWINDING_PHASETOPHASEVOLTAGE:
					property.SetValue(phaseToPhaseVoltage);
					break;

				case ModelCode.TRWINDING_POWERTRW:				
					property.SetValue(powerTransformer);
					break;

				case ModelCode.TRWINDING_TESTS:
					property.SetValue(windingTests);
					break;

				default:
					base.GetProperty(property);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.TRWINDING_CONNTYPE:
					connectionType = (WindingConnection)property.AsEnum();
					break;

				case ModelCode.TRWINDING_GROUNDED:
					grounded = property.AsBool();
					break;

				case ModelCode.TRWINDING_RATEDS:
					ratedS = property.AsFloat();
					break;

				case ModelCode.TRWINDING_RATEDU:
					ratedU = property.AsFloat();
					break;

				case ModelCode.TRWINDING_WINDTYPE:
					windingType = (WindingType) property.AsEnum();
					break;

				case ModelCode.TRWINDING_PHASETOGRNDVOLTAGE:
					phaseToGroundVoltage = property.AsFloat();
					break;

				case ModelCode.TRWINDING_PHASETOPHASEVOLTAGE:
					phaseToPhaseVoltage = property.AsFloat();					
					break;

				case ModelCode.TRWINDING_POWERTRW:
					powerTransformer = property.AsReference();
					break;				

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override bool IsReferenced
		{
			get
			{
				return windingTests.Count!= 0 || base.IsReferenced;
			}
		}
		
		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (powerTransformer != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
			{
				references[ModelCode.TRWINDING_POWERTRW] = new List<long>();
				references[ModelCode.TRWINDING_POWERTRW].Add(powerTransformer);
			}

			if (windingTests != null && windingTests.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.TRWINDING_TESTS] = windingTests.GetRange(0, windingTests.Count);				
			}		

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.WINDINGTEST_POWERTRWINDING:
					windingTests.Add(globalId);
					break;

				default:
					base.AddReference(referenceId, globalId);
					break;
			}
		}

		public override void RemoveReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.WINDINGTEST_POWERTRWINDING:

					if (windingTests.Contains(globalId))
					{
						windingTests.Remove(globalId);
					}
					else
					{
						CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
					}

					break;

				default:
					base.RemoveReference(referenceId, globalId);
					break;
			}
		}
	
		#endregion IReference implementation
	}
}

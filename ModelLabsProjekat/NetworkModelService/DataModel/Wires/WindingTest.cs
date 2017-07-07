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
	public class WindingTest : IdentifiedObject
	{						
		private float leakageImpedance;		
		private float loadLoss;
		private float noLoadLoss;
		private float phaseShift;
		private float leakageImpedance0Percent;
		private float leakageImpedanceMaxPercent;
		private float leakageImpedanceMinPercent;		
		private long transformerWinding = 0;

		public WindingTest(long globalId)
			: base(globalId)
		{
		}

		public float LeakageImpedance
		{
			get
			{
				return leakageImpedance;
			}

			set
			{
				leakageImpedance = value;
			}
		}		

		public float LoadLoss
		{
			get
			{
				return loadLoss;
			}

			set
			{
				loadLoss = value;
			}
		}

		public float NoLoadLoss
		{
			get
			{
				return noLoadLoss;
			}

			set
			{
				noLoadLoss = value;
			}
		}
		
		public float PhaseShift
		{
			get
			{
				return phaseShift;
			}

			set
			{
				phaseShift = value;
			}
		}
		
		public float LeakageImpedance0Percent
		{
			get
			{
				return leakageImpedance0Percent;
			}

			set
			{
				leakageImpedance0Percent = value;
			}
		}

		public float LeakageImpedanceMaxPercent
		{
			get
			{
				return leakageImpedanceMaxPercent;
			}

			set
			{
				leakageImpedanceMaxPercent = value;
			}
		}

		public float LeakageImpedanceMinPercent
		{
			get
			{
				return leakageImpedanceMinPercent;
			}

			set
			{
				leakageImpedanceMinPercent = value;
			}
		}

		public long TransformerWinding
		{
			get
			{
				return transformerWinding;
			}

			set
			{
				transformerWinding = value;
			}
		}

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				WindingTest x = (WindingTest)obj;
				return (x.transformerWinding == this.transformerWinding &&
						x.leakageImpedance == this.leakageImpedance &&
						x.leakageImpedance0Percent == this.leakageImpedance0Percent &&
						x.leakageImpedanceMaxPercent == this.leakageImpedanceMaxPercent &&
						x.leakageImpedanceMinPercent == this.leakageImpedanceMinPercent &&
						x.loadLoss == this.loadLoss &&
						x.noLoadLoss == this.noLoadLoss &&
						x.phaseShift == this.phaseShift);
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
				case ModelCode.WINDINGTEST_LEAKIMPDN:			
				case ModelCode.WINDINGTEST_LOADLOSS:			
				case ModelCode.WINDINGTEST_NOLOADLOSS:			
				case ModelCode.WINDINGTEST_PHASESHIFT:	
				case ModelCode.WINDINGTEST_LEAKIMPDN0PERCENT:
				case ModelCode.WINDINGTEST_LEAKIMPDNMINPERCENT:
				case ModelCode.WINDINGTEST_LEAKIMPDNMAXPERCENT:
				case ModelCode.WINDINGTEST_POWERTRWINDING:

					return true;
				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.WINDINGTEST_LEAKIMPDN:
					property.SetValue(leakageImpedance);
					break;

				case ModelCode.WINDINGTEST_LOADLOSS:
					property.SetValue(loadLoss);
					break;

				case ModelCode.WINDINGTEST_NOLOADLOSS:
					property.SetValue(noLoadLoss);
					break;

				case ModelCode.WINDINGTEST_PHASESHIFT:
					property.SetValue(phaseShift);
					break;

				case ModelCode.WINDINGTEST_LEAKIMPDN0PERCENT:
					property.SetValue(leakageImpedance0Percent);
					break;

				case ModelCode.WINDINGTEST_LEAKIMPDNMINPERCENT:
					property.SetValue(leakageImpedanceMinPercent);
					break;

				case ModelCode.WINDINGTEST_LEAKIMPDNMAXPERCENT:
					property.SetValue(leakageImpedanceMaxPercent);
					break;

				case ModelCode.WINDINGTEST_POWERTRWINDING:
					property.SetValue(transformerWinding);
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
				case ModelCode.WINDINGTEST_LEAKIMPDN:
					leakageImpedance = property.AsFloat();
					break;

				case ModelCode.WINDINGTEST_LOADLOSS:
					loadLoss = property.AsFloat();					
					break;

				case ModelCode.WINDINGTEST_NOLOADLOSS:
					noLoadLoss = property.AsFloat();
					break;

				case ModelCode.WINDINGTEST_PHASESHIFT:
					phaseShift = property.AsFloat();					
					break;

				case ModelCode.WINDINGTEST_LEAKIMPDN0PERCENT:
					leakageImpedance0Percent = property.AsFloat();					
					break;

				case ModelCode.WINDINGTEST_LEAKIMPDNMINPERCENT:
					leakageImpedanceMinPercent = property.AsFloat();					
					break;

				case ModelCode.WINDINGTEST_LEAKIMPDNMAXPERCENT:
					leakageImpedanceMaxPercent = property.AsFloat();					
					break;

				case ModelCode.WINDINGTEST_POWERTRWINDING:
					transformerWinding = property.AsReference();					
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
			if (transformerWinding != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
			{
				references[ModelCode.WINDINGTEST_POWERTRWINDING] = new List<long>();
				references[ModelCode.WINDINGTEST_POWERTRWINDING].Add(transformerWinding);
			}

			base.GetReferences(references, refType);
		}	

		#endregion IReference implementation
	}
}

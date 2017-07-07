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
	public class PowerTransformer : Equipment
	{
		private bool autotransformer = false;

		private TransformerFunction function;

		private List<long> transformerWindings = new List<long>();		

		public PowerTransformer(long globalId)
			: base(globalId)
		{
		}

		public bool Autotransformer
		{
			get { return autotransformer;}
			set { autotransformer = value;}
		}

		public TransformerFunction Function
		{
			get { return function;}
			set { function = value;}
		}

		public List<long> TransformerWindings
		{
			get { return transformerWindings;}
			set { transformerWindings = value;}
		}
			

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				PowerTransformer x = (PowerTransformer)obj;
				return (x.function == this.function && x.autotransformer == this.autotransformer &&
						CompareHelper.CompareLists(x.TransformerWindings, this.TransformerWindings, true));
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
				case ModelCode.POWERTR_AUTO:
				case ModelCode.POWERTR_FUNC:
				case ModelCode.POWERTR_WINDINGS:				
					return true;

				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{

				case ModelCode.POWERTR_FUNC:
					prop.SetValue((short)function);
					break;				

				case ModelCode.POWERTR_AUTO:
					prop.SetValue(autotransformer);
					break;

				case ModelCode.POWERTR_WINDINGS:
					prop.SetValue(transformerWindings);
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
				case ModelCode.POWERTR_AUTO:
					autotransformer = property.AsBool();
					break;

				case ModelCode.POWERTR_FUNC:					
					function = (TransformerFunction)property.AsEnum();
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
				return (transformerWindings.Count > 0) || base.IsReferenced;
			}
		}
	
		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (transformerWindings != null && transformerWindings.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.POWERTR_WINDINGS] = transformerWindings.GetRange(0, transformerWindings.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.TRWINDING_POWERTRW:
					transformerWindings.Add(globalId);
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
				case ModelCode.TRWINDING_POWERTRW:

					if (transformerWindings.Contains(globalId))
					{
						transformerWindings.Remove(globalId);
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

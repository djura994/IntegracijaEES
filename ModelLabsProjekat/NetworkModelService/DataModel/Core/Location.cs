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
	public class Location : IdentifiedObject
	{
		private string corporateCode = string.Empty;
		private string category = string.Empty;
		private List<long> powerSystemResources = new List<long>();				

		public Location(long globalId) : base(globalId) 
		{
		}
		
		public List<long> PowerSystemResources
		{
			get
			{
				return powerSystemResources;
			}

			set
			{
				powerSystemResources = value;
			}
		}

		public string CorporateCode
		{
			get { return corporateCode; }
			set { corporateCode = value; }
		}

		public string Category
		{
			get { return category; }
			set { category = value; }
		}
	
		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Location x = (Location)obj;
				return (x.category == this.category && x.corporateCode == this.corporateCode && CompareHelper.CompareLists(x.powerSystemResources, this.powerSystemResources, true));
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
				case ModelCode.LOCATION_CATEGORY:
				case ModelCode.LOCATION_CORPORATECODE:
				case ModelCode.LOCATION_PSRS:
					return true;
					
				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.LOCATION_CATEGORY:
					prop.SetValue(category);
					break;

				case ModelCode.LOCATION_CORPORATECODE:
					prop.SetValue(corporateCode);
					break;

				case ModelCode.LOCATION_PSRS:
					prop.SetValue(powerSystemResources);
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
				case ModelCode.LOCATION_CORPORATECODE:
					corporateCode = property.AsString();
					break;

				case ModelCode.LOCATION_CATEGORY:
					category = property.AsString();
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
				return powerSystemResources.Count > 0 || base.IsReferenced;
			}
		}
	
		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (powerSystemResources != null && powerSystemResources.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.LOCATION_PSRS] = powerSystemResources.GetRange(0, powerSystemResources.Count);
			}		

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.PSR_LOCATION:					
					powerSystemResources.Add(globalId);
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
				case ModelCode.PSR_LOCATION:

					if (powerSystemResources.Contains(globalId))
					{
						powerSystemResources.Remove(globalId);
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

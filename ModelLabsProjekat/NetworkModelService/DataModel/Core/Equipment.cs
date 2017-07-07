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
	public class Equipment : PowerSystemResource
	{		
		private bool isUnderground;
		private bool isPrivate;
						
		public Equipment(long globalId) : base(globalId) 
		{
		}
	
		public bool IsUnderground
		{
			get
			{
				return isUnderground;
			}

			set
			{
				isUnderground = value;
			}
		}

		public bool IsPrivate
		{
			get 
			{
				return isPrivate; 
			}
			
			set
			{ 
				isPrivate = value; 
			}
		}

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Equipment x = (Equipment)obj;
				return ((x.isUnderground == this.isUnderground) &&
						(x.isPrivate == this.isPrivate));
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
				case ModelCode.EQUIPMENT_ISUNDERGROUND:
				case ModelCode.EQUIPMENT_ISPRIVATE:
		
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.EQUIPMENT_ISUNDERGROUND:
					property.SetValue(isUnderground);
					break;

				case ModelCode.EQUIPMENT_ISPRIVATE:
					property.SetValue(isPrivate);
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
				case ModelCode.EQUIPMENT_ISUNDERGROUND:					
					isUnderground = property.AsBool();
					break;

				case ModelCode.EQUIPMENT_ISPRIVATE:
					isPrivate = property.AsBool();
					break;
			
				default:
					base.SetProperty(property);
					break;
			}
		}		

		#endregion IAccess implementation
	}
}

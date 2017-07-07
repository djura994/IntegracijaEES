using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using FTN.Common;


namespace FTN.Services.NetworkModelService.DataModel.Core
{
	
	public class PowerSystemResource : IdentifiedObject
    {
      

        public PowerSystemResource(long globalId)
            : base(globalId)
        {
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

      

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                default:
                    base.SetProperty(property);
                    break;
            }
        }

      
	}
}

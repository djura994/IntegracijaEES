using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Services.NetworkModelService.DataModel.LoadModel;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class SwitchSchedule : SeasonDayTypeSchedule
    {
        private long sswitch = 0;

        public SwitchSchedule(long globalId)
            : base(globalId)
        {
        }

        public long Sswitch
        {
            get { return sswitch; }
            set { sswitch = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SwitchSchedule x = (SwitchSchedule)obj;
                return (x.sswitch == this.sswitch);
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
                case ModelCode.SWITCHSCH_SWITCH:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.SWITCHSCH_SWITCH:
                    prop.SetValue(sswitch);
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
                case ModelCode.SWITCHSCH_SWITCH:
                    sswitch = property.AsReference();
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
            if (sswitch != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.REGUSCHEDULE_REGCONTR] = new List<long>();
                references[ModelCode.REGUSCHEDULE_REGCONTR].Add(sswitch);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation


    }
}

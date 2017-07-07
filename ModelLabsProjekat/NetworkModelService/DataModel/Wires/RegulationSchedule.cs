using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Services.NetworkModelService.DataModel.LoadModel;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RegulationSchedule : SeasonDayTypeSchedule
    {
        private long regulatingControl = 0;

        public RegulationSchedule(long globalId)
            : base(globalId)
        {
        }

        public long RegulatingControl
        {
            get { return regulatingControl; }
            set { regulatingControl = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegulationSchedule x = (RegulationSchedule)obj;
                return (x.regulatingControl == this.regulatingControl);
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
                case ModelCode.REGUSCHEDULE_REGCONTR:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.REGUSCHEDULE_REGCONTR:
                    prop.SetValue(regulatingControl);
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
                case ModelCode.REGUSCHEDULE_REGCONTR:
                    regulatingControl = property.AsReference();
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
            if (regulatingControl != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.REGUSCHEDULE_REGCONTR] = new List<long>();
                references[ModelCode.REGUSCHEDULE_REGCONTR].Add(regulatingControl);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation


    }
}

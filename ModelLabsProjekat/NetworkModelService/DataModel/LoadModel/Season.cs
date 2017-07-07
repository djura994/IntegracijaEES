using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.LoadModel
{
    public class Season : IdentifiedObject
    {
        private long endDate;

        private long startDate;

        private List<long> seasonDayTypeSchedules = new List<long>();

        public Season(long globalId)
            : base(globalId)
        {
        }

        public long EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public long StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public List<long> SeasonDayTypeSchedules
        {
            get { return seasonDayTypeSchedules; }
            set { seasonDayTypeSchedules = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Season x = (Season)obj;
                return (x.endDate == this.endDate && x.startDate == this.startDate &&
                    CompareHelper.CompareLists(x.seasonDayTypeSchedules, this.seasonDayTypeSchedules));
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

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.SEASON_EDATE:
                case ModelCode.SEASON_SDATE:
                case ModelCode.SEASON_SDTS:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SEASON_EDATE:
                    property.SetValue(endDate);
                    break;

                case ModelCode.SEASON_SDATE:
                    property.SetValue(startDate);
                    break;

                case ModelCode.SEASON_SDTS:
                    property.SetValue(seasonDayTypeSchedules);
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
                case ModelCode.SEASON_EDATE:
                    endDate = property.AsLong();
                    break;

                case ModelCode.SEASON_SDATE:
                    startDate = property.AsLong();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return seasonDayTypeSchedules.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (seasonDayTypeSchedules != null && seasonDayTypeSchedules.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.SEASON_SDTS] = seasonDayTypeSchedules.GetRange(0, seasonDayTypeSchedules.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SDTS_SEASON:
                    seasonDayTypeSchedules.Add(globalId);
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
                case ModelCode.SDTS_SEASON:

                    if (seasonDayTypeSchedules.Contains(globalId))
                    {
                        seasonDayTypeSchedules.Remove(globalId);
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

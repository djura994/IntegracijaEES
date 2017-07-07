using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Common;



namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class Switch : ConductingEquipment
    {
        private bool normalOpen;

        private bool retained;

        private int switchOnCount;

        private long switchOnDate;

        private List<long> switchSchedules = new List<long>();

        public Switch(long globalId)
            : base(globalId)
        {
        }

        public bool NormalOpen
        {
            get { return normalOpen; }
            set { normalOpen = value; }
        }

        public bool Retained
        {
            get { return retained; }
            set { retained = value; }
        }

        public int SwitchOnCount
        {
            get { return switchOnCount; }
            set { switchOnCount = value; }
        }

        public long SwitchOnDate
        {
            get { return switchOnDate; }
            set { switchOnDate = value; }
        }

        public List<long> SwitchSchedules
        {
            get { return switchSchedules; }
            set { switchSchedules = value; }
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Switch x = (Switch)obj;
                return (x.normalOpen == this.normalOpen &&
                        x.retained == this.retained && x.switchOnCount == this.switchOnCount &&
                        x.switchOnDate == this.switchOnDate &&
                        CompareHelper.CompareLists(x.switchSchedules, this.switchSchedules));
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
                case ModelCode.SWITCH_NOROPEN:
                case ModelCode.SWITCH_RETAINED:
                case ModelCode.SWITCH_SWONCOUNT:
                case ModelCode.SWITCH_SWONDATE:
                case ModelCode.SWITCH_SWSCH:

                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SWITCH_NOROPEN:
                    property.SetValue(normalOpen);
                    break;

                case ModelCode.SWITCH_RETAINED:
                    property.SetValue(retained);
                    break;

                case ModelCode.SWITCH_SWONCOUNT:
                    property.SetValue(switchOnCount);
                    break;

                case ModelCode.SWITCH_SWONDATE:
                    property.SetValue(switchOnDate);
                    break;
                case ModelCode.SWITCH_SWSCH:
                    property.SetValue(switchSchedules);
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
                case ModelCode.SWITCH_NOROPEN:
                    normalOpen = property.AsBool();
                    break;

                case ModelCode.SWITCH_RETAINED:
                    retained = property.AsBool();
                    break;

                case ModelCode.SWITCH_SWONCOUNT:
                    switchOnCount = property.AsInt();
                    break;

                case ModelCode.SWITCH_SWONDATE:
                    switchOnDate = property.AsLong();
                    break;


                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override bool IsReferenced
        {
            get
            {
                return switchSchedules.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (switchSchedules != null && switchSchedules.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.SEASON_SDTS] = switchSchedules.GetRange(0, switchSchedules.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SDTS_SEASON:
                    switchSchedules.Add(globalId);
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

                    if (switchSchedules.Contains(globalId))
                    {
                        switchSchedules.Remove(globalId);
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
    }
}
      


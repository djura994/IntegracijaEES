using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RegulatingControl : PowerSystemResource
    {
        private bool discrete;


        private RegulatingControlModeKind mode;

        private PhaseCode monitoredPhase;

        private float targetRange;

        private float targetValue;

        private List<long> regulationSchedule = new List<long>();



        public RegulatingControl(long globalId)
            : base(globalId)
        {
        }

        public bool Discrete
        {
            get { return discrete; }
            set { discrete = value; }
        }

        public RegulatingControlModeKind Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public PhaseCode MonitoredPhase
        {
            get { return monitoredPhase; }
            set { monitoredPhase = value; }
        }

        public float TargetRange
        {
            get { return targetRange; }
            set { targetRange = value; }
        }

        public float TargetValue
        {
            get { return targetValue; }
            set { targetValue = value; }
        }

        public List<long> RegulationSchedule
        {
            get { return regulationSchedule; }
            set { regulationSchedule = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegulatingControl x = (RegulatingControl)obj;
                return (x.discrete == this.discrete && x.mode == this.mode && x.monitoredPhase == this.monitoredPhase &&
                        x.targetRange == this.targetRange && x.targetValue == this.targetValue &&
                        CompareHelper.CompareLists(x.regulationSchedule, this.regulationSchedule));
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
                case ModelCode.REGUCONTROL_DISCRETE:
                case ModelCode.REGUCONTROL_MODE:
                case ModelCode.REGUCONTROL_MONPHASE:
                case ModelCode.REGUCONTROL_TARRANGE:
                case ModelCode.REGUCONTROL_TARVAL:
                case ModelCode.REGUCONTROL_REGSCHEDULE:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGUCONTROL_DISCRETE:
                    property.SetValue(discrete);
                    break;

                case ModelCode.REGUCONTROL_MODE:
                    property.SetValue((short)mode);
                    break;

                case ModelCode.REGUCONTROL_MONPHASE:
                    property.SetValue((short)monitoredPhase);
                    break;

                case ModelCode.REGUCONTROL_TARRANGE:
                    property.SetValue(targetRange);
                    break;

                case ModelCode.REGUCONTROL_TARVAL:
                    property.SetValue(targetValue);
                    break;

                case ModelCode.REGUCONTROL_REGSCHEDULE:
                    property.SetValue(regulationSchedule);
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
                case ModelCode.REGUCONTROL_DISCRETE:
                    discrete = property.AsBool();
                    break;

                case ModelCode.REGUCONTROL_MODE:
                    mode = (RegulatingControlModeKind)property.AsEnum();
                    break;

                case ModelCode.REGUCONTROL_MONPHASE:
                    monitoredPhase = (PhaseCode)property.AsEnum();
                    break;

                case ModelCode.REGUCONTROL_TARRANGE:
                    targetRange = property.AsFloat();
                    break;

                case ModelCode.REGUCONTROL_TARVAL:
                    targetValue = property.AsFloat();
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
                return regulationSchedule.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {

            if (regulationSchedule != null && regulationSchedule.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGUCONTROL_REGSCHEDULE] = regulationSchedule.GetRange(0, regulationSchedule.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGUSCHEDULE_REGCONTR:
                    regulationSchedule.Add(globalId);
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
                case ModelCode.REGUSCHEDULE_REGCONTR:

                    if (regulationSchedule.Contains(globalId))
                    {
                        regulationSchedule.Remove(globalId);
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

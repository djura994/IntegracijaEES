using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class RegularTimePoint : IdentifiedObject
    {
        private int sequenceNumber;

        private float value1;

        private float value2;

        private long intervalSchedule = 0;

        public RegularTimePoint(long globalId)
            : base(globalId)
        {
        }

        public int SequenceNumber
        {
            get { return sequenceNumber; }
            set { sequenceNumber = value; }
        }

        public float Value1
        {
            get { return value1; }
            set { value1 = value; }
        }

        public float Value2
        {
            get { return value2; }
            set { value2 = value; }
        }

        public long IntervalSchedule
        {
            get { return intervalSchedule; }
            set { intervalSchedule = value; }
        }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegularTimePoint x = (RegularTimePoint)obj;
                return (x.sequenceNumber == this.sequenceNumber && x.value1 == this.value1 && x.value2 == this.value2 &&
                        x.intervalSchedule == this.intervalSchedule);
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
                case ModelCode.RTP_SEQNUMBER:
                case ModelCode.RTP_VAL1:
                case ModelCode.RTP_VAL2:
                case ModelCode.RTP_INTERVALSCHEDULE:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.RTP_SEQNUMBER:
                    property.SetValue(sequenceNumber);
                    break;

                case ModelCode.RTP_VAL1:
                    property.SetValue(value1);
                    break;

                case ModelCode.RTP_VAL2:
                    property.SetValue(value2);
                    break;

                case ModelCode.RTP_INTERVALSCHEDULE:
                    property.SetValue(intervalSchedule);
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
                case ModelCode.RTP_SEQNUMBER:
                    sequenceNumber = property.AsInt();
                    break;

                case ModelCode.RTP_VAL1:
                    value1 = property.AsFloat();
                    break;

                case ModelCode.RTP_VAL2:
                    value2 = property.AsFloat();
                    break;

                case ModelCode.RTP_INTERVALSCHEDULE:
                    intervalSchedule = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (intervalSchedule != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.RTP_INTERVALSCHEDULE] = new List<long>();
                references[ModelCode.RTP_INTERVALSCHEDULE].Add(intervalSchedule);
            }

            base.GetReferences(references, refType);
        }



    }
}

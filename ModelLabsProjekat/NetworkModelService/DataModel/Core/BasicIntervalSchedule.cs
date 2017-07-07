using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class BasicIntervalSchedule : IdentifiedObject
    {
        private long startTime;

        private UnitSymbol value1Unit;


        public BasicIntervalSchedule(long globalId)
            : base(globalId)
        {
        }

        public long StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

      
        public UnitSymbol Value1Unit
        {
            get { return value1Unit; }
            set { value1Unit = value; }
        }

       


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                BasicIntervalSchedule x = (BasicIntervalSchedule)obj;
                return (x.startTime == this.startTime &&  x.value1Unit == this.value1Unit 
                        );
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
                case ModelCode.BIS_STARTTIME:
                case ModelCode.BIS_VAL1UNIT:
                
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.BIS_STARTTIME:
                    property.SetValue(startTime);
                    break;

                case ModelCode.BIS_VAL1UNIT:
                    property.SetValue((short)value1Unit);
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
                case ModelCode.BIS_STARTTIME:
                    startTime = property.AsLong();
                    break;

                
                case ModelCode.BIS_VAL1UNIT:
                    value1Unit = (UnitSymbol)property.AsEnum();
                    break;


                default:
                    base.SetProperty(property);
                    break;
            }
        }

    }
}

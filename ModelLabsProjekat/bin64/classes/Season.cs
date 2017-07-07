//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FTN {
    using System;
    using FTN;
    using System.Collections.Generic;
    
    
    /// A specified time period of the year.
    public class Season : IdentifiedObject {
        
        /// Date season ends.
        private System.DateTime? cim_endDate;
        
        private const bool isEndDateMandatory = false;
        
        private const string _endDatePrefix = "cim";
        
        /// Schedules that use this Season.
        private List<SeasonDayTypeSchedule> cim_SeasonDayTypeSchedules = new List<SeasonDayTypeSchedule>();
        
        private const bool isSeasonDayTypeSchedulesMandatory = false;
        
        private const string _SeasonDayTypeSchedulesPrefix = "cim";
        
        /// Date season starts.
        private System.DateTime? cim_startDate;
        
        private const bool isStartDateMandatory = false;
        
        private const string _startDatePrefix = "cim";
        
        public virtual System.DateTime EndDate {
            get {
                return this.cim_endDate.GetValueOrDefault();
            }
            set {
                this.cim_endDate = value;
            }
        }
        
        public virtual bool EndDateHasValue {
            get {
                return this.cim_endDate != null;
            }
        }
        
        public static bool IsEndDateMandatory {
            get {
                return isEndDateMandatory;
            }
        }
        
        public static string EndDatePrefix {
            get {
                return _endDatePrefix;
            }
        }
        
        public virtual List<SeasonDayTypeSchedule> SeasonDayTypeSchedules {
            get {
                return this.cim_SeasonDayTypeSchedules;
            }
            set {
                this.cim_SeasonDayTypeSchedules = value;
            }
        }
        
        public virtual bool SeasonDayTypeSchedulesHasValue {
            get {
                return this.cim_SeasonDayTypeSchedules != null;
            }
        }
        
        public static bool IsSeasonDayTypeSchedulesMandatory {
            get {
                return isSeasonDayTypeSchedulesMandatory;
            }
        }
        
        public static string SeasonDayTypeSchedulesPrefix {
            get {
                return _SeasonDayTypeSchedulesPrefix;
            }
        }
        
        public virtual System.DateTime StartDate {
            get {
                return this.cim_startDate.GetValueOrDefault();
            }
            set {
                this.cim_startDate = value;
            }
        }
        
        public virtual bool StartDateHasValue {
            get {
                return this.cim_startDate != null;
            }
        }
        
        public static bool IsStartDateMandatory {
            get {
                return isStartDateMandatory;
            }
        }
        
        public static string StartDatePrefix {
            get {
                return _startDatePrefix;
            }
        }
    }
}

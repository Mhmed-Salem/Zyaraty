using System;

namespace Zyarat.Models.Services.CompetitionService
{
    public class TimeOffSetHandler
    {
        private readonly int _offsetHours;

        public TimeOffSetHandler(int offsetHours)
        {
            _offsetHours = offsetHours;
        }

        public DateTime GetDate()
        {
            var now=DateTime.Now;
            var time = now.TimeOfDay;
            if (time.Hours>0&&time.Hours<_offsetHours)
            {
                now=now.AddDays(-1);
            }

            return now.Date;
        }
        
        public DateTime GetDate(DateTime dateTime)
        {
            var now=dateTime;
            var time = now.TimeOfDay;
            if (time.Hours>0&&time.Hours<_offsetHours)
            {
                now=now.AddDays(-1);
            }

            return now.Date;
        }    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DayColor
{
    public class TempoLikeCalendars
    {
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public List<DaysValue> values { get; set; }
    }
}

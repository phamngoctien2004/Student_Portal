using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class DateHelpers
    {
        public static DateOnly GetEndDate(DateOnly startDate)
        {
            int weeksToAdd = 15;
            return startDate.AddDays(weeksToAdd * 7);
        }
    }
}

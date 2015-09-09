using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Resources;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingForManagers.Domain.Attributes
{
    class DateTimeRangeAttribute: RangeAttribute
    {
        public DateTimeRangeAttribute(int since)
            : base(typeof(DateTime),
                    DateTime.Now.AddYears(-since).ToShortDateString(),
                    DateTime.Now.ToShortDateString())
        { } 
    }
}

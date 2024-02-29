using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    public class Validation
    {

        public DateTime Validate(DateTime time)
        {

            if (!DateTime.TryParse(time.ToString(), out var _))
            {
                time = DateTime.Today;
            }
           

            return time;
        }


    }
}

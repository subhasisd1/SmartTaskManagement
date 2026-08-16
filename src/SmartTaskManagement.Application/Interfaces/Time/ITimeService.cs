using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaskManagement.Application.Interfaces.Time
{
    public interface ITimeService
    {
        DateTime ConvertUtcToIst(DateTime utcTime);
        DateTime GetCurrentIstTime();
    }

}

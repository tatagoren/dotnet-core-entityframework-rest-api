using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.Model.Enums
{
    public enum CallStatusEnum
    {
        NOT_CALLED = 0,
        NOT_ANSWERED = 1,
        BUSY = 2,
        ANSWERED_WAITING = 3,
        ANSWERED_SUCCESS = 4,
        ANSWERED_FAILED = 5
    };
}

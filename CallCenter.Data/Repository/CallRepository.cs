using CallCenter.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.Data.Repository
{
    public class CallRepository : RepositoryBase<Call>, ICallRepository
    {
        public CallRepository(CallCenterContext context) : base(context)
        {

        }


    }
}

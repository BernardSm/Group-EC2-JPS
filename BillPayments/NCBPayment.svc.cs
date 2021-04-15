using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BillPayments
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NCBPayment" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select NCBPayment.svc or NCBPayment.svc.cs at the Solution Explorer and start debugging.
    public class NCBPayment : INCBPayment
    {
        public void BNS(string cardnumber)
        {
            throw new NotImplementedException();
        }

        public void NCB(string cardnumber)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BillPayments
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INCBPayment" in both code and config file together.
    [ServiceContract]
    public interface INCBPayment
    {
        [OperationContract]
        void NCB(string cardnumber);

        [OperationContract]
        void BNS(string cardnumber);
    }
}

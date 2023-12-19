using Biodent.DataAccess;

namespace Biodent.Api.Common
{
    public class AutoGenerateNo
    {
        GenerateNoDAL _generate;
        public AutoGenerateNo()
        {
            _generate = new GenerateNoDAL();
        }
        public string GET_InvoiceNo()
        {
            int lastNo = _generate.GetByGenerateType("Order").LastValue;
            string zero = null;
            string invno = null;

            if (lastNo < 10)
            {
                zero = "000000";
            }

            if (lastNo > 9 && lastNo < 100)
            {
                zero = "00000";
            }

            if (lastNo > 99 && lastNo < 1000)
            {
                zero = "0000";
            }

            if (lastNo > 999 && lastNo < 10000)
            {
                zero = "000";
            }

            if (lastNo > 9999 && lastNo < 100000)
            {
                zero = "00";
            }

            if (lastNo > 99999 && lastNo < 100000)
            {
                zero = "0";
            }
            return invno = "L" + zero + lastNo;
        }

        public string GET_PaymentNo()
        {

            int lastNo = _generate.GetByGenerateType("Payment").LastValue;
            string zero = null;
            string invno = null;

            if (lastNo < 10)
            {
                zero = "000000";
            }

            if (lastNo > 9 && lastNo < 100)
            {
                zero = "00000";
            }

            if (lastNo > 99 && lastNo < 1000)
            {
                zero = "0000";
            }

            if (lastNo > 999 && lastNo < 10000)
            {
                zero = "000";
            }

            if (lastNo > 9999 && lastNo < 100000)
            {
                zero = "00";
            }

            if (lastNo > 99999 && lastNo < 100000)
            {
                zero = "0";
            }
            return invno = "PR" + zero + lastNo;
        }
    }
}

using FTD_Asia_Test.Entities;
using FTD_Asia_Test.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FTD_Asia_Test.Services
{
    public class DiscountRulesService: IDiscountRulesService
    {
        public DiscountedItem GetDiscountedItem(long totalAmount)
        {
            DiscountedItem result = new DiscountedItem();

            double afterDiscountRate = 1;

            //Base Discount Check
            if (totalAmount < 20000)
            {
                afterDiscountRate = 1;
            }
            else if (totalAmount <= 50000)
            {
                afterDiscountRate -= 0.05;
            }
            else if (totalAmount <= 80000)
            {
                afterDiscountRate -= 0.07;
            }
            else if (totalAmount <= 120000)
            {
                afterDiscountRate -= 0.10;
            }
            else
            {
                afterDiscountRate = 0.85;
            }

            //Condition Discount Check
            if (IsPrimeNumber(totalAmount) && totalAmount > 50000)
            {
                afterDiscountRate -= 0.08;
            }

            if (IsLastDigit5(totalAmount) && totalAmount > 90000)
            {
                afterDiscountRate -= 0.10;
            }

            //Cap on Maximum Discount Check
            if (afterDiscountRate < 0.80)
            {
                afterDiscountRate = 0.80;
            }

            result.TotalDiscount = (long)(Math.Round(1 - afterDiscountRate, 3) * 100); 
            result.FinalAmount = (long)Math.Round(totalAmount * afterDiscountRate, MidpointRounding.AwayFromZero);

            return result;
        }

        private bool IsPrimeNumber(long number)
        {
            if (number <= 1)
                return false;

            if (number == 2)
                return true;

            if (number % 2 == 0)
                return false;

            int boundary = (int)Math.Sqrt(number);

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        private bool IsLastDigit5(long number)
        {
            string numberStr = number.ToString();
            char lastDigit = numberStr[numberStr.Length - 3];

            if (lastDigit == '5')
                return true;
            else
                return false;
        }
    }
}

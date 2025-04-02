using System;
using System.Text;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

//2.Програмно промоделювати алгоритм множення чисел у форматі з фіксованою крапкою після молодшого розряду за схемою №2 при розрядності множників 8, які представляються у прямому коді.

namespace lab_3_ipz
{
    internal class Task2
    {
        private static bool Check(string line)
        {

            foreach (var el in line)
            {
                if (el != '1' && el != '0')
                {
                    Console.WriteLine("Ви ввели щось не те, спробуйте ще раз");
                    return false;
                }

            }
            if (line.Length != 8)
            {
                Console.WriteLine("Довжина вказана не правильно, спробуйте ще раз");
                return false;
            }
            return true;
        }

        private static int[] BinaryADD(int[] binary1, int[] binary2)
        {
            int length = Math.Max(binary1.Length, binary2.Length) + 1;
            int[] result = new int[length];
            int carry = 0;

            for (int i = 0; i < length - 1; i++)
            {
                int bit1 = (binary1.Length - 1 - i >= 0) ? binary1[binary1.Length - 1 - i] : 0;
                int bit2 = (binary2.Length - 1 - i >= 0) ? binary2[binary2.Length - 1 - i] : 0;

                int sum = bit1 + bit2 + carry;
                result[length - 1 - i] = sum % 2;
                carry = sum / 2;
            }

            result[0] = carry;
            return result;
        }

        private static void Output(int[] num)
        {
            foreach (var el in num)
            {
                Console.Write(el + " ");
            }
            Console.WriteLine();
        }

        static int[] ShiftArrayRight(int[] array)
        {
            int length = array.Length;
            for (int i = length - 1; i > 0; i--)
            {
                array[i] = array[i - 1];
            }
            array[0] = 0;
            return array;
        }

        static int BinaryToDecimal(string binary)
        {
            bool isNegative = binary[0] == '1'; // скорочена перевірка чи є перше число (біт) одиницею
            int result = 0;

            for (int i = binary.Length - 1; i >= 0; i--)
            {
                if (binary[i] == '1')
                {
                    result += (int)Math.Pow(2, binary.Length - 1 - i);
                }
            }

            return isNegative ? -result : result;
        }

        private static string ProductBin(string m1, string m2)
        {
            int[] add_or_no = ToArr(m1);

            int[] arr_for_add = ToArr(m2);
            Array.Resize(ref arr_for_add, 16);

            int[] res_arr = new int[16];

            for (int i = 0; i < add_or_no.Length; i++)
            {
                if (add_or_no[i] == 1)
                {
                    arr_for_add = ShiftArrayRight(arr_for_add);
                    res_arr = BinaryADD(arr_for_add, res_arr);

                }
                if (add_or_no[i] == 0)
                {
                    arr_for_add = ShiftArrayRight(arr_for_add);
                }
            }
            string result = string.Join("", res_arr);
            //Console.WriteLine(result);
            return result;
        }

        private static int[] ToArr(string num)
        {
            int[] res = new int[num.Length];
            for (int i = 0; i < num.Length; i++)
            {
                res[i] = num[i] - '0';  // Перетворюємо '0' або '1' у 0 або 1
            }
            return res;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            string line1, line2;
            //bool check1, check2;
            do
            {
                Console.WriteLine("""
                Введіть перше бінарне число у вигляді прямого коду (8 знаків):
                (Приклад: 00111111)
                """);
                line1 = Console.ReadLine();

            } while (!Check(line1));

            int dec1 = BinaryToDecimal(line1);
            Console.WriteLine($"Отже перше введене вами число:{dec1} ");
            do
            {
                Console.WriteLine("""
                Введіть друге бінарне число у вигляді прямого коду (8 знаків):
                (Приклад: 10110011)
                """);
                line2 = Console.ReadLine();

            } while (!Check(line2));

            int dec2 = BinaryToDecimal(line2);
            Console.WriteLine($"Отже друге введене вами число:{dec2} ");
            string res = ProductBin(line1, line2);

            if (line1[0] == '0' && line2[0] == '1' || line1[0] == '1' && line2[0] == '0')
            {
                string res_for_minus = "1" + res.Substring(1);
                Console.WriteLine($"Результат у двійковому вигляді: {res_for_minus}");

                int resD_minus = BinaryToDecimal(res);
                Console.WriteLine($"Десятковий вид: {resD_minus * (-1)}");
            }
            else
            {
                Console.WriteLine($"Результат у двійковому вигляді: {res}");
                int resD = BinaryToDecimal(res);
                Console.WriteLine($"Десятковий вид: {resD}");

            }

        }

    }
}

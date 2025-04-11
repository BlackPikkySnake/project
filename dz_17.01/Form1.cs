using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace dz_17._01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void workbut_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем уравнение из текстового поля и убираем пробелы
                string equation = textBox1.Text.Replace(" ", "");

                // Разбираем уравнение
                double a, b, c;
                if (!ParseQuadraticEquation(equation, out a, out b, out c))
                {
                    textBox1.Text = "Ошибка: Некорректный формат уравнения. Пример: ax^2+bx+c=0";
                    return;
                }

                // Вычисляем дискриминант
                double discriminant = b * b - 4 * a * c;
                if (discriminant > 0)
                {
                    double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                    double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                    textBox2.Text = $"Два корня:\r\nx1 = {x1:F2}\r\nx2 = {x2:F2}";
                }
                else if (discriminant == 0)
                {
                    double x = -b / (2 * a);
                    textBox2.Text = $"Один корень:\r\nx = {x:F2}";
                }
                else
                {
                    textBox2.Text = "Корней нет (дискриминант отрицательный)";
                }

            }
            catch (Exception ex)
            {
                textBox2.Text = $"Произошла ошибка: {ex.Message}";
            }
        }

        private bool ParseQuadraticEquation(string equation, out double a, out double b, out double c)
        {
            a = 0;
            b = 0;
            c = 0;

            string pattern = @"^([+-]?\s*(?:\d*\.?\d*)?)x\^2([+-]\s*(?:\d*\.?\d*)?)x([+-]\s*(?:\d*\.?\d*))?=0$";
            Match match = Regex.Match(equation, pattern);

            if (match.Success)
            {
                try
                {
                    // Разбираем коэффициент 'a'
                    string aStr = match.Groups[1].Value;
                    if (string.IsNullOrEmpty(aStr) || aStr == "+" || aStr == "-")
                    {
                        a = (aStr == "-") ? -1 : 1; // Если нет цифр, подразумеваем 1 или -1
                    }
                    else
                    {
                        a = double.Parse(aStr.Replace("x^2", ""));
                    }

                    // Разбираем коэффициент 'b'
                    string bStr = match.Groups[2].Value;
                    if (string.IsNullOrEmpty(bStr) || bStr == "+" || bStr == "-")
                    {
                        b = (bStr == "-") ? -1 : 1; // Если нет цифр, подразумеваем 1 или -1
                    }
                    else
                    {
                        b = double.Parse(bStr.Replace("x", ""));
                    }


                    // Разбираем коэффициент 'c'
                    string cStr = match.Groups[3].Value;
                    c = double.Parse(cStr);

                    return true;
                }
                catch
                {
                    return false; // Ошибка при преобразовании в число
                }
            }
            else
            {
                return false; // Не удалось найти соответствие шаблону
            }
        }

    }
}

using System;

namespace PolynomialLib
{
    // Поліном від однієї змінної: a0 + a1*x + a2*x^2 + ... + an*x^n
    public class Polynomial
    {
        // Похибка для порівняння дійсних чисел
        private const double Eps = 1e-9;

        // Коефіцієнти: coeffs[i] — коефіцієнт при x^i
        private double[] coeffs;

        // Розмірність полінома (кількість коефіцієнтів)
        public int Size { get; private set; }

        // Степінь полінома
        public int Degree
        {
            get { return Size - 1; }
        }

        // Конструктор за замовчуванням: нульовий поліном
        public Polynomial() : this(new double[] { 0 })
        {
        }

        // Конструктор з масивом коефіцієнтів (від молодшого до старшого)
        public Polynomial(double[] coefficients)
        {
            if (coefficients == null)
                throw new ArgumentNullException(nameof(coefficients));
            if (coefficients.Length == 0)
                throw new ArgumentException("Масив коефіцієнтів не може бути порожнім", nameof(coefficients));

            SetCoefficients(coefficients, coefficients.Length);
        }

        // Копіюючий конструктор
        public Polynomial(Polynomial other) : this(other.coeffs)
        {
        }

        // ===== Допоміжні закриті методи =====

        // Копіює перші count коефіцієнтів і відкидає нульові старші коефіцієнти
        private void SetCoefficients(double[] source, int count)
        {
            int size = count;
            while (size > 1 && Math.Abs(source[size - 1]) < Eps)
                size--;
            coeffs = new double[size];
            Array.Copy(source, coeffs, size);
            Size = size;
        }

        // Чи є поліном нульовим
        private bool IsZero()
        {
            return Size == 1 && Math.Abs(coeffs[0]) < Eps;
        }

        // ===== Індексатори =====

        // Коефіцієнт при x^i
        public double this[int i]
        {
            get
            {
                if (i < 0)
                    throw new IndexOutOfRangeException("Номер коефіцієнта не може бути від'ємним");
                return i < Size ? coeffs[i] : 0;
            }
            set
            {
                if (i < 0)
                    throw new IndexOutOfRangeException("Номер коефіцієнта не може бути від'ємним");
                int newSize = Math.Max(Size, i + 1);
                double[] temp = new double[newSize];
                Array.Copy(coeffs, temp, Size);
                temp[i] = value;
                SetCoefficients(temp, newSize);
            }
        }

        // Значення полінома в точці x (схема Горнера)
        public double this[double x]
        {
            get
            {
                double result = 0;
                for (int i = Size - 1; i >= 0; i--)
                    result = result * x + coeffs[i];
                return result;
            }
        }

        // ===== Функція введення =====

        // Введення полінома з консолі: степінь, потім коефіцієнти від a0 до an
        public void Input()
        {
            Console.Write("Введіть степінь полінома: ");
            int degree = Convert.ToInt32(Console.ReadLine());
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree), "Степінь не може бути від'ємним");

            double[] temp = new double[degree + 1];
            for (int i = 0; i <= degree; i++)
            {
                Console.Write("a" + i + " = ");
                temp[i] = Convert.ToDouble(Console.ReadLine());
            }
            SetCoefficients(temp, temp.Length);
        }

        // ===== Арифметичні операції =====

        // Додавання поліномів
        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            int size = Math.Max(a.Size, b.Size);
            double[] result = new double[size];
            for (int i = 0; i < size; i++)
                result[i] = a[i] + b[i];
            return new Polynomial(result);
        }

        // Віднімання поліномів
        public static Polynomial operator -(Polynomial a, Polynomial b)
        {
            int size = Math.Max(a.Size, b.Size);
            double[] result = new double[size];
            for (int i = 0; i < size; i++)
                result[i] = a[i] - b[i];
            return new Polynomial(result);
        }

        // Додавання числа до полінома
        public static Polynomial operator +(Polynomial p, double number)
        {
            Polynomial result = new Polynomial(p);
            result[0] = result[0] + number;
            return result;
        }

        public static Polynomial operator +(double number, Polynomial p)
        {
            return p + number;
        }

        // Множення полінома на число
        public static Polynomial operator *(Polynomial p, double number)
        {
            double[] result = new double[p.Size];
            for (int i = 0; i < p.Size; i++)
                result[i] = p.coeffs[i] * number;
            return new Polynomial(result);
        }

        public static Polynomial operator *(double number, Polynomial p)
        {
            return p * number;
        }

        // ===== Порівняння =====

        public static bool operator ==(Polynomial a, Polynomial b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            if (a.Size != b.Size)
                return false;
            for (int i = 0; i < a.Size; i++)
                if (Math.Abs(a.coeffs[i] - b.coeffs[i]) > Eps)
                    return false;
            return true;
        }

        public static bool operator !=(Polynomial a, Polynomial b)
        {
            return !(a == b);
        }

        // ===== Операції true і false =====
        // Інтерпретація: поліном "істинний", якщо він не нульовий

        public static bool operator true(Polynomial p)
        {
            return !p.IsZero();
        }

        public static bool operator false(Polynomial p)
        {
            return p.IsZero();
        }

        // ===== Приведення типів =====

        // Неявне: число -> поліном нульового степеня (константа)
        public static implicit operator Polynomial(double number)
        {
            return new Polynomial(new double[] { number });
        }

        // Явне: поліном -> число, можливе лише для полінома нульового степеня
        public static explicit operator double(Polynomial p)
        {
            if (p.Degree != 0)
                throw new InvalidCastException("Перетворити на число можна лише поліном нульового степеня");
            return p.coeffs[0];
        }

        // ===== Перевизначені функції класу object =====

        public override bool Equals(object obj)
        {
            return obj is Polynomial && this == (Polynomial)obj;
        }

        public override int GetHashCode()
        {
            return Size.GetHashCode();
        }

        // Вигляд: 3x^2 - 2x + 1
        public override string ToString()
        {
            if (IsZero())
                return "0";

            string result = "";
            for (int i = Size - 1; i >= 0; i--)
            {
                double c = coeffs[i];
                if (Math.Abs(c) < Eps)
                    continue;

                if (result == "")
                    result += c < 0 ? "-" : "";
                else
                    result += c < 0 ? " - " : " + ";

                double abs = Math.Abs(c);
                if (i == 0 || Math.Abs(abs - 1) > Eps)
                    result += abs;

                if (i == 1)
                    result += "x";
                else if (i > 1)
                    result += "x^" + i;
            }
            return result;
        }
    }
}
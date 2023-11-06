using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordEnthropy.Enthropy
{
    public class EnthropyCalculator
    {
        private int _L;
        private int _R;
        private double _E;
        public double GetEnthropyValue { get { return _E; } }

        public double Calculate(string password)
        {
            if (string.IsNullOrEmpty(password))
                return 0d;
            if (!ValidatePassword(password))
                return 0d;

            InitializeValues(password);

            _E = CalculateEnthropy();

            return _E;
        }

        private double CalculateEnthropy()
        {
            return (_L * Math.Log2(_R));
        }

        private void InitializeValues(string password)
        {
            _R = ContainsDigit(password) ? 10 : 0;

            _R += password.Any((ch) =>
            {
                if (char.IsBetween(ch, 'a', 'z'))
                    return true;
                else
                    return false;
            }) ? 26 : 0;

            _R += password.Any((ch) =>
            {
                if (char.IsBetween(ch, 'A', 'Z'))
                    return true;
                else
                    return false;
            }) ? 26 : 0;

            _L = password.Length;
        }

        private bool ValidatePassword(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsDigit(password[i]))
                    continue;

                if ((char.IsLetter(password[i])))
                {

                    if (!(char.IsBetween(password[i], 'a', 'z') || char.IsBetween(password[i], 'A', 'Z')))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool ContainsDigit(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsDigit(password[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

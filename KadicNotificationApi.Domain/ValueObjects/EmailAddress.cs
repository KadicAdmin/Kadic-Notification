using System.Text.RegularExpressions;

namespace KadicNotificationApi.Domain.ValueObjects
{
    public class EmailAddress
    {
        public string Value { get; private set; }
               
        public EmailAddress(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty.", nameof(value));

            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Invalid email format.", nameof(value));

            Value = value;
        }

        public override string ToString() => Value;

        // Igualdad basada en el valor
        public override bool Equals(object? obj) =>
            obj is EmailAddress other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
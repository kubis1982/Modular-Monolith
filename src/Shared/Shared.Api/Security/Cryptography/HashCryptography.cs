namespace ModularMonolith.Shared.Security.Cryptography
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class HashCryptography(HashAlgorithm hashAlgorithm) : IDisposable
    {
        void IDisposable.Dispose() => hashAlgorithm.Dispose();

        public string Compute(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input ?? string.Empty));
            StringBuilder stringBuilder = new();
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public bool Verify(string input, string hash)
        {
            string hashOfInput = Compute(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}

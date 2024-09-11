namespace Kubis1982.Shared.Enumerations {
    public abstract class StringEnumeration(string key, string name) : Enumeration<string>(key, name) {
        public override bool Equals(object? obj) {
            if (obj is not StringEnumeration otherValue) {
                return false;
            }
            return Key.Equals(otherValue.Key, System.StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode() => Key.ToUpper().GetHashCode();
    }
}

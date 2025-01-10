namespace ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Ordering {
    using System.Collections.Generic;

    public static class Statuses {
        public static Dictionary<byte, string> OrderStatuses => new() {
                {0, "Niepotwierdzone"},
                {1, "Potwierdzone"},
                {2, "W realizacji"},
                {3, "Zrealizowane"},
                {4, "Zakończone"}
            };

        public static Dictionary<byte, string> DeliveryStatuses => new() {
                {0, "Niepotwierdzona"},
                {1, "Potwierdzona"},
                {2, "W realizacji"},
                {3, "Zrealizowana"},
                {4, "Zakończona"}
            };
    }
}
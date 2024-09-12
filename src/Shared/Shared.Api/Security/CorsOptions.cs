﻿namespace Kubis1982.Shared.Security
{
    using System.Collections.Generic;

    internal sealed class CorsOptions
    {
        public bool AllowCredentials { get; set; }
        public IEnumerable<string>? AllowedOrigins { get; set; }
        public IEnumerable<string>? AllowedMethods { get; set; }
        public IEnumerable<string>? AllowedHeaders { get; set; }
        public IEnumerable<string>? ExposedHeaders { get; set; }
    }
}

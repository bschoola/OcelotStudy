// <copyright file="EndpointConfiguration.cs" company="Air Liquide">
// Copyright (c) Air Liquide. All rights reserved.
// </copyright>

namespace Delta.Standard.Gateway.Api.Configurations
{
    public class EndpointConfiguration
    {
        public const string SectionName = "EndPoints";

        public string Standard { get; set; } = string.Empty;

        public string StandardHealth => $"{Standard}/health";
    }
}

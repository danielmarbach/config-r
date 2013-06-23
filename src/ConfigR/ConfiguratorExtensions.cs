﻿// <copyright file="ConfiguratorExtensions.cs" company="ConfigR contributors">
//  Copyright (c) ConfigR contributors. (configr.net@gmail.com)
// </copyright>

namespace ConfigR
{
    using System.Diagnostics.CodeAnalysis;

    public static class ConfiguratorExtensions
    {
        public static dynamic Get(this IConfigurator configurator, string key)
        {
            return configurator.Get<dynamic>(key);
        }

        public static dynamic GetOrDefault(this IConfigurator configurator, string key)
        {
            return configurator.GetOrDefault<dynamic>(key);
        }

        [SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate", Justification = "By design.")]
        public static bool TryGet(this IConfigurator configurator, string key, out dynamic value)
        {
            return configurator.TryGet<dynamic>(key, out value);
        }

        public static T Get<T>(this IConfigurator configurator, string key)
        {
            Guard.AgainstNullArgument("configurator", configurator);

            return (T)configurator.Configuration[key];
        }

        public static T GetOrDefault<T>(this IConfigurator configurator, string key)
        {
            Guard.AgainstNullArgument("configurator", configurator);

            dynamic value;
            return configurator.Configuration.TryGetValue(key, out value) ? (T)value : default(T);
        }

        public static bool TryGet<T>(this IConfigurator configurator, string key, out T value)
        {
            Guard.AgainstNullArgument("configurator", configurator);

            value = default(T);
            dynamic dynamicValue;
            if (!configurator.Configuration.TryGetValue(key, out dynamicValue))
            {
                return false;
            }

            value = (T)dynamicValue;
            return true;
        }
    }
}
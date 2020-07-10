using Microsoft.Extensions.Localization;
using System;
using System.Collections.Concurrent;

namespace DbStringLocalizerSample.Localizer
{
    /// <summary>
    /// DbStringLocalizerのファクトリ
    /// 
    /// <see cref="ResourceManagerStringLocalizerFactory"/>
    /// </summary>
    public class DbStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ConcurrentDictionary<RuntimeTypeHandle, DbStringLocalizer> _localizerCache =
            new ConcurrentDictionary<RuntimeTypeHandle, DbStringLocalizer>();

        private readonly DbLocalizedStringSourceProvider _dbLocalizedStringSourceProvider;

        public DbStringLocalizerFactory(DbLocalizedStringSourceProvider dbLocalizedStringSourceProvider)
        {
            _dbLocalizedStringSourceProvider = dbLocalizedStringSourceProvider;
        }

        /// <inheritdoc/>
        public IStringLocalizer Create(string baseName, string location)
        {
            throw new NotImplementedException("Not Implemented");
        }

        /// <inheritdoc/>
        public IStringLocalizer Create(Type resourceSource)
        {
            return _localizerCache.GetOrAdd(resourceSource.TypeHandle, _ => CreateDbStringLocalizer(resourceSource));
        }

        private DbStringLocalizer CreateDbStringLocalizer(Type resourceSource)
        {
            DbLocalizedStringSource source = _dbLocalizedStringSourceProvider.GetLocalizedStrings(resourceSource);
            return new DbStringLocalizer(source);
        }
    }
}

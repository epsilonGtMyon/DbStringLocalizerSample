using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DbStringLocalizerSample.Localizer
{
    /// <summary>
    /// データベースを使用したIStringLocalizerの実装
    /// <see cref="ResourceManagerStringLocalizer"/>
    /// </summary>
    public class DbStringLocalizer : IStringLocalizer
    {
        private readonly DbLocalizedStringSource _dbLocalizedStringSource;

        public DbStringLocalizer(DbLocalizedStringSource dbLocalizedStringSource)
        {
            _dbLocalizedStringSource = dbLocalizedStringSource;
        }

        /// <inheritdoc/>
        public LocalizedString this[string name]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var value = GetString(name);

                return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: null);
            }
        }

        /// <inheritdoc/>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);

                return new LocalizedString(name, value, resourceNotFound: format == null, searchedLocation: null);
            }
        }

        private string GetString(string name, CultureInfo culture = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var keyCulture = culture ?? CultureInfo.CurrentUICulture;

            return _dbLocalizedStringSource.GetString(name, keyCulture);
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            IEnumerable<string> allKey = _dbLocalizedStringSource.GetAllKey();

            var culture = CultureInfo.CurrentUICulture;
            foreach (var key in allKey)
            {
                var value = GetString(key, culture);
                yield return new LocalizedString(key, value ?? key, resourceNotFound: value == null, searchedLocation: null);
            }
        }

        /// <summary>
        /// インターフェースのこのメソッドがObsoleteなので実装していません。
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        [Obsolete("This method is obsolete. Use `CurrentCulture` and `CurrentUICulture` instead.")]
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException("Not Implemented");
        }
    }
}

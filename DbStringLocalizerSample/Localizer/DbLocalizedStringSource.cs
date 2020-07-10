using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DbStringLocalizerSample.Localizer
{
    /// <summary>
    /// データベースから取得した国際化リソースのソースを保持するクラス
    /// </summary>
    public class DbLocalizedStringSource
    {
        private readonly IDictionary<string, LocalizationRecord> _records;

        public DbLocalizedStringSource(IDictionary<string, LocalizationRecord> records)
        {
            _records = records;
        }

        public static DbLocalizedStringSource FromEnumerable(IEnumerable<LocalizationRecord> src)
        {
            IDictionary<string, LocalizationRecord> records = src.ToDictionary(x => x.Key);
            return new DbLocalizedStringSource(records);
        }

        public IEnumerable<string> GetAllKey()
        {
            return _records.Keys;
        }

        public string GetString(string name, CultureInfo currentUICulture)
        {
            if (_records.TryGetValue(name, out LocalizationRecord record))
            {
                switch (currentUICulture.Name)
                {
                    case "ja": return record.Ja;
                    case "en": return record.En;
                }
            }
            return null;
        }
    }
}

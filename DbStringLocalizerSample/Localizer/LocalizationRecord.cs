namespace DbStringLocalizerSample.Localizer
{
    /// <summary>
    /// データベースで管理されている国際化リソースのレコード
    /// </summary>
    public class LocalizationRecord
    {
        public string Key { get; set; }

        public string Ja { get; set; }

        public string En { get; set; }
    }
}

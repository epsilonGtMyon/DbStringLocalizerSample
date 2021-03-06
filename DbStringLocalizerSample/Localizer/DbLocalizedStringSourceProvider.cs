﻿using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DbStringLocalizerSample.Localizer
{
    /// <summary>
    /// データベースから国際化リソースのソースを取得するクラス
    /// </summary>
    public class DbLocalizedStringSourceProvider
    {
        private const string connectionString = "Host=localhost;Database=test_db;Username=test_user;Password=test_user";

        public DbLocalizedStringSource GetLocalizedStrings(Type resourceSource)
        {
            using IDbConnection con = new NpgsqlConnection(connectionString);
            con.Open();
            using IDbTransaction tran = con.BeginTransaction();


            string sql = @"
SELECT
   key_name as Key
  ,ja       as Ja
  ,en       as En
FROM
   localization_resource
WHERE
   category = @category
ORDER BY
  key
";

            var param = new
            {
                category = resourceSource.Name
            };

            IEnumerable<LocalizationRecord> records = con.Query<LocalizationRecord>(sql, param, tran);
            return DbLocalizedStringSource.FromEnumerable(records);

        }
    }
}

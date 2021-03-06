﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Xunit;

namespace Npgsql.EntityFrameworkCore.PostgreSQL.FunctionalTests
{
    public class InheritanceNpgsqlTest : InheritanceTestBase<InheritanceNpgsqlFixture>
    {
        public override void Can_use_of_type_animal()
        {
            base.Can_use_of_type_animal();

            Assert.Equal(
                @"SELECT ""t"".""Species"", ""t"".""CountryId"", ""t"".""Discriminator"", ""t"".""Name"", ""t"".""EagleId"", ""t"".""IsFlightless"", ""t"".""Group"", ""t"".""FoundOn""
FROM (
    SELECT ""a0"".""Species"", ""a0"".""CountryId"", ""a0"".""Discriminator"", ""a0"".""Name"", ""a0"".""EagleId"", ""a0"".""IsFlightless"", ""a0"".""Group"", ""a0"".""FoundOn""
    FROM ""Animal"" AS ""a0""
    WHERE ""a0"".""Discriminator"" IN ('Kiwi', 'Eagle')
) AS ""t""
ORDER BY ""t"".""Species""",
                Sql);
        }

        public override void Can_use_of_type_bird()
        {
            base.Can_use_of_type_bird();

            Assert.Equal(
                @"SELECT ""t"".""Species"", ""t"".""CountryId"", ""t"".""Discriminator"", ""t"".""Name"", ""t"".""EagleId"", ""t"".""IsFlightless"", ""t"".""Group"", ""t"".""FoundOn""
FROM (
    SELECT ""a0"".""Species"", ""a0"".""CountryId"", ""a0"".""Discriminator"", ""a0"".""Name"", ""a0"".""EagleId"", ""a0"".""IsFlightless"", ""a0"".""Group"", ""a0"".""FoundOn""
    FROM ""Animal"" AS ""a0""
    WHERE ""a0"".""Discriminator"" IN ('Kiwi', 'Eagle')
) AS ""t""
ORDER BY ""t"".""Species""",
                Sql);
        }

        public override void Can_use_of_type_bird_first()
        {
            base.Can_use_of_type_bird_first();

            Assert.Equal(
                @"SELECT ""t"".""Species"", ""t"".""CountryId"", ""t"".""Discriminator"", ""t"".""Name"", ""t"".""EagleId"", ""t"".""IsFlightless"", ""t"".""Group"", ""t"".""FoundOn""
FROM (
    SELECT ""a0"".""Species"", ""a0"".""CountryId"", ""a0"".""Discriminator"", ""a0"".""Name"", ""a0"".""EagleId"", ""a0"".""IsFlightless"", ""a0"".""Group"", ""a0"".""FoundOn""
    FROM ""Animal"" AS ""a0""
    WHERE ""a0"".""Discriminator"" IN ('Kiwi', 'Eagle')
) AS ""t""
ORDER BY ""t"".""Species""
LIMIT 1",
                Sql);
        }

        public override void Can_use_of_type_kiwi()
        {
            base.Can_use_of_type_kiwi();

            Assert.Equal(
                @"SELECT ""a"".""Species"", ""a"".""CountryId"", ""a"".""Discriminator"", ""a"".""Name"", ""a"".""EagleId"", ""a"".""IsFlightless"", ""a"".""Group"", ""a"".""FoundOn""
FROM ""Animal"" AS ""a""
WHERE ""a"".""Discriminator"" = 'Kiwi'",
                Sql);
        }

        public override void Can_use_of_type_rose()
        {
            base.Can_use_of_type_rose();

            Assert.Equal(
                @"SELECT ""p"".""Species"", ""p"".""CountryId"", ""p"".""Genus"", ""p"".""Name"", ""p"".""HasThorns""
FROM ""Plant"" AS ""p""
WHERE ""p"".""Genus"" = 0",
                Sql);
        }

        public override void Can_query_all_animals()
        {
            base.Can_query_all_animals();

            Assert.Equal(
                @"SELECT ""a"".""Species"", ""a"".""CountryId"", ""a"".""Discriminator"", ""a"".""Name"", ""a"".""EagleId"", ""a"".""IsFlightless"", ""a"".""Group"", ""a"".""FoundOn""
FROM ""Animal"" AS ""a""
WHERE ""a"".""Discriminator"" IN ('Kiwi', 'Eagle')
ORDER BY ""a"".""Species""",
                Sql);
        }

        public override void Can_query_all_plants()
        {
            base.Can_query_all_plants();

            Assert.Equal(
                @"SELECT ""a"".""Species"", ""a"".""CountryId"", ""a"".""Genus"", ""a"".""Name"", ""a"".""HasThorns""
FROM ""Plant"" AS ""a""
WHERE ""a"".""Genus"" IN (0, 1)
ORDER BY ""a"".""Species""",
                Sql);
        }

        public override void Can_filter_all_animals()
        {
            base.Can_filter_all_animals();

            Assert.Equal(
                @"SELECT ""a"".""Species"", ""a"".""CountryId"", ""a"".""Discriminator"", ""a"".""Name"", ""a"".""EagleId"", ""a"".""IsFlightless"", ""a"".""Group"", ""a"".""FoundOn""
FROM ""Animal"" AS ""a""
WHERE ""a"".""Discriminator"" IN ('Kiwi', 'Eagle') AND (""a"".""Name"" = 'Great spotted kiwi')
ORDER BY ""a"".""Species""",
                Sql);
        }

        public override void Can_query_all_birds()
        {
            base.Can_query_all_birds();

            Assert.Equal(
                @"SELECT ""a"".""Species"", ""a"".""CountryId"", ""a"".""Discriminator"", ""a"".""Name"", ""a"".""EagleId"", ""a"".""IsFlightless"", ""a"".""Group"", ""a"".""FoundOn""
FROM ""Animal"" AS ""a""
WHERE ""a"".""Discriminator"" IN ('Kiwi', 'Eagle')
ORDER BY ""a"".""Species""",
                Sql);
        }

        public override void Can_query_just_kiwis()
        {
            base.Can_query_just_kiwis();

            Assert.Equal(
                @"SELECT ""a"".""Species"", ""a"".""CountryId"", ""a"".""Discriminator"", ""a"".""Name"", ""a"".""EagleId"", ""a"".""IsFlightless"", ""a"".""FoundOn""
FROM ""Animal"" AS ""a""
WHERE ""a"".""Discriminator"" = 'Kiwi'
LIMIT 2",
                Sql);
        }

        public override void Can_query_just_roses()
        {
            base.Can_query_just_roses();

            Assert.Equal(
                @"SELECT ""p"".""Species"", ""p"".""CountryId"", ""p"".""Genus"", ""p"".""Name"", ""p"".""HasThorns""
FROM ""Plant"" AS ""p""
WHERE ""p"".""Genus"" = 0
LIMIT 2",
                Sql);
        }

        public override void Can_include_prey()
        {
            base.Can_include_prey();

            // TODO: Assert on SQL
        }

        public override void Can_include_animals()
        {
            base.Can_include_animals();

            Assert.Equal(
                @"SELECT ""c"".""Id"", ""c"".""Name""
FROM ""Country"" AS ""c""
ORDER BY ""c"".""Name"", ""c"".""Id""

SELECT ""a"".""Species"", ""a"".""CountryId"", ""a"".""Discriminator"", ""a"".""Name"", ""a"".""EagleId"", ""a"".""IsFlightless"", ""a"".""Group"", ""a"".""FoundOn""
FROM ""Animal"" AS ""a""
INNER JOIN (
    SELECT DISTINCT ""c"".""Name"", ""c"".""Id""
    FROM ""Country"" AS ""c""
) AS ""c0"" ON ""a"".""CountryId"" = ""c0"".""Id""
WHERE ""a"".""Discriminator"" IN ('Kiwi', 'Eagle')
ORDER BY ""c0"".""Name"", ""c0"".""Id""",
                Sql);
        }

        public override void Can_insert_update_delete()
        {
            base.Can_insert_update_delete();

            // TODO: Assert on SQL
        }

        public InheritanceNpgsqlTest(InheritanceNpgsqlFixture fixture)
            : base(fixture)
        {
        }

        private static string Sql => TestSqlLoggerFactory.Sql;
    }
}

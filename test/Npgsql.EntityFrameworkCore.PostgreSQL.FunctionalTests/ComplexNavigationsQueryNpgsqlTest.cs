﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Npgsql.EntityFrameworkCore.PostgreSQL.FunctionalTests.Utilities;
using Xunit.Abstractions;

namespace Npgsql.EntityFrameworkCore.PostgreSQL.FunctionalTests
{
    public class ComplexNavigationsQueryNpgsqlTest : ComplexNavigationsQueryTestBase<NpgsqlTestStore, ComplexNavigationsQueryNpgsqlFixture>
    {
        public ComplexNavigationsQueryNpgsqlTest(ComplexNavigationsQueryNpgsqlFixture fixture)
            : base(fixture)
        {
        }

        protected override void ClearLog() => TestSqlLoggerFactory.Reset();

        public override void Multi_level_include_one_to_many_optional_and_one_to_many_optional_produces_valid_sql()
        {
            base.Multi_level_include_one_to_many_optional_and_one_to_many_optional_produces_valid_sql();

            // TODO: Assert on SQL
        }

        public override void Multi_level_include_correct_PK_is_chosen_as_the_join_predicate_for_queries_that_join_same_table_multiple_times()
        {
            base.Multi_level_include_correct_PK_is_chosen_as_the_join_predicate_for_queries_that_join_same_table_multiple_times();

            // TODO: Assert on SQL
        }

        public override void Multi_level_include_with_short_circuiting()
        {
            base.Multi_level_include_with_short_circuiting();

            // TODO: Assert on SQL
        }

        public override void Join_navigation_key_access_optional()
        {
            base.Join_navigation_key_access_optional();

            Assert.Equal(
                @"SELECT ""e1"".""Id"", ""e2"".""Id""
FROM ""Level1"" AS ""e1""
INNER JOIN ""Level2"" AS ""e2"" ON ""e1"".""Id"" = (
    SELECT ""subQuery0"".""Id""
    FROM ""Level1"" AS ""subQuery0""
    WHERE ""subQuery0"".""Id"" = ""e2"".""Level1_Optional_Id""
    LIMIT 1
)",
                Sql);
        }

        public override void Join_navigation_key_access_required()
        {
            base.Join_navigation_key_access_required();

            Assert.Equal(
                @"SELECT ""e1"".""Id"", ""e2"".""Id""
FROM ""Level1"" AS ""e1""
INNER JOIN ""Level2"" AS ""e2"" ON ""e1"".""Id"" = ""e2"".""Level1_Required_Id""",
                Sql);
        }

        public override void Navigation_key_access_optional_comparison()
        {
            base.Navigation_key_access_optional_comparison();

            Assert.Equal(
                @"SELECT ""e2"".""Id"", ""e2"".""Level1_Optional_Id"", ""e2"".""Level1_Required_Id"", ""e2"".""Name"", ""e2"".""OneToMany_Optional_InverseId"", ""e2"".""OneToMany_Optional_Self_InverseId"", ""e2"".""OneToMany_Required_InverseId"", ""e2"".""OneToMany_Required_Self_InverseId"", ""e2"".""OneToOne_Optional_PK_InverseId"", ""e2"".""OneToOne_Optional_SelfId"", ""e2.OneToOne_Optional_PK_Inverse"".""Id"", ""e2.OneToOne_Optional_PK_Inverse"".""Name"", ""e2.OneToOne_Optional_PK_Inverse"".""OneToMany_Optional_Self_InverseId"", ""e2.OneToOne_Optional_PK_Inverse"".""OneToMany_Required_Self_InverseId"", ""e2.OneToOne_Optional_PK_Inverse"".""OneToOne_Optional_SelfId""
FROM ""Level2"" AS ""e2""
LEFT JOIN ""Level1"" AS ""e2.OneToOne_Optional_PK_Inverse"" ON ""e2"".""OneToOne_Optional_PK_InverseId"" = ""e2.OneToOne_Optional_PK_Inverse"".""Id""
ORDER BY ""e2"".""OneToOne_Optional_PK_InverseId""",
                Sql);
        }

        public override void Navigation_key_access_required_comparison()
        {
            base.Navigation_key_access_required_comparison();

            Assert.Equal(
                @"SELECT ""e2"".""Id""
FROM ""Level2"" AS ""e2""
WHERE ""e2"".""Id"" > 5", Sql);
        }

        public override void Join_navigation_in_outer_selector_translated_to_extra_join()
        {
            base.Join_navigation_in_outer_selector_translated_to_extra_join();

            // See issue#4458
            Assert.Contains(
                @"SELECT ""e2"".""Id""
FROM ""Level2"" AS ""e2""",
                Sql);

            Assert.Contains(
                @"SELECT ""e1"".""Id"", ""e1"".""Name"", ""e1"".""OneToMany_Optional_Self_InverseId"", ""e1"".""OneToMany_Required_Self_InverseId"", ""e1"".""OneToOne_Optional_SelfId"", ""e1.OneToOne_Optional_FK"".""Id"", ""e1.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""e1.OneToOne_Optional_FK"".""Level1_Required_Id"", ""e1.OneToOne_Optional_FK"".""Name"", ""e1.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e1.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e1.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e1.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e1.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e1.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e1""
LEFT JOIN ""Level2"" AS ""e1.OneToOne_Optional_FK"" ON ""e1"".""Id"" = ""e1.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""e1"".""Id""",
                Sql);
        }

        public override void Join_navigation_in_outer_selector_translated_to_extra_join_nested()
        {
            base.Join_navigation_in_outer_selector_translated_to_extra_join_nested();

            // See issue#4458
            Assert.Contains(
                @"SELECT ""e3"".""Id""
FROM ""Level3"" AS ""e3""",
                Sql);

            Assert.Contains(
                @"SELECT ""e1.OneToOne_Required_FK"".""Id"", ""e1.OneToOne_Required_FK"".""Level1_Optional_Id"", ""e1.OneToOne_Required_FK"".""Level1_Required_Id"", ""e1.OneToOne_Required_FK"".""Name"", ""e1.OneToOne_Required_FK"".""OneToMany_Optional_InverseId"", ""e1.OneToOne_Required_FK"".""OneToMany_Optional_Self_InverseId"", ""e1.OneToOne_Required_FK"".""OneToMany_Required_InverseId"", ""e1.OneToOne_Required_FK"".""OneToMany_Required_Self_InverseId"", ""e1.OneToOne_Required_FK"".""OneToOne_Optional_PK_InverseId"", ""e1.OneToOne_Required_FK"".""OneToOne_Optional_SelfId"", ""e1.OneToOne_Required_FK.OneToOne_Optional_FK"".""Id"", ""e1.OneToOne_Required_FK.OneToOne_Optional_FK"".""Level2_Optional_Id"", ""e1.OneToOne_Required_FK.OneToOne_Optional_FK"".""Level2_Required_Id"", ""e1.OneToOne_Required_FK.OneToOne_Optional_FK"".""Name"", ""e1.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e1.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e1.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e1.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e1.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e1.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId"", ""e1"".""Id""
FROM ""Level1"" AS ""e1""
INNER JOIN ""Level2"" AS ""e1.OneToOne_Required_FK"" ON ""e1"".""Id"" = ""e1.OneToOne_Required_FK"".""Level1_Required_Id""
LEFT JOIN ""Level3"" AS ""e1.OneToOne_Required_FK.OneToOne_Optional_FK"" ON ""e1.OneToOne_Required_FK"".""Id"" = ""e1.OneToOne_Required_FK.OneToOne_Optional_FK"".""Level2_Optional_Id""
ORDER BY ""e1.OneToOne_Required_FK"".""Id""",
                Sql);
        }

        public override void Join_navigation_in_inner_selector_translated_to_subquery()
        {
            base.Join_navigation_in_inner_selector_translated_to_subquery();

            Assert.Equal(
                @"SELECT ""e2"".""Id"", ""e1"".""Id""
FROM ""Level2"" AS ""e2""
INNER JOIN ""Level1"" AS ""e1"" ON ""e2"".""Id"" = (
    SELECT ""subQuery0"".""Id""
    FROM ""Level2"" AS ""subQuery0""
    WHERE ""subQuery0"".""Level1_Optional_Id"" = ""e1"".""Id""
    LIMIT 1
)",
                Sql);
        }

        public override void Join_navigations_in_inner_selector_translated_to_multiple_subquery_without_collision()
        {
            base.Join_navigations_in_inner_selector_translated_to_multiple_subquery_without_collision();

            Assert.Equal(
                @"SELECT ""e2"".""Id"", ""e1"".""Id"", ""e3"".""Id""
FROM ""Level2"" AS ""e2""
INNER JOIN ""Level1"" AS ""e1"" ON ""e2"".""Id"" = (
    SELECT ""subQuery0"".""Id""
    FROM ""Level2"" AS ""subQuery0""
    WHERE ""subQuery0"".""Level1_Optional_Id"" = ""e1"".""Id""
    LIMIT 1
)
INNER JOIN ""Level3"" AS ""e3"" ON ""e2"".""Id"" = (
    SELECT ""subQuery2"".""Id""
    FROM ""Level2"" AS ""subQuery2""
    WHERE ""subQuery2"".""Id"" = ""e3"".""Level2_Optional_Id""
    LIMIT 1
)",
                Sql);
        }

        public override void Join_navigation_translated_to_subquery_non_key_join()
        {
            base.Join_navigation_translated_to_subquery_non_key_join();

            Assert.Equal(
                @"SELECT ""e2"".""Id"", ""e2"".""Name"", ""e1"".""Id"", ""e1"".""Name""
FROM ""Level2"" AS ""e2""
INNER JOIN ""Level1"" AS ""e1"" ON ""e2"".""Name"" = (
    SELECT ""subQuery0"".""Name""
    FROM ""Level2"" AS ""subQuery0""
    WHERE ""subQuery0"".""Level1_Optional_Id"" = ""e1"".""Id""
    LIMIT 1
)",
                Sql);
        }

        public override void Join_navigation_translated_to_subquery_self_ref()
        {
            base.Join_navigation_translated_to_subquery_self_ref();

            Assert.Equal(
                @"SELECT ""e1"".""Id"", ""e2"".""Id""
FROM ""Level1"" AS ""e1""
INNER JOIN ""Level1"" AS ""e2"" ON ""e1"".""Id"" = (
    SELECT ""subQuery0"".""Id""
    FROM ""Level1"" AS ""subQuery0""
    WHERE ""subQuery0"".""Id"" = ""e2"".""OneToMany_Optional_Self_InverseId""
    LIMIT 1
)",
                Sql);
        }

        public override void Join_navigation_translated_to_subquery_nested()
        {
            base.Join_navigation_translated_to_subquery_nested();

            Assert.Contains(
                @"SELECT ""e1"".""Id""
FROM ""Level1"" AS ""e1""",
                Sql);

            Assert.Contains(
                @"SELECT ""subQuery"".""Id"", ""subQuery"".""Level1_Optional_Id"", ""subQuery"".""Level1_Required_Id"", ""subQuery"".""Name"", ""subQuery"".""OneToMany_Optional_InverseId"", ""subQuery"".""OneToMany_Optional_Self_InverseId"", ""subQuery"".""OneToMany_Required_InverseId"", ""subQuery"".""OneToMany_Required_Self_InverseId"", ""subQuery"".""OneToOne_Optional_PK_InverseId"", ""subQuery"".""OneToOne_Optional_SelfId"", ""subQuery.OneToOne_Optional_FK"".""Id"", ""subQuery.OneToOne_Optional_FK"".""Level2_Optional_Id"", ""subQuery.OneToOne_Optional_FK"".""Level2_Required_Id"", ""subQuery.OneToOne_Optional_FK"".""Name"", ""subQuery.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""subQuery.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""subQuery.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""subQuery.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""subQuery.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""subQuery.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level2"" AS ""subQuery""
LEFT JOIN ""Level3"" AS ""subQuery.OneToOne_Optional_FK"" ON ""subQuery"".""Id"" = ""subQuery.OneToOne_Optional_FK"".""Level2_Optional_Id""
ORDER BY ""subQuery"".""Id""",
                Sql);
        }

        public override void Join_navigation_translated_to_subquery_deeply_nested_non_key_join()
        {
            base.Join_navigation_translated_to_subquery_deeply_nested_non_key_join();

            Assert.Contains(
                @"SELECT ""e1"".""Id"", ""e1"".""Name""
FROM ""Level1"" AS ""e1""",
                Sql);

            Assert.Contains(
                @"SELECT ""subQuery.OneToOne_Optional_FK.OneToOne_Required_PK"".""Id"", ""subQuery.OneToOne_Optional_FK.OneToOne_Required_PK"".""Level3_Optional_Id"", ""subQuery.OneToOne_Optional_FK.OneToOne_Required_PK"".""Level3_Required_Id"", ""subQuery.OneToOne_Optional_FK.OneToOne_Required_PK"".""Name"", ""subQuery.OneToOne_Optional_FK.OneToOne_Required_PK"".""OneToMany_Optional_InverseId"", ""subQuery.OneToOne_Optional_FK.OneToOne_Required_PK"".""OneToMany_Optional_Self_InverseId"", ""subQuery.OneToOne_Optional_FK.OneToOne_Required_PK"".""OneToMany_Required_InverseId"", ""subQuery.OneToOne_Optional_FK.OneToOne_Required_PK"".""OneToMany_Required_Self_InverseId"", ""subQuery.OneToOne_Optional_FK.OneToOne_Required_PK"".""OneToOne_Optional_PK_InverseId"", ""subQuery.OneToOne_Optional_FK.OneToOne_Required_PK"".""OneToOne_Optional_SelfId""
FROM ""Level4"" AS ""subQuery.OneToOne_Optional_FK.OneToOne_Required_PK""",
                Sql);

            Assert.Contains(
                @"SELECT ""subQuery"".""Id"", ""subQuery"".""Level1_Optional_Id"", ""subQuery"".""Level1_Required_Id"", ""subQuery"".""Name"", ""subQuery"".""OneToMany_Optional_InverseId"", ""subQuery"".""OneToMany_Optional_Self_InverseId"", ""subQuery"".""OneToMany_Required_InverseId"", ""subQuery"".""OneToMany_Required_Self_InverseId"", ""subQuery"".""OneToOne_Optional_PK_InverseId"", ""subQuery"".""OneToOne_Optional_SelfId"", ""subQuery.OneToOne_Optional_FK"".""Id"", ""subQuery.OneToOne_Optional_FK"".""Level2_Optional_Id"", ""subQuery.OneToOne_Optional_FK"".""Level2_Required_Id"", ""subQuery.OneToOne_Optional_FK"".""Name"", ""subQuery.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""subQuery.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""subQuery.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""subQuery.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""subQuery.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""subQuery.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level2"" AS ""subQuery""
LEFT JOIN ""Level3"" AS ""subQuery.OneToOne_Optional_FK"" ON ""subQuery"".""Id"" = ""subQuery.OneToOne_Optional_FK"".""Level2_Optional_Id""
ORDER BY ""subQuery"".""Id""",
                Sql);
        }

        public override void Multiple_complex_includes()
        {
            base.Multiple_complex_includes();

            // TODO: Assert on SQL
        }

        public override void Multiple_complex_includes_self_ref()
        {
            base.Multiple_complex_includes_self_ref();

            // TODO: Assert on SQL
        }

        public override void Multiple_complex_include_select()
        {
            base.Multiple_complex_include_select();

            // TODO: Assert on SQL
        }

        public override void Select_nav_prop_reference_optional1()
        {
            base.Select_nav_prop_reference_optional1();

            Assert.Equal(
                @"SELECT ""e"".""Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Optional_FK"".""Id"", ""e.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""e.OneToOne_Optional_FK"".""Level1_Required_Id"", ""e.OneToOne_Optional_FK"".""Name"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
LEFT JOIN ""Level2"" AS ""e.OneToOne_Optional_FK"" ON ""e"".""Id"" = ""e.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""e"".""Id""",
                Sql);
        }

        public override void Select_nav_prop_reference_optional1_via_DefaultIfEmpty()
        {
            base.Select_nav_prop_reference_optional1_via_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT ""l2"".""Id"", ""l2"".""Level1_Optional_Id"", ""l2"".""Level1_Required_Id"", ""l2"".""Name"", ""l2"".""OneToMany_Optional_InverseId"", ""l2"".""OneToMany_Optional_Self_InverseId"", ""l2"".""OneToMany_Required_InverseId"", ""l2"".""OneToMany_Required_Self_InverseId"", ""l2"".""OneToOne_Optional_PK_InverseId"", ""l2"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
LEFT JOIN ""Level2"" AS ""l2"" ON ""l1"".""Id"" = ""l2"".""Level1_Optional_Id""
ORDER BY ""l1"".""Id""",
                Sql);
        }

        public override void Select_nav_prop_reference_optional2()
        {
            base.Select_nav_prop_reference_optional2();

            Assert.Equal(
                @"SELECT ""e"".""Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Optional_FK"".""Id"", ""e.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""e.OneToOne_Optional_FK"".""Level1_Required_Id"", ""e.OneToOne_Optional_FK"".""Name"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
LEFT JOIN ""Level2"" AS ""e.OneToOne_Optional_FK"" ON ""e"".""Id"" = ""e.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""e"".""Id""",
                Sql);
        }

        public override void Select_nav_prop_reference_optional2_via_DefaultIfEmpty()
        {
            base.Select_nav_prop_reference_optional2_via_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT ""l2"".""Id"", ""l2"".""Level1_Optional_Id"", ""l2"".""Level1_Required_Id"", ""l2"".""Name"", ""l2"".""OneToMany_Optional_InverseId"", ""l2"".""OneToMany_Optional_Self_InverseId"", ""l2"".""OneToMany_Required_InverseId"", ""l2"".""OneToMany_Required_Self_InverseId"", ""l2"".""OneToOne_Optional_PK_InverseId"", ""l2"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
LEFT JOIN ""Level2"" AS ""l2"" ON ""l1"".""Id"" = ""l2"".""Level1_Optional_Id""
ORDER BY ""l1"".""Id""",
                Sql);
        }

        public override void Select_nav_prop_reference_optional3()
        {
            base.Select_nav_prop_reference_optional3();

            Assert.Equal(
                @"SELECT ""e"".""Id"", ""e"".""Level1_Optional_Id"", ""e"".""Level1_Required_Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_InverseId"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_PK_InverseId"", ""e"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Optional_FK_Inverse"".""Id"", ""e.OneToOne_Optional_FK_Inverse"".""Name"", ""e.OneToOne_Optional_FK_Inverse"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK_Inverse"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK_Inverse"".""OneToOne_Optional_SelfId""
FROM ""Level2"" AS ""e""
LEFT JOIN ""Level1"" AS ""e.OneToOne_Optional_FK_Inverse"" ON ""e"".""Level1_Optional_Id"" = ""e.OneToOne_Optional_FK_Inverse"".""Id""
ORDER BY ""e"".""Level1_Optional_Id""",
                Sql);
        }

        public override void Where_nav_prop_reference_optional1()
        {
            base.Where_nav_prop_reference_optional1();

            Assert.Equal(
                @"SELECT ""e"".""Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Optional_FK"".""Id"", ""e.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""e.OneToOne_Optional_FK"".""Level1_Required_Id"", ""e.OneToOne_Optional_FK"".""Name"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
LEFT JOIN ""Level2"" AS ""e.OneToOne_Optional_FK"" ON ""e"".""Id"" = ""e.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""e"".""Id""",
                Sql);
        }

        public override void Where_nav_prop_reference_optional1_via_DefaultIfEmpty()
        {
            base.Where_nav_prop_reference_optional1_via_DefaultIfEmpty();

            Assert.Contains(
                @"SELECT ""l2Right"".""Id"", ""l2Right"".""Level1_Optional_Id"", ""l2Right"".""Level1_Required_Id"", ""l2Right"".""Name"", ""l2Right"".""OneToMany_Optional_InverseId"", ""l2Right"".""OneToMany_Optional_Self_InverseId"", ""l2Right"".""OneToMany_Required_InverseId"", ""l2Right"".""OneToMany_Required_Self_InverseId"", ""l2Right"".""OneToOne_Optional_PK_InverseId"", ""l2Right"".""OneToOne_Optional_SelfId""
FROM ""Level2"" AS ""l2Right""",
                Sql);

            Assert.Contains(
                @"SELECT ""l2Left"".""Id"", ""l2Left"".""Level1_Optional_Id"", ""l2Left"".""Level1_Required_Id"", ""l2Left"".""Name"", ""l2Left"".""OneToMany_Optional_InverseId"", ""l2Left"".""OneToMany_Optional_Self_InverseId"", ""l2Left"".""OneToMany_Required_InverseId"", ""l2Left"".""OneToMany_Required_Self_InverseId"", ""l2Left"".""OneToOne_Optional_PK_InverseId"", ""l2Left"".""OneToOne_Optional_SelfId"", ""l1"".""Id""
FROM ""Level1"" AS ""l1""
LEFT JOIN ""Level2"" AS ""l2Left"" ON ""l1"".""Id"" = ""l2Left"".""Level1_Optional_Id""
ORDER BY ""l1"".""Id""",
                Sql);
        }

        public override void Where_nav_prop_reference_optional2()
        {
            base.Where_nav_prop_reference_optional2();

            Assert.Equal(
                @"SELECT ""e"".""Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Optional_FK"".""Id"", ""e.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""e.OneToOne_Optional_FK"".""Level1_Required_Id"", ""e.OneToOne_Optional_FK"".""Name"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
LEFT JOIN ""Level2"" AS ""e.OneToOne_Optional_FK"" ON ""e"".""Id"" = ""e.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""e"".""Id""",
                Sql);
        }

        public override void Where_nav_prop_reference_optional2_via_DefaultIfEmpty()
        {
            base.Where_nav_prop_reference_optional2_via_DefaultIfEmpty();

            Assert.Contains(
                @"SELECT ""l2Right"".""Id"", ""l2Right"".""Level1_Optional_Id"", ""l2Right"".""Level1_Required_Id"", ""l2Right"".""Name"", ""l2Right"".""OneToMany_Optional_InverseId"", ""l2Right"".""OneToMany_Optional_Self_InverseId"", ""l2Right"".""OneToMany_Required_InverseId"", ""l2Right"".""OneToMany_Required_Self_InverseId"", ""l2Right"".""OneToOne_Optional_PK_InverseId"", ""l2Right"".""OneToOne_Optional_SelfId""
FROM ""Level2"" AS ""l2Right""",
                Sql);

            Assert.Contains(
                @"SELECT ""l2Left"".""Id"", ""l2Left"".""Level1_Optional_Id"", ""l2Left"".""Level1_Required_Id"", ""l2Left"".""Name"", ""l2Left"".""OneToMany_Optional_InverseId"", ""l2Left"".""OneToMany_Optional_Self_InverseId"", ""l2Left"".""OneToMany_Required_InverseId"", ""l2Left"".""OneToMany_Required_Self_InverseId"", ""l2Left"".""OneToOne_Optional_PK_InverseId"", ""l2Left"".""OneToOne_Optional_SelfId"", ""l1"".""Id""
FROM ""Level1"" AS ""l1""
LEFT JOIN ""Level2"" AS ""l2Left"" ON ""l1"".""Id"" = ""l2Left"".""Level1_Optional_Id""
ORDER BY ""l1"".""Id""",
                Sql);
        }

        public override void Select_multiple_nav_prop_reference_optional()
        {
            base.Select_multiple_nav_prop_reference_optional();

            Assert.Contains(
                @"SELECT ""e.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Id"", ""e.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Level2_Optional_Id"", ""e.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Level2_Required_Id"", ""e.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Name"", ""e.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""e.OneToOne_Optional_FK.OneToOne_Optional_FK""",
                Sql);

            Assert.Contains(
                @"SELECT ""e"".""Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Optional_FK"".""Id"", ""e.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""e.OneToOne_Optional_FK"".""Level1_Required_Id"", ""e.OneToOne_Optional_FK"".""Name"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
LEFT JOIN ""Level2"" AS ""e.OneToOne_Optional_FK"" ON ""e"".""Id"" = ""e.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""e"".""Id""",
                Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_member_compared_to_value()
        {
            base.Where_multiple_nav_prop_reference_optional_member_compared_to_value();

            Assert.Contains(
                @"SELECT ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Id"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Level2_Optional_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Level2_Required_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Name"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK""", Sql);

            Assert.Contains(
                @"SELECT ""l1"".""Id"", ""l1"".""Name"", ""l1"".""OneToMany_Optional_Self_InverseId"", ""l1"".""OneToMany_Required_Self_InverseId"", ""l1"".""OneToOne_Optional_SelfId"", ""l1.OneToOne_Optional_FK"".""Id"", ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""l1.OneToOne_Optional_FK"".""Level1_Required_Id"", ""l1.OneToOne_Optional_FK"".""Name"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
LEFT JOIN ""Level2"" AS ""l1.OneToOne_Optional_FK"" ON ""l1"".""Id"" = ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""l1"".""Id""", Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_member_compared_to_null()
        {
            base.Where_multiple_nav_prop_reference_optional_member_compared_to_null();

            Assert.Contains(
                @"SELECT ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Id"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Level2_Optional_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Level2_Required_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Name"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK""",
                Sql);

            Assert.Contains(
                @"SELECT ""l1"".""Id"", ""l1"".""Name"", ""l1"".""OneToMany_Optional_Self_InverseId"", ""l1"".""OneToMany_Required_Self_InverseId"", ""l1"".""OneToOne_Optional_SelfId"", ""l1.OneToOne_Optional_FK"".""Id"", ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""l1.OneToOne_Optional_FK"".""Level1_Required_Id"", ""l1.OneToOne_Optional_FK"".""Name"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
LEFT JOIN ""Level2"" AS ""l1.OneToOne_Optional_FK"" ON ""l1"".""Id"" = ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""l1"".""Id""",
                Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_compared_to_null1()
        {
            base.Where_multiple_nav_prop_reference_optional_compared_to_null1();

            Assert.Contains(
                @"SELECT ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Id"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Level2_Optional_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Level2_Required_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Name"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK""",
                Sql);

            Assert.Contains(
                @"SELECT ""l1"".""Id"", ""l1"".""Name"", ""l1"".""OneToMany_Optional_Self_InverseId"", ""l1"".""OneToMany_Required_Self_InverseId"", ""l1"".""OneToOne_Optional_SelfId"", ""l1.OneToOne_Optional_FK"".""Id"", ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""l1.OneToOne_Optional_FK"".""Level1_Required_Id"", ""l1.OneToOne_Optional_FK"".""Name"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
LEFT JOIN ""Level2"" AS ""l1.OneToOne_Optional_FK"" ON ""l1"".""Id"" = ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""l1"".""Id""",
                Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_compared_to_null2()
        {
            base.Where_multiple_nav_prop_reference_optional_compared_to_null2();

            Assert.Equal(
                @"SELECT ""l3"".""Id"", ""l3"".""Level2_Optional_Id"", ""l3"".""Level2_Required_Id"", ""l3"".""Name"", ""l3"".""OneToMany_Optional_InverseId"", ""l3"".""OneToMany_Optional_Self_InverseId"", ""l3"".""OneToMany_Required_InverseId"", ""l3"".""OneToMany_Required_Self_InverseId"", ""l3"".""OneToOne_Optional_PK_InverseId"", ""l3"".""OneToOne_Optional_SelfId"", ""l3.OneToOne_Optional_FK_Inverse"".""Id"", ""l3.OneToOne_Optional_FK_Inverse"".""Level1_Optional_Id"", ""l3.OneToOne_Optional_FK_Inverse"".""Level1_Required_Id"", ""l3.OneToOne_Optional_FK_Inverse"".""Name"", ""l3.OneToOne_Optional_FK_Inverse"".""OneToMany_Optional_InverseId"", ""l3.OneToOne_Optional_FK_Inverse"".""OneToMany_Optional_Self_InverseId"", ""l3.OneToOne_Optional_FK_Inverse"".""OneToMany_Required_InverseId"", ""l3.OneToOne_Optional_FK_Inverse"".""OneToMany_Required_Self_InverseId"", ""l3.OneToOne_Optional_FK_Inverse"".""OneToOne_Optional_PK_InverseId"", ""l3.OneToOne_Optional_FK_Inverse"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""l3""
LEFT JOIN ""Level2"" AS ""l3.OneToOne_Optional_FK_Inverse"" ON ""l3"".""Level2_Optional_Id"" = ""l3.OneToOne_Optional_FK_Inverse"".""Id""
ORDER BY ""l3"".""Level2_Optional_Id""",
                Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_compared_to_null3()
        {
            base.Where_multiple_nav_prop_reference_optional_compared_to_null3();

            Assert.Contains(
                @"SELECT ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Id"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Level2_Optional_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Level2_Required_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""Name"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""l1.OneToOne_Optional_FK.OneToOne_Optional_FK""",
                Sql);

            Assert.Contains(
                @"SELECT ""l1"".""Id"", ""l1"".""Name"", ""l1"".""OneToMany_Optional_Self_InverseId"", ""l1"".""OneToMany_Required_Self_InverseId"", ""l1"".""OneToOne_Optional_SelfId"", ""l1.OneToOne_Optional_FK"".""Id"", ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""l1.OneToOne_Optional_FK"".""Level1_Required_Id"", ""l1.OneToOne_Optional_FK"".""Name"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
LEFT JOIN ""Level2"" AS ""l1.OneToOne_Optional_FK"" ON ""l1"".""Id"" = ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""l1"".""Id""",
                Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_compared_to_null4()
        {
            base.Where_multiple_nav_prop_reference_optional_compared_to_null4();

            Assert.Equal(
                @"SELECT ""l3"".""Id"", ""l3"".""Level2_Optional_Id"", ""l3"".""Level2_Required_Id"", ""l3"".""Name"", ""l3"".""OneToMany_Optional_InverseId"", ""l3"".""OneToMany_Optional_Self_InverseId"", ""l3"".""OneToMany_Required_InverseId"", ""l3"".""OneToMany_Required_Self_InverseId"", ""l3"".""OneToOne_Optional_PK_InverseId"", ""l3"".""OneToOne_Optional_SelfId"", ""l3.OneToOne_Optional_FK_Inverse"".""Id"", ""l3.OneToOne_Optional_FK_Inverse"".""Level1_Optional_Id"", ""l3.OneToOne_Optional_FK_Inverse"".""Level1_Required_Id"", ""l3.OneToOne_Optional_FK_Inverse"".""Name"", ""l3.OneToOne_Optional_FK_Inverse"".""OneToMany_Optional_InverseId"", ""l3.OneToOne_Optional_FK_Inverse"".""OneToMany_Optional_Self_InverseId"", ""l3.OneToOne_Optional_FK_Inverse"".""OneToMany_Required_InverseId"", ""l3.OneToOne_Optional_FK_Inverse"".""OneToMany_Required_Self_InverseId"", ""l3.OneToOne_Optional_FK_Inverse"".""OneToOne_Optional_PK_InverseId"", ""l3.OneToOne_Optional_FK_Inverse"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""l3""
LEFT JOIN ""Level2"" AS ""l3.OneToOne_Optional_FK_Inverse"" ON ""l3"".""Level2_Optional_Id"" = ""l3.OneToOne_Optional_FK_Inverse"".""Id""
ORDER BY ""l3"".""Level2_Optional_Id""",
                Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_compared_to_null5()
        {
            base.Where_multiple_nav_prop_reference_optional_compared_to_null5();

            Assert.Contains(
                @"SELECT ""e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK"".""Id"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK"".""Level3_Optional_Id"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK"".""Level3_Required_Id"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK"".""Name"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK"".""OneToOne_Optional_SelfId""
FROM ""Level4"" AS ""e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK""",
                Sql);

            Assert.Contains(
                @"SELECT ""e.OneToOne_Optional_FK.OneToOne_Required_FK"".""Id"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK"".""Level2_Optional_Id"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK"".""Level2_Required_Id"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK"".""Name"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""e.OneToOne_Optional_FK.OneToOne_Required_FK""",
                Sql);

            Assert.Contains(
                @"SELECT ""e"".""Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Optional_FK"".""Id"", ""e.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""e.OneToOne_Optional_FK"".""Level1_Required_Id"", ""e.OneToOne_Optional_FK"".""Name"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
LEFT JOIN ""Level2"" AS ""e.OneToOne_Optional_FK"" ON ""e"".""Id"" = ""e.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""e"".""Id""",
                Sql);
        }

        public override void Select_multiple_nav_prop_reference_required()
        {
            base.Select_multiple_nav_prop_reference_required();

            Assert.Equal(
                @"SELECT ""e.OneToOne_Required_FK.OneToOne_Required_FK"".""Id""
FROM ""Level1"" AS ""e""
INNER JOIN ""Level2"" AS ""e.OneToOne_Required_FK"" ON ""e"".""Id"" = ""e.OneToOne_Required_FK"".""Level1_Required_Id""
INNER JOIN ""Level3"" AS ""e.OneToOne_Required_FK.OneToOne_Required_FK"" ON ""e.OneToOne_Required_FK"".""Id"" = ""e.OneToOne_Required_FK.OneToOne_Required_FK"".""Level2_Required_Id""",
                Sql);
        }

        public override void Select_multiple_nav_prop_optional_required()
        {
            base.Select_multiple_nav_prop_optional_required();

            Assert.Contains(
                @"SELECT ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""Id"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""Level2_Optional_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""Level2_Required_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""Name"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""l1.OneToOne_Optional_FK.OneToOne_Required_FK""",
                Sql);

            Assert.Contains(
                @"SELECT ""l1"".""Id"", ""l1"".""Name"", ""l1"".""OneToMany_Optional_Self_InverseId"", ""l1"".""OneToMany_Required_Self_InverseId"", ""l1"".""OneToOne_Optional_SelfId"", ""l1.OneToOne_Optional_FK"".""Id"", ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""l1.OneToOne_Optional_FK"".""Level1_Required_Id"", ""l1.OneToOne_Optional_FK"".""Name"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
LEFT JOIN ""Level2"" AS ""l1.OneToOne_Optional_FK"" ON ""l1"".""Id"" = ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""l1"".""Id""",
                Sql);
        }

        public override void Where_multiple_nav_prop_optional_required()
        {
            base.Where_multiple_nav_prop_optional_required();

            Assert.Contains(
                @"SELECT ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""Id"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""Level2_Optional_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""Level2_Required_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""Name"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""l1.OneToOne_Optional_FK.OneToOne_Required_FK""",
                Sql);

            Assert.Contains(
                @"SELECT ""l1"".""Id"", ""l1"".""Name"", ""l1"".""OneToMany_Optional_Self_InverseId"", ""l1"".""OneToMany_Required_Self_InverseId"", ""l1"".""OneToOne_Optional_SelfId"", ""l1.OneToOne_Optional_FK"".""Id"", ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""l1.OneToOne_Optional_FK"".""Level1_Required_Id"", ""l1.OneToOne_Optional_FK"".""Name"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
LEFT JOIN ""Level2"" AS ""l1.OneToOne_Optional_FK"" ON ""l1"".""Id"" = ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""l1"".""Id""",
                Sql);
        }

        public override void SelectMany_navigation_comparison1()
        {
            base.SelectMany_navigation_comparison1();

            Assert.Equal(
                @"SELECT ""l11"".""Id"", ""l12"".""Id""
FROM ""Level1"" AS ""l11""
CROSS JOIN ""Level1"" AS ""l12""
WHERE ""l11"".""Id"" = ""l12"".""Id""",
                Sql);

            /*
            Assert.Equal(
                @"SELECT ""l11"".""Id"", ""l11"".""Name"", ""l11"".""OneToMany_Optional_Self_InverseId"", ""l11"".""OneToMany_Required_Self_InverseId"", ""l11"".""OneToOne_Optional_SelfId"", ""l12"".""Id"", ""l12"".""Name"", ""l12"".""OneToMany_Optional_Self_InverseId"", ""l12"".""OneToMany_Required_Self_InverseId"", ""l12"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l11""
CROSS JOIN ""Level1"" AS ""l12""",
                Sql);*/
        }

        public override void SelectMany_navigation_comparison2()
        {
            base.SelectMany_navigation_comparison2();

            Assert.Equal(
                @"",
                Sql);
        }

        public override void SelectMany_navigation_comparison3()
        {
            base.SelectMany_navigation_comparison3();

            Assert.Equal(
                @"",
                Sql);
        }

        public override void Where_complex_predicate_with_with_nav_prop_and_OrElse1()
        {
            base.Where_complex_predicate_with_with_nav_prop_and_OrElse1();

            Assert.Equal(
                @"",
                Sql);
        }

        public override void Where_complex_predicate_with_with_nav_prop_and_OrElse2()
        {
            base.Where_complex_predicate_with_with_nav_prop_and_OrElse2();

            Assert.Contains(
                @"SELECT ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""Id"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""Level2_Optional_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""Level2_Required_Id"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""Name"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK.OneToOne_Required_FK"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""l1.OneToOne_Optional_FK.OneToOne_Required_FK""",
                Sql);

            Assert.Contains(
                @"SELECT ""l1"".""Id"", ""l1"".""Name"", ""l1"".""OneToMany_Optional_Self_InverseId"", ""l1"".""OneToMany_Required_Self_InverseId"", ""l1"".""OneToOne_Optional_SelfId"", ""l1.OneToOne_Optional_FK"".""Id"", ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""l1.OneToOne_Optional_FK"".""Level1_Required_Id"", ""l1.OneToOne_Optional_FK"".""Name"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
LEFT JOIN ""Level2"" AS ""l1.OneToOne_Optional_FK"" ON ""l1"".""Id"" = ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""l1"".""Id""",
                Sql);
        }

        public override void Where_complex_predicate_with_with_nav_prop_and_OrElse3()
        {
            base.Where_complex_predicate_with_with_nav_prop_and_OrElse3();

            Assert.Contains(
                @"SELECT ""l1.OneToOne_Optional_FK"".""Id"", ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""l1.OneToOne_Optional_FK"".""Level1_Required_Id"", ""l1.OneToOne_Optional_FK"".""Name"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level2"" AS ""l1.OneToOne_Optional_FK""",
                Sql);

            Assert.Contains(
                @"SELECT ""l1"".""Id"", ""l1"".""Name"", ""l1"".""OneToMany_Optional_Self_InverseId"", ""l1"".""OneToMany_Required_Self_InverseId"", ""l1"".""OneToOne_Optional_SelfId"", ""l1.OneToOne_Required_FK"".""Id"", ""l1.OneToOne_Required_FK"".""Level1_Optional_Id"", ""l1.OneToOne_Required_FK"".""Level1_Required_Id"", ""l1.OneToOne_Required_FK"".""Name"", ""l1.OneToOne_Required_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Required_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Required_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Required_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Required_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Required_FK"".""OneToOne_Optional_SelfId"", ""l1.OneToOne_Required_FK.OneToOne_Optional_FK"".""Id"", ""l1.OneToOne_Required_FK.OneToOne_Optional_FK"".""Level2_Optional_Id"", ""l1.OneToOne_Required_FK.OneToOne_Optional_FK"".""Level2_Required_Id"", ""l1.OneToOne_Required_FK.OneToOne_Optional_FK"".""Name"", ""l1.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
INNER JOIN ""Level2"" AS ""l1.OneToOne_Required_FK"" ON ""l1"".""Id"" = ""l1.OneToOne_Required_FK"".""Level1_Required_Id""
LEFT JOIN ""Level3"" AS ""l1.OneToOne_Required_FK.OneToOne_Optional_FK"" ON ""l1.OneToOne_Required_FK"".""Id"" = ""l1.OneToOne_Required_FK.OneToOne_Optional_FK"".""Level2_Optional_Id""
ORDER BY ""l1.OneToOne_Required_FK"".""Id""",
                Sql);
        }

        public override void Complex_navigations_with_predicate_projected_into_anonymous_type()
        {
            base.Complex_navigations_with_predicate_projected_into_anonymous_type();

            Assert.Equal(
                @"SELECT ""e.OneToOne_Required_FK"".""Id"", ""e.OneToOne_Required_FK"".""Level1_Optional_Id"", ""e.OneToOne_Required_FK"".""Level1_Required_Id"", ""e.OneToOne_Required_FK"".""Name"", ""e.OneToOne_Required_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Required_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Required_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Required_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Required_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Required_FK"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Required_FK.OneToOne_Optional_FK"".""Id"", ""e.OneToOne_Required_FK.OneToOne_Optional_FK"".""Level2_Optional_Id"", ""e.OneToOne_Required_FK.OneToOne_Optional_FK"".""Level2_Required_Id"", ""e.OneToOne_Required_FK.OneToOne_Optional_FK"".""Name"", ""e.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Required_FK.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Required_FK.OneToOne_Required_FK"".""Id"", ""e"".""Name""
FROM ""Level1"" AS ""e""
INNER JOIN ""Level2"" AS ""e.OneToOne_Required_FK"" ON ""e"".""Id"" = ""e.OneToOne_Required_FK"".""Level1_Required_Id""
INNER JOIN ""Level3"" AS ""e.OneToOne_Required_FK.OneToOne_Required_FK"" ON ""e.OneToOne_Required_FK"".""Id"" = ""e.OneToOne_Required_FK.OneToOne_Required_FK"".""Level2_Required_Id""
LEFT JOIN ""Level3"" AS ""e.OneToOne_Required_FK.OneToOne_Optional_FK"" ON ""e.OneToOne_Required_FK"".""Id"" = ""e.OneToOne_Required_FK.OneToOne_Optional_FK"".""Level2_Optional_Id""
ORDER BY ""e.OneToOne_Required_FK"".""Id""",
                Sql);
        }

        public override void Optional_navigation_projected_into_DTO()
        {
            base.Optional_navigation_projected_into_DTO();

            Assert.Equal(
                @"SELECT ""e"".""Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Optional_FK"".""Id"", ""e.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""e.OneToOne_Optional_FK"".""Level1_Required_Id"", ""e.OneToOne_Optional_FK"".""Name"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
LEFT JOIN ""Level2"" AS ""e.OneToOne_Optional_FK"" ON ""e"".""Id"" = ""e.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""e"".""Id""",
                Sql);
        }

        public override void OrderBy_nav_prop_reference_optional()
        {
            base.OrderBy_nav_prop_reference_optional();

            Assert.Equal(
                @"SELECT ""e"".""Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Optional_FK"".""Id"", ""e.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""e.OneToOne_Optional_FK"".""Level1_Required_Id"", ""e.OneToOne_Optional_FK"".""Name"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
LEFT JOIN ""Level2"" AS ""e.OneToOne_Optional_FK"" ON ""e"".""Id"" = ""e.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""e"".""Id""",
                Sql);
        }

        public override void OrderBy_nav_prop_reference_optional_via_DefaultIfEmpty()
        {
            base.OrderBy_nav_prop_reference_optional_via_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT ""l2"".""Id"", ""l2"".""Level1_Optional_Id"", ""l2"".""Level1_Required_Id"", ""l2"".""Name"", ""l2"".""OneToMany_Optional_InverseId"", ""l2"".""OneToMany_Optional_Self_InverseId"", ""l2"".""OneToMany_Required_InverseId"", ""l2"".""OneToMany_Required_Self_InverseId"", ""l2"".""OneToOne_Optional_PK_InverseId"", ""l2"".""OneToOne_Optional_SelfId"", ""l1"".""Id""
FROM ""Level1"" AS ""l1""
LEFT JOIN ""Level2"" AS ""l2"" ON ""l1"".""Id"" = ""l2"".""Level1_Optional_Id""
ORDER BY ""l1"".""Id""",
                Sql);
        }

        public override void Result_operator_nav_prop_reference_optional()
        {
            base.Result_operator_nav_prop_reference_optional();

            Assert.Equal(
                @"SELECT ""e"".""Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Optional_FK"".""Id"", ""e.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""e.OneToOne_Optional_FK"".""Level1_Required_Id"", ""e.OneToOne_Optional_FK"".""Name"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
LEFT JOIN ""Level2"" AS ""e.OneToOne_Optional_FK"" ON ""e"".""Id"" = ""e.OneToOne_Optional_FK"".""Level1_Optional_Id""
ORDER BY ""e"".""Id""",
                Sql);
        }

        public override void Result_operator_nav_prop_reference_optional_via_DefaultIfEmpty()
        {
            base.Result_operator_nav_prop_reference_optional_via_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT ""l2"".""Id"", ""l2"".""Level1_Optional_Id"", ""l2"".""Level1_Required_Id"", ""l2"".""Name"", ""l2"".""OneToMany_Optional_InverseId"", ""l2"".""OneToMany_Optional_Self_InverseId"", ""l2"".""OneToMany_Required_InverseId"", ""l2"".""OneToMany_Required_Self_InverseId"", ""l2"".""OneToOne_Optional_PK_InverseId"", ""l2"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
LEFT JOIN ""Level2"" AS ""l2"" ON ""l1"".""Id"" = ""l2"".""Level1_Optional_Id""
ORDER BY ""l1"".""Id""",
                Sql);
        }

        public override void Include_with_optional_navigation()
        {
            base.Include_with_optional_navigation();

            Assert.Equal(
                @"SELECT ""e"".""Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Optional_FK"".""Id"", ""e.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""e.OneToOne_Optional_FK"".""Level1_Required_Id"", ""e.OneToOne_Optional_FK"".""Name"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId"", ""l"".""Id"", ""l"".""Level1_Optional_Id"", ""l"".""Level1_Required_Id"", ""l"".""Name"", ""l"".""OneToMany_Optional_InverseId"", ""l"".""OneToMany_Optional_Self_InverseId"", ""l"".""OneToMany_Required_InverseId"", ""l"".""OneToMany_Required_Self_InverseId"", ""l"".""OneToOne_Optional_PK_InverseId"", ""l"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
LEFT JOIN ""Level2"" AS ""e.OneToOne_Optional_FK"" ON ""e"".""Id"" = ""e.OneToOne_Optional_FK"".""Level1_Optional_Id""
LEFT JOIN ""Level2"" AS ""l"" ON ""l"".""Level1_Optional_Id"" = ""e"".""Id""
ORDER BY ""e"".""Id""",
                Sql);
        }

        public override void Include_nested_with_optional_navigation()
        {
            base.Include_nested_with_optional_navigation();

            Assert.Equal(
                @"SELECT ""e"".""Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_SelfId"", ""e.OneToOne_Optional_FK"".""Id"", ""e.OneToOne_Optional_FK"".""Level1_Optional_Id"", ""e.OneToOne_Optional_FK"".""Level1_Required_Id"", ""e.OneToOne_Optional_FK"".""Name"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""e.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""e.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId"", ""l"".""Id"", ""l"".""Level1_Optional_Id"", ""l"".""Level1_Required_Id"", ""l"".""Name"", ""l"".""OneToMany_Optional_InverseId"", ""l"".""OneToMany_Optional_Self_InverseId"", ""l"".""OneToMany_Required_InverseId"", ""l"".""OneToMany_Required_Self_InverseId"", ""l"".""OneToOne_Optional_PK_InverseId"", ""l"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
LEFT JOIN ""Level2"" AS ""e.OneToOne_Optional_FK"" ON ""e"".""Id"" = ""e.OneToOne_Optional_FK"".""Level1_Optional_Id""
LEFT JOIN ""Level2"" AS ""l"" ON ""l"".""Level1_Optional_Id"" = ""e"".""Id""
ORDER BY ""e"".""Id"", ""l"".""Id""

SELECT ""l0"".""Id"", ""l0"".""Level2_Optional_Id"", ""l0"".""Level2_Required_Id"", ""l0"".""Name"", ""l0"".""OneToMany_Optional_InverseId"", ""l0"".""OneToMany_Optional_Self_InverseId"", ""l0"".""OneToMany_Required_InverseId"", ""l0"".""OneToMany_Required_Self_InverseId"", ""l0"".""OneToOne_Optional_PK_InverseId"", ""l0"".""OneToOne_Optional_SelfId"", ""l2"".""Id"", ""l2"".""Level3_Optional_Id"", ""l2"".""Level3_Required_Id"", ""l2"".""Name"", ""l2"".""OneToMany_Optional_InverseId"", ""l2"".""OneToMany_Optional_Self_InverseId"", ""l2"".""OneToMany_Required_InverseId"", ""l2"".""OneToMany_Required_Self_InverseId"", ""l2"".""OneToOne_Optional_PK_InverseId"", ""l2"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""l0""
INNER JOIN (
    SELECT DISTINCT ""e"".""Id"", ""l"".""Id"" AS ""Id0""
    FROM ""Level1"" AS ""e""
    LEFT JOIN ""Level2"" AS ""e.OneToOne_Optional_FK"" ON ""e"".""Id"" = ""e.OneToOne_Optional_FK"".""Level1_Optional_Id""
    LEFT JOIN ""Level2"" AS ""l"" ON ""l"".""Level1_Optional_Id"" = ""e"".""Id""
) AS ""l1"" ON ""l0"".""OneToMany_Required_InverseId"" = ""l1"".""Id0""
LEFT JOIN ""Level4"" AS ""l2"" ON ""l2"".""Level3_Required_Id"" = ""l0"".""Id""
ORDER BY ""l1"".""Id"", ""l1"".""Id0""",
                Sql);
        }

        public override void Include_with_groupjoin_skip_and_take()
        {
            base.Include_with_groupjoin_skip_and_take();

            // TODO: Assert on SQL
        }

        public override void Join_flattening_bug_4539()
        {
            base.Join_flattening_bug_4539();

            Assert.Equal(
                @"",
                Sql);
        }

        public override void Query_source_materialization_bug_4547()
        {
            base.Query_source_materialization_bug_4547();

            Assert.Contains(@"SELECT ""e1"".""Id""
FROM ""Level1"" AS ""e1""", Sql);

            Assert.Contains(@"SELECT ""subQuery2"".""Id"", ""subQuery2"".""Level1_Optional_Id"", ""subQuery2"".""Level1_Required_Id"", ""subQuery2"".""Name"", ""subQuery2"".""OneToMany_Optional_InverseId"", ""subQuery2"".""OneToMany_Optional_Self_InverseId"", ""subQuery2"".""OneToMany_Required_InverseId"", ""subQuery2"".""OneToMany_Required_Self_InverseId"", ""subQuery2"".""OneToOne_Optional_PK_InverseId"", ""subQuery2"".""OneToOne_Optional_SelfId"", ""subQuery3"".""Id"", ""subQuery3"".""Level2_Optional_Id"", ""subQuery3"".""Level2_Required_Id"", ""subQuery3"".""Name"", ""subQuery3"".""OneToMany_Optional_InverseId"", ""subQuery3"".""OneToMany_Optional_Self_InverseId"", ""subQuery3"".""OneToMany_Required_InverseId"", ""subQuery3"".""OneToMany_Required_Self_InverseId"", ""subQuery3"".""OneToOne_Optional_PK_InverseId"", ""subQuery3"".""OneToOne_Optional_SelfId""
FROM ""Level2"" AS ""subQuery2""
LEFT JOIN ""Level3"" AS ""subQuery3"" ON ""subQuery2"".""Id"" = ""subQuery3"".""Level2_Optional_Id""
ORDER BY ""subQuery2"".""Id""", Sql);

            Assert.Contains(@"SELECT ""subQuery2"".""Id"", ""subQuery2"".""Level1_Optional_Id"", ""subQuery2"".""Level1_Required_Id"", ""subQuery2"".""Name"", ""subQuery2"".""OneToMany_Optional_InverseId"", ""subQuery2"".""OneToMany_Optional_Self_InverseId"", ""subQuery2"".""OneToMany_Required_InverseId"", ""subQuery2"".""OneToMany_Required_Self_InverseId"", ""subQuery2"".""OneToOne_Optional_PK_InverseId"", ""subQuery2"".""OneToOne_Optional_SelfId"", ""subQuery3"".""Id"", ""subQuery3"".""Level2_Optional_Id"", ""subQuery3"".""Level2_Required_Id"", ""subQuery3"".""Name"", ""subQuery3"".""OneToMany_Optional_InverseId"", ""subQuery3"".""OneToMany_Optional_Self_InverseId"", ""subQuery3"".""OneToMany_Required_InverseId"", ""subQuery3"".""OneToMany_Required_Self_InverseId"", ""subQuery3"".""OneToOne_Optional_PK_InverseId"", ""subQuery3"".""OneToOne_Optional_SelfId""
FROM ""Level2"" AS ""subQuery2""
LEFT JOIN ""Level3"" AS ""subQuery3"" ON ""subQuery2"".""Id"" = ""subQuery3"".""Level2_Optional_Id""
ORDER BY ""subQuery2"".""Id""",
                Sql);
        }

        public override void SelectMany_navigation_property()
        {
            base.SelectMany_navigation_property();

            Assert.Equal(
                @"SELECT ""l1.OneToMany_Optional"".""Id"", ""l1.OneToMany_Optional"".""Level1_Optional_Id"", ""l1.OneToMany_Optional"".""Level1_Required_Id"", ""l1.OneToMany_Optional"".""Name"", ""l1.OneToMany_Optional"".""OneToMany_Optional_InverseId"", ""l1.OneToMany_Optional"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToMany_Optional"".""OneToMany_Required_InverseId"", ""l1.OneToMany_Optional"".""OneToMany_Required_Self_InverseId"", ""l1.OneToMany_Optional"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToMany_Optional"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
INNER JOIN ""Level2"" AS ""l1.OneToMany_Optional"" ON ""l1"".""Id"" = ""l1.OneToMany_Optional"".""OneToMany_Optional_InverseId""",
                Sql);
        }

        public override void SelectMany_navigation_property_and_projection()
        {
            base.SelectMany_navigation_property_and_projection();

            Assert.Equal(
                @"SELECT ""l1.OneToMany_Optional"".""Name""
FROM ""Level1"" AS ""l1""
INNER JOIN ""Level2"" AS ""l1.OneToMany_Optional"" ON ""l1"".""Id"" = ""l1.OneToMany_Optional"".""OneToMany_Optional_InverseId""",
                Sql);
        }

        public override void SelectMany_navigation_property_and_filter_before()
        {
            base.SelectMany_navigation_property_and_filter_before();

            Assert.Equal(
                @"SELECT ""e.OneToMany_Optional"".""Id"", ""e.OneToMany_Optional"".""Level1_Optional_Id"", ""e.OneToMany_Optional"".""Level1_Required_Id"", ""e.OneToMany_Optional"".""Name"", ""e.OneToMany_Optional"".""OneToMany_Optional_InverseId"", ""e.OneToMany_Optional"".""OneToMany_Optional_Self_InverseId"", ""e.OneToMany_Optional"".""OneToMany_Required_InverseId"", ""e.OneToMany_Optional"".""OneToMany_Required_Self_InverseId"", ""e.OneToMany_Optional"".""OneToOne_Optional_PK_InverseId"", ""e.OneToMany_Optional"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
INNER JOIN ""Level2"" AS ""e.OneToMany_Optional"" ON ""e"".""Id"" = ""e.OneToMany_Optional"".""OneToMany_Optional_InverseId""
WHERE ""e"".""Id"" = 1",
                Sql);
        }

        public override void SelectMany_navigation_property_and_filter_after()
        {
            base.SelectMany_navigation_property_and_filter_after();

            Assert.Equal(
                @"SELECT ""l1.OneToMany_Optional"".""Id"", ""l1.OneToMany_Optional"".""Level1_Optional_Id"", ""l1.OneToMany_Optional"".""Level1_Required_Id"", ""l1.OneToMany_Optional"".""Name"", ""l1.OneToMany_Optional"".""OneToMany_Optional_InverseId"", ""l1.OneToMany_Optional"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToMany_Optional"".""OneToMany_Required_InverseId"", ""l1.OneToMany_Optional"".""OneToMany_Required_Self_InverseId"", ""l1.OneToMany_Optional"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToMany_Optional"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
INNER JOIN ""Level2"" AS ""l1.OneToMany_Optional"" ON ""l1"".""Id"" = ""l1.OneToMany_Optional"".""OneToMany_Optional_InverseId""
WHERE ""l1.OneToMany_Optional"".""Id"" <> 6",
                Sql);
        }

        public override void SelectMany_nested_navigation_property_required()
        {
            base.SelectMany_nested_navigation_property_required();

            Assert.Equal(
                @"SELECT ""l1.OneToOne_Required_FK.OneToMany_Optional"".""Id"", ""l1.OneToOne_Required_FK.OneToMany_Optional"".""Level2_Optional_Id"", ""l1.OneToOne_Required_FK.OneToMany_Optional"".""Level2_Required_Id"", ""l1.OneToOne_Required_FK.OneToMany_Optional"".""Name"", ""l1.OneToOne_Required_FK.OneToMany_Optional"".""OneToMany_Optional_InverseId"", ""l1.OneToOne_Required_FK.OneToMany_Optional"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToOne_Required_FK.OneToMany_Optional"".""OneToMany_Required_InverseId"", ""l1.OneToOne_Required_FK.OneToMany_Optional"".""OneToMany_Required_Self_InverseId"", ""l1.OneToOne_Required_FK.OneToMany_Optional"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToOne_Required_FK.OneToMany_Optional"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
INNER JOIN ""Level2"" AS ""l1.OneToOne_Required_FK"" ON ""l1"".""Id"" = ""l1.OneToOne_Required_FK"".""Level1_Required_Id""
INNER JOIN ""Level3"" AS ""l1.OneToOne_Required_FK.OneToMany_Optional"" ON ""l1.OneToOne_Required_FK"".""Id"" = ""l1.OneToOne_Required_FK.OneToMany_Optional"".""OneToMany_Optional_InverseId""",
                Sql);
        }

        public override void SelectMany_nested_navigation_property_optional_and_projection()
        {
            base.SelectMany_nested_navigation_property_optional_and_projection();

            Assert.Equal(
                @"SELECT ""l1.OneToOne_Optional_FK.OneToMany_Optional"".""Name""
FROM ""Level1"" AS ""l1""
INNER JOIN ""Level2"" AS ""l1.OneToOne_Optional_FK"" ON ""l1"".""Id"" = ""l1.OneToOne_Optional_FK"".""Level1_Optional_Id""
INNER JOIN ""Level3"" AS ""l1.OneToOne_Optional_FK.OneToMany_Optional"" ON ""l1.OneToOne_Optional_FK"".""Id"" = ""l1.OneToOne_Optional_FK.OneToMany_Optional"".""OneToMany_Optional_InverseId""",
                Sql);
        }

        public override void Multiple_SelectMany_calls()
        {
            base.Multiple_SelectMany_calls();

            Assert.Equal(
                @"SELECT ""e.OneToMany_Optional.OneToMany_Optional"".""Id"", ""e.OneToMany_Optional.OneToMany_Optional"".""Level2_Optional_Id"", ""e.OneToMany_Optional.OneToMany_Optional"".""Level2_Required_Id"", ""e.OneToMany_Optional.OneToMany_Optional"".""Name"", ""e.OneToMany_Optional.OneToMany_Optional"".""OneToMany_Optional_InverseId"", ""e.OneToMany_Optional.OneToMany_Optional"".""OneToMany_Optional_Self_InverseId"", ""e.OneToMany_Optional.OneToMany_Optional"".""OneToMany_Required_InverseId"", ""e.OneToMany_Optional.OneToMany_Optional"".""OneToMany_Required_Self_InverseId"", ""e.OneToMany_Optional.OneToMany_Optional"".""OneToOne_Optional_PK_InverseId"", ""e.OneToMany_Optional.OneToMany_Optional"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
INNER JOIN ""Level2"" AS ""e.OneToMany_Optional"" ON ""e"".""Id"" = ""e.OneToMany_Optional"".""OneToMany_Optional_InverseId""
INNER JOIN ""Level3"" AS ""e.OneToMany_Optional.OneToMany_Optional"" ON ""e.OneToMany_Optional"".""Id"" = ""e.OneToMany_Optional.OneToMany_Optional"".""OneToMany_Optional_InverseId""",
                Sql);
        }

        public override void SelectMany_navigation_property_with_another_navigation_in_subquery()
        {
            base.SelectMany_navigation_property_with_another_navigation_in_subquery();

            Assert.Equal(
                @"SELECT ""l1.OneToMany_Optional"".""Id"", ""l1.OneToMany_Optional"".""Level1_Optional_Id"", ""l1.OneToMany_Optional"".""Level1_Required_Id"", ""l1.OneToMany_Optional"".""Name"", ""l1.OneToMany_Optional"".""OneToMany_Optional_InverseId"", ""l1.OneToMany_Optional"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToMany_Optional"".""OneToMany_Required_InverseId"", ""l1.OneToMany_Optional"".""OneToMany_Required_Self_InverseId"", ""l1.OneToMany_Optional"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToMany_Optional"".""OneToOne_Optional_SelfId"", ""l1.OneToMany_Optional.OneToOne_Optional_FK"".""Id"", ""l1.OneToMany_Optional.OneToOne_Optional_FK"".""Level2_Optional_Id"", ""l1.OneToMany_Optional.OneToOne_Optional_FK"".""Level2_Required_Id"", ""l1.OneToMany_Optional.OneToOne_Optional_FK"".""Name"", ""l1.OneToMany_Optional.OneToOne_Optional_FK"".""OneToMany_Optional_InverseId"", ""l1.OneToMany_Optional.OneToOne_Optional_FK"".""OneToMany_Optional_Self_InverseId"", ""l1.OneToMany_Optional.OneToOne_Optional_FK"".""OneToMany_Required_InverseId"", ""l1.OneToMany_Optional.OneToOne_Optional_FK"".""OneToMany_Required_Self_InverseId"", ""l1.OneToMany_Optional.OneToOne_Optional_FK"".""OneToOne_Optional_PK_InverseId"", ""l1.OneToMany_Optional.OneToOne_Optional_FK"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""l1""
INNER JOIN ""Level2"" AS ""l1.OneToMany_Optional"" ON ""l1"".""Id"" = ""l1.OneToMany_Optional"".""OneToMany_Optional_InverseId""
LEFT JOIN ""Level3"" AS ""l1.OneToMany_Optional.OneToOne_Optional_FK"" ON ""l1.OneToMany_Optional"".""Id"" = ""l1.OneToMany_Optional.OneToOne_Optional_FK"".""Level2_Optional_Id""
ORDER BY ""l1.OneToMany_Optional"".""Id""",
                Sql);
        }

        // issue #3491
        //[Fact]
        public virtual void Multiple_complex_includes_from_sql()
        {
            using (var context = CreateContext())
            {
                var query = context.LevelOne.FromSql("SELECT * FROM [Level1]")
                    .Include(e => e.OneToOne_Optional_FK)
                    .ThenInclude(e => e.OneToMany_Optional)
                    .Include(e => e.OneToMany_Optional)
                    .ThenInclude(e => e.OneToOne_Optional_FK);

                var result = query.ToList();
            }

            Assert.Equal(
                @"",
                Sql);
        }

        private static string Sql => TestSqlLoggerFactory.Sql;
    }
}

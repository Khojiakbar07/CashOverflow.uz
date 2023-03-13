﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using CashOverflow.Models.Jobs;
using CashOverflow.Models.Jobs.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace CashOverflow.Services.Foundations.Jobs
{
	public partial class JobService
	{
        private static void ValidateJobOnAdd(Job job )
        {
            ValidateJobNotNull(job);

            Validate(
                (Rule: IsInvalid(job.Id), Parameter: nameof(Job.Id)),
                (Rule: IsInvalid(job.Title), Parameter: nameof(Job.Title)),
                (Rule: IsInvalid(job.Level), Parameter: nameof(Job.Level)),
                (Rule: IsInvalid(job.CreatedDate), Parameter: nameof(Job.CreatedDate)),
                (Rule: IsInvalid(job.UpdatedDate), Parameter: nameof(Job.UpdatedDate))
                );
        }

        private static void ValidateJobNotNull(Job job)
        {
            if (job is null)
            {
                throw new NullJobException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string title) => new
        {
            Condition = String.IsNullOrWhiteSpace(title),
            Message = "Title is required"
        };

        private static dynamic IsInvalid(Level level) => new
        {
            Condition = level==default,
            Message = "Level is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule,string Parameter)[] validations)
        {
            var invalidJobException = new InvalidJobException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if(rule.Condition)
                {
                    invalidJobException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidJobException.ThrowIfContainsErrors();
        }
    }
}


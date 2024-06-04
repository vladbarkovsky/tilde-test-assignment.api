﻿using AutoMapper;
using TildeTestAssignment.Application.Common.Mapping.Interfaces;
using TildeTestAssignment.ORM.Entities;

namespace TildeTestAssignment.Application.Statistics.Models
{
    public class BalanceStatusVM : IMapped
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public BalanceStatus BalanceStatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, BalanceStatusVM>()
                .ForMember(d => d.BalanceStatus, o => o.MapFrom<BalanceStatusResolver>());
        }
    }

    public enum BalanceStatus
    {
        Negative = 0,
        Neutral = 1,
        Positive = 2
    }

    public class BalanceStatusResolver : IValueResolver<Person, BalanceStatusVM, BalanceStatus>
    {
        public BalanceStatus Resolve(Person source, BalanceStatusVM destination, BalanceStatus destMember, ResolutionContext context)
        {
            if (source.Balance < 0)
            {
                return BalanceStatus.Negative;
            }

            return source.Balance == 0 ? BalanceStatus.Neutral : BalanceStatus.Positive;
        }
    }
}
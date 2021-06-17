﻿using Core.Enums;
using Data.Interfaces;
using System;

namespace Services.Domain
{
    public record UserListViewModel : IListViewModel
    {
        public int Id { get; init; }

        public string Username { get; init; }

        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public string Email { get; init; }

        public DateTime Birthdate { get; init; }

        public string PhoneNumber { get; init; }

        public GenderType Gender { get; init; }

        public int? TeamId { get; init; }

        public bool IsActive { get; init; }
    }
}
﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System.Threading.Tasks;
using CashOverflow.Brokers.Storages;
using CashOverflow.Models.Locations;

namespace CashOverflow.Services.Foundations.Locations
{
    public class LocationService : ILocationService
    {
        private readonly IStorageBroker storageBroker;

        public LocationService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Location> AddLocationAsync(Location location) =>
            await this.storageBroker.InsertLocationAsync(location);
    }
}

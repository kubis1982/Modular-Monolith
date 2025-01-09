namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    using ModularMonolith.Modules.Contractors.Domain.Contractors.Events;
    using ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions;
    using ModularMonolith.Shared.Kernel.Types;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class Contractor : DomainEntity<ContractorId, int, EntityType>, IAggregateRoot
    {
        private readonly List<ContractorAddress> addresses = [];

        internal ContractorName Name { get; private set; }
        internal ContractorCode Code { get; private set; }
        internal string? Description { get; private set; } = string.Empty;
        internal bool IsBlocked { get; private set; }
        internal Country Country { get; private set; } = "PL";
        internal IReadOnlyCollection<ContractorAddress> Addresses => addresses;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Contractor()
        {
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private Contractor(ContractorCode code, ContractorName name, string? description, Country country, Address address)
        {
            this.Code = code;
            this.Name = name;
            this.Description = description;
            this.Country = country;

            ContractorAddress contractorAddress = ContractorAddress.Create(address);
            contractorAddress.SetDefault();
            addresses.Add(contractorAddress);

            AddEvent(new Events.ContractorCreatedEvent(this, name, code, description, country, contractorAddress));
        }

        public static Contractor Create(ContractorCode code, ContractorName name, string? description, Country country, Address address)
        {
            return new Contractor(code, name, description, country, address);
        }

        public void Update(ContractorCode code, ContractorName name, string? description, Country country, int? addressId, Address address)
        {
            Code = code;
            Name = name;
            Description = description;
            Country = country;

            GetAddressOrDefault(addressId ?? 0)?.Update(address);

            AddEvent(new Events.ContractorUpdatedEvent(this, code, name, description, country));
        }

        public void Remove(IContractorUsageService contractorUsageService)
        {
            if (contractorUsageService.IsUsed(Id, out var entityName))
            {
                throw new RemovingUsedContractorException(entityName);
            }
            AddEvent(new Events.ContractorRemovedEvent(this));
        }

        public void Block()
        {
            if (IsBlocked == false)
            {
                IsBlocked = true;
                AddEvent(new Events.ContractorBlockedEvent(this));
            }
        }

        public void Unblock()
        {
            if (IsBlocked == true)
            {
                IsBlocked = false;
                AddEvent(new Events.ContractorUnblockedEvent(this));
            }
        }

        public ContractorAddress CreateAddress(Address address)
        {
            ContractorAddress contractorAddress = ContractorAddress.Create(address);
            addresses.Add(contractorAddress);
            AddEvent(new ContractorCreatedAddressEvent(this, contractorAddress));
            return contractorAddress;
        }

        public void UpdateAddress(ContractorAddressId addressId, Address address)
        {
            ContractorAddress contractorAddress = GetAddress(addressId);
            contractorAddress.Update(address);
            AddEvent(new ContractorAddressUpdatedEvent(this, contractorAddress));
        }

        private ContractorAddress? GetAddressOrDefault(ContractorAddressId addressId)
        {
            return addresses.SingleOrDefault(n => n.Id == addressId);
        }

        private ContractorAddress GetAddress(ContractorAddressId addressId)
        {
            return GetAddressOrDefault(addressId) ?? throw new AddressNotFoundException(addressId);
        }

        private ContractorAddress? GetDefaultAddress()
        {
            return addresses.FirstOrDefault(n => n.IsDefault);
        }

        public void RemoveAddress(ContractorAddressId addressId)
        {
            var address = GetAddressOrDefault(addressId);

            if (address == GetDefaultAddress())
            {
                throw new RemovingDefaultAddressException();
            }

            if (address != null)
            {
                if (addresses.Remove(address))
                {
                    AddEvent(new ContractorRemovedAddressEvent(this, addressId));
                }
            }
        }

        public void SetDefaultAddress(ContractorAddressId addressId)
        {
            foreach (var address in addresses)
            {
                if (address.Id == addressId)
                {
                    address.SetDefault();
                }
                else
                {
                    address.ResetDefault();
                }
            }
            AddEvent(new ContractorSetDefaultAddressEvent(this, addressId));
        }
    }
}

using Ex.Arq.Hex.Application.UseCase.Exceptions.Adresses.UseCases;
using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.AddAddress;
using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.DeleteAddress;
using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.DeleteAddressById;
using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.GetAddressById;
using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.SearchAdressesByCountry;
using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.UpdateAddress;
using Ex.Arq.Hex.InfrastructureAdapter.Out.AccessData.EntityFramework.Adresses.Repository;
using Ex.Arq.Hex.InfrastructureAdapter.Out.AccessData.EntityFramework.Contexts;
using Ex.Arq.Hex.Unit.Integration.Attributes;
using Xunit;

namespace Ex.Arq.Hex.Unit.Integration.UseCase.Adresses
{
    [TestCaseOrderer("Ex.Arq.Hex.Unit.Integration.Orderer.PriorityOrderer", "Ex.Arq.Hex.Unit.Integration")]
    public static class AddressTest
    {
        private static AddressRepository addressRepository;
        private static AddAddressInteractor addAddressInteractor;
        private static GetAddressByIdInteractor getAddressByIdInteractor;
        private static DeleteAddressByIdInteractor deleteAddressByIdInteractor;
        private static DeleteAddressInteractor deleteAddressInteractor;
        private static SearchAdressesByCountryInteractor searchAdressesByIdInteractor;
        private static UpdateAddressInteractor updateAddressInteractor;

        private static readonly List<string> _ids = new();
        private static readonly string _country = "Brazil";
        private static readonly string _state = "Minas Gerais";
        private static readonly string _zipCode = "37200-000";
        private static readonly string _city = "Lavras";
        private static readonly List<string> _streets =
            new() { "Perimetral", "Afonso Pena" };
        private static readonly List<int> _numbers =
            new() { 100, 150 };


        [Fact, TestPriority(1)]
        public static async Task Address_AddTwoAdressesAndSetIds_Success()
        {
            addressRepository = new AddressRepository(new AppDb());


            var addAddressInteractor = new AddAddressInteractor(addressRepository);


            var addAddressPortInPerimetral = new AddAddressPortIn(_country, _state,
                _zipCode, _city, _streets[0], _numbers[0]);
            var addAddressPortInAfonsoPena = new AddAddressPortIn(_country, _state,
                _zipCode, _city, _streets[1], _numbers[1]);


            AddAddressPortOut addAddressPortOutPerimetral = await addAddressInteractor
                .ExecuteAsync(addAddressPortInPerimetral);
            AddAddressPortOut addAddressPortOutAfonsoPena = await addAddressInteractor
                .ExecuteAsync(addAddressPortInAfonsoPena);


            Assert.NotEmpty(addAddressPortOutPerimetral.Id.ToString());
            Assert.Equal(_country, addAddressPortOutPerimetral.Country);
            Assert.Equal(_state, addAddressPortOutPerimetral.State);
            Assert.Equal(_zipCode, addAddressPortOutPerimetral.ZipCode);
            Assert.Equal(_city, addAddressPortOutPerimetral.City);
            Assert.Equal(_streets[0], addAddressPortOutPerimetral.Street);
            Assert.Equal(_numbers[0], addAddressPortOutPerimetral.Number);

            Assert.NotEmpty(addAddressPortOutAfonsoPena.Id.ToString());
            Assert.Equal(_country, addAddressPortOutAfonsoPena.Country);
            Assert.Equal(_state, addAddressPortOutAfonsoPena.State);
            Assert.Equal(_zipCode, addAddressPortOutAfonsoPena.ZipCode);
            Assert.Equal(_city, addAddressPortOutAfonsoPena.City);
            Assert.Equal(_streets[1], addAddressPortOutAfonsoPena.Street);
            Assert.Equal(_numbers[1], addAddressPortOutAfonsoPena.Number);


            _ids.Add(addAddressPortOutPerimetral.Id.ToString());
            _ids.Add(addAddressPortOutAfonsoPena.Id.ToString());
        }

        [Fact, TestPriority(2)]
        public static async Task Address_GetAddressById_Success()
        {
            addressRepository = new AddressRepository(new AppDb());
            getAddressByIdInteractor = new GetAddressByIdInteractor(addressRepository);

            var getAddressByIdPortIn =
                 new GetAddressByIdPortIn(Guid.Parse(_ids[0]));

            GetAddressByIdPortOut getAddressByIdPortOut =
                 await getAddressByIdInteractor.ExecuteAsync(getAddressByIdPortIn);

            Assert.Equal(Guid.Parse(_ids[0]), getAddressByIdPortOut.Id);
            Assert.Equal(_country, getAddressByIdPortOut.Country);
            Assert.Equal(_state, getAddressByIdPortOut.State);
            Assert.Equal(_zipCode, getAddressByIdPortOut.ZipCode);
            Assert.Equal(_city, getAddressByIdPortOut.City);
            Assert.Equal(_streets[0], getAddressByIdPortOut.Street);
            Assert.Equal(_numbers[0], getAddressByIdPortOut.Number);
        }

        [Fact, TestPriority(3)]
        public static async Task Address_SearchByCountry_Success()
        {
            addressRepository = new AddressRepository(new AppDb());
            searchAdressesByIdInteractor =
                new SearchAdressesByCountryInteractor(addressRepository);
            var key = "Bra";

            var searchAddressByIdInteractor = new SearchAddressByCountryPortIn(key);

            IReadOnlyCollection<SearchAddressByCountryPortOut> searchAdressesByIdInteractorPortOut =
                 await searchAdressesByIdInteractor.ExecuteAsync(searchAddressByIdInteractor);

            foreach (SearchAddressByCountryPortOut portOut in
                searchAdressesByIdInteractorPortOut)
            {
                Assert.Contains(key, portOut.Country);
            }
        }

        [Fact, TestPriority(4)]
        public static async Task Product_UpdateProduct_Success()
        {
            addressRepository = new AddressRepository(new AppDb());
            getAddressByIdInteractor = new GetAddressByIdInteractor(addressRepository);
            updateAddressInteractor = new UpdateAddressInteractor(addressRepository);
            _streets[0] = "Silvio Menicucci";
            _numbers[0] = 200;

            var updateProductPortIn = new UpdateAddressPortIn(Guid.Parse(_ids[0]), _country,
                _state, _zipCode, _city, _streets[0], _numbers[0]);

            await updateAddressInteractor.ExecuteAsync(updateProductPortIn);

            GetAddressByIdPortOut getAddressById = await getAddressByIdInteractor.
                ExecuteAsync(new GetAddressByIdPortIn(Guid.Parse(_ids[0])));

            Assert.Equal(Guid.Parse(_ids[0]), getAddressById.Id);
            Assert.Equal(_country, getAddressById.Country);
            Assert.Equal(_state, getAddressById.State);
            Assert.Equal(_zipCode, getAddressById.ZipCode);
            Assert.Equal(_city, getAddressById.City);
            Assert.Equal(_streets[0], getAddressById.Street);
            Assert.Equal(_numbers[0], getAddressById.Number);
        }

        [Fact, TestPriority(5)]
        public static async Task Address_DeleteById_Success()
        {
            addressRepository = new AddressRepository(new AppDb());
            getAddressByIdInteractor = new GetAddressByIdInteractor(addressRepository);
            deleteAddressByIdInteractor = new DeleteAddressByIdInteractor(addressRepository);

            var deleteAddressByIdPortIn = new DeleteAddressByIdPortIn(Guid.Parse(_ids[0]));

            await deleteAddressByIdInteractor.ExecuteAsync(deleteAddressByIdPortIn);

            await Assert.ThrowsAsync<GetAddressByIdInteractorException>(() => getAddressByIdInteractor.
                ExecuteAsync(new GetAddressByIdPortIn(Guid.Parse(_ids[0]))));
        }

        [Fact, TestPriority(6)]
        public static async Task Address_Delete_Success()
        {
            addressRepository = new AddressRepository(new AppDb());
            deleteAddressInteractor = new DeleteAddressInteractor(addressRepository);
            getAddressByIdInteractor = new GetAddressByIdInteractor(addressRepository);
            
            var deleteAddressPortIn = new DeleteAddressPortIn(Guid.Parse(_ids[1]), _country,
                _state, _zipCode, _city, _streets[1], _numbers[1]);

            await deleteAddressInteractor.ExecuteAsync(deleteAddressPortIn);

            await Assert.ThrowsAsync<GetAddressByIdInteractorException>(() => getAddressByIdInteractor.
                ExecuteAsync(new GetAddressByIdPortIn(Guid.Parse(_ids[1]))));
        }
    }
}

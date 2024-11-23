using AspNetMvcSample01.Models.DomainModels;
using AspNetMvcSample01.Models.DomainModels.PersonAggregates;
using System;

namespace AspNetMvcSample01.Models.Frameworks.Contracts
{
    public interface IPersonRepository
    {
        Task<List<Person>> Select();
        Task Insert(Person person);
        Task Delete(Person person);
        Task Delete(Guid Id);
        Task Edit(Person person);
        Task<Person> GetByIdAsync(Guid id);
    }
}

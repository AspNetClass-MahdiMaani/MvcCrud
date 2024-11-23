using AspNetMvcSample01.Models.DomainModels;
using AspNetMvcSample01.Models.DomainModels.PersonAggregates;
using AspNetMvcSample01.Models.Frameworks.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;

namespace AspNetMvcSample01.Models.Services
{
    public class PersonRepository : IPersonRepository
    {

        #region [- Ctor() -]
        private readonly ProjectDbContext _context;
        public PersonRepository(ProjectDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [- Select() -]
        public async Task<List<Person>> Select()
        {
            using (_context)
            {
                try
                {
                    var persons = await _context.Person.ToListAsync();
                    return persons;
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (_context.Person != null) _context.Dispose();
                }
            }
        }
        #endregion

        #region [- Insert() -]
        public async Task Insert(Person person)
        {

            using (_context)
            {
                try
                {
                    var p = new Person()
                    {
                        Id = Guid.NewGuid(),
                        FName = person.FName,
                        LName = person.LName
                    };

                    _context.Person.Add(p);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (_context.Person != null) _context.Dispose();
                }
            }
        }
        #endregion

        #region [- Delete() -] 

        public async Task Delete(Person person)
        {
            try
            {
                if (person != null)
                {
                    _context.Entry(person).State = EntityState.Deleted;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new DirectoryNotFoundException();
                }
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }


        public async Task Delete(Guid id)
        {

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            await Delete(person);

            if (id == null)
            {
                throw new Exception();
            }
        }
        #endregion

        #region [- Edit() -] 
        public async Task Edit(Person person)
        {
            using (_context)
            {
                try
                {
                    //await _context.Person.FindAsync(person.Id);
                    _context.Update(person);
                    await _context.SaveChangesAsync();

                    if (person.Id == null)
                    {
                        throw new Exception("PersonId cannot be null");
                    }

                    if (person == null)
                    {
                        throw new Exception("User Not Found");
                    }
                }

                catch
                {
                    throw new Exception();
                }

            }
        }
        #endregion

        #region [- GetByIdAsync() -] 
        public async Task<Person> GetByIdAsync(Guid id)
        {
            return await _context.Person
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        #endregion
    }
}

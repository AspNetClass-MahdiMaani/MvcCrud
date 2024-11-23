using AspNetMvcSample01.Models.DomainModels.PersonAggregates;
using AspNetMvcSample01.Models.Frameworks.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcSample01.Controllers
{
    public class PersonController : Controller
    {

        #region [- ctor -]
        private readonly IPersonRepository _personRepository;
        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        #endregion

        #region [- Index() -]
        public async Task<IActionResult> Index()
        {
            return View(await _personRepository.Select());
        }
        #endregion

        #region [- Create() -]

        public async Task <IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FName,LName")] Person person)
        {
            if (ModelState.IsValid)
            {
                await _personRepository.Insert(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }
        #endregion

        #region [- Delete() -]

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            return View(await _personRepository.GetByIdAsync((Guid)id));
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Person person)
        {

            await _personRepository.Delete(person);
            return RedirectToAction("Index");
        }
        #endregion

        #region [- Update() -]

        [HttpGet, ActionName("Update")]
        public async Task<IActionResult> Update(Guid id)
        {
            return View(await _personRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(Person person)
        {
            if (ModelState.IsValid)
            {
                await _personRepository.Edit(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }
        #endregion

        #region [- Details() -]
        public async Task<IActionResult> Details(Guid id)
        {
            return View(await _personRepository.GetByIdAsync(id));
        }
        #endregion
    }
}

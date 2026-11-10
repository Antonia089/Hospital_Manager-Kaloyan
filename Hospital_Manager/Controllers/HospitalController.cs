using Data_Hospital_Manager;
using Data_Hospital_Manager.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Manager.ViewModels.Hospital;
using Hospital_Manager.Core.Interfaces;

namespace Hospital_Manager.Controllers
{

    public class HospitalController : Controller
    {

        private readonly IHospitalService _hospitalService;

        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        
            
        public async Task<IActionResult> Index(string? search, int? minBeds, string? sortOrder)
        {
            var hospitals = await _hospitalService.GetAllHospitalsAsync(search, minBeds, sortOrder);           

            var model = new List<HospitalIndexViewModel>();//празен списък за ViewModel-ите

            foreach (var hospital in hospitals)
            {
                model.Add(new HospitalIndexViewModel
                {
                    Id = hospital.Id,
                    Name = hospital.Name,
                    Address = hospital.Address,
                    Email = hospital.Email,
                    PhoneNumber = hospital.PhoneNumber,
                    BedsCount = hospital.BedsCount
                });
            }
            ViewBag.Search = search;
            ViewBag.MinBeds = minBeds;
            ViewBag.SortOrder = sortOrder;
            return View(model);//Изпращаме списъка към View
        }
        

        public async Task<IActionResult> Details(int id)
        {
            var hospital = await _hospitalService.GetHospitalByIdAsync(id);

            if (hospital == null)
            {
                return NotFound();
            }

            var model = new HospitalDetailsViewModel
            {
                Id = hospital.Id,
                Name = hospital.Name,
                Address = hospital.Address,
                Email = hospital.Email,
                PhoneNumber = hospital.PhoneNumber,
                BedsCount = hospital.BedsCount,
                CreatedOn = hospital.CreatedOn
            };

            return View(model);
        }

        public IActionResult Create()//метод  който показва празната форма.
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HospitalCreateViewModel model)//обработва POST заявка
        {
            if (ModelState.IsValid)//Проверява валидацията
            {
                var hospital = new Hospital
                {
                    Name = model.Name,
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    BedsCount = model.BedsCount,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                };

                await _hospitalService.AddHospitalAsync(hospital);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var hospital = await _hospitalService.GetHospitalByIdAsync(id);

            if (hospital == null)
            {
                return NotFound();
            }

            var model = new HospitalEditViewModel
            {
                Id = hospital.Id,
                Name = hospital.Name,
                Address = hospital.Address,
                Email = hospital.Email,
                PhoneNumber = hospital.PhoneNumber,
                BedsCount = hospital.BedsCount
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HospitalEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var hospital = await _hospitalService.GetHospitalByIdAsync(id);

                if (hospital == null)
                {
                    return NotFound();
                }

                hospital.Name = model.Name;
                hospital.Address = model.Address;
                hospital.Email = model.Email;
                hospital.PhoneNumber = model.PhoneNumber;
                hospital.BedsCount = model.BedsCount;
                await _hospitalService.UpdateHospitalAsync(hospital);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)//Сигурни ли сте, че искате да изтриете тази болница?
        {
            var hospital = await _hospitalService.GetHospitalByIdAsync(id);

            if (hospital == null)
            {
                return NotFound();
            }

            var model = new HospitalDeleteViewModel
            {
                Id = hospital.Id,
                Name = hospital.Name
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _hospitalService.DeleteHospitalAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}



using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portal.BL.Interface;
using Portal.BL.Models;
using Portal.BL.Repository;
using Portal.DAL.Entity;

namespace Portal.PL.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IDepaertmentRep department;
        private readonly IMapper mapper;

        public DepartmentController(IDepaertmentRep department , IMapper mapper)
        {
            this.department = department;
            this.mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            
            var data = await department.GetAsync();
            var result = mapper.Map<IEnumerable<DepartmentVM>>(data);
            return View(result);
        }

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var data = mapper.Map<Department>(model);
                    await department.CreateAsync(data);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            //ModelState.Clear();
            return View(model);

        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Update(DepartmentVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var data = mapper.Map<Department>(model);
                    await department.UpdateAsync(data);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            //ModelState.Clear();
            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await department.DeleteAsync(id);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            //ModelState.Clear();
            return View();

        }


    }
}

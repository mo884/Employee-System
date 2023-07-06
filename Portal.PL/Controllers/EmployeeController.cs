using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.BL.Helper;
using Portal.BL.Interface;
using Portal.BL.Models;
using Portal.DAL.Entity;
using System.IO;

namespace Portal.PL.Controllers
{
    public class EmployeeController : Controller
    {


        #region Prop

        private readonly IEmployeeRep employee;
        private readonly IMapper mapper;
        private readonly IDepaertmentRep department;
        private readonly ICityRep city;
        private readonly IDistrictRep district;

        #endregion


        #region Ctor

        public EmployeeController(IEmployeeRep employee,IMapper mapper , IDepaertmentRep department , ICityRep city , IDistrictRep district)
        {
            this.employee = employee;
            this.mapper = mapper;
            this.department = department;
            this.city = city;
            this.district = district;
        }

        #endregion


        #region Actions

        public async Task<IActionResult> Index(string SearchValue)
        {

            if(SearchValue != null)
            {
                // Search
                var data = await employee.SearchAsync(x => x.Name.Contains(SearchValue) 
                                                        && x.IsActive == true 
                                                        && x.IsDeleted == false 
                                                        && x.Salary > 7000);

                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(result);
            }
            else
            {
                // Without Search
                var data = await employee.GetAsync(x => x.IsActive == true && x.IsDeleted == false);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(result);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await employee.GetByIdAsync(x=>x.Id == id && x.IsActive == true && x.IsDeleted == false);
            var result = mapper.Map<EmployeeVM>(data);
            var Dpt = await department.GetAsync();
            ViewBag.DepartmentList = new SelectList(Dpt, "Id", "Name", data.DepartmentId);

            return View(result);
        }


        public async Task<IActionResult> Create()
        {

            var data = await department.GetAsync();
            ViewBag.DepartmentList = new SelectList(data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    model.CvName = FileUploader.UploadFile("Docs",model.Cv);
                    model.ImageName = FileUploader.UploadFile("Imgs", model.Image);

                    var data = mapper.Map<Employee>(model);
                    await employee.CreateAsync(data);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            //ModelState.Clear();
            var Dpt = await department.GetAsync();
            ViewBag.DepartmentList = new SelectList(Dpt, "Id", "Name",model.DepartmentId);
            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var data = await employee.GetByIdAsync(x => x.Id == id && x.IsActive == true && x.IsDeleted == false);
            var result = mapper.Map<EmployeeVM>(data);
            var Dpt = await department.GetAsync();
            ViewBag.DepartmentList = new SelectList(Dpt, "Id", "Name", data.DepartmentId);

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(model);
                    await employee.UpdateAsync(data);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            //ModelState.Clear();
            var Dpt = await department.GetAsync();
            ViewBag.DepartmentList = new SelectList(Dpt, "Id", "Name", model.DepartmentId);

            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await employee.GetByIdAsync(x => x.Id == id && x.IsActive == true && x.IsDeleted == false);
            var result = mapper.Map<EmployeeVM>(data);
            var Dpt = await department.GetAsync();
            ViewBag.DepartmentList = new SelectList(Dpt, "Id", "Name", data.DepartmentId);

            return View(result);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(EmployeeVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    FileUploader.RemoveFile("Imgs", obj.ImageName);
                    FileUploader.RemoveFile("Docs", obj.CvName);

                    var result = mapper.Map<Employee>(obj);
                    await employee.DeleteAsync(result);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }



            //ModelState.Clear();
            var Dpt = await department.GetAsync();
            //ViewBag.DepartmentList = new SelectList(Dpt, "Id", "Name", );
            return View();

        }

        #endregion


        #region Ajax Call - Return JSon (Array OF Objects)


        // Get Cities Data Based On Country ID

        [HttpPost]
        public async Task<JsonResult> GetCitiesByCountryId(int CntryId)
        {
            var data = await city.GetAsync(x => x.CountryId == CntryId);
            var result = mapper.Map<IEnumerable<CityVM>>(data);
            return Json(result);
        }


        // Get Districts Data Based On City ID
        [HttpPost]
        public async Task<JsonResult> GetDistrictsByCityId(int CtyId)
        {
            var data = await district.GetAsync(x => x.CityId == CtyId);
            //var result = mapper.Map<IEnumerable<DictrictVM>>(data);
            return Json(data);
        }


        #endregion



    }
}

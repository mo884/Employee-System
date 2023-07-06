using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.BL.Helper;
using Portal.BL.Interface;
using Portal.BL.Models;
using Portal.DAL.Entity;

namespace Portal.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {


        private readonly IEmployeeRep employee;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeRep employee , IMapper mapper)
        {
            this.employee = employee;
            this.mapper = mapper;
        }


        //[HttpGet]
        //[Route("~/api/Employee/GetData")]
        //// https://localhost:7029//api/Employee/GetData
        //// https://www.ahmed.com//api/Employee/GetData
        //public string[] GetData()
        //{
        //    string[] data = new string[3] { "Ahmed", "Ali", "Sara" };
        //    return data;
        //}

        //[HttpGet]
        //[Route("~/api/Employee/GetNames")]
        //public string[] GetNames()
        //{
        //    string[] data = new string[3] { "aaaa", "bbb", "ccc" };
        //    return data;
        //}

        [HttpGet]
        [Route("~/api/Employee/GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var data = await employee.GetAsync(x => x.IsDeleted == false && x.IsActive == true);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return Ok(new ApiResponse<IEnumerable<EmployeeVM>>() { 
                
                    Code = 200,
                    Status = "Ok",
                    Message = "Data Found",
                    Data = result

                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {

                    Code = 404,
                    Status = "Not Found",
                    Message = "Data Not Found",
                    Data = ex.Message

                });
            }
        }


        [HttpGet]
        [Route("~/api/Employee/GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var data = await employee.GetByIdAsync(x => x.IsDeleted == false && x.IsActive == true && x.Id == id);
                var result = mapper.Map<EmployeeVM>(data);
                return Ok(new ApiResponse<EmployeeVM>()
                {

                    Code = 200,
                    Status = "Ok",
                    Message = "Data Found",
                    Data = result

                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {

                    Code = 404,
                    Status = "Not Found",
                    Message = "Data Not Found",
                    Data = ex.Message

                });
            }
        }


        [HttpPost]
        [Route("~/api/Employee/PostEmployee")]
        public async Task<IActionResult> PostEmployee(EmployeeVM model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var data = mapper.Map<Employee>(model);
                    var result = await employee.CreateAsync(data);
                    return Ok(new ApiResponse<Employee>()
                    {

                        Code = 201,
                        Status = "Created",
                        Message = "Data Saved",
                        Data = result
                    });

                }


                return BadRequest(new ApiResponse<string>()
                {
                    Code = 400,
                    Status = "Bad Request",
                    Message = "Validation Error"
                });



            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse<string>()
                {

                    Code = 400,
                    Status = "Bad Request",
                    Message = "Data Not Saved",
                    Data = ex.Message

                });
            }
        }


        [HttpPut]
        [Route("~/api/Employee/PutEmployee")]
        //[DisableCors]
        //[EnableCors("")]
        public async Task<IActionResult> PutEmployee(EmployeeVM model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var data = mapper.Map<Employee>(model);
                    await employee.UpdateAsync(data);
                    return Ok(new ApiResponse<string>()
                    {

                        Code = 201,
                        Status = "Updated",
                        Message = "Data Updated",
                        Data = "No result to return"
                    });
                }


                return BadRequest(new ApiResponse<string>()
                {
                    Code = 400,
                    Status = "Bad Request",
                    Message = "Validation Error"
                });



            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>()
                {

                    Code = 400,
                    Status = "Bad Request",
                    Message = "Data Not Updated",
                    Data = ex.Message

                });
            }
        }


        [HttpDelete]
        [Route("~/api/Employee/DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(EmployeeVM model)
        {
            try
            {
                    var data = mapper.Map<Employee>(model);
                    await employee.DeleteAsync(data);
                    return Ok(new ApiResponse<string>()
                    {
                        Code = 201,
                        Status = "Deleted",
                        Message = "Data Deleted",
                        Data = "No result to return"
                    });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>()
                {

                    Code = 400,
                    Status = "Bad Request",
                    Message = "Data Not Updated",
                    Data = ex.Message

                });
            }
        }

    }
}

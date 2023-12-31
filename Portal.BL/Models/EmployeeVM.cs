﻿using Portal.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Portal.BL.Models
{
    public class EmployeeVM
    {


        public EmployeeVM()
        {
            //IsActive = true;
            IsDeleted = false;
            //CreationDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [Range(2000,100000,ErrorMessage = "Salary Btw 2K : 100K")]
        public double Salary { get; set; }


        //12-StreetName-City-Country
        [RegularExpression("[1-9]{1,5}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}", ErrorMessage = "Enter Like 12-StreetName-City-Country")]
        public string Address { get; set; }


        [EmailAddress(ErrorMessage = "Email Invalid")]
        public string? Email { get; set; }
        public string? Notes { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsUpdated { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int DepartmentId { get; set; }
        public int DistrictId { get; set; }
        public Department? Department { get; set; }
        public District? District { get; set; }
        public string? ImageName { get; set; }
        public string? CvName { get; set; }

        public IFormFile? Image { get; set; }
        public IFormFile? Cv { get; set; }


    }
}

using AutoMapper;
using Portal.BL.Models;
using Portal.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BL.Mapper
{
    public class DomainProfile : Profile
    {


        public DomainProfile()
        {

            // From Entity To VM (Retreive)
            CreateMap<Department,DepartmentVM>();
            
            // From VM To Entity (Create - Edit - Delete)
            CreateMap<DepartmentVM, Department>();


            // From Entity To VM (Retreive)
            CreateMap<Employee, EmployeeVM>();

            // From VM To Entity (Create - Edit - Delete)
            CreateMap<EmployeeVM, Employee>();


            CreateMap<Country, CountryVM>();
            CreateMap<CountryVM, Country>();


            CreateMap<City, CityVM>();
            CreateMap<CityVM, City>();


            CreateMap<District, DistrictVM>();
            CreateMap<DistrictVM, District>();


        }

    }
}

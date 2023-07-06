﻿using Portal.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BL.Models
{
    public class DistrictVM
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
    }
}

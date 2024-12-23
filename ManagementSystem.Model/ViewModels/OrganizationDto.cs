using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Model.ViewModels
{
    public class OrganizationDto
    {
        public long Id { get; set; }
        public string? Parentid { get; set; }
        public string? code { get; set; }
        public string? Name { get; set; }
        public string? shortname { get; set; }
        public int? grade { get; set; }
        public string? pycode { get; set; }
        public int? property { get; set; }
        public string? orgcode { get; set; }
        public string? tel { get; set; }
        public string? address { get; set; }
        public string? licencecode { get; set; }
        public string? certificatecode { get; set; }
        public string? regionid { get; set; }
        public double? status { get; set; }
        public string? referenceid { get; set; }
        public string? healthcarecode { get; set; }
        public string? headofunitstaffid { get; set; }
        public string? statisticianstaffid { get; set; }
        public string? coderstaffid { get; set; }
        public string? orgcontactphone { get; set; }
        public int? hospitallevel { get; set; }
        public bool IsDeleted { get; set; }
    }
}

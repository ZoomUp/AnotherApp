using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherApp.Models
{

    public class DatumColor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string PantoneValue { get; set; }
    }
    public class AllColorsList
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public List<DatumColor> Data { get; set; }
        public Support Support { get; set; }
    }

    
}

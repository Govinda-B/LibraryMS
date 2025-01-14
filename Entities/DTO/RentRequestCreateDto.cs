﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class RentRequestCreateDto
    {
        [Required(ErrorMessage = "Book id is a required field.")]
        public int BookId { get; set; }
        [Required(ErrorMessage = "Username is a required field.")]
        public string username { get; set; }
        public DateTime requestdate { get; set; }
        [Required(ErrorMessage = "Start Date is a required field.")]
        public DateTime startdate { get; set; }
        [Required(ErrorMessage = "End Date is a required field.")]
        public DateTime enddate { get; set; }
    }
}

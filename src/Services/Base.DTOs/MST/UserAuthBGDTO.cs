using Database.Models;
using Database.Models.MST;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class UserAuthBGDTO
    {
        public bool isSDH { get; set; }
        public bool isTH { get; set; }
        public bool isCD { get; set; }
    }
} 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Model.ViewModels
{
    public class TokenInfoViewModel
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public double ExpiresIn { get; set; }
        public string TokenType { get; set; }

    }
}

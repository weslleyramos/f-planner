using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litz.Services.Models
{
    public class EditableResult
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool HasError { get; set; }
    }
}

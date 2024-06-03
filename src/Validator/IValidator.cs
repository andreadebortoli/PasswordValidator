using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator
{
    public interface IValidator
    {
        Response Validate(string password);
    }
}

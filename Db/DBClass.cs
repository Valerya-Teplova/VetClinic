using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Db
{
    internal class DBClass
    {
        private static VetClinicEntities _context;
        public static VetClinicEntities GetContext()
        {
            if (_context == null)
                _context = new VetClinicEntities();
            return _context;
        }
    }
}

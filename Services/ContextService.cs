using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto_final_pro_3.Models;

namespace Proyecto_final_pro_3.Services
{
    public class ContextService:IContextService
    {
        private readonly DB_A64A4C_SuperMercadoContext _context;       

        public ContextService(DB_A64A4C_SuperMercadoContext context) {
            _context = context; 
        }

        public  DB_A64A4C_SuperMercadoContext GetDBContext() {
            return _context; 
        }
    }
}

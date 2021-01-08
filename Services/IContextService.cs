using Proyecto_final_pro_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_final_pro_3.Services
{
    public interface IContextService
    {
       DB_A64A4C_SuperMercadoContext GetDBContext(); 
    }
}

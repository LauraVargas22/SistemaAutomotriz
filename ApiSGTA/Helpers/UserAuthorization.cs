using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGTA.Helpers
{
    public class UserAuthorization
    {
        public enum Rols
        {
            Administrator,
            Recepcionist,
            Mechanic
        }

        public const Rols rol_predeterminado = Rols.Administrator;
    }
}
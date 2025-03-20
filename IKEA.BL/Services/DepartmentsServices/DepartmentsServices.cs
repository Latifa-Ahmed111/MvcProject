using IKEA.DAl.Persistance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentsServices
{
    public class DepartmentsServices:IDepartmentsServices
    {
        //controller ==> Services ==>Reposatiory==>Context==>options options created By Clr By default (one Object per request )

        // we work By DI so we will pass Refrence of Reposatiory
        // We make refrence from IDepartmentsReposaiotry  to avoid that if we need call OrcaleDepartmentsReposaiotry مش عاىزين نلعب هنا
        private IDepartmentsReposaiotry Reposatiory;


        public DepartmentsServices(IDepartmentsReposaiotry _reposatiory)
        {

            Reposatiory = _reposatiory;

        }

        //Implementation   
    }
}

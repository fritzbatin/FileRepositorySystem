using Smits.Etg.FileRepositorySystem.DL;
using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smits.Etg.FileRepositorySystem.BL
{
  

    public class BusinessUnitBL
    {
        private BusinessUnitDL _businessUnitDL;

        public IEnumerable<BusinessUnit> GetAllBusinessUnitList()
        {
            _businessUnitDL = new BusinessUnitDL();
            return  _businessUnitDL.GetAllBusinessUnitList();
        }

        public BusinessUnit FindBusinessUnitById(int? Id)
        {
            _businessUnitDL = new BusinessUnitDL();
            return _businessUnitDL.FindBusinessUnitById(Id);
        }

        #region Validation
        public bool IsBUCodeExist(string BUCode, bool forUpdate = false, int? Id = 0)
        {
            _businessUnitDL = new BusinessUnitDL();
            return _businessUnitDL.IsBUCodeExist(BUCode, forUpdate, Id);

        }
        #endregion

        #region CRUD
        public int CreateBU(BusinessUnit businessUnit)
        {
            _businessUnitDL = new BusinessUnitDL();
            return _businessUnitDL.CreateBU(businessUnit);
        }

        public int UpdateBU(BusinessUnit businessUnit)
        {
            _businessUnitDL = new BusinessUnitDL();
            return _businessUnitDL.UpdateBU(businessUnit);
        }
        public int DeleteBusinessUnit(int id)
        {
            _businessUnitDL = new BusinessUnitDL();
            return _businessUnitDL.DeleteBusinessUnit(id);
        }
        public int sp_DeleteBU(int? id)
        {
            _businessUnitDL = new BusinessUnitDL();
            return _businessUnitDL.sp_DeleteBU(id);
        }

        #endregion

    }
}

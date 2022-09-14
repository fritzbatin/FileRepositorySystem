using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smits.Etg.FileRepositorySystem.DL
{
    public class BusinessUnitDL
    {
        private Entities db;

        public IEnumerable<BusinessUnit> GetAllBusinessUnitList()
        {
            using(db = new Entities())
            {
                return  db.BusinessUnits.OrderByDescending(e => (e.Modified.HasValue) ? e.Modified : e.Created).ToList();
            }
        }

        public BusinessUnit FindBusinessUnitById(int? Id)
        {
            using (db = new Entities())
            {
                BusinessUnit businessUnit = db.BusinessUnits.Find(Id);

                return businessUnit;
            }
        }

        #region Validation
        public bool IsBUCodeExist(string BUCode, bool forUpdate = false, int? Id = 0)
        {
            using (db = new Entities())
            {
                bool status = true;
                if (forUpdate == false)
                {
                    BusinessUnit businessUnit = db.BusinessUnits.Where(d => d.Code.ToLower() == BUCode.ToUpper()).FirstOrDefault();
                    if (businessUnit != null)
                    {
                        status = false;
                    }
                    else
                    {
                        status = true;
                    }
                }
                else
                {
                    BusinessUnit businessUnit = db.BusinessUnits.Where(d => d.Id == Id).FirstOrDefault();
                    var currentBusinessUnitCode = businessUnit.Code.ToString();
                    if (BUCode == currentBusinessUnitCode)
                    {
                        status = true;
                    }
                    else
                    {
                        businessUnit = db.BusinessUnits.Where(d => d.Code.ToUpper() == BUCode.ToUpper()).FirstOrDefault();
                        if (businessUnit != null)
                        {
                            status = false;
                        }
                        else
                        {
                            status = true;
                        }
                    }
                }

                return status;
            }

         
        }
        #endregion


        #region CRUD
        public int CreateBU(BusinessUnit businessUnit)
        {
            using (db = new Entities())
            {
                db.BusinessUnits.Add(businessUnit);
                db.SaveChanges();
                return businessUnit.Id;
            }
        }

        public int UpdateBU(BusinessUnit businessUnit)
        {
            using (db = new Entities())
            {
                var bu = db.BusinessUnits.Find(businessUnit.Id);
                if (bu != null)
                {
                    bu.Code = businessUnit.Code;
                    bu.Name = businessUnit.Name;
                    bu.Modified = businessUnit.Modified;
                    bu.ModifiedBy = businessUnit.ModifiedBy;

                    db.SaveChanges();
                    return businessUnit.Id;
                }
                else
                {
                    return 0;
                }

            }
        }
      
        public int sp_DeleteBU(int? id)
        {

            using (db = new Entities())
            {

                var idPar = new SqlParameter("Id", SqlDbType.Int);
                idPar.Value = id;

                var idParameter = id.HasValue ?
                    new SqlParameter("Id", id) :
                    new SqlParameter("Id", typeof(int));
                db.Database.SqlQuery<Entities>("exec BU_DeleteUnusedExistingRec @Id", idParameter).SingleOrDefault();
                var result = db.BusinessUnits.Find(id);
                if (result == null)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int DeleteBusinessUnit(int id)
        {
            using (db = new Entities())
            {
                try
                {
                    BusinessUnit bu = db.BusinessUnits.Find(id);
                    if (bu != null)
                    {
                        db.BusinessUnits.Remove(bu);
                        db.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }


            }
        }
        #endregion
    }
}

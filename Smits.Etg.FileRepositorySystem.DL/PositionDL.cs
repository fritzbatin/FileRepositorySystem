using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Smits.Etg.FileRepositorySystem.DL
{
    public class PositionDL
    {
        private Entities db;

        public bool IsPositionNameValid(string posName, bool forUpdate = false, int? Id = 0)
        {
            using (db = new Entities())
            {
                bool status = true;
                if (forUpdate == false)
                {
                    Position position = db.Positions.Where(d => d.Name.ToLower() == posName.ToLower()).FirstOrDefault();
                    if (position != null)
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
                    Position position = db.Positions.Where(d => d.Id == Id).FirstOrDefault();
                    var currentDepratmentCode = position.Name.ToString();
                    if (posName == currentDepratmentCode)
                    {
                        status = true;
                    }
                    else
                    {
                        position = db.Positions.Where(d => d.Name.ToLower() == posName.ToLower()).FirstOrDefault();
                        if (position != null)
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

        public Position FindPositionById(int? id)
        {
            using (db = new Entities())
            {
                Position position = db.Positions.Find(id);
                return position;
            }
        }
        public IEnumerable<vEmployeeListPerPosition> FindPositionByEmpId(int? id)
        {
            using (db = new Entities())
            {
                var pos1 = db.vEmployeeListPerPositions
                    .Where(e => e.Id == id)
                    .OrderByDescending(e => (e.Modified.HasValue) ? e.Modified : e.Created);
                return pos1.ToList();
            }
        }

        public IEnumerable<Position> GetAllPositionList()
        {
            using (db = new Entities())
            {
                var pos = db.Positions.OrderByDescending(e => (e.Modified.HasValue) ? e.Modified : e.Created);
                return pos.ToList();
            }
        }
        public IEnumerable<Position> GetAllPositionListForDropDown()
        {
            using (db = new Entities())
            {
                var pos = db.Positions.OrderBy(p => p.Name);
                return pos.ToList();
            }
        }

        #region CRUD
        public int CreatePosition(Position position)
        {
            using (db = new Entities())
            {
                db.Positions.Add(position);
                db.SaveChanges();
                return position.Id;
            }
        }

        public int UpdatePosition(Position position)
        {
            using (db = new Entities())
            {
                var pos = db.Positions.Find(position.Id);
                if (pos != null)
                {
                    pos.Name = position.Name;
                    pos.Description = position.Description;
                    pos.Modified = position.Modified;
                    pos.ModifiedBy = position.ModifiedBy;

                    var updated = db.SaveChanges();

                    return updated;
                }
                else
                {
                    return 0;
                }

            }
        }


        public int sp_DeletePosition(int? id)
        {
            using (db = new Entities())
            {

                var idPar = new SqlParameter("Id", SqlDbType.Int);
                idPar.Value = id;

                var idParameter = id.HasValue ?
                    new SqlParameter("Id", id) :
                    new SqlParameter("Id", typeof(int));
                db.Database.SqlQuery<Entities>("exec POS_DeleteUnusedExistingRec @Id", idParameter).SingleOrDefault();
                var result = db.Positions.Find(id);
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

        public int DeletePosition(int id)
        {
            using (db = new Entities())
            {
                try
                {
                    Position pos = db.Positions.Find(id);
                    if (pos != null)
                    {
                        db.Positions.Remove(pos);
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

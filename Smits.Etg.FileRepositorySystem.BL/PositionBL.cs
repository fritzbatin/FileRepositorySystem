using Smits.Etg.FileRepositorySystem.DL;
using Smits.Etg.FileRepositorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Smits.Etg.FileRepositorySystem.BL
{
    public class PositionBL
    {
        private PositionDL posbl;

        public bool IsPositionNameValid(string posName, bool forUpdate = false, int? Id = 0)
        {
            posbl = new PositionDL();
            return posbl.IsPositionNameValid(posName, forUpdate, Id);
        }

        public Position FindPositionById(int? id)
        {

            posbl = new PositionDL();
            return posbl.FindPositionById(id);
        }

        public IEnumerable<vEmployeeListPerPosition> FindPositionByEmpId(int? id)
        {
            posbl = new PositionDL();
            return posbl.FindPositionByEmpId(id);
        }
        public IEnumerable<Position> GetAllPositionList()
        {
            posbl = new PositionDL();
            return posbl.GetAllPositionList();
        }
        public IEnumerable<Position> GetAllPositionListForDropDown()
        {
            posbl = new PositionDL();
            return posbl.GetAllPositionListForDropDown();
        }

        #region CRUD
       
        public int CreatePosition(Position position)
        {
            posbl = new PositionDL();
            return posbl.CreatePosition(position);
        }
        public int UpdatePosition(Position position)
        {
            posbl = new PositionDL();
            return posbl.UpdatePosition(position);
        }

        public int DeletePosition(int id)
        {
            posbl = new PositionDL();
            return posbl.DeletePosition(id);
        }
        public int sp_DeletePosition(int? id)
        {
            posbl = new PositionDL();
            return posbl.sp_DeletePosition(id);
        }

        #endregion

    }
}

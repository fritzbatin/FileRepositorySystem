using Smits.Etg.FileRepositorySystem.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Smits.Etg.FileRepositorySystem.BL
{
    public class CustomMethodBL
    {
        private CustomMethodDL _cmethoddl;

        public string removeWhiteSpaces(string yourText)
        {
            _cmethoddl = new CustomMethodDL();
            return _cmethoddl.removeWhiteSpaces(yourText);
        }

    }
}

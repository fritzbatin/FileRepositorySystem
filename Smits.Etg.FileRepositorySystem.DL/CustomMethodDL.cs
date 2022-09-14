using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Smits.Etg.FileRepositorySystem.DL
{
    public class CustomMethodDL
    {
        public string removeWhiteSpaces(string yourText)
        {
            yourText = Regex.Replace(yourText, @"\s+", " ");
            yourText = yourText.Trim();
            return yourText;
        }
    }
}

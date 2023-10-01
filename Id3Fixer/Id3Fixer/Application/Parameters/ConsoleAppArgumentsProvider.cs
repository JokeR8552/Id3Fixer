using Id3Fixer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Id3Fixer.Application.Parameters
{
    internal class ConsoleAppArgumentsProvider : IArgumentsProvider
    {
        public ConsoleAppArgumentsProvider(string[] args)
        {
            Parameters = new Parameters(
                 args.GetSafe(0) ?? "C:\\Users\\imukhametov\\Documents\\My\\fix\\music",
                 args.GetSafe(1) ?? "ЖИТЬ.aimppl4");
        }


        public Parameters Parameters { get; }
    }
}

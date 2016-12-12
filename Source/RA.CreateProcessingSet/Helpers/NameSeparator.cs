using RA.CreateProcessingSet.Models;
using System.Linq;

namespace RA.CreateProcessingSet.Helpers
{
    public static class NameSeparator
    {
        public static Names Separate(string input, DestinationEnum destination)
        {
            if(destination == DestinationEnum.Custodian)
            {
                /* If the input has a comma, it is already splitted in the rigth order*/
                if (input.Contains(","))
                {
                    string[] parts = input.Split(new[] {','}, 2);
                    return new Names
                    {
                        FirstName = parts[1].Trim(),
                        LastName = parts[0].Trim(),
                    };
                }
                string[] splitFullName = input.Split(new[] {'_', ' ', ':'}, 2);
                if (splitFullName.Count() == 2)
                {
                    return new Names {
                        FirstName = splitFullName[0].Trim(),
                        LastName = splitFullName[1].Trim(),
                    };
                }
                else
                {
                    return new Names {
                        FirstName = input
                    };
                }
            }
            else
            {
                return new Names
                {
                    FirstName = input
                };
            }
        }
    }
}
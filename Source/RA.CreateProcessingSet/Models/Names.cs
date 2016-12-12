namespace RA.CreateProcessingSet.Models
{
    public class Names
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(LastName))
                {
                    return FirstName;
                }
                return string.Format("{0}, {1}", LastName, FirstName);
            }
        }
    }
}
using System;
namespace CoreXamarinPoc.iOS.Domains.Commons.Entities
{
    public class Data
    {
        public string id { get; set; }

        public string type { get; set; }

        public Data()
        {

        }

        public Data(string id, string type)
        {
            this.type = type;
            this.id = id;
        }
    }
}

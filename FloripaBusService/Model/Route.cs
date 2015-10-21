using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaBusService.Model
{
    public class Route
    {
        public string AgencyId { get; set; }
        public string Id { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}

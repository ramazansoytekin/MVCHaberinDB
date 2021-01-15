using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFHaberinDBService
{
        [DataContract]
        public class EkonomiDTO
        {
            [DataMember]
            public int HaberID { get; set; }
            [DataMember]
            public string HaberBaslik { get; set; }
            [DataMember]
            public string HaberAciklama { get; set; }
            [DataMember]
            public string HaberIcerik { get; set; }
            [DataMember]
            public DateTime? YayinlanmaTarihi { get; set; }
        }
}

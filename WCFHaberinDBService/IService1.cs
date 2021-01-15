using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFHaberinDBService
{
    
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<MansetDTO> ListMansetler();

        [OperationContract]
        List<SporDTO> ListSpor();

        [OperationContract]
        List<EkonomiDTO> ListEkonomi();

        [OperationContract]
        List<MagazinDTO> ListMagazin();

        [OperationContract]
        List<EmlakDTO> ListEmlak();

        [OperationContract]
        List<SaglikDTO> ListSaglik();

        [OperationContract]
        List<TeknolojiDTO> ListTeknoloji();

        [OperationContract]
        List<PolitikaDTO> ListPolitika();
        
        [OperationContract]
        List<DunyaDTO> ListDunya();
        
    }

}

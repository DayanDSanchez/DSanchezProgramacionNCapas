using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICandidato" in both code and config file together.
    [ServiceContract]
    public interface ICandidato
    {
        [OperationContract]
        [ServiceKnownType(typeof(ML.Candidato))]
        SL_WCF.Result CandidatoGetAllEF();
        [OperationContract]
        [ServiceKnownType(typeof(ML.Candidato))]
        SL_WCF.Result CandidatoGetByIdEF(int IdCandidato);
        [OperationContract]
        SL_WCF.Result CandidatoAddEF(ML.Candidato candidato);
        [OperationContract]
        SL_WCF.Result CandidatoDeleteEF(int IdCandidato);
        [OperationContract]
        SL_WCF.Result CandidatoUpdateEF(ML.Candidato candidato);
    }
}

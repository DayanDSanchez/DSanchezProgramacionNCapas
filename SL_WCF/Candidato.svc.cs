using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Candidato" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Candidato.svc or Candidato.svc.cs at the Solution Explorer and start debugging.
    public class Candidato : ICandidato
    {
        public SL_WCF.Result CandidatoGetAllEF()
        {
            ML.Result result = BL.Candidato.CandidatoGetAllEF();
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects,
            };
        }
        public SL_WCF.Result CandidatoGetByIdEF(int IdCandidato)
        {
            ML.Result result = BL.Candidato.CandidatoGetByIdEF(IdCandidato);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects,
            };
        }

        public SL_WCF.Result CandidatoAddEF(ML.Candidato candidato)
        {
            ML.Result result = BL.Candidato.CandidatoAddEF(candidato);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects,
            };
        }
        public SL_WCF.Result CandidatoDeleteEF(int IdCandidato)
        {
            ML.Result result = BL.Candidato.CandidatoDeleteEF(IdCandidato);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects,
            };
        }
        public SL_WCF.Result CandidatoUpdateEF(ML.Candidato candidato)
        {
            ML.Result result = BL.Candidato.CandidatoUpdateEF(candidato);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects,
            };
        }
    }
}

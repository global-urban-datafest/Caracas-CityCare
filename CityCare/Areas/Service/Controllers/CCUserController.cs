using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CityCare.Models;
using Common.Models;
using FORCOUtils;
using FORCOUtils.DALUtils;

namespace CityCare.Areas.Service.Controllers
{
    public class CCUserController : ApiController
    {
        
        IGenericRepository<CCUser> fUserRepository = new EntityFrameworkDataRepository<CCUser>(new CityCareEntities());

        
        /// <summary>
        /// PostSomeData
        /// </summary>
        /// <param name="aRegisterUserModel"></param>
        /// <returns></returns>
        [ResponseType(typeof(ResponseRegisterUserModel))]
        [HttpPost]
        public HttpResponseMessage PostRegisterUser([FromBody]RegisterUserModel aRegisterUserModel)
        {
            IGenericRepository<UserIdType> fUserIdTypeRepository = new EntityFrameworkDataRepository<UserIdType>(new CityCareEntities());

            try
            {
                //todo add validations
                var _NewUser = new CCUser
                {
                    Email = aRegisterUserModel.Email,
                    Name = aRegisterUserModel.Name,
                    Password = aRegisterUserModel.Password,
                    UserLevel = 0,
                    PersonalId = "ID",
                    PersonalIdTypeId = fUserIdTypeRepository.GetSingleLazyLoading().Id
                };


                fUserRepository.Add(_NewUser);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception aException)
            {
                var _Response = new ResponseRegisterUserModel
                {
                    IsRegistered = false,
                    ErroMessage = aException.Message
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, _Response);
            }
        }

        /// <summary>
        /// Get some data
        /// </summary>
        /// <param name="aUserAuthDataModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof (ResponseAuthDataModel))]
        public HttpResponseMessage PostValidateUser(UserAuthDataModel aUserAuthDataModel)
        {
            try
            {
                var _UserByEmailAndPass =
                    fUserRepository.GetSingleLazyLoading(
                        aUser => aUser.Email == aUserAuthDataModel.Email && aUser.Email == aUserAuthDataModel.Password);
                if (_UserByEmailAndPass != null)
                {
                    var _Response = new ResponseAuthDataModel
                    {
                        IsValidData = false,
                        fUserId = _UserByEmailAndPass.Id
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, _Response);
                }

                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            catch (Exception aException)
            {
                var _Response = new ResponseAuthDataModel
                {
                    IsValidData = false,
                    ErroMessage = aException.Message
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, _Response);
            }
        }


        /// <summary>
        ///Post some data
        /// </summary>
        /// <param name="aReportDataModels"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponseReportDataModel))]
        public HttpResponseMessage PostAddReports([FromBody]List<ReportDataModel> aReportDataModels)
        {
            try
            {
                
                IGenericRepository<Report> fUserReportRepository = new EntityFrameworkDataRepository<Report>(new CityCareEntities(),false);


                //var _Response = new ResponseReportDataModel
                //{
                //    fAreReportsInvalid = false,
                //    fReportModels = new List<ReportDataModel>()
                //};

                foreach (var _ReportDataModel in aReportDataModels)
                {
                    if (!string.IsNullOrEmpty(_ReportDataModel.ProposedReportType))
                    {


                        var _NewReport = new Report
                        {
                            CCUserId = _ReportDataModel.CCUserId,
                            Date = DateTime.Now,
                            Description = _ReportDataModel.Description,
                            Funds = _ReportDataModel.Funds,
                            IsValid = false,
                            Image = _ReportDataModel.Image,
                            ReportType = new ReportType
                            {
                                Name = _ReportDataModel.ProposedReportType,
                                Proposed = true
                            },
                            SiteAddress = _ReportDataModel.Latitud + "|" + _ReportDataModel.Latitud,

                        };
                        fUserReportRepository.Add(_NewReport);

                    }
                    else
                    {
                        var _NewReport = new Report
                        {
                            CCUserId = _ReportDataModel.CCUserId,
                            Date = DateTime.Now,
                            Description = _ReportDataModel.Description,
                            Funds = _ReportDataModel.Funds,
                            IsValid = false,
                            Image = _ReportDataModel.Image,
                            ReportTypeId = _ReportDataModel.ReportTypeId,
                            SiteAddress = _ReportDataModel.Latitud + "|" + _ReportDataModel.Latitud,

                        };
                        fUserReportRepository.Add(_NewReport);
                    }
                }
                var _Response = new ResponseReportDataModel
                {
                    fAreReportsInvalid = false
                };
                
                (fUserReportRepository as EntityFrameworkDataRepository<Report>).DisposeContext();
                return Request.CreateResponse(HttpStatusCode.OK, _Response);
            }
            catch (Exception aException)
            {
                var _Response = new ResponseReportDataModel
                {
                    fAreReportsInvalid = true,
                    ErroMessage = aException.Message
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, _Response);
            }
            


            //TODO improve using this code
            /*foreach (var _ReportDataModel in aReportDataModels)
            {
                try
                {

                    if (!string.IsNullOrEmpty(_ReportDataModel.ProposedReportType))
                    {
                        

                        var _NewReport = new Report
                        {
                            CCUserId = _ReportDataModel.CCUserId,
                            Date = DateTime.Now,
                            Description = _ReportDataModel.Description,
                            Funds = _ReportDataModel.Funds,
                            IsValid = false,
                            Image = _ReportDataModel.Image,
                            ReportType = new ReportType
                            {
                                Name = _ReportDataModel.ProposedReportType,
                                Proposed = true
                            },
                            SiteAddress = _ReportDataModel.Latitud + "|" + _ReportDataModel.Latitud,
                            
                        };
                        fUserReportRepository.Add(_NewReport);

                    }
                }
                catch (Exception aException)
                {
                    _ReportDataModel.ErrorAddingReport = aException.Message;
                    _Response.fReportModels.Add(_ReportDataModel);

                }
            }*/
            
        }
    }
}
